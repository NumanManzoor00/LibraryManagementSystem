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
    public partial class MemberView : UserControl
    {
        private readonly IMemberService service;
        private readonly BindingSource _dgvBindingSource = new BindingSource();

        public MemberView() : this(new MemberService()) { }

        public MemberView(IMemberService _service)
        {
            service = _service;
            InitializeComponent();
            dataGridViewMembers.DataSource = _dgvBindingSource;
            dataGridViewMembers.ColumnHeaderMouseClick += DataGridViewMembers_ColumnHeaderMouseClick;
            _ = RefreshGridAsync();
        }

        private async Task RefreshGridAsync()
        {
            toolStripButtonRefresh.Enabled = false;
            try
            {
                var members = await service.GetAllMembersAsync();
                var filtered = GetFiltered(members);
                _dgvBindingSource.DataSource = filtered.ToList();
                (this.FindForm() as DashboardForm)?.SetStatusRecord($"Records: {filtered.Count()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading members", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                toolStripButtonRefresh.Enabled = true;
            }
        }

        private IEnumerable<Member> GetFiltered(IEnumerable<Member> members)
        {
            string keyword = textBoxSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword)) return members;
            return members.Where(m =>
                m.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        private Member? GetSelectedMember() => _dgvBindingSource.Current as Member;

        private void ShowMemberForm(MemberFormModeEnum mode, Member? member = null, bool refresh = true)
        {
            using MemberForm form = new MemberForm(mode, member, service);
            form.ShowDialog();
            if (refresh) _ = RefreshGridAsync();
        }

        private string _sortColumn = "";
        private System.Windows.Forms.SortOrder _sortOrder = System.Windows.Forms.SortOrder.None;

        private void DataGridViewMembers_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            string colName = dataGridViewMembers.Columns[e.ColumnIndex].DataPropertyName;
            _sortOrder = _sortColumn == colName && _sortOrder == System.Windows.Forms.SortOrder.Ascending
                ? System.Windows.Forms.SortOrder.Descending
                : System.Windows.Forms.SortOrder.Ascending;
            _sortColumn = colName;
            dataGridViewMembers.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _sortOrder;

            var list = (_dgvBindingSource.DataSource as List<Member>) ?? new List<Member>();
            _dgvBindingSource.DataSource = _sortOrder == System.Windows.Forms.SortOrder.Ascending
                ? list.OrderBy(m => typeof(Member).GetProperty(colName)?.GetValue(m, null)).ToList()
                : list.OrderByDescending(m => typeof(Member).GetProperty(colName)?.GetValue(m, null)).ToList();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e) => ShowMemberForm(MemberFormModeEnum.Add);

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMember();
            if (selected == null) { MessageBox.Show("Please select a member to edit.", "Edit Member", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowMemberForm(MemberFormModeEnum.Edit, selected);
        }

        private void toolStripButtonView_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMember();
            if (selected == null) { MessageBox.Show("Please select a member to view.", "View Member", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ShowMemberForm(MemberFormModeEnum.View, selected, refresh: false);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMember();
            if (selected == null) { MessageBox.Show("Please select a member to delete.", "Delete Member", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show("Delete the selected member?", "Delete Member", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                service.DeleteMember(selected.Id);
                _ = RefreshGridAsync();
            }
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e) => _ = RefreshGridAsync();
        private void textBoxSearch_TextChanged(object sender, EventArgs e) => _ = RefreshGridAsync();
    }
}
