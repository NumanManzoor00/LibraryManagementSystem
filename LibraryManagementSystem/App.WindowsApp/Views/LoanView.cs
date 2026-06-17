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
    public partial class LoanView : UserControl
    {
        private const string AllStatuses = "--All--";

        private readonly ILoanService service;
        private readonly IBookService bookService;
        private readonly IMemberService memberService;
        private readonly BindingSource _dgvBindingSource = new BindingSource();

        public LoanView() : this(new LoanService(), new BookService(), new MemberService()) { }

        public LoanView(ILoanService _service, IBookService _bookService, IMemberService _memberService)
        {
            service = _service;
            bookService = _bookService;
            memberService = _memberService;
            InitializeComponent();
            LoadStatusFilter();
            dataGridViewLoans.DataSource = _dgvBindingSource;
            dataGridViewLoans.ColumnHeaderMouseClick += DataGridViewLoans_ColumnHeaderMouseClick;
            _ = RefreshGridAsync();
        }

        private void LoadStatusFilter()
        {
            comboBoxStatusFilter.Items.Clear();
            comboBoxStatusFilter.Items.Add(AllStatuses);
            foreach (LoanStatus s in Enum.GetValues(typeof(LoanStatus)))
                comboBoxStatusFilter.Items.Add(s);
            comboBoxStatusFilter.SelectedIndex = 0;
        }

        private async Task RefreshGridAsync()
        {
            toolStripButtonRefresh.Enabled = false;
            try
            {
                var loans = await service.GetAllLoansAsync();
                var filtered = GetFiltered(loans);
                _dgvBindingSource.DataSource = filtered.ToList();
                (this.FindForm() as DashboardForm)?.SetStatusRecord($"Records: {filtered.Count()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading loans", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                toolStripButtonRefresh.Enabled = true;
            }
        }

        private IEnumerable<Loan> GetFiltered(IEnumerable<Loan> loans)
        {
            string keyword = textBoxSearch.Text.Trim();
            LoanStatus? selectedStatus = comboBoxStatusFilter.SelectedItem is LoanStatus s ? s : null;

            if (!string.IsNullOrWhiteSpace(keyword))
                loans = loans.Where(l =>
                    l.BookTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    l.MemberName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (selectedStatus.HasValue)
                loans = loans.Where(l => l.Status == selectedStatus.Value);

            return loans;
        }

        private Loan? GetSelectedLoan() => _dgvBindingSource.Current as Loan;

        private void ShowLoanForm(LoanFormModeEnum mode, Loan? loan = null, bool refresh = true)
        {
            using LoanForm form = new LoanForm(mode, loan, service, bookService, memberService);
            form.ShowDialog();
            if (refresh) _ = RefreshGridAsync();
        }

        private string _sortColumn = "";
        private System.Windows.Forms.SortOrder _sortOrder = System.Windows.Forms.SortOrder.None;

        private void DataGridViewLoans_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            string colName = dataGridViewLoans.Columns[e.ColumnIndex].DataPropertyName;
            _sortOrder = _sortColumn == colName && _sortOrder == System.Windows.Forms.SortOrder.Ascending
                ? System.Windows.Forms.SortOrder.Descending
                : System.Windows.Forms.SortOrder.Ascending;
            _sortColumn = colName;
            dataGridViewLoans.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _sortOrder;

            var list = (_dgvBindingSource.DataSource as List<Loan>) ?? new List<Loan>();
            _dgvBindingSource.DataSource = _sortOrder == System.Windows.Forms.SortOrder.Ascending
                ? list.OrderBy(l => typeof(Loan).GetProperty(colName)?.GetValue(l, null)).ToList()
                : list.OrderByDescending(l => typeof(Loan).GetProperty(colName)?.GetValue(l, null)).ToList();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e) => ShowLoanForm(LoanFormModeEnum.Add);

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedLoan();
            if (selected == null) { MessageBox.Show("Please select a loan to edit.", "Edit Loan", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowLoanForm(LoanFormModeEnum.Edit, selected);
        }

        private void toolStripButtonView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedLoan();
            if (selected == null) { MessageBox.Show("Please select a loan to view.", "View Loan", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowLoanForm(LoanFormModeEnum.View, selected, refresh: false);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedLoan();
            if (selected == null) { MessageBox.Show("Please select a loan to delete.", "Delete Loan", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show("Delete the selected loan record? (This will not change the linked book's status.)", "Delete Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                service.DeleteLoan(selected.Id);
                _ = RefreshGridAsync();
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e) => _ = RefreshGridAsync();
        private void textBoxSearch_TextChanged(object sender, EventArgs e) => _ = RefreshGridAsync();
        private void comboBoxStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => _ = RefreshGridAsync();
    }
}
