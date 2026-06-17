namespace App.WindowsApp.Views
{
    partial class MemberView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            toolStrip = new ToolStrip();
            toolStripButtonAdd = new ToolStripButton();
            toolStripButtonEdit = new ToolStripButton();
            toolStripButtonView = new ToolStripButton();
            toolStripButtonDelete = new ToolStripButton();
            toolStripButtonRefresh = new ToolStripButton();
            textBoxSearch = new TextBox();
            labelSearch = new Label();
            dataGridViewMembers = new DataGridView();
            labelTitle = new Label();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMembers).BeginInit();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.BackColor = Color.White;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonAdd, toolStripButtonEdit, toolStripButtonView, toolStripButtonDelete, toolStripButtonRefresh });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(980, 25);
            toolStrip.TabIndex = 1;
            // 
            // toolStripButtonAdd
            // 
            toolStripButtonAdd.Name = "toolStripButtonAdd";
            toolStripButtonAdd.Size = new Size(44, 22);
            toolStripButtonAdd.Text = "+ Add";
            toolStripButtonAdd.Click += toolStripButtonAdd_Click;
            // 
            // toolStripButtonEdit
            // 
            toolStripButtonEdit.Name = "toolStripButtonEdit";
            toolStripButtonEdit.Size = new Size(46, 22);
            toolStripButtonEdit.Text = "✎ Edit";
            toolStripButtonEdit.Click += toolStripButtonEdit_Click;
            // 
            // toolStripButtonView
            // 
            toolStripButtonView.Name = "toolStripButtonView";
            toolStripButtonView.Size = new Size(51, 22);
            toolStripButtonView.Text = "👁 View";
            toolStripButtonView.Click += toolStripButtonView_Click;
            // 
            // toolStripButtonDelete
            // 
            toolStripButtonDelete.Name = "toolStripButtonDelete";
            toolStripButtonDelete.Size = new Size(59, 22);
            toolStripButtonDelete.Text = "🗑 Delete";
            toolStripButtonDelete.Click += toolStripButtonDelete_Click;
            // 
            // toolStripButtonRefresh
            // 
            toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            toolStripButtonRefresh.Size = new Size(63, 22);
            toolStripButtonRefresh.Text = "↻ Refresh";
            toolStripButtonRefresh.Click += toolStripButtonRefresh_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Font = new Font("Segoe UI", 9.5F);
            textBoxSearch.Location = new Point(72, 86);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(220, 24);
            textBoxSearch.TabIndex = 3;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // labelSearch
            // 
            labelSearch.Font = new Font("Segoe UI", 9.5F);
            labelSearch.Location = new Point(16, 88);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(52, 24);
            labelSearch.TabIndex = 2;
            labelSearch.Text = "Search:";
            // 
            // dataGridViewMembers
            // 
            dataGridViewMembers.AllowUserToAddRows = false;
            dataGridViewMembers.AllowUserToDeleteRows = false;
            dataGridViewMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMembers.BackgroundColor = Color.White;
            dataGridViewMembers.BorderStyle = BorderStyle.None;
            dataGridViewMembers.Font = new Font("Segoe UI", 9F);
            dataGridViewMembers.Location = new Point(16, 122);
            dataGridViewMembers.Name = "dataGridViewMembers";
            dataGridViewMembers.ReadOnly = true;
            dataGridViewMembers.RowHeadersVisible = false;
            dataGridViewMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMembers.Size = new Size(948, 480);
            dataGridViewMembers.TabIndex = 4;
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(30, 40, 60);
            labelTitle.Location = new Point(0, 25);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(340, 30);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Member Management";
            // 
            // MemberView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 252);
            Controls.Add(labelTitle);
            Controls.Add(toolStrip);
            Controls.Add(labelSearch);
            Controls.Add(textBoxSearch);
            Controls.Add(dataGridViewMembers);
            Font = new Font("Segoe UI", 9F);
            Name = "MemberView";
            Size = new Size(980, 620);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMembers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonView;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.DataGridView dataGridViewMembers;
        private System.Windows.Forms.Label labelTitle;
    }
}
