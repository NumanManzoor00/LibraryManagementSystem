using App.WindowsApp.Views;
using System;
using System.Windows.Forms;

namespace App.WindowsApp.Forms
{
    public partial class DashboardForm : Form
    {
        private Button? _activeButton;

        public DashboardForm()
        {
            InitializeComponent();

            // App icon for the title bar / taskbar
            try
            {
                using var bmp = Properties.Resources.app;
                this.Icon = Icon.FromHandle(bmp.GetHicon());
            }
            catch { /* icon is cosmetic only; ignore failures on non-Windows test hosts */ }

            // Update status time every minute
            var timer = new System.Windows.Forms.Timer { Interval = 60000 };
            timer.Tick += (s, e) => statusLabelTime.Text = DateTime.Now.ToString("HH:mm");
            timer.Start();

            ShowView(new DashboardView(), buttonDashboard, "Dashboard");
        }

        private void ShowView(UserControl view, Button sender, string section)
        {
            this.panelContent.Controls.Clear();
            view.Dock = DockStyle.Fill;
            this.panelContent.Controls.Add(view);

            // Highlight active button
            if (_activeButton != null)
                _activeButton.BackColor = System.Drawing.Color.FromArgb(30, 40, 60);
            _activeButton = sender;
            sender.BackColor = System.Drawing.Color.FromArgb(0, 90, 160);

            statusLabelInfo.Text = $"Section: {section}";
            statusLabelTime.Text = DateTime.Now.ToString("HH:mm");
        }

        /// <summary>Called by child views to update the record-count label in the status bar.</summary>
        public void SetStatusRecord(string text) => statusLabelRecord.Text = text;

        /// <summary>Called by child views to update the info label in the status bar.</summary>
        public void SetStatusInfo(string text) => statusLabelInfo.Text = text;

        private void buttonDashboard_Click(object sender, EventArgs e) =>
            ShowView(new DashboardView(), buttonDashboard, "Dashboard");

        private void buttonBooks_Click(object sender, EventArgs e) =>
            ShowView(new BookView(), buttonBooks, "Books");

        private void buttonMembers_Click(object sender, EventArgs e) =>
            ShowView(new MemberView(), buttonMembers, "Members");

        private void buttonLibrarians_Click(object sender, EventArgs e) =>
            ShowView(new LibrarianView(), buttonLibrarians, "Librarians");

        private void buttonLoans_Click(object sender, EventArgs e) =>
            ShowView(new LoanView(), buttonLoans, "Loans");

        private void buttonCharts_Click(object sender, EventArgs e) =>
            ShowView(new LiveChartView(), buttonCharts, "Charts");
    }
}
