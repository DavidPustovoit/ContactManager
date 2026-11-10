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

        public bool Validate(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.FirstName) || !Regex.IsMatch(contact.FirstName, @"^(?=.*\p{L})[\p{L}\p{M}\-'’ʼ ]+$"))
            {
                ErrorMessage = "Ім'я є обов'язковим і повинно містити хоча б одну літеру.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(contact.LastName) && !Regex.IsMatch(contact.LastName, @"^(?=.*\p{L})[\p{L}\p{M}\-'’ʼ ]+$"))
            {
                ErrorMessage = "Прізвище містить некоректні символи або не має жодної літери.";
                return false;
            }

            int digitCount = contact.Phone?.Count(char.IsAsciiDigit) ?? 0;
            if (string.IsNullOrWhiteSpace(contact.Phone) || !Regex.IsMatch(contact.Phone, @"^\+?[0-9\-\(\) ]+$") || digitCount < 7 || digitCount > 15)
            {
                ErrorMessage = "Телефон: лише цифри, «+» на початку, дефіси, дужки й пробіли (від 7 до 15 цифр).";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(contact.Email) && !Regex.IsMatch(contact.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Некоректний формат Email адреси.";
                return false;
            }

            if (_repository.IsPhoneDuplicate(contact.Phone, contact.Id == 0 ? (int?)null : contact.Id))
            {
                ErrorMessage = "Контакт із таким номером телефону вже існує в базі.";
                return false;
            }

            return true;
        }
    }
}