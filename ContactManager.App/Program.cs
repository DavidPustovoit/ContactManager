using System;
using System.Threading;
using System.Windows.Forms;
using ContactManager.App.Forms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.ThreadException += (sender, args) =>
                MessageBox.Show($"Виникла помилка:\n{args.Exception.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}