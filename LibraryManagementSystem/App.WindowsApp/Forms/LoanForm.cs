using App.Core.Enums;
using App.Core.Interfaces;
using App.Core.Models;
using System;
using System.Windows.Forms;

namespace App.WindowsApp.Forms
{
    /// <summary>
    /// Add/Edit/View form for a Loan. Beyond the basic required-field validation used
    /// elsewhere in the app, this form keeps the linked Book's Status in sync with the
    /// Loan's lifecycle: issuing a loan marks the book Borrowed, marking a loan Returned
    /// marks the book Available again.
    /// </summary>
    public partial class LoanForm : Form
    {
        private readonly LoanFormModeEnum _mode;
        private readonly Loan? _loan;
        private readonly ILoanService _service;
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;

        public LoanForm(LoanFormModeEnum mode, Loan? loan, ILoanService service, IBookService bookService, IMemberService memberService)
        {
            _mode = mode;
            _loan = loan;
            _service = service;
            _bookService = bookService;
            _memberService = memberService;
            InitializeComponent();
            ConfigureMode();
            LoadDropdowns();
            if (loan != null) PopulateFields(loan);
        }

        private void LoadDropdowns()
        {
            comboBoxBook.Items.Clear();
            bool restrictToAvailable = _mode == LoanFormModeEnum.Add;
            foreach (var b in _bookService.GetAllBooks())
            {
                if (!restrictToAvailable || b.Status == BookStatus.Available)
                    comboBoxBook.Items.Add(b);
            }
            comboBoxBook.DisplayMember = "Title";
            comboBoxBook.ValueMember = "Id";

            comboBoxMember.Items.Clear();
            foreach (var m in _memberService.GetAllMembers())
                comboBoxMember.Items.Add(m);
            comboBoxMember.DisplayMember = "Name";
            comboBoxMember.ValueMember = "Id";
        }

        private void ConfigureMode()
        {
            bool isView = _mode == LoanFormModeEnum.View;
            bool isAdd = _mode == LoanFormModeEnum.Add;

            comboBoxBook.Enabled = !isView;
            comboBoxMember.Enabled = !isView;
            dateTimePickerIssueDate.Enabled = !isView;
            dateTimePickerDueDate.Enabled = !isView;
            buttonSave.Visible = !isView;

            comboBoxStatus.Items.Clear();
            if (isAdd)
            {
                // A brand-new loan always starts out Issued; the book is not yet returnable.
                comboBoxStatus.Items.Add(LoanStatus.Issued);
                comboBoxStatus.SelectedIndex = 0;
                comboBoxStatus.Enabled = false;
                dateTimePickerReturnDate.Enabled = false;
            }
            else
            {
                foreach (LoanStatus s in Enum.GetValues(typeof(LoanStatus)))
                    comboBoxStatus.Items.Add(s);
                comboBoxStatus.SelectedIndex = 0;
                comboBoxStatus.Enabled = !isView;
            }

            comboBoxStatus.SelectedIndexChanged += (s, e) => UpdateReturnDateState();
            UpdateReturnDateState();

            this.Text = _mode switch
            {
                LoanFormModeEnum.Add => "Issue Book",
                LoanFormModeEnum.Edit => "Edit Loan",
                _ => "View Loan"
            };
        }

        private void UpdateReturnDateState()
        {
            bool isView = _mode == LoanFormModeEnum.View;
            bool isReturned = comboBoxStatus.SelectedItem is LoanStatus s && s == LoanStatus.Returned;
            dateTimePickerReturnDate.Enabled = isReturned && !isView;
            if (isReturned && dateTimePickerReturnDate.Value.Date == DateTime.Today && _mode == LoanFormModeEnum.Edit)
                dateTimePickerReturnDate.Value = DateTime.Today;
        }

        private void PopulateFields(Loan loan)
        {
            textBoxId.Text = loan.Id;
            dateTimePickerIssueDate.Value = loan.IssueDate;
            dateTimePickerDueDate.Value = loan.DueDate;
            dateTimePickerReturnDate.Value = loan.ReturnDate ?? DateTime.Today;
            comboBoxStatus.SelectedItem = loan.Status;

            foreach (var item in comboBoxBook.Items)
            {
                if (item is Book b && b.Id == loan.BookId)
                { comboBoxBook.SelectedItem = item; break; }
            }
            foreach (var item in comboBoxMember.Items)
            {
                if (item is Member m && m.Id == loan.MemberId)
                { comboBoxMember.SelectedItem = item; break; }
            }
            UpdateReturnDateState();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxBook.SelectedItem is not Book selectedBook)
            {
                MessageBox.Show("Please select a book.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxMember.SelectedItem is not Member selectedMember)
            {
                MessageBox.Show("Please select a member.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePickerDueDate.Value.Date < dateTimePickerIssueDate.Value.Date)
            {
                MessageBox.Show("Due date cannot be earlier than the issue date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoanStatus selectedStatus = comboBoxStatus.SelectedItem is LoanStatus s ? s : LoanStatus.Issued;

            if (_mode == LoanFormModeEnum.Add)
            {
                Loan loan = new Loan
                {
                    BookId = selectedBook.Id,
                    MemberId = selectedMember.Id,
                    IssueDate = dateTimePickerIssueDate.Value.Date,
                    DueDate = dateTimePickerDueDate.Value.Date,
                    ReturnDate = null,
                    Status = LoanStatus.Issued
                };
                _service.AddLoan(loan);

                // A freshly issued loan takes the book off the shelf.
                selectedBook.Status = BookStatus.Borrowed;
                _bookService.UpdateBook(selectedBook);
            }
            else
            {
                _loan!.BookId = selectedBook.Id;
                _loan.MemberId = selectedMember.Id;
                _loan.IssueDate = dateTimePickerIssueDate.Value.Date;
                _loan.DueDate = dateTimePickerDueDate.Value.Date;
                _loan.Status = selectedStatus;
                _loan.ReturnDate = selectedStatus == LoanStatus.Returned ? dateTimePickerReturnDate.Value.Date : null;
                _service.UpdateLoan(_loan);

                // Keep the book's shelf status consistent with the loan's outcome.
                selectedBook.Status = selectedStatus == LoanStatus.Returned ? BookStatus.Available : BookStatus.Borrowed;
                _bookService.UpdateBook(selectedBook);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
