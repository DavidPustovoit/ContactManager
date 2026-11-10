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
            // Надійний шлях до папки з виконуваним файлом
            string dbPath = Path.Combine(AppContext.BaseDirectory, "contacts.db");
            _connectionString = $"Data Source={dbPath}";
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
            catch (Exception ex)
            {
                throw new Exception($"Критична помилка ініціалізації бази даних: {ex.Message}");
            }
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
            catch (Exception ex)
            {
                throw new Exception($"Помилка завантаження контактів: {ex.Message}");
            }
        }

        public IEnumerable<Contact> SearchContacts(string keyword)
        {
            var all = GetAllContacts();
            if (string.IsNullOrWhiteSpace(keyword)) return all;

            return all.Where(c =>
                c.FullName.Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
                c.Phone.Contains(keyword, StringComparison.CurrentCultureIgnoreCase) ||
                c.Email.Contains(keyword, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public void AddContact(Contact contact)
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                string sql = "INSERT INTO Contacts (FirstName, LastName, Phone, Email, Address, Notes) VALUES (@FirstName, @LastName, @Phone, @Email, @Address, @Notes)";
                db.Execute(sql, contact);
            }
            catch (Exception ex) { throw new Exception($"Помилка збереження: {ex.Message}"); }
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                string sql = "UPDATE Contacts SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, Email=@Email, Address=@Address, Notes=@Notes WHERE Id=@Id";
                db.Execute(sql, contact);
            }
            catch (Exception ex) { throw new Exception($"Помилка оновлення: {ex.Message}"); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                using IDbConnection db = new SqliteConnection(_connectionString);
                db.Execute("DELETE FROM Contacts WHERE Id = @Id", new { Id = id });
            }
            catch (Exception ex) { throw new Exception($"Помилка видалення: {ex.Message}"); }
        }
    }
}