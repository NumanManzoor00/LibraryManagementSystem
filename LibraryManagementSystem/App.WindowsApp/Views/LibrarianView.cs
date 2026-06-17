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
    public partial class LibrarianView : UserControl
    {
        private readonly ILibrarianService service;
        private readonly BindingSource _dgvBindingSource = new BindingSource();

        public LibrarianView() : this(new LibrarianService()) { }

        public LibrarianView(ILibrarianService _service)
        {
            service = _service;
            InitializeComponent();
            dataGridViewLibrarians.DataSource = _dgvBindingSource;
            dataGridViewLibrarians.ColumnHeaderMouseClick += DataGridViewLibrarians_ColumnHeaderMouseClick;
            _ = RefreshGridAsync();
        }

        private async Task RefreshGridAsync()
        {
            toolStripButtonRefresh.Enabled = false;
            try
            {
                var librarians = await service.GetAllLibrariansAsync();
                var filtered = GetFiltered(librarians);
                _dgvBindingSource.DataSource = filtered.ToList();
                (this.FindForm() as DashboardForm)?.SetStatusRecord($"Records: {filtered.Count()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading librarians", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                toolStripButtonRefresh.Enabled = true;
            }
        }

        private IEnumerable<Librarian> GetFiltered(IEnumerable<Librarian> librarians)
        {
            string keyword = textBoxSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword)) return librarians;
            return librarians.Where(l =>
                l.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                l.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        private Librarian? GetSelectedLibrarian() => _dgvBindingSource.Current as Librarian;

        private void ShowLibrarianForm(LibrarianFormModeEnum mode, Librarian? librarian = null, bool refresh = true)
        {
            using LibrarianForm form = new LibrarianForm(mode, librarian, service);
            form.ShowDialog();
            if (refresh) _ = RefreshGridAsync();
        }

        private string _sortColumn = "";
        private System.Windows.Forms.SortOrder _sortOrder = System.Windows.Forms.SortOrder.None;

        private void DataGridViewLibrarians_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            string colName = dataGridViewLibrarians.Columns[e.ColumnIndex].DataPropertyName;
            _sortOrder = _sortColumn == colName && _sortOrder == System.Windows.Forms.SortOrder.Ascending
                ? System.Windows.Forms.SortOrder.Descending
                : System.Windows.Forms.SortOrder.Ascending;
            _sortColumn = colName;
            dataGridViewLibrarians.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _sortOrder;

            var list = (_dgvBindingSource.DataSource as List<Librarian>) ?? new List<Librarian>();
            _dgvBindingSource.DataSource = _sortOrder == System.Windows.Forms.SortOrder.Ascending
                ? list.OrderBy(l => typeof(Librarian).GetProperty(colName)?.GetValue(l, null)).ToList()
                : list.OrderByDescending(l => typeof(Librarian).GetProperty(colName)?.GetValue(l, null)).ToList();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e) => ShowLibrarianForm(LibrarianFormModeEnum.Add);

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedLibrarian();
            if (selected == null) { MessageBox.Show("Please select a librarian to edit.", "Edit Librarian", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowLibrarianForm(LibrarianFormModeEnum.Edit, selected);
        }

        private void toolStripButtonView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedLibrarian();
            if (selected == null) { MessageBox.Show("Please select a librarian to view.", "View Librarian", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowLibrarianForm(LibrarianFormModeEnum.View, selected, refresh: false);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedLibrarian();
            if (selected == null) { MessageBox.Show("Please select a librarian to delete.", "Delete Librarian", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show("Delete the selected librarian?", "Delete Librarian", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                service.DeleteLibrarian(selected.Id);
                _ = RefreshGridAsync();
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e) => _ = RefreshGridAsync();
        private void textBoxSearch_TextChanged(object sender, EventArgs e) => _ = RefreshGridAsync();
    }
}
