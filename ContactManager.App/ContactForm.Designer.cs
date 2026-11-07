namespace ContactManager.App;

partial class ContactForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblFirstName, lblLastName, lblPhone, lblEmail, lblAddress, lblNotes;
    private System.Windows.Forms.TextBox txtFirstName, txtLastName, txtPhone, txtEmail, txtAddress, txtNotes;
    private System.Windows.Forms.Button btnSave, btnCancel;

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
        this.lblFirstName = new System.Windows.Forms.Label();
        this.txtFirstName = new System.Windows.Forms.TextBox();
        this.lblLastName = new System.Windows.Forms.Label();
        this.txtLastName = new System.Windows.Forms.TextBox();
        this.lblPhone = new System.Windows.Forms.Label();
        this.txtPhone = new System.Windows.Forms.TextBox();
        this.lblEmail = new System.Windows.Forms.Label();
        this.txtEmail = new System.Windows.Forms.TextBox();
        this.lblAddress = new System.Windows.Forms.Label();
        this.txtAddress = new System.Windows.Forms.TextBox();
        this.lblNotes = new System.Windows.Forms.Label();
        this.txtNotes = new System.Windows.Forms.TextBox();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.SuspendLayout();

        int yOffset = 20;

        // FirstName
        this.lblFirstName.Location = new System.Drawing.Point(20, yOffset);
        this.lblFirstName.Text = "Ім'я (*):";
        this.txtFirstName.Location = new System.Drawing.Point(120, yOffset - 3);
        this.txtFirstName.Size = new System.Drawing.Size(200, 23);
        yOffset += 40;

        // LastName
        this.lblLastName.Location = new System.Drawing.Point(20, yOffset);
        this.lblLastName.Text = "Прізвище:";
        this.txtLastName.Location = new System.Drawing.Point(120, yOffset - 3);
        this.txtLastName.Size = new System.Drawing.Size(200, 23);
        yOffset += 40;

        // Phone
        this.lblPhone.Location = new System.Drawing.Point(20, yOffset);
        this.lblPhone.Text = "Телефон (*):";
        this.txtPhone.Location = new System.Drawing.Point(120, yOffset - 3);
        this.txtPhone.Size = new System.Drawing.Size(200, 23);
        yOffset += 40;

        // Email
        this.lblEmail.Location = new System.Drawing.Point(20, yOffset);
        this.lblEmail.Text = "Email:";
        this.txtEmail.Location = new System.Drawing.Point(120, yOffset - 3);
        this.txtEmail.Size = new System.Drawing.Size(200, 23);
        yOffset += 40;

        // Address
        this.lblAddress.Location = new System.Drawing.Point(20, yOffset);
        this.lblAddress.Text = "Адреса:";
        this.txtAddress.Location = new System.Drawing.Point(120, yOffset - 3);
        this.txtAddress.Size = new System.Drawing.Size(200, 23);
        yOffset += 40;

        // Notes
        this.lblNotes.Location = new System.Drawing.Point(20, yOffset);
        this.lblNotes.Text = "Нотатки:";
        this.txtNotes.Location = new System.Drawing.Point(120, yOffset - 3);
        this.txtNotes.Multiline = true;
        this.txtNotes.Size = new System.Drawing.Size(200, 60);
        yOffset += 80;

        // Buttons
        this.btnSave.Location = new System.Drawing.Point(120, yOffset);
        this.btnSave.Text = "Зберегти";
        this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

        this.btnCancel.Location = new System.Drawing.Point(220, yOffset);
        this.btnCancel.Text = "Скасувати";
        this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

        // Form
        this.ClientSize = new System.Drawing.Size(350, yOffset + 50);
        this.Controls.Add(this.lblFirstName);
        this.Controls.Add(this.txtFirstName);
        this.Controls.Add(this.lblLastName);
        this.Controls.Add(this.txtLastName);
        this.Controls.Add(this.lblPhone);
        this.Controls.Add(this.txtPhone);
        this.Controls.Add(this.lblEmail);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.lblAddress);
        this.Controls.Add(this.txtAddress);
        this.Controls.Add(this.lblNotes);
        this.Controls.Add(this.txtNotes);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.btnCancel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Контакт";

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}