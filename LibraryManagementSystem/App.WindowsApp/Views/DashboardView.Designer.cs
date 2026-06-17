namespace App.WindowsApp.Views
{
    partial class DashboardView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new Label();
            panelCards = new Panel();
            panelStatusChart = new Panel();
            panelGenreChart = new Panel();
            buttonRefresh = new Button();

            labelTotalBooks = new Label();
            labelAvailableBooks = new Label();
            labelBorrowedBooks = new Label();
            labelReservedBooks = new Label();
            labelLostBooks = new Label();
            labelMembersCount = new Label();
            labelLibrariansCount = new Label();
            labelLoansCount = new Label();

            panelCards.SuspendLayout();
            SuspendLayout();

            // Title
            labelTitle.Text = "Dashboard Overview";
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(30, 40, 60);
            labelTitle.Location = new Point(20, 16);
            labelTitle.Size = new Size(400, 36);
            labelTitle.AutoSize = false;

            // Refresh button
            buttonRefresh.Text = "↻  Refresh";
            buttonRefresh.Font = new Font("Segoe UI", 9.5F);
            buttonRefresh.Location = new Point(820, 16);
            buttonRefresh.Size = new Size(100, 32);
            buttonRefresh.BackColor = Color.FromArgb(0, 120, 212);
            buttonRefresh.ForeColor = Color.White;
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.FlatAppearance.BorderSize = 0;
            buttonRefresh.Click += buttonRefresh_Click;

            // panelCards - stat cards
            panelCards.Location = new Point(20, 60);
            panelCards.Size = new Size(930, 180);
            panelCards.BackColor = Color.Transparent;

            // Build stat cards
            Action<Label, string, string, int, Color> addCard = (lbl, caption, value, x, accent) =>
            {
                var card = new Panel();
                card.Location = new Point(x, 0);
                card.Size = new Size(108, 80);
                card.BackColor = Color.White;
                card.Paint += (s, e) =>
                {
                    using var pen = new Pen(accent, 3);
                    e.Graphics.DrawLine(pen, 0, card.Height - 3, card.Width, card.Height - 3);
                };

                var lblCaption = new Label();
                lblCaption.Text = caption;
                lblCaption.Font = new Font("Segoe UI", 8F);
                lblCaption.ForeColor = Color.Gray;
                lblCaption.Location = new Point(8, 10);
                lblCaption.Size = new Size(92, 16);

                lbl.Text = value;
                lbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
                lbl.ForeColor = accent;
                lbl.Location = new Point(8, 28);
                lbl.Size = new Size(92, 36);

                card.Controls.Add(lblCaption);
                card.Controls.Add(lbl);
                panelCards.Controls.Add(card);
            };

            addCard(labelTotalBooks, "Total Books", "0", 0, Color.FromArgb(0, 120, 212));
            addCard(labelAvailableBooks, "Available", "0", 118, Color.SeaGreen);
            addCard(labelBorrowedBooks, "Borrowed", "0", 236, Color.CornflowerBlue);
            addCard(labelReservedBooks, "Reserved", "0", 354, Color.Goldenrod);
            addCard(labelLostBooks, "Lost", "0", 472, Color.Firebrick);
            addCard(labelMembersCount, "Members", "0", 590, Color.MediumSeaGreen);
            addCard(labelLibrariansCount, "Librarians", "0", 708, Color.MediumPurple);
            addCard(labelLoansCount, "Loans", "0", 826, Color.SteelBlue);

            // Charts
            panelStatusChart.Location = new Point(20, 260);
            panelStatusChart.Size = new Size(440, 180);
            panelStatusChart.BackColor = Color.White;
            panelStatusChart.Paint += panelStatusChart_Paint;

            panelGenreChart.Location = new Point(480, 260);
            panelGenreChart.Size = new Size(470, 180);
            panelGenreChart.BackColor = Color.White;
            panelGenreChart.Paint += panelGenreChart_Paint;

            // UserControl
            Controls.Add(labelTitle);
            Controls.Add(buttonRefresh);
            Controls.Add(panelCards);
            Controls.Add(panelStatusChart);
            Controls.Add(panelGenreChart);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 252);
            Name = "DashboardView";
            Size = new Size(980, 680);
            Font = new Font("Segoe UI", 9F);

            panelCards.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.Panel panelStatusChart;
        private System.Windows.Forms.Panel panelGenreChart;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label labelTotalBooks;
        private System.Windows.Forms.Label labelAvailableBooks;
        private System.Windows.Forms.Label labelBorrowedBooks;
        private System.Windows.Forms.Label labelReservedBooks;
        private System.Windows.Forms.Label labelLostBooks;
        private System.Windows.Forms.Label labelMembersCount;
        private System.Windows.Forms.Label labelLibrariansCount;
        private System.Windows.Forms.Label labelLoansCount;
    }
}
