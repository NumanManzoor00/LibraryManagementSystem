namespace App.WindowsApp.Forms
{
    partial class LibrarianForm
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
            labelName = new Label();
            textBoxName = new TextBox();
            labelEmail = new Label();
            textBoxEmail = new TextBox();
            buttonSave = new Button();

            SuspendLayout();

            int labelX = 20;
            int inputX = 140;
            int labelW = 110;
            int inputW = 280;
            int rowH = 30;
            int startY = 20;
            int gap = 40;

            AddRow(labelId, "Librarian ID:", textBoxId, startY, labelX, inputX, labelW, inputW, rowH);
            textBoxId.ReadOnly = true;
            textBoxId.BackColor = Color.FromArgb(240, 240, 240);

            AddRow(labelName, "Name:", textBoxName, startY + gap, labelX, inputX, labelW, inputW, rowH);

            AddRow(labelEmail, "Email:", textBoxEmail, startY + gap * 2, labelX, inputX, labelW, inputW, rowH);

            buttonSave.Location = new Point(inputX, startY + gap * 3 + 10);
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
                labelName, textBoxName,
                labelEmail, textBoxEmail,
                buttonSave
            });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 200);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LibrarianForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Librarian";
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
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Button buttonSave;
    }
}
