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
            CurrentContact.FirstName = txtFirstName.Text.Trim();
            CurrentContact.LastName = txtLastName.Text.Trim();
            CurrentContact.Phone = txtPhone.Text.Trim();
            CurrentContact.Email = txtEmail.Text.Trim();
            CurrentContact.Address = txtAddress.Text.Trim();
            CurrentContact.Notes = txtNotes.Text.Trim();

            if (!_validator.Validate(CurrentContact, CurrentContact.Id == 0 ? (int?)null : CurrentContact.Id))
            {
                MessageBox.Show(_validator.ErrorMessage, "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}