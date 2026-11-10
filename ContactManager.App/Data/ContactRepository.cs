using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using ContactManager.App.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ContactManager.App.Data
{
    public class ContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository()
        {
            string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactManagerApp");
            Directory.CreateDirectory(appData); // Створюємо папку, якщо її немає

            _connectionString = $"Data Source={Path.Combine(appData, "contacts.db")}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Contacts (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT NOT NULL,
                        LastName TEXT,
                        Phone TEXT NOT NULL,
                        Email TEXT,
                        Address TEXT,
                        Notes TEXT,
                        CreatedAt DATETIME DEFAULT (datetime('now', 'localtime'))
                    );
                    CREATE INDEX IF NOT EXISTS idx_contacts_phone ON Contacts(Phone);";

                db.Execute(createTableQuery);
            }
            catch (Exception ex) { throw new InvalidOperationException("Помилка ініціалізації БД", ex); }
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                var culture = new CultureInfo("uk-UA");
                return db.Query<Contact>("SELECT * FROM Contacts")
                         .OrderBy(c => c.FirstName, StringComparer.Create(culture, true))
                         .ThenBy(c => c.LastName, StringComparer.Create(culture, true))
                         .ToList();
            }
            catch (Exception ex) { throw new InvalidOperationException("Помилка завантаження контактів", ex); }
        }

        public IEnumerable<Contact> SearchContacts(string keyword)
        {
            var all = GetAllContacts();
            if (string.IsNullOrWhiteSpace(keyword)) return all;

            return all.Where(c =>
                c.FullName.Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
                c.Phone.Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
                (c.Email != null && c.Email.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)))
                .ToList();
        }

        public bool IsPhoneDuplicate(string newPhone, int? currentId)
        {
            string digits = new string(newPhone.Where(char.IsDigit).ToArray());
            if (digits.Length >= 9) digits = digits.Substring(digits.Length - 9);

            using IDbConnection db = new SqliteConnection(_connectionString);
            var existingPhones = db.Query("SELECT Id, Phone FROM Contacts WHERE Id != COALESCE(@Id, -1)", new { Id = currentId });

            foreach (var row in existingPhones)
            {
                string dbDigits = new string(((string)row.Phone).Where(char.IsDigit).ToArray());
                if (dbDigits.EndsWith(digits)) return true;
            }
            return false;
        }

        public void AddContact(Contact contact)
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                db.Execute("INSERT INTO Contacts (FirstName, LastName, Phone, Email, Address, Notes) VALUES (@FirstName, @LastName, @Phone, @Email, @Address, @Notes)", contact);
            }
            catch (Exception ex) { throw new InvalidOperationException("Помилка збереження", ex); }
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                db.Execute("UPDATE Contacts SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, Email=@Email, Address=@Address, Notes=@Notes WHERE Id=@Id", contact);
            }
            catch (Exception ex) { throw new InvalidOperationException("Помилка оновлення", ex); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                db.Execute("DELETE FROM Contacts WHERE Id = @Id", new { Id = id });
            }
            catch (Exception ex) { throw new InvalidOperationException("Помилка видалення", ex); }
        }
    }
}