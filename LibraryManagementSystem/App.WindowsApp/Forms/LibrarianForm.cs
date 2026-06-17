using App.Core.Interfaces;
using App.Core.Models;
using System;
using System.Windows.Forms;

namespace App.WindowsApp.Forms
{
    public partial class LibrarianForm : Form
    {
        private readonly LibrarianFormModeEnum _mode;
        private readonly Librarian? _librarian;
        private readonly ILibrarianService _service;

        public LibrarianForm(LibrarianFormModeEnum mode, Librarian? librarian, ILibrarianService service)
        {
            _mode = mode;
            _librarian = librarian;
            _service = service;
            InitializeComponent();
            ConfigureMode();
            if (librarian != null) PopulateFields(librarian);
        }

        private void ConfigureMode()
        {
            bool isView = _mode == LibrarianFormModeEnum.View;
            textBoxName.ReadOnly = isView;
            textBoxEmail.ReadOnly = isView;
            buttonSave.Visible = !isView;

            this.Text = _mode switch
            {
                LibrarianFormModeEnum.Add => "Add Librarian",
                LibrarianFormModeEnum.Edit => "Edit Librarian",
                _ => "View Librarian"
            };
        }

        private void PopulateFields(Librarian librarian)
        {
            textBoxId.Text = librarian.Id;
            textBoxName.Text = librarian.Name;
            textBoxEmail.Text = librarian.Email;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_mode == LibrarianFormModeEnum.Add)
            {
                Librarian librarian = new Librarian
                {
                    Name = textBoxName.Text.Trim(),
                    Email = textBoxEmail.Text.Trim()
                };
                _service.AddLibrarian(librarian);
            }
            else
            {
                _librarian!.Name = textBoxName.Text.Trim();
                _librarian.Email = textBoxEmail.Text.Trim();
                _service.UpdateLibrarian(_librarian);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
