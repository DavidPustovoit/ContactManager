using System.Linq;
using System.Text.RegularExpressions;
using ContactManager.App.Models;
using ContactManager.App.Data;

namespace ContactManager.App.Services
{
    public class ContactValidator
    {
        private readonly ContactRepository _repository;
        public string ErrorMessage { get; private set; } = string.Empty;

        public ContactValidator(ContactRepository repository)
        {
            _repository = repository;
        }

        public bool Validate(Contact contact, int? currentContactId = null)
        {
            if (string.IsNullOrWhiteSpace(contact.FirstName))
            {
                ErrorMessage = "Ім'я є обов'язковим для заповнення.";
                return false;
            }

            if (!Regex.IsMatch(contact.FirstName, @"^[\p{L}\-']+$"))
            {
                ErrorMessage = "Ім'я містить некоректні символи (дозволені лише літери, дефіс та апостроф).";
                return false;
            }

            if (string.IsNullOrWhiteSpace(contact.Phone) || !Regex.IsMatch(contact.Phone, @"^[\d\+\-\(\) ]+$"))
            {
                ErrorMessage = "Телефон порожній або містить некоректні символи.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(contact.Email) && !Regex.IsMatch(contact.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Некоректний формат Email адреси.";
                return false;
            }

            var allContacts = _repository.GetAllContacts();
            if (allContacts.Any(c => c.Phone == contact.Phone && c.Id != currentContactId))
            {
                ErrorMessage = "Контакт із таким номером телефону вже існує в базі.";
                return false;
            }

            return true;
        }
    }
}