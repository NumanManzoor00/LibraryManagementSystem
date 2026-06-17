using App.Core.Enums;
using App.Core.Interfaces;
using App.Core.Models;
using System;
using System.Windows.Forms;

namespace App.WindowsApp.Forms
{
    public partial class BookForm : Form
    {
        private readonly BookFormModeEnum _mode;
        private readonly Book? _book;
        private readonly IBookService _service;

        public BookForm(BookFormModeEnum mode, Book? book, IBookService service)
        {
            _mode = mode;
            _book = book;
            _service = service;
            InitializeComponent();
            ConfigureMode();
            if (book != null) PopulateFields(book);
        }

        private void ConfigureMode()
        {
            bool isView = _mode == BookFormModeEnum.View;

            textBoxTitle.ReadOnly = isView;
            textBoxAuthor.ReadOnly = isView;
            textBoxISBN.ReadOnly = isView;
            comboBoxGenre.Enabled = !isView;
            comboBoxStatus.Enabled = !isView;
            buttonSave.Visible = !isView;

            comboBoxGenre.Items.Clear();
            foreach (BookGenre g in Enum.GetValues(typeof(BookGenre)))
                comboBoxGenre.Items.Add(g);
            comboBoxGenre.SelectedIndex = 0;

            comboBoxStatus.Items.Clear();
            foreach (BookStatus s in Enum.GetValues(typeof(BookStatus)))
                comboBoxStatus.Items.Add(s);
            comboBoxStatus.SelectedIndex = 0;

            this.Text = _mode switch
            {
                BookFormModeEnum.Add => "Add Book",
                BookFormModeEnum.Edit => "Edit Book",
                _ => "View Book"
            };
        }

        private void PopulateFields(Book book)
        {
            textBoxId.Text = book.Id;
            textBoxTitle.Text = book.Title;
            textBoxAuthor.Text = book.Author;
            textBoxISBN.Text = book.ISBN;
            comboBoxGenre.SelectedItem = book.Genre;
            comboBoxStatus.SelectedItem = book.Status;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxAuthor.Text))
            {
                MessageBox.Show("Author is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_mode == BookFormModeEnum.Add)
            {
                Book book = new Book
                {
                    Title = textBoxTitle.Text.Trim(),
                    Author = textBoxAuthor.Text.Trim(),
                    ISBN = textBoxISBN.Text.Trim(),
                    Genre = (BookGenre)comboBoxGenre.SelectedItem!,
                    Status = (BookStatus)comboBoxStatus.SelectedItem!,
                    AddedDate = DateTime.Today
                };
                _service.AddBook(book);
            }
            else
            {
                _book!.Title = textBoxTitle.Text.Trim();
                _book.Author = textBoxAuthor.Text.Trim();
                _book.ISBN = textBoxISBN.Text.Trim();
                _book.Genre = (BookGenre)comboBoxGenre.SelectedItem!;
                _book.Status = (BookStatus)comboBoxStatus.SelectedItem!;
                _service.UpdateBook(_book);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
