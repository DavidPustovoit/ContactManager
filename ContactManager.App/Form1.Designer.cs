namespace ContactManager.App;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.MenuStrip menuStripMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStripMain;
    private System.Windows.Forms.ToolStripButton btnAdd;
    private System.Windows.Forms.ToolStripButton btnEdit;
    private System.Windows.Forms.ToolStripButton btnDelete;
    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.DataGridView dgvContacts;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.menuStripMain = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMain = new System.Windows.Forms.ToolStrip();
        this.btnAdd = new System.Windows.Forms.ToolStripButton();
        this.btnEdit = new System.Windows.Forms.ToolStripButton();
        this.btnDelete = new System.Windows.Forms.ToolStripButton();
        this.panelTop = new System.Windows.Forms.Panel();
        this.lblSearch = new System.Windows.Forms.Label();
        this.txtSearch = new System.Windows.Forms.TextBox();
        this.btnSearch = new System.Windows.Forms.Button();
        this.dgvContacts = new System.Windows.Forms.DataGridView();

        this.menuStripMain.SuspendLayout();
        this.toolStripMain.SuspendLayout();
        this.panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
        this.SuspendLayout();

        // menuStripMain
        this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem, this.helpToolStripMenuItem });
        this.menuStripMain.Location = new System.Drawing.Point(0, 0);
        this.menuStripMain.Name = "menuStripMain";
        this.menuStripMain.Size = new System.Drawing.Size(800, 24);

        // fileToolStripMenuItem
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.exitToolStripMenuItem });
        this.fileToolStripMenuItem.Text = "Файл";

        // exitToolStripMenuItem
        this.exitToolStripMenuItem.Text = "Вихід";
        this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);

        // helpToolStripMenuItem
        this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.aboutToolStripMenuItem });
        this.helpToolStripMenuItem.Text = "Допомога";

        // aboutToolStripMenuItem
        this.aboutToolStripMenuItem.Text = "Про програму";
        this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);

        // toolStripMain
        this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.btnAdd, this.btnEdit, this.btnDelete });
        this.toolStripMain.Location = new System.Drawing.Point(0, 24);
        this.toolStripMain.Name = "toolStripMain";
        this.toolStripMain.Size = new System.Drawing.Size(800, 25);

        // btnAdd
        this.btnAdd.Text = "➕ Додати";
        this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

        // btnEdit
        this.btnEdit.Text = "✏️ Редагувати";
        this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);

        // btnDelete
        this.btnDelete.Text = "🗑️ Видалити";
        this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

        // panelTop
        this.panelTop.Controls.Add(this.btnSearch);
        this.panelTop.Controls.Add(this.txtSearch);
        this.panelTop.Controls.Add(this.lblSearch);
        this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelTop.Location = new System.Drawing.Point(0, 49);
        this.panelTop.Name = "panelTop";
        this.panelTop.Size = new System.Drawing.Size(800, 40);

        // lblSearch
        this.lblSearch.AutoSize = true;
        this.lblSearch.Location = new System.Drawing.Point(12, 13);
        this.lblSearch.Text = "Пошук:";

        // txtSearch
        this.txtSearch.Location = new System.Drawing.Point(65, 10);
        this.txtSearch.Size = new System.Drawing.Size(300, 23);

        // btnSearch
        this.btnSearch.Location = new System.Drawing.Point(375, 9);
        this.btnSearch.Text = "Шукати";
        this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

        // dgvContacts
        this.dgvContacts.AllowUserToAddRows = false;
        this.dgvContacts.AllowUserToDeleteRows = false;
        this.dgvContacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvContacts.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvContacts.Location = new System.Drawing.Point(0, 89);
        this.dgvContacts.Name = "dgvContacts";
        this.dgvContacts.ReadOnly = true;
        this.dgvContacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvContacts.Size = new System.Drawing.Size(800, 361);

        // Form1
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.dgvContacts);
        this.Controls.Add(this.panelTop);
        this.Controls.Add(this.toolStripMain);
        this.Controls.Add(this.menuStripMain);
        this.MainMenuStrip = this.menuStripMain;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "База контактів";
        this.Load += new System.EventHandler(this.Form1_Load);

        this.menuStripMain.ResumeLayout(false);
        this.menuStripMain.PerformLayout();
        this.toolStripMain.ResumeLayout(false);
        this.toolStripMain.PerformLayout();
        this.panelTop.ResumeLayout(false);
        this.panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}