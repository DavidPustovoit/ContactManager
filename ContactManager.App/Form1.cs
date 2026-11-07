using ContactManager.App.Data;
using ContactManager.App.Models;
using System;
using System.Windows.Forms;

namespace ContactManager.App
{
    public partial class Form1 : Form
    {
        private readonly ContactRepository _repository;

        public Form1()
        {
            InitializeComponent();
            _repository = new ContactRepository(); // Ініціалізація підключення до БД
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContactsData();
        }

        // Метод для завантаження даних у DataGridView
        private void LoadContactsData()
        {
            try
            {
                var contacts = _repository.GetAllContacts();
                dgvContacts.DataSource = contacts;

                // Ховаємо технічні колонки або ті, що не потрібні в загальному списку
                var colId = dgvContacts.Columns["Id"];
                if (colId != null) colId.Visible = false;

                var colCreatedAt = dgvContacts.Columns["CreatedAt"];
                if (colCreatedAt != null) colCreatedAt.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- ОБРОБНИКИ ПОДІЙ ІНТЕРФЕЙСУ ---

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Курсовий проєкт: Розробка desktop-застосунку для ведення бази контактів.\nРозробник: Пустовойт Давид Олександрович", "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new ContactForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _repository.AddContact(form.CurrentContact);
                    LoadContactsData(); // Оновлюємо таблицю
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Будь ласка, оберіть контакт для редагування.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Отримуємо виділений контакт з таблиці
            var selectedContact = dgvContacts.SelectedRows[0].DataBoundItem as Contact;
            if (selectedContact == null) return;

            using (var form = new ContactForm(selectedContact))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _repository.UpdateContact(form.CurrentContact);
                    LoadContactsData();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Будь ласка, оберіть контакт для видалення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedContact = dgvContacts.SelectedRows[0].DataBoundItem as Contact;
            if (selectedContact == null) return;

            var result = MessageBox.Show($"Ви дійсно хочете видалити контакт {selectedContact.FullName}?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _repository.DeleteContact(selectedContact.Id);
                LoadContactsData();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadContactsData(); // Якщо поле порожнє - завантажуємо всі контакти
            }
            else
            {
                // Виконуємо пошук і оновлюємо таблицю
                var searchResults = _repository.SearchContacts(keyword);
                dgvContacts.DataSource = searchResults;
            }
        }
    }
}