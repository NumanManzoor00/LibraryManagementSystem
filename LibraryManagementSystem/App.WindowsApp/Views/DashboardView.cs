using App.Core.Enums;
using App.Core.Services;
using App.WindowsApp.Forms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.WindowsApp.Views
{
    public partial class DashboardView : UserControl
    {
        private readonly BookService bookService = new BookService();
        private readonly MemberService memberService = new MemberService();
        private readonly LibrarianService librarianService = new LibrarianService();
        private readonly LoanService loanService = new LoanService();

        private int totalBooks, availableBooks, borrowedBooks, reservedBooks, lostBooks;
        private int fictionBooks, nonFictionBooks, referenceBooks, periodicalBooks;

        public DashboardView()
        {
            InitializeComponent();
            _ = LoadSummaryAsync();
        }

        private void buttonRefresh_Click(object sender, EventArgs e) => _ = LoadSummaryAsync();

        private async Task LoadSummaryAsync()
        {
            buttonRefresh.Enabled = false;
            buttonRefresh.Text = "Loading...";
            try
            {
                var books = await bookService.GetAllBooksAsync();
                var members = await memberService.GetAllMembersAsync();
                var librarians = await librarianService.GetAllLibrariansAsync();
                var loans = await loanService.GetAllLoansAsync();

                totalBooks = books.Count;
                availableBooks = books.Count(b => b.Status == BookStatus.Available);
                borrowedBooks = books.Count(b => b.Status == BookStatus.Borrowed);
                reservedBooks = books.Count(b => b.Status == BookStatus.Reserved);
                lostBooks = books.Count(b => b.Status == BookStatus.Lost);

                fictionBooks = books.Count(b => b.Genre == BookGenre.Fiction);
                nonFictionBooks = books.Count(b => b.Genre == BookGenre.NonFiction);
                referenceBooks = books.Count(b => b.Genre == BookGenre.Reference);
                periodicalBooks = books.Count(b => b.Genre == BookGenre.Periodical);

                labelTotalBooks.Text = totalBooks.ToString();
                labelAvailableBooks.Text = availableBooks.ToString();
                labelBorrowedBooks.Text = borrowedBooks.ToString();
                labelReservedBooks.Text = reservedBooks.ToString();
                labelLostBooks.Text = lostBooks.ToString();
                labelMembersCount.Text = members.Count.ToString();
                labelLibrariansCount.Text = librarians.Count.ToString();
                labelLoansCount.Text = loans.Count.ToString();

                (this.FindForm() as DashboardForm)?.SetStatusRecord($"Total books: {totalBooks}");
                panelStatusChart.Invalidate();
                panelGenreChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonRefresh.Enabled = true;
                buttonRefresh.Text = "↻  Refresh";
            }
        }

        private void panelStatusChart_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawChartTitle(e.Graphics, panelStatusChart.ClientRectangle, "Books by Status (GDI+)");
            int total = availableBooks + borrowedBooks + reservedBooks + lostBooks;
            if (total == 0) { DrawEmptyChart(e.Graphics, panelStatusChart.ClientRectangle, "No books yet"); return; }

            Rectangle pieBounds = GetPieBounds(panelStatusChart.ClientRectangle);
            float startAngle = -90F;
            (int count, Color color, string label)[] segments =
            {
                (availableBooks, Color.SeaGreen, "Available"),
                (borrowedBooks, Color.CornflowerBlue, "Borrowed"),
                (reservedBooks, Color.Goldenrod, "Reserved"),
                (lostBooks, Color.Firebrick, "Lost")
            };

            using Pen borderPen = new Pen(Color.White, 2F);
            foreach (var (count, color, label) in segments)
            {
                if (count == 0) continue;
                float sweep = count * 360F / total;
                using SolidBrush brush = new SolidBrush(color);
                e.Graphics.FillPie(brush, pieBounds, startAngle, sweep);
                startAngle += sweep;
            }
            e.Graphics.DrawEllipse(borderPen, pieBounds);

            int legendX = pieBounds.Right + 14, legendY = pieBounds.Top + 8;
            foreach (var (count, color, label) in segments)
            {
                DrawLegendItem(e.Graphics, legendX, legendY, color, label, count);
                legendY += 26;
            }
        }

        private void panelGenreChart_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawChartTitle(e.Graphics, panelGenreChart.ClientRectangle, "Books by Genre (GDI+)");

            int maxValue = Math.Max(1, new[] { fictionBooks, nonFictionBooks, referenceBooks, periodicalBooks }.Max());
            int top = 50, left = 100, maxBarWidth = Math.Max(20, panelGenreChart.Width - left - 30), barH = 16, gap = 14;

            DrawBar(e.Graphics, "Fiction", fictionBooks, maxValue, left, top, maxBarWidth, barH, Color.CornflowerBlue);
            DrawBar(e.Graphics, "Non-Fiction", nonFictionBooks, maxValue, left, top + (barH + gap), maxBarWidth, barH, Color.MediumSeaGreen);
            DrawBar(e.Graphics, "Reference", referenceBooks, maxValue, left, top + (barH + gap) * 2, maxBarWidth, barH, Color.Goldenrod);
            DrawBar(e.Graphics, "Periodical", periodicalBooks, maxValue, left, top + (barH + gap) * 3, maxBarWidth, barH, Color.MediumPurple);
        }

        private static void DrawChartTitle(Graphics g, Rectangle b, string title)
        {
            using Font f = new Font("Segoe UI", 10F, FontStyle.Bold);
            TextRenderer.DrawText(g, title, f, new Rectangle(12, 10, b.Width - 24, 24), Color.FromArgb(35, 45, 60), TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private static void DrawEmptyChart(Graphics g, Rectangle b, string msg)
        {
            using Font f = new Font("Segoe UI", 9F);
            TextRenderer.DrawText(g, msg, f, new Rectangle(12, 48, b.Width - 24, 32), Color.DimGray, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private static Rectangle GetPieBounds(Rectangle b)
        {
            int size = Math.Min(92, Math.Max(48, Math.Min(b.Width / 3, b.Height - 58)));
            return new Rectangle(18, 48, size, size);
        }

        private static void DrawLegendItem(Graphics g, int x, int y, Color color, string label, int value)
        {
            using SolidBrush brush = new SolidBrush(color);
            using Font f = new Font("Segoe UI", 9F);
            g.FillRectangle(brush, x, y + 4, 12, 12);
            TextRenderer.DrawText(g, $"{label}: {value}", f, new Rectangle(x + 16, y, 140, 20), Color.FromArgb(45, 45, 45), TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private static void DrawBar(Graphics g, string label, int value, int maxValue, int x, int y, int maxWidth, int height, Color color)
        {
            int width = Math.Max(2, value * maxWidth / maxValue);
            using SolidBrush barBrush = new SolidBrush(color);
            using SolidBrush trackBrush = new SolidBrush(Color.FromArgb(235, 239, 244));
            using Font lf = new Font("Segoe UI", 9F);
            TextRenderer.DrawText(g, label, lf, new Rectangle(8, y - 2, x - 12, height + 4), Color.FromArgb(45, 45, 45), TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            g.FillRectangle(trackBrush, x, y, maxWidth, height);
            g.FillRectangle(barBrush, x, y, width, height);
            TextRenderer.DrawText(g, value.ToString(), lf, new Rectangle(x + maxWidth - 38, y - 2, 38, height + 4), Color.FromArgb(35, 35, 35), TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        }
    }
}
