using FontAwesome.Sharp;
using InstaFollowAnalyzer.Services;
using System.Diagnostics;

namespace InstaFollowAnalyzer
{
    public partial class MainForm : Form
    {
        private readonly InstaJsonService _instaJsonService = new();
        private readonly FollowerComparisonService _comparisonService = new();

        private List<string> _following = [];
        private List<string> _followers = [];
        private bool _followingLoaded;
        private bool _followersLoaded;
        private List<InstagramUserRow> _notFollowingBackRows = [];
        private List<InstagramUserRow> _youDoNotFollowBackRows = [];
        private List<InstagramUserRow> _mutualFollowerRows = [];
        private ResultType _activeResultType = ResultType.NotFollowingBack;
        private TableLayoutPanel? _bodyLayout;
        private readonly Label _emptyStateLabel = new();

        public MainForm()
        {
            InitializeComponent();
            txtSearch.Enabled = false;
            ConfigureHeaderButtons();
            ConfigureResultsTabs();
            ConfigureResultsGrid();
            RegisterGridLinkEvents();
            BuildResponsiveLayout();
            SetResultsAreaVisible(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateResponsiveContentWidth();
        }

        private async void btnLoadFollowing_Click(object sender, EventArgs e)
        {
            string? filePath = SelectJsonFile();

            if (filePath is null) return;

            try
            {
                _following = await _instaJsonService.ReadFollowingUsernamesAsync(filePath);
                _followingLoaded = true;
                MarkHeaderButtonAsLoaded(btnLoadFollowing, "Seguidos cargados");

                UpdateCounters(0, 0, 0);
                CompareFilesIfReady();
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Archivo incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al leer el archivo de seguidos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnLoadFollowers_Click(object sender, EventArgs e)
        {
            string? filePath = SelectJsonFile();

            if (filePath is null) return;
            
            try
            {
                _followers = await _instaJsonService.ReadFollowerUsernamesAsync(filePath);
                _followersLoaded = true;
                MarkHeaderButtonAsLoaded(btnLoadFollowers, "Seguidores cargados");

                UpdateCounters(0, 0, 0);
                CompareFilesIfReady();
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Archivo incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al leer el archivo de seguidores:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private static string? SelectJsonFile()
        {
            using OpenFileDialog openFileDialog = new();

            openFileDialog.Title = "Selecciona un archivo JSON";
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";

            return openFileDialog.ShowDialog() == DialogResult.OK
                ? openFileDialog.FileName
                : null;
        }

        private void CompareFilesIfReady()
        {
            if (!_followingLoaded || !_followersLoaded)
            {
                return;
            }

            List<string> notFollowingYouBack =
                _comparisonService.GetUsersNotFollowingYouBack(_following, _followers);

            List<string> youDoNotFollowBack =
                _comparisonService.GetFollowersYouDoNotFollowBack(_following, _followers);

            List<string> mutualFollowers =
                _comparisonService.GetMutualFollowers(_following, _followers);

            UpdateCounters(
                notFollowingYouBack.Count,
                youDoNotFollowBack.Count,
                mutualFollowers.Count
            );

            LoadComparisonResults(notFollowingYouBack, youDoNotFollowBack, mutualFollowers);
        }

        private void ConfigureResultsGrid()
        {
            ConfigureUsersGrid(dgvResults);
        }

        private void ConfigureResultsTabs()
        {
            btnTabNotFollowingBack.Click += (_, _) => SelectResultType(ResultType.NotFollowingBack);
            btnTabYouDoNotFollowBack.Click += (_, _) => SelectResultType(ResultType.YouDoNotFollowBack);
            btnTabMutualFollowers.Click += (_, _) => SelectResultType(ResultType.Mutual);
            UpdateTabButtonStyles();
        }

        private static void ConfigureUsersGrid(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.Columns.Clear();
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.EnableHeadersVisualStyles = false;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 36;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ScrollBars = ScrollBars.Vertical;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 35);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 247, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(34, 40, 49);

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 40, 49);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Usuario",
                DataPropertyName = nameof(InstagramUserRow.Username),
                Name = "Username",
                Width = 560,
                MinimumWidth = 220,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    ForeColor = Color.FromArgb(34, 40, 49),
                    SelectionBackColor = Color.FromArgb(224, 247, 250),
                    SelectionForeColor = Color.FromArgb(34, 40, 49)
                }
            });

            grid.Columns.Add(new DataGridViewLinkColumn
            {
                HeaderText = "Perfil",
                Name = "OpenProfile",
                Text = "Abrir ↗",
                UseColumnTextForLinkValue = true,
                Width = 90,
                MinimumWidth = 90,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                LinkColor = Color.FromArgb(0, 121, 130),
                ActiveLinkColor = Color.FromArgb(0, 173, 181),
                VisitedLinkColor = Color.FromArgb(0, 121, 130)
            });

            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(34, 40, 49);
                column.HeaderCell.Style.ForeColor = Color.White;
                column.HeaderCell.Style.Font = new Font(grid.Font, FontStyle.Bold);
                column.HeaderCell.Style.SelectionBackColor = Color.FromArgb(34, 40, 49);
                column.HeaderCell.Style.SelectionForeColor = Color.White;
            }
        }

        private void SelectResultType(ResultType resultType)
        {
            _activeResultType = resultType;
            UpdateTabButtonStyles();
            ApplySearchFilter();
        }

        private void UpdateTabButtonStyles()
        {
            StyleTabButton(
                btnTabNotFollowingBack,
                _activeResultType == ResultType.NotFollowingBack,
                Color.FromArgb(255, 241, 242),
                Color.FromArgb(190, 18, 60)
            );

            StyleTabButton(
                btnTabYouDoNotFollowBack,
                _activeResultType == ResultType.YouDoNotFollowBack,
                Color.FromArgb(239, 246, 255),
                Color.FromArgb(37, 99, 235)
            );

            StyleTabButton(
                btnTabMutualFollowers,
                _activeResultType == ResultType.Mutual,
                Color.FromArgb(236, 253, 245),
                Color.FromArgb(4, 120, 87)
            );
        }

        private static void StyleTabButton(Button button, bool isSelected, Color selectedBackColor, Color selectedForeColor)
        {
            button.BackColor = isSelected ? selectedBackColor : Color.FromArgb(248, 250, 252);
            button.ForeColor = isSelected ? selectedForeColor : Color.FromArgb(71, 85, 105);
            button.Font = new Font(button.Font, isSelected ? FontStyle.Bold : FontStyle.Regular);
        }

        // Reorganiza los controles creados por el diseñador en un layout centrado y adaptable
        private void BuildResponsiveLayout()
        {
            if (_bodyLayout is not null) return;
            
            SuspendLayout();

            Controls.Remove(statFollowingPanel);
            Controls.Remove(statFollowersPanel);
            Controls.Remove(statNotFollowingBackPanel);
            Controls.Remove(statYouDoNotFollowBackPanel);
            Controls.Remove(statMutualPanel);
            Controls.Remove(searchPanel);
            Controls.Remove(tabButtonsPanel);
            Controls.Remove(dgvResults);

            _bodyLayout = new TableLayoutPanel
            {
                BackColor = BackColor,
                ColumnCount = 3,
                Dock = DockStyle.Fill,
                Padding = new Padding(24, topPanel.Height + 32, 24, 32),
                RowCount = 7
            };

            _bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, GetResponsiveContentWidth()));
            _bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            _bodyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _bodyLayout.Controls.Add(CreateStatsLayout(), 1, 1);
            _bodyLayout.Controls.Add(ConfigureSearchLayout(), 1, 3);
            _bodyLayout.Controls.Add(CreateTabButtonsLayout(), 1, 5);

            dgvResults.Dock = DockStyle.Fill;
            dgvResults.Margin = Padding.Empty;
            _bodyLayout.Controls.Add(dgvResults, 1, 6);

            _bodyLayout.Controls.Add(ConfigureEmptyStateLabel(), 1, 3);
            _bodyLayout.SetRowSpan(_emptyStateLabel, 4);

            Controls.Add(_bodyLayout);
            topPanel.BringToFront();

            ResumeLayout(false);
        }

        private TableLayoutPanel CreateStatsLayout()
        {
            TableLayoutPanel statsLayout = new()
            {
                ColumnCount = 5,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                RowCount = 1
            };

            for (int i = 0; i < 5; i++)
            {
                statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            }

            AddStatCard(statsLayout, statFollowingPanel, 0);
            AddStatCard(statsLayout, statFollowersPanel, 1);
            AddStatCard(statsLayout, statNotFollowingBackPanel, 2);
            AddStatCard(statsLayout, statYouDoNotFollowBackPanel, 3);
            AddStatCard(statsLayout, statMutualPanel, 4);

            return statsLayout;
        }

        private static void AddStatCard(TableLayoutPanel statsLayout, Panel statPanel, int column)
        {
            statPanel.Dock = DockStyle.Fill;
            statPanel.Margin = new Padding(9, 0, 9, 0);
            statsLayout.Controls.Add(statPanel, column, 0);
        }

        private void ConfigureHeaderButtons()
        {
            ConfigureHeaderButton(btnLoadFollowing);
            ConfigureHeaderButton(btnLoadFollowers);
        }

        private static void ConfigureHeaderButton(IconButton button)
        {
            button.IconSize = 17;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.Padding = new Padding(0);
        }

        private static void MarkHeaderButtonAsLoaded(IconButton button, string text)
        {
            button.Text = text;
            button.IconChar = IconChar.Check;
            button.BackColor = Color.FromArgb(16, 185, 129);
            button.ForeColor = Color.White;
            button.IconColor = Color.White;
        }

        private Panel ConfigureSearchLayout()
        {
            searchPanel.Controls.Clear();
            searchPanel.Dock = DockStyle.Fill;
            searchPanel.Margin = Padding.Empty;
            searchPanel.Padding = new Padding(12, 9, 12, 9);

            TableLayoutPanel searchLayout = new()
            {
                BackColor = searchPanel.BackColor,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                RowCount = 1
            };

            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            btnSearchIcon.Dock = DockStyle.Fill;
            btnSearchIcon.Margin = Padding.Empty;
            btnSearchIcon.Padding = new Padding(0);
            btnSearchIcon.Text = "";

            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Margin = new Padding(8, 4, 0, 0);

            searchLayout.Controls.Add(btnSearchIcon, 0, 0);
            searchLayout.Controls.Add(txtSearch, 1, 0);
            searchPanel.Controls.Add(searchLayout);

            return searchPanel;
        }

        private Label ConfigureEmptyStateLabel()
        {
            _emptyStateLabel.Dock = DockStyle.Fill;
            _emptyStateLabel.Margin = Padding.Empty;
            _emptyStateLabel.Padding = new Padding(24, 48, 24, 0);
            _emptyStateLabel.Text = "Carga los archivos de seguidos y seguidores para comenzar";
            _emptyStateLabel.TextAlign = ContentAlignment.TopCenter;
            _emptyStateLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _emptyStateLabel.ForeColor = Color.FromArgb(71, 85, 105);
            _emptyStateLabel.BackColor = BackColor;

            return _emptyStateLabel;
        }

        private Panel CreateTabButtonsLayout()
        {
            tabButtonsPanel.Controls.Clear();
            tabButtonsPanel.Dock = DockStyle.Fill;
            tabButtonsPanel.Margin = Padding.Empty;

            TableLayoutPanel tabsLayout = new()
            {
                ColumnCount = 3,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                RowCount = 1
            };

            for (int i = 0; i < 3; i++)
            {
                tabsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            }

            AddTabButton(tabsLayout, btnTabNotFollowingBack, 0);
            AddTabButton(tabsLayout, btnTabYouDoNotFollowBack, 1);
            AddTabButton(tabsLayout, btnTabMutualFollowers, 2);

            tabButtonsPanel.Controls.Add(tabsLayout);
            return tabButtonsPanel;
        }

        private static void AddTabButton(TableLayoutPanel tabsLayout, Button button, int column)
        {
            button.Dock = DockStyle.Fill;
            button.Margin = Padding.Empty;
            tabsLayout.Controls.Add(button, column, 0);
        }

        private void UpdateResponsiveContentWidth()
        {
            if (_bodyLayout is null)
            {
                return;
            }

            _bodyLayout.ColumnStyles[1].Width = GetResponsiveContentWidth();
        }

        private int GetResponsiveContentWidth()
        {
            const int preferredContentWidth = 700;
            const int minimumContentWidth = 420;
            const int horizontalPadding = 48;

            int availableWidth = Math.Max(minimumContentWidth, ClientSize.Width - horizontalPadding);
            return Math.Min(preferredContentWidth, availableWidth);
        }

        private void SetResultsAreaVisible(bool isReady)
        {
            searchPanel.Visible = isReady;
            tabButtonsPanel.Visible = isReady;
            dgvResults.Visible = isReady;
            _emptyStateLabel.Visible = !isReady;
            txtSearch.Enabled = isReady;
        }

        private void RegisterGridLinkEvents()
        {
            dgvResults.CellContentClick += dgvUsers_CellContentClick;
        }

        private void LoadComparisonResults(
            List<string> notFollowingYouBack,
            List<string> youDoNotFollowBack,
            List<string> mutualFollowers)
        {
            _notFollowingBackRows = CreateRows(notFollowingYouBack);
            _youDoNotFollowBackRows = CreateRows(youDoNotFollowBack);
            _mutualFollowerRows = CreateRows(mutualFollowers);

            SetResultsAreaVisible(true);
            _activeResultType = ResultType.NotFollowingBack;
            UpdateTabButtonStyles();
            ApplySearchFilter();
        }

        private static List<InstagramUserRow> CreateRows(List<string> usernames)
        {
            return usernames
                .Select(username => new InstagramUserRow
                {
                    Username = username,
                    ProfileUrl = BuildInstagramProfileUrl(username)
                })
                .ToList();
        }

        private void UpdateCounters(
            int notFollowingYouBackCount,
            int youDoNotFollowBackCount,
            int mutualFollowersCount)
        {
            lblFollowingCount.Text = _following.Count.ToString();
            lblFollowersCount.Text = _followers.Count.ToString();
            lblNotFollowingBackCount.Text = notFollowingYouBackCount.ToString();
            lblYouDoNotFollowBackCount.Text = youDoNotFollowBackCount.ToString();
            lblMutualCount.Text = mutualFollowersCount.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            string searchText = txtSearch.Text.Trim();

            SetGridDataSource(FilterRows(GetActiveRows(), searchText));
        }

        private List<InstagramUserRow> GetActiveRows()
        {
            return _activeResultType switch
            {
                ResultType.NotFollowingBack => _notFollowingBackRows,
                ResultType.YouDoNotFollowBack => _youDoNotFollowBackRows,
                ResultType.Mutual => _mutualFollowerRows,
                _ => []
            };
        }

        private static List<InstagramUserRow> FilterRows(
            List<InstagramUserRow> rows,
            string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return rows;
            }

            return rows
                .Where(row => row.Username.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void SetGridDataSource(
            List<InstagramUserRow> rows)
        {
            // Reinicia el enlace de datos para que la tabla se refresque al cambiar de pestaña o realizar una nueva búsqueda
            dgvResults.DataSource = null;
            dgvResults.DataSource = rows;
        }

        private static string BuildInstagramProfileUrl(string username)
        {
            return $"https://www.instagram.com/{username}";
        }

        private void dgvUsers_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (sender is not DataGridView grid ||
                e.RowIndex < 0 ||
                e.ColumnIndex < 0 ||
                grid.Columns[e.ColumnIndex].Name != "OpenProfile")
            {
                return;
            }

            string? url = (grid.Rows[e.RowIndex].DataBoundItem as InstagramUserRow)?.ProfileUrl;

            if (string.IsNullOrWhiteSpace(url)) return;
            
            // Usa el shell del sistema para abrir el perfil de instagram en el navegador predeterminado del usuario
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        // Clase interna para representar una fila de usuario de Instagram en la tabla de resultados
        private class InstagramUserRow
        {
            public string Username { get; set; } = "";
            public string ProfileUrl { get; set; } = "";
        }

        // Define un enum para representar los tipos de resultados de la comparación de seguidores y seguidos
        private enum ResultType
        {
            NotFollowingBack,
            YouDoNotFollowBack,
            Mutual
        }

      
    }
}
