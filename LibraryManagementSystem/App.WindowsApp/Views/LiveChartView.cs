using App.Core.Enums;
using App.Core.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Linq;
using System.Windows.Forms;

namespace App.WindowsApp.Views
{
    /// <summary>
    /// LiveCharts2 chart view — satisfies the required Charting Module from the feature spec.
    /// Chart 1: PieChart — Books by Status
    /// Chart 2: BarChart — Books by Genre
    /// Both update automatically when Refresh is clicked.
    /// </summary>
    public partial class LiveChartView : UserControl
    {
        private readonly BookService _bookService = new BookService();
        private PieChart _pieChart = null!;
        private CartesianChart _barChart = null!;
        private Button _btnRefresh = null!;
        private Label _labelTitle = null!;
        private Label _labelPie = null!;
        private Label _labelBar = null!;

        public LiveChartView()
        {
            InitLayout();
            LoadCharts();
        }

        private void InitLayout()
        {
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 252);
            this.Dock = DockStyle.Fill;

            _labelTitle = new Label
            {
                Text = "LiveCharts2 — Book Analytics",
                Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(30, 40, 60),
                Location = new System.Drawing.Point(20, 14),
                Size = new System.Drawing.Size(500, 32),
                AutoSize = false
            };

            _btnRefresh = new Button
            {
                Text = "↻  Refresh Charts",
                Font = new System.Drawing.Font("Segoe UI", 9.5F),
                Location = new System.Drawing.Point(820, 14),
                Size = new System.Drawing.Size(140, 32),
                BackColor = System.Drawing.Color.FromArgb(0, 120, 212),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnRefresh.FlatAppearance.BorderSize = 0;
            _btnRefresh.Click += (s, e) => LoadCharts();

            _labelPie = new Label
            {
                Text = "Books by Status",
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(40, 50, 70),
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(460, 24),
                AutoSize = false
            };

            _labelBar = new Label
            {
                Text = "Books by Genre",
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(40, 50, 70),
                Location = new System.Drawing.Point(500, 60),
                Size = new System.Drawing.Size(460, 24),
                AutoSize = false
            };

            _pieChart = new PieChart
            {
                Location = new System.Drawing.Point(20, 88),
                Size = new System.Drawing.Size(460, 320),
                BackColor = System.Drawing.Color.White,
                LegendPosition = LiveChartsCore.Measure.LegendPosition.Bottom
            };

            _barChart = new CartesianChart
            {
                Location = new System.Drawing.Point(500, 88),
                Size = new System.Drawing.Size(460, 320),
                BackColor = System.Drawing.Color.White
            };

            this.Controls.AddRange(new Control[]
            {
                _labelTitle, _btnRefresh,
                _labelPie, _labelBar,
                _pieChart, _barChart
            });
        }

        private void LoadCharts()
        {
            _btnRefresh.Enabled = false;
            _btnRefresh.Text = "Loading...";
            try
            {
                var books = _bookService.GetAllBooks();

                // --- Pie Chart: Books by Status ---
                int available = books.Count(b => b.Status == BookStatus.Available);
                int borrowed = books.Count(b => b.Status == BookStatus.Borrowed);
                int reserved = books.Count(b => b.Status == BookStatus.Reserved);
                int lost = books.Count(b => b.Status == BookStatus.Lost);

                _pieChart.Series = new ISeries[]
                {
                    new PieSeries<double> { Values = new double[] { available }, Name = "Available", Fill = new SolidColorPaint(SKColors.SeaGreen) },
                    new PieSeries<double> { Values = new double[] { borrowed },  Name = "Borrowed",  Fill = new SolidColorPaint(SKColors.CornflowerBlue) },
                    new PieSeries<double> { Values = new double[] { reserved },  Name = "Reserved",  Fill = new SolidColorPaint(SKColors.Goldenrod) },
                    new PieSeries<double> { Values = new double[] { lost },      Name = "Lost",      Fill = new SolidColorPaint(SKColors.Firebrick) },
                };

                // --- Bar Chart: Books by Genre ---
                int fiction = books.Count(b => b.Genre == BookGenre.Fiction);
                int nonFiction = books.Count(b => b.Genre == BookGenre.NonFiction);
                int reference = books.Count(b => b.Genre == BookGenre.Reference);
                int periodical = books.Count(b => b.Genre == BookGenre.Periodical);

                _barChart.Series = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Name = "Book Count",
                        Values = new double[] { fiction, nonFiction, reference, periodical },
                        Fill = new SolidColorPaint(SKColor.Parse("#0078D4"))
                    }
                };

                _barChart.XAxes = new[]
                {
                    new Axis
                    {
                        Labels = new[] { "Fiction", "Non-Fiction", "Reference", "Periodical" },
                        LabelsRotation = 0
                    }
                };

                _barChart.YAxes = new[]
                {
                    new Axis { MinLimit = 0 }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading chart data:\n{ex.Message}", "Chart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _btnRefresh.Enabled = true;
                _btnRefresh.Text = "↻  Refresh Charts";
            }
        }
    }
}
