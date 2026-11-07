using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ContactManager.App.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ContactManager.App.Data
{
    public class ContactRepository
    {
        // Шлях до локального файлу бази даних
        private const string DbFileName = "contacts.db";
        private readonly string _connectionString = $"Data Source={DbFileName}";

        public ContactRepository()
        {
            InitializeDatabase();
        }

        // Автоматичне створення таблиці при першому запуску програми
        private void InitializeDatabase()
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
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                )";

            db.Execute(createTableQuery);
        }

        // Поки що додамо лише метод отримання всіх контактів
        public IEnumerable<Contact> GetAllContacts()
        {
            using IDbConnection db = new SqliteConnection(_connectionString);
            return db.Query<Contact>("SELECT * FROM Contacts ORDER BY FirstName, LastName").ToList();
        }

        // Додавання контакту
        public void AddContact(Contact contact)
        {
            using IDbConnection db = new SqliteConnection(_connectionString);
            string sql = @"INSERT INTO Contacts (FirstName, LastName, Phone, Email, Address, Notes) 
                           VALUES (@FirstName, @LastName, @Phone, @Email, @Address, @Notes)";
            db.Execute(sql, contact);
        }

        // Оновлення контакту
        public void UpdateContact(Contact contact)
        {
            using IDbConnection db = new SqliteConnection(_connectionString);
            string sql = @"UPDATE Contacts 
                           SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, 
                               Email = @Email, Address = @Address, Notes = @Notes 
                           WHERE Id = @Id";
            db.Execute(sql, contact);
        }

        // Видалення контакту
        public void DeleteContact(int id)
        {
            using IDbConnection db = new SqliteConnection(_connectionString);
            db.Execute("DELETE FROM Contacts WHERE Id = @Id", new { Id = id });
        }

        // Пошук контактів (за іменем, прізвищем або телефоном)
        public IEnumerable<Contact> SearchContacts(string keyword)
        {
            using IDbConnection db = new SqliteConnection(_connectionString);
            string sql = @"SELECT * FROM Contacts 
                           WHERE FirstName LIKE @Keyword OR LastName LIKE @Keyword OR Phone LIKE @Keyword 
                           ORDER BY FirstName, LastName";
            return db.Query<Contact>(sql, new { Keyword = $"%{keyword}%" }).ToList();
        }
    }
}