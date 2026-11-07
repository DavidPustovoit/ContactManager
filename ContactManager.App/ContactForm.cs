using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ContactManager.App.Models;

namespace ContactManager.App
{
    public partial class ContactForm : Form
    {
        public Contact CurrentContact { get; private set; }

        // Якщо передаємо null - це створення нового, якщо передаємо контакт - редагування
        public ContactForm(Contact? contact = null)
        {
            InitializeComponent();

            if (contact != null)
            {
                CurrentContact = contact;
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtEmail.Text = contact.Email;
                txtAddress.Text = contact.Address;
                txtNotes.Text = contact.Notes;
                this.Text = "Редагувати контакт";
            }
            else
            {
                CurrentContact = new Contact();
                this.Text = "Додати контакт";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Валідація: Обов'язкові поля
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поля 'Ім'я' та 'Телефон' є обов'язковими для заповнення!", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Валідація: Email (якщо він заповнений)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    MessageBox.Show("Введіть коректну Email адресу!", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Запис даних в об'єкт
            CurrentContact.FirstName = txtFirstName.Text.Trim();
            CurrentContact.LastName = txtLastName.Text.Trim();
            CurrentContact.Phone = txtPhone.Text.Trim();
            CurrentContact.Email = txtEmail.Text.Trim();
            CurrentContact.Address = txtAddress.Text.Trim();
            CurrentContact.Notes = txtNotes.Text.Trim();

            this.DialogResult = DialogResult.OK; // Сигнал головній формі, що все успішно
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}