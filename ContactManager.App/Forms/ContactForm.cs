using System;
using System.Windows.Forms;
using ContactManager.App.Models;
using ContactManager.App.Services;
using ContactManager.App.Data;

namespace ContactManager.App.Forms
{
    public partial class ContactForm : Form
    {
        public Contact CurrentContact { get; private set; }
        private readonly ContactValidator _validator;

        public ContactForm(ContactRepository repository, Contact? contact = null)
        {
            InitializeComponent();
            _validator = new ContactValidator(repository);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            CurrentContact = contact == null ? new Contact() : new Contact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = contact.Address,
                Notes = contact.Notes,
                CreatedAt = contact.CreatedAt
            };

            txtFirstName.Text = CurrentContact.FirstName;
            txtLastName.Text = CurrentContact.LastName;
            txtPhone.Text = CurrentContact.Phone;
            txtEmail.Text = CurrentContact.Email;
            txtAddress.Text = CurrentContact.Address;
            txtNotes.Text = CurrentContact.Notes;

            this.Text = contact == null ? "Додати контакт" : "Редагувати контакт";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            CurrentContact.FirstName = txtFirstName.Text.Trim();
            CurrentContact.LastName = txtLastName.Text.Trim();
            CurrentContact.Phone = txtPhone.Text.Trim();
            CurrentContact.Email = txtEmail.Text.Trim();
            CurrentContact.Address = txtAddress.Text.Trim();
            CurrentContact.Notes = txtNotes.Text.Trim();

            try
            {
                if (!_validator.Validate(CurrentContact))
                {
                    MessageBox.Show(_validator.ErrorMessage, "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка перевірки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}