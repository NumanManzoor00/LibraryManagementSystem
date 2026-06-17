namespace App.WindowsApp.Forms
{
    partial class DashboardForm
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
            panelSidebar = new Panel();
            labelAppTitle = new Label();
            buttonDashboard = new Button();
            buttonBooks = new Button();
            buttonMembers = new Button();
            buttonLibrarians = new Button();
            buttonLoans = new Button();
            buttonCharts = new Button();
            panelContent = new Panel();
            statusStrip = new StatusStrip();
            statusLabelInfo = new ToolStripStatusLabel();
            statusLabelRecord = new ToolStripStatusLabel();
            statusLabelTime = new ToolStripStatusLabel();
            panelSidebar.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(30, 40, 60);
            panelSidebar.Controls.Add(labelAppTitle);
            panelSidebar.Controls.Add(buttonDashboard);
            panelSidebar.Controls.Add(buttonBooks);
            panelSidebar.Controls.Add(buttonMembers);
            panelSidebar.Controls.Add(buttonLibrarians);
            panelSidebar.Controls.Add(buttonLoans);
            panelSidebar.Controls.Add(buttonCharts);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(185, 702);
            panelSidebar.TabIndex = 0;
            // 
            // labelAppTitle
            // 
            labelAppTitle.BackColor = Color.FromArgb(20, 30, 50);
            labelAppTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelAppTitle.ForeColor = Color.White;
            labelAppTitle.Location = new Point(0, 0);
            labelAppTitle.Name = "labelAppTitle";
            labelAppTitle.Size = new Size(185, 65);
            labelAppTitle.TabIndex = 0;
            labelAppTitle.Text = "  Library\r\n  Management System";
            labelAppTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonDashboard
            // 
            buttonDashboard.BackColor = Color.FromArgb(30, 40, 60);
            buttonDashboard.FlatAppearance.BorderSize = 0;
            buttonDashboard.FlatStyle = FlatStyle.Flat;
            buttonDashboard.Font = new Font("Segoe UI", 10F);
            buttonDashboard.ForeColor = Color.FromArgb(210, 220, 240);
            buttonDashboard.Image = Properties.Resources.icons8_dashboard_32;
            buttonDashboard.Location = new Point(0, 75);
            buttonDashboard.Name = "buttonDashboard";
            buttonDashboard.Padding = new Padding(20, 0, 0, 0);
            buttonDashboard.Size = new Size(185, 44);
            buttonDashboard.TabIndex = 1;
            buttonDashboard.Text = "Dashboard";
            buttonDashboard.TextAlign = ContentAlignment.MiddleLeft;
            buttonDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDashboard.UseVisualStyleBackColor = false;
            buttonDashboard.Click += buttonDashboard_Click;
            // 
            // buttonBooks
            // 
            buttonBooks.BackColor = Color.FromArgb(30, 40, 60);
            buttonBooks.FlatAppearance.BorderSize = 0;
            buttonBooks.FlatStyle = FlatStyle.Flat;
            buttonBooks.Font = new Font("Segoe UI", 10F);
            buttonBooks.ForeColor = Color.FromArgb(210, 220, 240);
            buttonBooks.Image = Properties.Resources.icons8_books_32;
            buttonBooks.Location = new Point(0, 125);
            buttonBooks.Name = "buttonBooks";
            buttonBooks.Padding = new Padding(20, 0, 0, 0);
            buttonBooks.Size = new Size(185, 44);
            buttonBooks.TabIndex = 2;
            buttonBooks.Text = "Books";
            buttonBooks.TextAlign = ContentAlignment.MiddleLeft;
            buttonBooks.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonBooks.UseVisualStyleBackColor = false;
            buttonBooks.Click += buttonBooks_Click;
            // 
            // buttonMembers
            // 
            buttonMembers.BackColor = Color.FromArgb(30, 40, 60);
            buttonMembers.FlatAppearance.BorderSize = 0;
            buttonMembers.FlatStyle = FlatStyle.Flat;
            buttonMembers.Font = new Font("Segoe UI", 10F);
            buttonMembers.ForeColor = Color.FromArgb(210, 220, 240);
            buttonMembers.Image = Properties.Resources.icons8_members_32;
            buttonMembers.Location = new Point(0, 175);
            buttonMembers.Name = "buttonMembers";
            buttonMembers.Padding = new Padding(20, 0, 0, 0);
            buttonMembers.Size = new Size(185, 44);
            buttonMembers.TabIndex = 3;
            buttonMembers.Text = "Members";
            buttonMembers.TextAlign = ContentAlignment.MiddleLeft;
            buttonMembers.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMembers.UseVisualStyleBackColor = false;
            buttonMembers.Click += buttonMembers_Click;
            // 
            // buttonLibrarians
            // 
            buttonLibrarians.BackColor = Color.FromArgb(30, 40, 60);
            buttonLibrarians.FlatAppearance.BorderSize = 0;
            buttonLibrarians.FlatStyle = FlatStyle.Flat;
            buttonLibrarians.Font = new Font("Segoe UI", 10F);
            buttonLibrarians.ForeColor = Color.FromArgb(210, 220, 240);
            buttonLibrarians.Image = Properties.Resources.icons8_librarian_32;
            buttonLibrarians.Location = new Point(0, 225);
            buttonLibrarians.Name = "buttonLibrarians";
            buttonLibrarians.Padding = new Padding(20, 0, 0, 0);
            buttonLibrarians.Size = new Size(185, 44);
            buttonLibrarians.TabIndex = 4;
            buttonLibrarians.Text = "Librarians";
            buttonLibrarians.TextAlign = ContentAlignment.MiddleLeft;
            buttonLibrarians.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonLibrarians.UseVisualStyleBackColor = false;
            buttonLibrarians.Click += buttonLibrarians_Click;
            // 
            // buttonLoans
            // 
            buttonLoans.BackColor = Color.FromArgb(30, 40, 60);
            buttonLoans.FlatAppearance.BorderSize = 0;
            buttonLoans.FlatStyle = FlatStyle.Flat;
            buttonLoans.Font = new Font("Segoe UI", 10F);
            buttonLoans.ForeColor = Color.FromArgb(210, 220, 240);
            buttonLoans.Image = Properties.Resources.icons8_loans_32;
            buttonLoans.Location = new Point(0, 275);
            buttonLoans.Name = "buttonLoans";
            buttonLoans.Padding = new Padding(20, 0, 0, 0);
            buttonLoans.Size = new Size(185, 44);
            buttonLoans.TabIndex = 5;
            buttonLoans.Text = "Loans";
            buttonLoans.TextAlign = ContentAlignment.MiddleLeft;
            buttonLoans.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonLoans.UseVisualStyleBackColor = false;
            buttonLoans.Click += buttonLoans_Click;
            // 
            // buttonCharts
            // 
            buttonCharts.BackColor = Color.FromArgb(30, 40, 60);
            buttonCharts.FlatAppearance.BorderSize = 0;
            buttonCharts.FlatStyle = FlatStyle.Flat;
            buttonCharts.Font = new Font("Segoe UI", 10F);
            buttonCharts.ForeColor = Color.FromArgb(210, 220, 240);
            buttonCharts.Image = Properties.Resources.icons8_charts_32;
            buttonCharts.Location = new Point(0, 325);
            buttonCharts.Name = "buttonCharts";
            buttonCharts.Padding = new Padding(20, 0, 0, 0);
            buttonCharts.Size = new Size(185, 44);
            buttonCharts.TabIndex = 6;
            buttonCharts.Text = "Charts";
            buttonCharts.TextAlign = ContentAlignment.MiddleLeft;
            buttonCharts.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCharts.UseVisualStyleBackColor = false;
            buttonCharts.Click += buttonCharts_Click;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(245, 247, 252);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(185, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(925, 678);
            panelContent.TabIndex = 0;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabelInfo, statusLabelRecord, statusLabelTime });
            statusStrip.Location = new Point(185, 678);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(925, 24);
            statusStrip.TabIndex = 1;
            // 
            // statusLabelInfo
            // 
            statusLabelInfo.Name = "statusLabelInfo";
            statusLabelInfo.Size = new Size(868, 19);
            statusLabelInfo.Spring = true;
            statusLabelInfo.Text = "Ready";
            // 
            // statusLabelRecord
            // 
            statusLabelRecord.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelRecord.Name = "statusLabelRecord";
            statusLabelRecord.Size = new Size(4, 19);
            // 
            // statusLabelTime
            // 
            statusLabelTime.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelTime.Name = "statusLabelTime";
            statusLabelTime.Size = new Size(38, 19);
            statusLabelTime.Text = "18:01";
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1110, 702);
            Controls.Add(panelContent);
            Controls.Add(statusStrip);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(900, 600);
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Library Management System";
            panelSidebar.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button buttonDashboard;
        private System.Windows.Forms.Button buttonBooks;
        private System.Windows.Forms.Button buttonMembers;
        private System.Windows.Forms.Button buttonLibrarians;
        private System.Windows.Forms.Button buttonLoans;
        private System.Windows.Forms.Button buttonCharts;
        private System.Windows.Forms.Label labelAppTitle;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelInfo;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelRecord;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelTime;
    }
}
