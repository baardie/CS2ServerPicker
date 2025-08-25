namespace CS2_Server_Picker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnUnblock;
        private System.Windows.Forms.ComboBox cmbPresets;
        private System.Windows.Forms.Panel bottomPanel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bindingSource = new BindingSource(components);
            btnApply = new Button();
            btnUnblock = new Button();
            cmbPresets = new ComboBox();
            bottomPanel = new Panel();
            lblStatus = new Label();
            progressBarLoading = new ProgressBar();
            grpRecommended = new GroupBox();
            flowRecommended = new FlowLayoutPanel();
            btnRefreshRegions = new Button();
            btnSortPing = new Button();
            btnClearAllowed = new Button();
            label1 = new Label();
            cardPanel1 = new CS2_Server_Picker.UI.Custom.CardPanel();
            chkBlockAllButSelected = new CheckBox();
            gridRegions = new DataGridView();
            colAllowed = new DataGridViewCheckBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPing = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            bottomPanel.SuspendLayout();
            grpRecommended.SuspendLayout();
            cardPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRegions).BeginInit();
            SuspendLayout();
            // 
            // btnApply
            // 
            btnApply.Location = new Point(20, 394);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(100, 40);
            btnApply.TabIndex = 1;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            // 
            // btnUnblock
            // 
            btnUnblock.Location = new Point(397, 394);
            btnUnblock.Name = "btnUnblock";
            btnUnblock.Size = new Size(120, 40);
            btnUnblock.TabIndex = 2;
            btnUnblock.Text = "Unblock All";
            btnUnblock.UseVisualStyleBackColor = true;
            // 
            // cmbPresets
            // 
            cmbPresets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPresets.FormattingEnabled = true;
            cmbPresets.IntegralHeight = false;
            cmbPresets.Location = new Point(23, 319);
            cmbPresets.Name = "cmbPresets";
            cmbPresets.Size = new Size(256, 23);
            cmbPresets.TabIndex = 3;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.FromArgb(223, 223, 223);
            bottomPanel.BorderStyle = BorderStyle.FixedSingle;
            bottomPanel.Controls.Add(lblStatus);
            bottomPanel.Controls.Add(progressBarLoading);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 625);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(1156, 53);
            bottomPanel.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Location = new Point(-1, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(1156, 24);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBarLoading
            // 
            progressBarLoading.Dock = DockStyle.Bottom;
            progressBarLoading.Location = new Point(0, 27);
            progressBarLoading.Name = "progressBarLoading";
            progressBarLoading.Size = new Size(1154, 24);
            progressBarLoading.Style = ProgressBarStyle.Marquee;
            progressBarLoading.TabIndex = 5;
            // 
            // grpRecommended
            // 
            grpRecommended.BackColor = Color.Transparent;
            grpRecommended.Controls.Add(flowRecommended);
            grpRecommended.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpRecommended.Location = new Point(20, 21);
            grpRecommended.Name = "grpRecommended";
            grpRecommended.Size = new Size(500, 170);
            grpRecommended.TabIndex = 8;
            grpRecommended.TabStop = false;
            grpRecommended.Text = "Recommended Regions";
            // 
            // flowRecommended
            // 
            flowRecommended.AutoSize = true;
            flowRecommended.Dock = DockStyle.Fill;
            flowRecommended.FlowDirection = FlowDirection.TopDown;
            flowRecommended.Location = new Point(3, 19);
            flowRecommended.Name = "flowRecommended";
            flowRecommended.Size = new Size(494, 148);
            flowRecommended.TabIndex = 0;
            // 
            // btnRefreshRegions
            // 
            btnRefreshRegions.Location = new Point(149, 197);
            btnRefreshRegions.Name = "btnRefreshRegions";
            btnRefreshRegions.Size = new Size(120, 40);
            btnRefreshRegions.TabIndex = 7;
            btnRefreshRegions.Text = "Refresh Regions";
            btnRefreshRegions.UseVisualStyleBackColor = true;
            // 
            // btnSortPing
            // 
            btnSortPing.Location = new Point(23, 197);
            btnSortPing.Name = "btnSortPing";
            btnSortPing.Size = new Size(120, 40);
            btnSortPing.TabIndex = 6;
            btnSortPing.Text = "Sort by Ping";
            btnSortPing.UseVisualStyleBackColor = true;
            // 
            // btnClearAllowed
            // 
            btnClearAllowed.Location = new Point(397, 194);
            btnClearAllowed.Name = "btnClearAllowed";
            btnClearAllowed.Size = new Size(120, 40);
            btnClearAllowed.TabIndex = 5;
            btnClearAllowed.Text = "Clear Allowed";
            btnClearAllowed.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(23, 258);
            label1.Name = "label1";
            label1.Size = new Size(256, 48);
            label1.TabIndex = 0;
            label1.Text = "Select a preset to filter on region (e.g uk/eu/na), this will override the recommeded region options.";
            // 
            // cardPanel1
            // 
            cardPanel1.Controls.Add(btnUnblock);
            cardPanel1.Controls.Add(btnRefreshRegions);
            cardPanel1.Controls.Add(grpRecommended);
            cardPanel1.Controls.Add(label1);
            cardPanel1.Controls.Add(btnApply);
            cardPanel1.Controls.Add(btnSortPing);
            cardPanel1.Controls.Add(chkBlockAllButSelected);
            cardPanel1.Controls.Add(cmbPresets);
            cardPanel1.Controls.Add(btnClearAllowed);
            cardPanel1.Location = new Point(12, 12);
            cardPanel1.Name = "cardPanel1";
            cardPanel1.Size = new Size(541, 459);
            cardPanel1.TabIndex = 8;
            // 
            // chkBlockAllButSelected
            // 
            chkBlockAllButSelected.AutoSize = true;
            chkBlockAllButSelected.BackColor = Color.Transparent;
            chkBlockAllButSelected.Checked = true;
            chkBlockAllButSelected.CheckState = CheckState.Checked;
            chkBlockAllButSelected.Location = new Point(306, 321);
            chkBlockAllButSelected.Name = "chkBlockAllButSelected";
            chkBlockAllButSelected.Size = new Size(153, 19);
            chkBlockAllButSelected.TabIndex = 4;
            chkBlockAllButSelected.Text = "Block all except selected";
            chkBlockAllButSelected.UseVisualStyleBackColor = false;
            // 
            // gridRegions
            // 
            gridRegions.AllowUserToAddRows = false;
            gridRegions.AllowUserToDeleteRows = false;
            gridRegions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRegions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridRegions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridRegions.Columns.AddRange(new DataGridViewColumn[] { colAllowed, colCode, colName, colPing });
            gridRegions.Dock = DockStyle.Right;
            gridRegions.Location = new Point(583, 0);
            gridRegions.Name = "gridRegions";
            gridRegions.ScrollBars = ScrollBars.Vertical;
            gridRegions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridRegions.Size = new Size(573, 625);
            gridRegions.TabIndex = 9;
            // 
            // colAllowed
            // 
            colAllowed.DataPropertyName = "Allowed";
            colAllowed.FalseValue = "false";
            colAllowed.HeaderText = "Allowed";
            colAllowed.IndeterminateValue = "";
            colAllowed.Name = "colAllowed";
            colAllowed.SortMode = DataGridViewColumnSortMode.Automatic;
            colAllowed.TrueValue = "true";
            // 
            // colCode
            // 
            colCode.DataPropertyName = "Code";
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Name";
            colName.Name = "colName";
            // 
            // colPing
            // 
            colPing.DataPropertyName = "PingMs";
            colPing.HeaderText = "Ping (ms)";
            colPing.Name = "colPing";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1156, 678);
            Controls.Add(gridRegions);
            Controls.Add(cardPanel1);
            Controls.Add(bottomPanel);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CS2 Server Picker (.NET 9)";
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            bottomPanel.ResumeLayout(false);
            grpRecommended.ResumeLayout(false);
            grpRecommended.PerformLayout();
            cardPanel1.ResumeLayout(false);
            cardPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridRegions).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ProgressBar progressBarLoading;
        private Label label1;
        private Label lblStatus;
        private Button btnClearAllowed;
        private Button btnSortPing;
        private Button btnRefreshRegions;
        private GroupBox grpRecommended;
        private FlowLayoutPanel flowRecommended;
        private UI.Custom.CardPanel cardPanel1;
        private DataGridView gridRegions;
        private CheckBox chkBlockAllButSelected;
        private DataGridViewCheckBoxColumn colAllowed;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colPing;
    }
}
