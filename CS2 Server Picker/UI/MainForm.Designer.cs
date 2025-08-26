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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            buttonGithub = new Button();
            buttonDonate = new Button();
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
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnApply.BackColor = Color.FromArgb(255, 140, 0);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(23, 526);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(100, 40);
            btnApply.TabIndex = 1;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = false;
            // 
            // btnUnblock
            // 
            btnUnblock.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUnblock.BackColor = Color.FromArgb(40, 40, 40);
            btnUnblock.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnUnblock.FlatAppearance.BorderSize = 2;
            btnUnblock.FlatStyle = FlatStyle.Flat;
            btnUnblock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUnblock.ForeColor = Color.White;
            btnUnblock.Location = new Point(140, 526);
            btnUnblock.Name = "btnUnblock";
            btnUnblock.Size = new Size(120, 40);
            btnUnblock.TabIndex = 2;
            btnUnblock.Text = "Unblock All";
            btnUnblock.UseVisualStyleBackColor = false;
            // 
            // cmbPresets
            // 
            cmbPresets.BackColor = Color.FromArgb(35, 35, 35);
            cmbPresets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPresets.ForeColor = Color.White;
            cmbPresets.FormattingEnabled = true;
            cmbPresets.IntegralHeight = false;
            cmbPresets.Location = new Point(23, 319);
            cmbPresets.Name = "cmbPresets";
            cmbPresets.Size = new Size(256, 23);
            cmbPresets.TabIndex = 3;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.FromArgb(25, 25, 25);
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
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(255, 140, 0);
            lblStatus.Location = new Point(-1, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(1156, 24);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBarLoading
            // 
            progressBarLoading.BackColor = Color.FromArgb(40, 40, 40);
            progressBarLoading.Dock = DockStyle.Bottom;
            progressBarLoading.ForeColor = Color.FromArgb(255, 140, 0);
            progressBarLoading.Location = new Point(0, 27);
            progressBarLoading.Name = "progressBarLoading";
            progressBarLoading.Size = new Size(1154, 24);
            progressBarLoading.Style = ProgressBarStyle.Continuous;
            progressBarLoading.TabIndex = 5;
            // 
            // grpRecommended
            // 
            grpRecommended.BackColor = Color.Transparent;
            grpRecommended.Controls.Add(flowRecommended);
            grpRecommended.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpRecommended.ForeColor = Color.White;
            grpRecommended.Location = new Point(20, 21);
            grpRecommended.Name = "grpRecommended";
            grpRecommended.Size = new Size(466, 170);
            grpRecommended.TabIndex = 8;
            grpRecommended.TabStop = false;
            grpRecommended.Text = "Recommended Regions";
            // 
            // flowRecommended
            // 
            flowRecommended.AutoSize = true;
            flowRecommended.BackColor = Color.Transparent;
            flowRecommended.Dock = DockStyle.Fill;
            flowRecommended.FlowDirection = FlowDirection.TopDown;
            flowRecommended.Location = new Point(3, 21);
            flowRecommended.Name = "flowRecommended";
            flowRecommended.Size = new Size(460, 146);
            flowRecommended.TabIndex = 0;
            // 
            // btnRefreshRegions
            // 
            btnRefreshRegions.BackColor = Color.FromArgb(40, 40, 40);
            btnRefreshRegions.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnRefreshRegions.FlatAppearance.BorderSize = 2;
            btnRefreshRegions.FlatStyle = FlatStyle.Flat;
            btnRefreshRegions.Font = new Font("Segoe UI", 10F);
            btnRefreshRegions.ForeColor = Color.White;
            btnRefreshRegions.Location = new Point(149, 197);
            btnRefreshRegions.Name = "btnRefreshRegions";
            btnRefreshRegions.Size = new Size(151, 40);
            btnRefreshRegions.TabIndex = 7;
            btnRefreshRegions.Text = "Refresh Regions";
            btnRefreshRegions.UseVisualStyleBackColor = false;
            // 
            // btnSortPing
            // 
            btnSortPing.BackColor = Color.FromArgb(40, 40, 40);
            btnSortPing.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnSortPing.FlatAppearance.BorderSize = 2;
            btnSortPing.FlatStyle = FlatStyle.Flat;
            btnSortPing.Font = new Font("Segoe UI", 10F);
            btnSortPing.ForeColor = Color.White;
            btnSortPing.Location = new Point(23, 197);
            btnSortPing.Name = "btnSortPing";
            btnSortPing.Size = new Size(120, 40);
            btnSortPing.TabIndex = 6;
            btnSortPing.Text = "Sort by Ping";
            btnSortPing.UseVisualStyleBackColor = false;
            // 
            // btnClearAllowed
            // 
            btnClearAllowed.BackColor = Color.FromArgb(40, 40, 40);
            btnClearAllowed.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnClearAllowed.FlatAppearance.BorderSize = 2;
            btnClearAllowed.FlatStyle = FlatStyle.Flat;
            btnClearAllowed.Font = new Font("Segoe UI", 10F);
            btnClearAllowed.ForeColor = Color.White;
            btnClearAllowed.Location = new Point(363, 194);
            btnClearAllowed.Name = "btnClearAllowed";
            btnClearAllowed.Size = new Size(120, 40);
            btnClearAllowed.TabIndex = 5;
            btnClearAllowed.Text = "Clear Allowed";
            btnClearAllowed.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(23, 258);
            label1.Name = "label1";
            label1.Size = new Size(256, 48);
            label1.TabIndex = 0;
            label1.Text = "Select a preset to filter on region (e.g uk/eu/na), this will override the recommeded region options.";
            // 
            // cardPanel1
            // 
            cardPanel1.BackColor = Color.Transparent;
            cardPanel1.Controls.Add(buttonGithub);
            cardPanel1.Controls.Add(buttonDonate);
            cardPanel1.Controls.Add(btnUnblock);
            cardPanel1.Controls.Add(btnRefreshRegions);
            cardPanel1.Controls.Add(grpRecommended);
            cardPanel1.Controls.Add(label1);
            cardPanel1.Controls.Add(btnApply);
            cardPanel1.Controls.Add(btnSortPing);
            cardPanel1.Controls.Add(chkBlockAllButSelected);
            cardPanel1.Controls.Add(cmbPresets);
            cardPanel1.Controls.Add(btnClearAllowed);
            cardPanel1.ForeColor = Color.White;
            cardPanel1.Location = new Point(12, 12);
            cardPanel1.Name = "cardPanel1";
            cardPanel1.Size = new Size(505, 588);
            cardPanel1.TabIndex = 8;
            // 
            // buttonGithub
            // 
            buttonGithub.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonGithub.BackColor = Color.White;
            buttonGithub.BackgroundImage = Properties.Resources.GitHub_Logo_ee398b662d42;
            buttonGithub.BackgroundImageLayout = ImageLayout.Stretch;
            buttonGithub.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            buttonGithub.FlatStyle = FlatStyle.Flat;
            buttonGithub.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonGithub.ForeColor = Color.White;
            buttonGithub.Location = new Point(396, 508);
            buttonGithub.Name = "buttonGithub";
            buttonGithub.Size = new Size(90, 26);
            buttonGithub.TabIndex = 10;
            buttonGithub.UseVisualStyleBackColor = false;
            buttonGithub.Click += buttonGithub_Click;
            // 
            // buttonDonate
            // 
            buttonDonate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDonate.BackgroundImage = Properties.Resources.pp_logo_100px;
            buttonDonate.BackgroundImageLayout = ImageLayout.Stretch;
            buttonDonate.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            buttonDonate.FlatStyle = FlatStyle.Flat;
            buttonDonate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonDonate.ForeColor = Color.White;
            buttonDonate.Location = new Point(396, 540);
            buttonDonate.Name = "buttonDonate";
            buttonDonate.Size = new Size(90, 26);
            buttonDonate.TabIndex = 9;
            buttonDonate.UseVisualStyleBackColor = false;
            buttonDonate.Click += buttonDonate_Click;
            // 
            // chkBlockAllButSelected
            // 
            chkBlockAllButSelected.AutoSize = true;
            chkBlockAllButSelected.BackColor = Color.Transparent;
            chkBlockAllButSelected.Checked = true;
            chkBlockAllButSelected.CheckState = CheckState.Checked;
            chkBlockAllButSelected.ForeColor = Color.White;
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
            gridRegions.BackgroundColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridRegions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridRegions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridRegions.Columns.AddRange(new DataGridViewColumn[] { colAllowed, colCode, colName, colPing });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(35, 35, 35);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 140, 0);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridRegions.DefaultCellStyle = dataGridViewCellStyle2;
            gridRegions.Dock = DockStyle.Right;
            gridRegions.EnableHeadersVisualStyles = false;
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
            BackColor = Color.FromArgb(18, 18, 18);
            BackgroundImage = Properties.Resources.cs2bg_resized;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1156, 678);
            Controls.Add(gridRegions);
            Controls.Add(cardPanel1);
            Controls.Add(bottomPanel);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
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
        private Button buttonDonate;
        private Button buttonGithub;
    }
}
