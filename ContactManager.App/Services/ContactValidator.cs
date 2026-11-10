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
            if (string.IsNullOrWhiteSpace(contact.FirstName) || !Regex.IsMatch(contact.FirstName, @"^[\p{L}\p{M}\-'’ʼ ]+$"))
            {
                ErrorMessage = "Ім'я є обов'язковим і може містити лише літери, пробіли, дефіси та апострофи.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(contact.LastName) && !Regex.IsMatch(contact.LastName, @"^[\p{L}\p{M}\-'’ʼ ]+$"))
            {
                ErrorMessage = "Прізвище містить некоректні символи.";
                return false;
            }

            int digitCount = contact.Phone?.Count(char.IsDigit) ?? 0;
            if (string.IsNullOrWhiteSpace(contact.Phone) || digitCount < 7 || digitCount > 15)
            {
                ErrorMessage = "Телефон повинен містити від 7 до 15 цифр.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(contact.Email) && !Regex.IsMatch(contact.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Некоректний формат Email адреси.";
                return false;
            }

            // Перевірка на дублікат (співпадіння останніх 9 цифр)
            if (_repository.IsPhoneDuplicate(contact.Phone, contact.Id == 0 ? (int?)null : contact.Id))
            {
                ErrorMessage = "Контакт із таким номером телефону вже існує.";
                return false;
            }

            return true;
        }
    }
}