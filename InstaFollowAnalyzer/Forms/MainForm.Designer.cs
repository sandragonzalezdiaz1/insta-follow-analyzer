using FontAwesome.Sharp;

namespace InstaFollowAnalyzer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblFollowingCaption = new Label();
            lblFollowingCount = new Label();
            topPanel = new Panel();
            lblAppTitle = new Label();
            btnLoadFollowing = new IconButton();
            btnLoadFollowers = new IconButton();
            searchPanel = new Panel();
            btnSearchIcon = new IconButton();
            txtSearch = new TextBox();
            tabButtonsPanel = new Panel();
            btnTabNotFollowingBack = new Button();
            btnTabYouDoNotFollowBack = new Button();
            btnTabMutualFollowers = new Button();
            lblFollowersCount = new Label();
            lblNotFollowingBackCount = new Label();
            lblYouDoNotFollowBackCount = new Label();
            lblMutualCount = new Label();
            statFollowersPanel = new Panel();
            lblFollowersCaption = new Label();
            statNotFollowingBackPanel = new Panel();
            lblNotFollowingBackCaption = new Label();
            statYouDoNotFollowBackPanel = new Panel();
            lblYouDoNotFollowBackCaption = new Label();
            statMutualPanel = new Panel();
            lblMutualCaption = new Label();
            dgvResults = new DataGridView();
            notifyIcon1 = new NotifyIcon(components);
            statFollowingPanel = new Panel();
            topPanel.SuspendLayout();
            searchPanel.SuspendLayout();
            tabButtonsPanel.SuspendLayout();
            statFollowersPanel.SuspendLayout();
            statNotFollowingBackPanel.SuspendLayout();
            statYouDoNotFollowBackPanel.SuspendLayout();
            statMutualPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            statFollowingPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblFollowingCaption
            // 
            lblFollowingCaption.Dock = DockStyle.Bottom;
            lblFollowingCaption.ForeColor = Color.FromArgb(56, 93, 99);
            lblFollowingCaption.Location = new Point(0, 66);
            lblFollowingCaption.Name = "lblFollowingCaption";
            lblFollowingCaption.Size = new Size(188, 26);
            lblFollowingCaption.TabIndex = 0;
            lblFollowingCaption.Text = "Sigues";
            lblFollowingCaption.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFollowingCount
            // 
            lblFollowingCount.Dock = DockStyle.Top;
            lblFollowingCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblFollowingCount.ForeColor = Color.FromArgb(0, 121, 130);
            lblFollowingCount.Location = new Point(0, 12);
            lblFollowingCount.Name = "lblFollowingCount";
            lblFollowingCount.Size = new Size(188, 58);
            lblFollowingCount.TabIndex = 1;
            lblFollowingCount.Text = "0";
            lblFollowingCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(34, 40, 49);
            topPanel.Controls.Add(lblAppTitle);
            topPanel.Controls.Add(btnLoadFollowing);
            topPanel.Controls.Add(btnLoadFollowers);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1169, 86);
            topPanel.TabIndex = 0;
            // 
            // lblAppTitle
            // 
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(36, 26);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(260, 36);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "InstaFollowAnalyzer";
            // 
            // btnLoadFollowing
            // 
            btnLoadFollowing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadFollowing.BackColor = Color.FromArgb(0, 173, 181);
            btnLoadFollowing.FlatAppearance.BorderSize = 0;
            btnLoadFollowing.FlatStyle = FlatStyle.Flat;
            btnLoadFollowing.ForeColor = Color.White;
            btnLoadFollowing.IconChar = IconChar.FolderBlank;
            btnLoadFollowing.IconColor = Color.White;
            btnLoadFollowing.IconFont = IconFont.Auto;
            btnLoadFollowing.IconSize = 22;
            btnLoadFollowing.ImageAlign = ContentAlignment.MiddleLeft;
            btnLoadFollowing.Location = new Point(662, 24);
            btnLoadFollowing.Name = "btnLoadFollowing";
            btnLoadFollowing.Padding = new Padding(14, 0, 14, 0);
            btnLoadFollowing.Size = new Size(220, 38);
            btnLoadFollowing.TabIndex = 1;
            btnLoadFollowing.Text = "Cargar seguidos";
            btnLoadFollowing.TextAlign = ContentAlignment.MiddleRight;
            btnLoadFollowing.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLoadFollowing.UseVisualStyleBackColor = false;
            btnLoadFollowing.Click += btnLoadFollowing_Click;
            // 
            // btnLoadFollowers
            // 
            btnLoadFollowers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadFollowers.BackColor = Color.FromArgb(238, 238, 238);
            btnLoadFollowers.FlatAppearance.BorderSize = 0;
            btnLoadFollowers.FlatStyle = FlatStyle.Flat;
            btnLoadFollowers.ForeColor = Color.FromArgb(34, 40, 49);
            btnLoadFollowers.IconChar = IconChar.FolderBlank;
            btnLoadFollowers.IconColor = Color.FromArgb(34, 40, 49);
            btnLoadFollowers.IconFont = IconFont.Auto;
            btnLoadFollowers.IconSize = 22;
            btnLoadFollowers.ImageAlign = ContentAlignment.MiddleLeft;
            btnLoadFollowers.Location = new Point(904, 24);
            btnLoadFollowers.Name = "btnLoadFollowers";
            btnLoadFollowers.Padding = new Padding(14, 0, 14, 0);
            btnLoadFollowers.Size = new Size(220, 38);
            btnLoadFollowers.TabIndex = 2;
            btnLoadFollowers.Text = "Cargar seguidores";
            btnLoadFollowers.TextAlign = ContentAlignment.MiddleRight;
            btnLoadFollowers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLoadFollowers.UseVisualStyleBackColor = false;
            btnLoadFollowers.Click += btnLoadFollowers_Click;
            // 
            // searchPanel
            // 
            searchPanel.Anchor = AnchorStyles.Top;
            searchPanel.BackColor = Color.FromArgb(232, 249, 250);
            searchPanel.Controls.Add(btnSearchIcon);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Location = new Point(234, 248);
            searchPanel.Name = "searchPanel";
            searchPanel.Padding = new Padding(12, 8, 12, 8);
            searchPanel.Size = new Size(700, 50);
            searchPanel.TabIndex = 6;
            // 
            // btnSearchIcon
            // 
            btnSearchIcon.BackColor = Color.Transparent;
            btnSearchIcon.Enabled = false;
            btnSearchIcon.FlatAppearance.BorderSize = 0;
            btnSearchIcon.FlatStyle = FlatStyle.Flat;
            btnSearchIcon.ForeColor = Color.FromArgb(0, 121, 130);
            btnSearchIcon.IconChar = IconChar.Search;
            btnSearchIcon.IconColor = Color.FromArgb(0, 121, 130);
            btnSearchIcon.IconFont = IconFont.Auto;
            btnSearchIcon.IconSize = 22;
            btnSearchIcon.Location = new Point(12, 9);
            btnSearchIcon.Name = "btnSearchIcon";
            btnSearchIcon.Size = new Size(34, 32);
            btnSearchIcon.TabIndex = 0;
            btnSearchIcon.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(232, 249, 250);
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.ForeColor = Color.FromArgb(34, 40, 49);
            txtSearch.Location = new Point(55, 13);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Buscar usuario...";
            txtSearch.Size = new Size(389, 24);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // tabButtonsPanel
            // 
            tabButtonsPanel.Controls.Add(btnTabNotFollowingBack);
            tabButtonsPanel.Controls.Add(btnTabYouDoNotFollowBack);
            tabButtonsPanel.Controls.Add(btnTabMutualFollowers);
            tabButtonsPanel.Location = new Point(234, 328);
            tabButtonsPanel.Name = "tabButtonsPanel";
            tabButtonsPanel.Size = new Size(700, 34);
            tabButtonsPanel.TabIndex = 7;
            // 
            // btnTabNotFollowingBack
            // 
            btnTabNotFollowingBack.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnTabNotFollowingBack.FlatStyle = FlatStyle.Flat;
            btnTabNotFollowingBack.Location = new Point(0, 0);
            btnTabNotFollowingBack.Name = "btnTabNotFollowingBack";
            btnTabNotFollowingBack.Size = new Size(233, 34);
            btnTabNotFollowingBack.TabIndex = 0;
            btnTabNotFollowingBack.Text = "No me siguen";
            btnTabNotFollowingBack.UseVisualStyleBackColor = false;
            // 
            // btnTabYouDoNotFollowBack
            // 
            btnTabYouDoNotFollowBack.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnTabYouDoNotFollowBack.FlatStyle = FlatStyle.Flat;
            btnTabYouDoNotFollowBack.Location = new Point(233, 0);
            btnTabYouDoNotFollowBack.Name = "btnTabYouDoNotFollowBack";
            btnTabYouDoNotFollowBack.Size = new Size(233, 34);
            btnTabYouDoNotFollowBack.TabIndex = 1;
            btnTabYouDoNotFollowBack.Text = "No sigo";
            btnTabYouDoNotFollowBack.UseVisualStyleBackColor = false;
            // 
            // btnTabMutualFollowers
            // 
            btnTabMutualFollowers.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnTabMutualFollowers.FlatStyle = FlatStyle.Flat;
            btnTabMutualFollowers.Location = new Point(466, 0);
            btnTabMutualFollowers.Name = "btnTabMutualFollowers";
            btnTabMutualFollowers.Size = new Size(234, 34);
            btnTabMutualFollowers.TabIndex = 2;
            btnTabMutualFollowers.Text = "Mutuos";
            btnTabMutualFollowers.UseVisualStyleBackColor = false;
            // 
            // lblFollowersCount
            // 
            lblFollowersCount.Dock = DockStyle.Top;
            lblFollowersCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblFollowersCount.ForeColor = Color.FromArgb(0, 121, 130);
            lblFollowersCount.Location = new Point(0, 12);
            lblFollowersCount.Name = "lblFollowersCount";
            lblFollowersCount.Size = new Size(188, 58);
            lblFollowersCount.TabIndex = 2;
            lblFollowersCount.Text = "0";
            lblFollowersCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNotFollowingBackCount
            // 
            lblNotFollowingBackCount.Dock = DockStyle.Top;
            lblNotFollowingBackCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblNotFollowingBackCount.ForeColor = Color.FromArgb(0, 121, 130);
            lblNotFollowingBackCount.Location = new Point(0, 12);
            lblNotFollowingBackCount.Name = "lblNotFollowingBackCount";
            lblNotFollowingBackCount.Size = new Size(188, 58);
            lblNotFollowingBackCount.TabIndex = 3;
            lblNotFollowingBackCount.Text = "0";
            lblNotFollowingBackCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblYouDoNotFollowBackCount
            // 
            lblYouDoNotFollowBackCount.Dock = DockStyle.Top;
            lblYouDoNotFollowBackCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblYouDoNotFollowBackCount.ForeColor = Color.FromArgb(0, 121, 130);
            lblYouDoNotFollowBackCount.Location = new Point(0, 12);
            lblYouDoNotFollowBackCount.Name = "lblYouDoNotFollowBackCount";
            lblYouDoNotFollowBackCount.Size = new Size(188, 58);
            lblYouDoNotFollowBackCount.TabIndex = 4;
            lblYouDoNotFollowBackCount.Text = "0";
            lblYouDoNotFollowBackCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMutualCount
            // 
            lblMutualCount.Dock = DockStyle.Top;
            lblMutualCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblMutualCount.ForeColor = Color.FromArgb(0, 121, 130);
            lblMutualCount.Location = new Point(0, 12);
            lblMutualCount.Name = "lblMutualCount";
            lblMutualCount.Size = new Size(188, 58);
            lblMutualCount.TabIndex = 5;
            lblMutualCount.Text = "0";
            lblMutualCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statFollowersPanel
            // 
            statFollowersPanel.BackColor = Color.FromArgb(232, 249, 250);
            statFollowersPanel.Controls.Add(lblFollowersCaption);
            statFollowersPanel.Controls.Add(lblFollowersCount);
            statFollowersPanel.Location = new Point(263, 116);
            statFollowersPanel.Name = "statFollowersPanel";
            statFollowersPanel.Padding = new Padding(0, 12, 0, 12);
            statFollowersPanel.Size = new Size(188, 104);
            statFollowersPanel.TabIndex = 2;
            statFollowersPanel.Anchor = AnchorStyles.Top;
            // 
            // lblFollowersCaption
            // 
            lblFollowersCaption.Dock = DockStyle.Bottom;
            lblFollowersCaption.ForeColor = Color.FromArgb(56, 93, 99);
            lblFollowersCaption.Location = new Point(0, 66);
            lblFollowersCaption.Name = "lblFollowersCaption";
            lblFollowersCaption.Size = new Size(188, 26);
            lblFollowersCaption.TabIndex = 0;
            lblFollowersCaption.Text = "Te siguen";
            lblFollowersCaption.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statNotFollowingBackPanel
            // 
            statNotFollowingBackPanel.BackColor = Color.FromArgb(232, 249, 250);
            statNotFollowingBackPanel.Controls.Add(lblNotFollowingBackCaption);
            statNotFollowingBackPanel.Controls.Add(lblNotFollowingBackCount);
            statNotFollowingBackPanel.Location = new Point(490, 116);
            statNotFollowingBackPanel.Name = "statNotFollowingBackPanel";
            statNotFollowingBackPanel.Padding = new Padding(0, 12, 0, 12);
            statNotFollowingBackPanel.Size = new Size(188, 104);
            statNotFollowingBackPanel.TabIndex = 3;
            statNotFollowingBackPanel.Anchor = AnchorStyles.Top;
            // 
            // lblNotFollowingBackCaption
            // 
            lblNotFollowingBackCaption.Dock = DockStyle.Bottom;
            lblNotFollowingBackCaption.ForeColor = Color.FromArgb(56, 93, 99);
            lblNotFollowingBackCaption.Location = new Point(0, 66);
            lblNotFollowingBackCaption.Name = "lblNotFollowingBackCaption";
            lblNotFollowingBackCaption.Size = new Size(188, 26);
            lblNotFollowingBackCaption.TabIndex = 0;
            lblNotFollowingBackCaption.Text = "No te siguen";
            lblNotFollowingBackCaption.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statYouDoNotFollowBackPanel
            // 
            statYouDoNotFollowBackPanel.BackColor = Color.FromArgb(232, 249, 250);
            statYouDoNotFollowBackPanel.Controls.Add(lblYouDoNotFollowBackCaption);
            statYouDoNotFollowBackPanel.Controls.Add(lblYouDoNotFollowBackCount);
            statYouDoNotFollowBackPanel.Location = new Point(717, 116);
            statYouDoNotFollowBackPanel.Name = "statYouDoNotFollowBackPanel";
            statYouDoNotFollowBackPanel.Padding = new Padding(0, 12, 0, 12);
            statYouDoNotFollowBackPanel.Size = new Size(188, 104);
            statYouDoNotFollowBackPanel.TabIndex = 4;
            statYouDoNotFollowBackPanel.Anchor = AnchorStyles.Top;
            // 
            // lblYouDoNotFollowBackCaption
            // 
            lblYouDoNotFollowBackCaption.Dock = DockStyle.Bottom;
            lblYouDoNotFollowBackCaption.ForeColor = Color.FromArgb(56, 93, 99);
            lblYouDoNotFollowBackCaption.Location = new Point(0, 66);
            lblYouDoNotFollowBackCaption.Name = "lblYouDoNotFollowBackCaption";
            lblYouDoNotFollowBackCaption.Size = new Size(188, 26);
            lblYouDoNotFollowBackCaption.TabIndex = 0;
            lblYouDoNotFollowBackCaption.Text = "No sigues";
            lblYouDoNotFollowBackCaption.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statMutualPanel
            // 
            statMutualPanel.BackColor = Color.FromArgb(232, 249, 250);
            statMutualPanel.Controls.Add(lblMutualCaption);
            statMutualPanel.Controls.Add(lblMutualCount);
            statMutualPanel.Location = new Point(944, 116);
            statMutualPanel.Name = "statMutualPanel";
            statMutualPanel.Padding = new Padding(0, 12, 0, 12);
            statMutualPanel.Size = new Size(188, 104);
            statMutualPanel.TabIndex = 5;
            statMutualPanel.Anchor = AnchorStyles.Top;
            // 
            // lblMutualCaption
            // 
            lblMutualCaption.Dock = DockStyle.Bottom;
            lblMutualCaption.ForeColor = Color.FromArgb(56, 93, 99);
            lblMutualCaption.Location = new Point(0, 66);
            lblMutualCaption.Name = "lblMutualCaption";
            lblMutualCaption.Size = new Size(188, 26);
            lblMutualCaption.TabIndex = 0;
            lblMutualCaption.Text = "Mutuos";
            lblMutualCaption.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.GridColor = Color.FromArgb(230, 230, 230);
            dgvResults.Location = new Point(234, 362);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.RowHeadersWidth = 62;
            dgvResults.Size = new Size(700, 263);
            dgvResults.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // statFollowingPanel
            // 
            statFollowingPanel.BackColor = Color.FromArgb(232, 249, 250);
            statFollowingPanel.Controls.Add(lblFollowingCaption);
            statFollowingPanel.Controls.Add(lblFollowingCount);
            statFollowingPanel.Location = new Point(36, 116);
            statFollowingPanel.Name = "statFollowingPanel";
            statFollowingPanel.Padding = new Padding(0, 12, 0, 12);
            statFollowingPanel.Size = new Size(188, 104);
            statFollowingPanel.TabIndex = 1;
            statFollowingPanel.Anchor = AnchorStyles.Top;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1169, 657);
            Controls.Add(dgvResults);
            Controls.Add(tabButtonsPanel);
            Controls.Add(searchPanel);
            Controls.Add(statMutualPanel);
            Controls.Add(statYouDoNotFollowBackPanel);
            Controls.Add(statNotFollowingBackPanel);
            Controls.Add(statFollowersPanel);
            Controls.Add(statFollowingPanel);
            Controls.Add(topPanel);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InstaFollowAnalyzer";
            WindowState = FormWindowState.Maximized;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            tabButtonsPanel.ResumeLayout(false);
            statFollowersPanel.ResumeLayout(false);
            statNotFollowingBackPanel.ResumeLayout(false);
            statYouDoNotFollowBackPanel.ResumeLayout(false);
            statMutualPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            statFollowingPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private Label lblAppTitle;
        private IconButton btnLoadFollowing;
        private IconButton btnLoadFollowers;
        private Panel searchPanel;
        private IconButton btnSearchIcon;
        private TextBox txtSearch;
        private Panel tabButtonsPanel;
        private Button btnTabNotFollowingBack;
        private Button btnTabYouDoNotFollowBack;
        private Button btnTabMutualFollowers;
        private Label lblFollowingCount;
        private Label lblFollowersCount;
        private Label lblNotFollowingBackCount;
        private Label lblYouDoNotFollowBackCount;
        private Label lblMutualCount;
        private Panel statFollowingPanel;
        private Label lblFollowingCaption;
        private Panel statFollowersPanel;
        private Label lblFollowersCaption;
        private Panel statNotFollowingBackPanel;
        private Label lblNotFollowingBackCaption;
        private Panel statYouDoNotFollowBackPanel;
        private Label lblYouDoNotFollowBackCaption;
        private Panel statMutualPanel;
        private Label lblMutualCaption;
        private DataGridView dgvResults;
        private NotifyIcon notifyIcon1;
    }
}
