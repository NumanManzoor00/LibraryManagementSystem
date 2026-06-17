namespace App.WindowsApp.Forms
{
    partial class LoanForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelId = new Label();
            textBoxId = new TextBox();
            labelBook = new Label();
            comboBoxBook = new ComboBox();
            labelMember = new Label();
            comboBoxMember = new ComboBox();
            labelIssueDate = new Label();
            dateTimePickerIssueDate = new DateTimePicker();
            labelDueDate = new Label();
            dateTimePickerDueDate = new DateTimePicker();
            labelStatus = new Label();
            comboBoxStatus = new ComboBox();
            labelReturnDate = new Label();
            dateTimePickerReturnDate = new DateTimePicker();
            buttonSave = new Button();

            SuspendLayout();

            int labelX = 20;
            int inputX = 150;
            int labelW = 120;
            int inputW = 300;
            int rowH = 30;
            int startY = 20;
            int gap = 40;

            AddRow(labelId, "Loan ID:", textBoxId, startY, labelX, inputX, labelW, inputW, rowH);
            textBoxId.ReadOnly = true;
            textBoxId.BackColor = Color.FromArgb(240, 240, 240);

            AddRow(labelBook, "Book:", comboBoxBook, startY + gap, labelX, inputX, labelW, inputW, rowH);
            comboBoxBook.DropDownStyle = ComboBoxStyle.DropDownList;

            AddRow(labelMember, "Member:", comboBoxMember, startY + gap * 2, labelX, inputX, labelW, inputW, rowH);
            comboBoxMember.DropDownStyle = ComboBoxStyle.DropDownList;

            AddRow(labelIssueDate, "Issue Date:", dateTimePickerIssueDate, startY + gap * 3, labelX, inputX, labelW, inputW, rowH);
            dateTimePickerIssueDate.Format = DateTimePickerFormat.Short;

            AddRow(labelDueDate, "Due Date:", dateTimePickerDueDate, startY + gap * 4, labelX, inputX, labelW, inputW, rowH);
            dateTimePickerDueDate.Format = DateTimePickerFormat.Short;

            AddRow(labelStatus, "Status:", comboBoxStatus, startY + gap * 5, labelX, inputX, labelW, inputW, rowH);
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            AddRow(labelReturnDate, "Return Date:", dateTimePickerReturnDate, startY + gap * 6, labelX, inputX, labelW, inputW, rowH);
            dateTimePickerReturnDate.Format = DateTimePickerFormat.Short;

            buttonSave.Location = new Point(inputX, startY + gap * 7 + 10);
            buttonSave.Size = new Size(100, 34);
            buttonSave.Text = "Save";
            buttonSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonSave.BackColor = Color.FromArgb(0, 120, 212);
            buttonSave.ForeColor = Color.White;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.Click += buttonSave_Click;

            Controls.AddRange(new Control[]
            {
                labelId, textBoxId,
                labelBook, comboBoxBook,
                labelMember, comboBoxMember,
                labelIssueDate, dateTimePickerIssueDate,
                labelDueDate, dateTimePickerDueDate,
                labelStatus, comboBoxStatus,
                labelReturnDate, dateTimePickerReturnDate,
                buttonSave
            });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 380);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoanForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Loan";
            Font = new Font("Segoe UI", 9F);

            ResumeLayout(false);
        }

        private void AddRow(Label lbl, string lblText, Control ctrl, int y, int labelX, int inputX, int labelW, int inputW, int rowH)
        {
            lbl.Text = lblText;
            lbl.Location = new Point(labelX, y + 4);
            lbl.Size = new Size(labelW, rowH);
            lbl.Font = new Font("Segoe UI", 9.5F);

            ctrl.Location = new Point(inputX, y);
            ctrl.Size = new Size(inputW, rowH);
            ctrl.Font = new Font("Segoe UI", 9.5F);
        }

        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelBook;
        private System.Windows.Forms.ComboBox comboBoxBook;
        private System.Windows.Forms.Label labelMember;
        private System.Windows.Forms.ComboBox comboBoxMember;
        private System.Windows.Forms.Label labelIssueDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerIssueDate;
        private System.Windows.Forms.Label labelDueDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDueDate;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelReturnDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerReturnDate;
        private System.Windows.Forms.Button buttonSave;
    }
}
