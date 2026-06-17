using App.Core.Enums;
using App.Core.Interfaces;
using App.Core.Models;
using App.Core.Services;
using App.WindowsApp.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.WindowsApp.Views
{
    public partial class BookView : UserControl
    {
        private const string AllStatuses = "--All--";

        private readonly IBookService service;
        private readonly BindingSource _dgvBindingSource = new BindingSource();

        public BookView() : this(new BookService()) { }

        public BookView(IBookService _service)
        {
            service = _service;
            InitializeComponent();
            LoadStatusFilter();
            dataGridViewBooks.DataSource = _dgvBindingSource;

            // Column sorting: allow clicking column headers
            dataGridViewBooks.ColumnHeaderMouseClick += DataGridViewBooks_ColumnHeaderMouseClick;

            _ = RefreshGridAsync();
        }

        private void LoadStatusFilter()
        {
            comboBoxStatusFilter.Items.Clear();
            comboBoxStatusFilter.Items.Add(AllStatuses);
            foreach (BookStatus s in Enum.GetValues(typeof(BookStatus)))
                comboBoxStatusFilter.Items.Add(s);
            comboBoxStatusFilter.SelectedIndex = 0;
        }

        private async Task RefreshGridAsync()
        {
            SetLoadingState(true);
            try
            {
                var books = await service.GetAllBooksAsync();
                var filtered = ApplyFilter(books);
                _dgvBindingSource.DataSource = filtered.ToList();
                UpdateStatusBar(filtered.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading books", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool loading)
        {
            toolStripButtonRefresh.Enabled = !loading;
            toolStripButtonAdd.Enabled = !loading;
            toolStripButtonRefresh.Text = loading ? "Loading..." : "↻ Refresh";
        }

        private IEnumerable<Book> ApplyFilter(IEnumerable<Book> books)
        {
            string keyword = textBoxSearch.Text.Trim();
            BookStatus? selectedStatus = GetSelectedStatus();

            if (!string.IsNullOrWhiteSpace(keyword))
                books = books.Where(b =>
                    b.Id.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    b.ISBN.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (selectedStatus.HasValue)
                books = books.Where(b => b.Status == selectedStatus.Value);

            return books;
        }

        private void UpdateStatusBar(int count)
        {
            var dashboard = this.FindForm() as DashboardForm;
            dashboard?.SetStatusRecord($"Records: {count}");
        }

        private BookStatus? GetSelectedStatus() =>
            comboBoxStatusFilter.SelectedItem is BookStatus s ? s : null;

        private Book? GetSelectedBook() => _dgvBindingSource.Current as Book;

        private void ShowBookForm(BookFormModeEnum mode, Book? book = null, bool refreshAfterClose = true)
        {
            using BookForm form = new BookForm(mode, book, service);
            form.ShowDialog();
            if (refreshAfterClose) _ = RefreshGridAsync();
        }

        // Column sort support
        private string _sortColumn = "";
        private System.Windows.Forms.SortOrder _sortOrder = System.Windows.Forms.SortOrder.None;

        private void DataGridViewBooks_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            string colName = dataGridViewBooks.Columns[e.ColumnIndex].DataPropertyName;
            if (_sortColumn == colName)
                _sortOrder = _sortOrder == System.Windows.Forms.SortOrder.Ascending
                    ? System.Windows.Forms.SortOrder.Descending
                    : System.Windows.Forms.SortOrder.Ascending;
            else
            {
                _sortColumn = colName;
                _sortOrder = System.Windows.Forms.SortOrder.Ascending;
            }

            dataGridViewBooks.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _sortOrder;

            var list = (_dgvBindingSource.DataSource as List<Book>) ?? new List<Book>();
            List<Book> sorted = (_sortOrder == System.Windows.Forms.SortOrder.Ascending)
                ? list.OrderBy(b => typeof(Book).GetProperty(colName)?.GetValue(b, null)).ToList()
                : list.OrderByDescending(b => typeof(Book).GetProperty(colName)?.GetValue(b, null)).ToList();
            _dgvBindingSource.DataSource = sorted;
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e) => ShowBookForm(BookFormModeEnum.Add);

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            Book? selected = GetSelectedBook();
            if (selected == null) { MessageBox.Show("Please select a book to edit.", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowBookForm(BookFormModeEnum.Edit, selected);
        }

        private void toolStripButtonView_Click(object sender, EventArgs e)
        {
            Book? selected = GetSelectedBook();
            if (selected == null) { MessageBox.Show("Please select a book to view.", "View Book", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowBookForm(BookFormModeEnum.View, selected, refreshAfterClose: false);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            Book? selected = GetSelectedBook();
            if (selected == null) { MessageBox.Show("Please select a book to delete.", "Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show("Delete the selected book?", "Delete Book", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                service.DeleteBook(selected.Id);
                _ = RefreshGridAsync();
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e) => _ = RefreshGridAsync();
        private void textBoxSearch_TextChanged(object sender, EventArgs e) => _ = RefreshGridAsync();
        private void comboBoxStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => _ = RefreshGridAsync();
    }
}
