using System;
using System.Windows.Forms;
using ContactManager.App.Data;

namespace ContactManager.App.Forms
{
    public partial class Form1 : Form
    {
        private ContactRepository _repository;

        public Form1()
        {
            InitializeComponent();

            dgvContacts.DataBindingComplete += (s, e) => FormatGrid();
            dgvContacts.MultiSelect = false;

            try
            {
                _repository = new ContactRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критична помилка ініціалізації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContactsData();
        }

        private void LoadContactsData(string keyword = "")
        {
            try
            {
                var contacts = string.IsNullOrWhiteSpace(keyword)
                    ? _repository.GetAllContacts()
                    : _repository.SearchContacts(keyword);

                dgvContacts.DataSource = contacts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvContacts.Columns.Count == 0) return;

            dgvContacts.Columns["Id"].Visible = false;
            dgvContacts.Columns["FirstName"].Visible = false;
            dgvContacts.Columns["LastName"].Visible = false;
            dgvContacts.Columns["CreatedAt"].Visible = false;

            dgvContacts.Columns["FullName"].HeaderText = "Повне ім'я";
            dgvContacts.Columns["FullName"].DisplayIndex = 0;

            dgvContacts.Columns["Phone"].HeaderText = "Телефон";
            dgvContacts.Columns["Phone"].DisplayIndex = 1;

            dgvContacts.Columns["Email"].HeaderText = "Ел. пошта";
            dgvContacts.Columns["Email"].DisplayIndex = 2;

            dgvContacts.Columns["Address"].HeaderText = "Адреса";
            dgvContacts.Columns["Address"].DisplayIndex = 3;

            dgvContacts.Columns["Notes"].HeaderText = "Нотатки";
            dgvContacts.Columns["Notes"].DisplayIndex = 4;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new ContactForm(_repository))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repository.AddContact(form.CurrentContact);
                        LoadContactsData(txtSearch.Text.Trim());
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0) return;

            var selectedContact = dgvContacts.SelectedRows[0].DataBoundItem as ContactManager.App.Models.Contact;
            if (selectedContact == null) return;

            using (var form = new ContactForm(_repository, selectedContact))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repository.UpdateContact(form.CurrentContact);
                        LoadContactsData(txtSearch.Text.Trim());
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0) return;

            var selectedContact = dgvContacts.SelectedRows[0].DataBoundItem as ContactManager.App.Models.Contact;
            if (selectedContact == null) return;

            if (MessageBox.Show($"Видалити контакт {selectedContact.FullName}?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _repository.DeleteContact(selectedContact.Id);
                    LoadContactsData(txtSearch.Text.Trim());
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e) => LoadContactsData(txtSearch.Text.Trim());

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageBox.Show("Курсовий проєкт: Розробка desktop-застосунку для ведення бази контактів.\nРозробник: Пустовойт Давид Олександрович", "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}