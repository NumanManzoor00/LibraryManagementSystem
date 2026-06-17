namespace App.WindowsApp.Views
{
    partial class BookView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            toolStrip = new ToolStrip();
            toolStripButtonAdd = new ToolStripButton();
            toolStripButtonEdit = new ToolStripButton();
            toolStripButtonView = new ToolStripButton();
            toolStripButtonDelete = new ToolStripButton();
            toolStripButtonRefresh = new ToolStripButton();
            textBoxSearch = new TextBox();
            comboBoxStatusFilter = new ComboBox();
            labelSearch = new Label();
            labelFilter = new Label();
            dataGridViewBooks = new DataGridView();
            labelTitle = new Label();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
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
            // comboBoxStatusFilter
            // 
            comboBoxStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatusFilter.Font = new Font("Segoe UI", 9.5F);
            comboBoxStatusFilter.Location = new Point(364, 86);
            comboBoxStatusFilter.Name = "comboBoxStatusFilter";
            comboBoxStatusFilter.Size = new Size(160, 25);
            comboBoxStatusFilter.TabIndex = 5;
            comboBoxStatusFilter.SelectedIndexChanged += comboBoxStatusFilter_SelectedIndexChanged;
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
            // labelFilter
            // 
            labelFilter.Font = new Font("Segoe UI", 9.5F);
            labelFilter.Location = new Point(308, 88);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(52, 24);
            labelFilter.TabIndex = 4;
            labelFilter.Text = "Status:";
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.AllowUserToDeleteRows = false;
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBooks.BackgroundColor = Color.White;
            dataGridViewBooks.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewBooks.Font = new Font("Segoe UI", 9F);
            dataGridViewBooks.Location = new Point(16, 122);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.RowHeadersVisible = false;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.Size = new Size(948, 480);
            dataGridViewBooks.TabIndex = 6;
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(30, 40, 60);
            labelTitle.Location = new Point(0, 25);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(340, 30);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Book Catalog";
            // 
            // BookView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 252);
            Controls.Add(labelTitle);
            Controls.Add(toolStrip);
            Controls.Add(labelSearch);
            Controls.Add(textBoxSearch);
            Controls.Add(labelFilter);
            Controls.Add(comboBoxStatusFilter);
            Controls.Add(dataGridViewBooks);
            Font = new Font("Segoe UI", 9F);
            Name = "BookView";
            Size = new Size(980, 620);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
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
        private System.Windows.Forms.ComboBox comboBoxStatusFilter;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.Label labelTitle;
    }
}
