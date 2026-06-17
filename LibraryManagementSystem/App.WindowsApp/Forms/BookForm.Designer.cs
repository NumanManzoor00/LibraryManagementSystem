namespace App.WindowsApp.Forms
{
    partial class BookForm
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
            labelTitle = new Label();
            textBoxTitle = new TextBox();
            labelAuthor = new Label();
            textBoxAuthor = new TextBox();
            labelISBN = new Label();
            textBoxISBN = new TextBox();
            labelGenre = new Label();
            comboBoxGenre = new ComboBox();
            labelStatus = new Label();
            comboBoxStatus = new ComboBox();
            buttonSave = new Button();

            SuspendLayout();

            int labelX = 20;
            int inputX = 140;
            int labelW = 110;
            int inputW = 320;
            int rowH = 30;
            int startY = 20;
            int gap = 40;

            AddRow(labelId, "Book ID:", textBoxId, startY, labelX, inputX, labelW, inputW, rowH);
            textBoxId.ReadOnly = true;
            textBoxId.BackColor = Color.FromArgb(240, 240, 240);

            AddRow(labelTitle, "Title:", textBoxTitle, startY + gap, labelX, inputX, labelW, inputW, rowH);

            AddRow(labelAuthor, "Author:", textBoxAuthor, startY + gap * 2, labelX, inputX, labelW, inputW, rowH);

            AddRow(labelISBN, "ISBN:", textBoxISBN, startY + gap * 3, labelX, inputX, labelW, inputW, rowH);

            AddRow(labelGenre, "Genre:", comboBoxGenre, startY + gap * 4, labelX, inputX, labelW, inputW, rowH);
            comboBoxGenre.DropDownStyle = ComboBoxStyle.DropDownList;

            AddRow(labelStatus, "Status:", comboBoxStatus, startY + gap * 5, labelX, inputX, labelW, inputW, rowH);
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            buttonSave.Location = new Point(inputX, startY + gap * 6 + 10);
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
                labelTitle, textBoxTitle,
                labelAuthor, textBoxAuthor,
                labelISBN, textBoxISBN,
                labelGenre, comboBoxGenre,
                labelStatus, comboBoxStatus,
                buttonSave
            });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 320);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BookForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Book";
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
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.TextBox textBoxAuthor;
        private System.Windows.Forms.Label labelISBN;
        private System.Windows.Forms.TextBox textBoxISBN;
        private System.Windows.Forms.Label labelGenre;
        private System.Windows.Forms.ComboBox comboBoxGenre;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button buttonSave;
    }
}
