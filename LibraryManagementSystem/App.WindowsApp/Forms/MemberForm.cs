using App.Core.Interfaces;
using App.Core.Models;
using System;
using System.Windows.Forms;

namespace App.WindowsApp.Forms
{
    public partial class MemberForm : Form
    {
        private readonly MemberFormModeEnum _mode;
        private readonly Member? _member;
        private readonly IMemberService _service;

        public MemberForm(MemberFormModeEnum mode, Member? member, IMemberService service)
        {
            _mode = mode;
            _member = member;
            _service = service;
            InitializeComponent();
            ConfigureMode();
            if (member != null) PopulateFields(member);
        }

        private void ConfigureMode()
        {
            bool isView = _mode == MemberFormModeEnum.View;
            textBoxName.ReadOnly = isView;
            textBoxEmail.ReadOnly = isView;
            textBoxPhone.ReadOnly = isView;
            buttonSave.Visible = !isView;

            this.Text = _mode switch
            {
                MemberFormModeEnum.Add => "Add Member",
                MemberFormModeEnum.Edit => "Edit Member",
                _ => "View Member"
            };
        }

        private void PopulateFields(Member member)
        {
            textBoxId.Text = member.Id;
            textBoxName.Text = member.Name;
            textBoxEmail.Text = member.Email;
            textBoxPhone.Text = member.Phone;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_mode == MemberFormModeEnum.Add)
            {
                Member member = new Member
                {
                    Name = textBoxName.Text.Trim(),
                    Email = textBoxEmail.Text.Trim(),
                    Phone = textBoxPhone.Text.Trim()
                };
                _service.AddMember(member);
            }
            else
            {
                _member!.Name = textBoxName.Text.Trim();
                _member.Email = textBoxEmail.Text.Trim();
                _member.Phone = textBoxPhone.Text.Trim();
                _service.UpdateMember(_member);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
