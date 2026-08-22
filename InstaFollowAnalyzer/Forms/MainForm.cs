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
        private List<InstagramUserRow> _notFollowingBackRows = [];
        private List<InstagramUserRow> _youDoNotFollowBackRows = [];
        private List<InstagramUserRow> _mutualFollowerRows = [];
        private ResultsView _activeResultsView = ResultsView.NotFollowingBack;

        public MainForm()
        {
            InitializeComponent();
            ConfigureResultsTabs();
            ConfigureUsersGrids();
            RegisterGridLinkEvents();
            CenterContent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterContent();
        }

        private async void btnLoadFollowing_Click(object sender, EventArgs e)
        {
            string? filePath = SelectJsonFile();

            if (filePath is null)
            {
                return;
            }

            try
            {
                _following = await _instaJsonService.ReadFollowingUsernamesAsync(filePath);

                MessageBox.Show(
                    $"Archivo de seguidos cargado correctamente.\nUsuarios encontrados: {_following.Count}",
                    "Following cargado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                UpdateCounters(0, 0, 0);
                CompareIfBothFilesAreLoaded();
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

            if (filePath is null)
            {
                return;
            }

            try
            {
                _followers = await _instaJsonService.ReadFollowerUsernamesAsync(filePath);

                MessageBox.Show(
                    $"Archivo de seguidores cargado correctamente.\nUsuarios encontrados: {_followers.Count}",
                    "Followers cargado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                UpdateCounters(0, 0, 0);
                CompareIfBothFilesAreLoaded();
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

        private void CompareIfBothFilesAreLoaded()
        {
            if (_following.Count == 0 || _followers.Count == 0)
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

            ShowUsersInTabs(notFollowingYouBack, youDoNotFollowBack, mutualFollowers);
        }

        private void ConfigureUsersGrids()
        {
            ConfigureUsersGrid(dgvNotFollowingBack);
        }

        private void ConfigureResultsTabs()
        {
            btnTabNotFollowingBack.Click += (_, _) => SelectResultsView(ResultsView.NotFollowingBack);
            btnTabYouDoNotFollowBack.Click += (_, _) => SelectResultsView(ResultsView.YouDoNotFollowBack);
            btnTabMutualFollowers.Click += (_, _) => SelectResultsView(ResultsView.MutualFollowers);
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

        private void SelectResultsView(ResultsView resultsView)
        {
            _activeResultsView = resultsView;
            UpdateTabButtonStyles();
            ApplySearchFilter();
        }

        private void UpdateTabButtonStyles()
        {
            StyleTabButton(
                btnTabNotFollowingBack,
                _activeResultsView == ResultsView.NotFollowingBack,
                Color.FromArgb(255, 241, 242),
                Color.FromArgb(190, 18, 60)
            );

            StyleTabButton(
                btnTabYouDoNotFollowBack,
                _activeResultsView == ResultsView.YouDoNotFollowBack,
                Color.FromArgb(239, 246, 255),
                Color.FromArgb(37, 99, 235)
            );

            StyleTabButton(
                btnTabMutualFollowers,
                _activeResultsView == ResultsView.MutualFollowers,
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

        private void CenterContent()
        {
            const int statWidth = 188;
            const int statGap = 28;
            const int statTop = 116;
            const int searchWidth = 700;
            const int tableWidth = 700;
            const int resultsTop = 328;

            int statsTotalWidth = (statWidth * 5) + (statGap * 4);
            int statsLeft = Math.Max(24, (ClientSize.Width - statsTotalWidth) / 2);

            statFollowingPanel.Location = new Point(statsLeft, statTop);
            statFollowersPanel.Location = new Point(statsLeft + (statWidth + statGap), statTop);
            statNotFollowingBackPanel.Location = new Point(statsLeft + ((statWidth + statGap) * 2), statTop);
            statYouDoNotFollowBackPanel.Location = new Point(statsLeft + ((statWidth + statGap) * 3), statTop);
            statMutualPanel.Location = new Point(statsLeft + ((statWidth + statGap) * 4), statTop);

            searchPanel.Width = searchWidth;
            searchPanel.Left = Math.Max(24, (ClientSize.Width - searchPanel.Width) / 2);
            txtSearch.Width = searchPanel.Width - 70;

            int resultsLeft = Math.Max(24, (ClientSize.Width - tableWidth) / 2);
            tabButtonsPanel.SetBounds(resultsLeft, resultsTop, tableWidth, 34);

            int tabWidth = tableWidth / 3;
            btnTabNotFollowingBack.SetBounds(0, 0, tabWidth, tabButtonsPanel.Height);
            btnTabYouDoNotFollowBack.SetBounds(tabWidth, 0, tabWidth, tabButtonsPanel.Height);
            btnTabMutualFollowers.SetBounds(tabWidth * 2, 0, tableWidth - (tabWidth * 2), tabButtonsPanel.Height);

            dgvNotFollowingBack.SetBounds(
                resultsLeft,
                tabButtonsPanel.Bottom,
                tableWidth,
                Math.Max(160, ClientSize.Height - tabButtonsPanel.Bottom - 32)
            );
        }

        private void RegisterGridLinkEvents()
        {
            dgvNotFollowingBack.CellContentClick += dgvUsers_CellContentClick;
        }

        private void ShowUsersInTabs(
            List<string> notFollowingYouBack,
            List<string> youDoNotFollowBack,
            List<string> mutualFollowers)
        {
            _notFollowingBackRows = CreateRows(notFollowingYouBack);
            _youDoNotFollowBackRows = CreateRows(youDoNotFollowBack);
            _mutualFollowerRows = CreateRows(mutualFollowers);

            _activeResultsView = ResultsView.NotFollowingBack;
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

            SetGridDataSource(dgvNotFollowingBack, FilterRows(GetActiveRows(), searchText));
        }

        private List<InstagramUserRow> GetActiveRows()
        {
            return _activeResultsView switch
            {
                ResultsView.NotFollowingBack => _notFollowingBackRows,
                ResultsView.YouDoNotFollowBack => _youDoNotFollowBackRows,
                ResultsView.MutualFollowers => _mutualFollowerRows,
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

        private static void SetGridDataSource(
            DataGridView grid,
            List<InstagramUserRow> rows)
        {
            grid.DataSource = null;
            grid.DataSource = rows;
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

            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private class InstagramUserRow
        {
            public string Username { get; set; } = "";
            public string ProfileUrl { get; set; } = "";
        }

        private enum ResultsView
        {
            NotFollowingBack,
            YouDoNotFollowBack,
            MutualFollowers
        }

      
    }
}
