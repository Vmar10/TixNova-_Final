using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TixNova__Final
{
    public class TixNovaMenuControl : UserControl
    {
        public TixNovaMenuControl()
        {
            this.Size = new Size(480, 280);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;

            InitializeMenuContent();
            this.Region = new Region(GetRegionPath());
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT: Makes the background invisible
                return cp;
            }
        }

        private void InitializeMenuContent()
        {
            int col1X = 0;
            int col2X = 240;

            AddHeader("GENRE", col1X, 40);
            string[] genres = { "Action & Adventure", "Sci-Fi & Fantasy", "Horror & Thriller", "Comedy & Family", "Drama", "Action", "Animation" };
            for (int i = 0; i < genres.Length; i++)
            {
                AddItem(genres[i], col1X, 75 + (i * 25));
            }

            AddHeader("DISCOVER", col2X, 40);
            string[] discover = { "Coming Soon", "Rated G", "Rated PG", "Rated SPG","PG-13", "Staff Picks" };
            for (int i = 0; i < discover.Length; i++)
            {
                AddItem(discover[i], col2X, 75 + (i * 25));
            }
        }

        private void AddHeader(string text, int xOffset, int y)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(xOffset, y),
                Size = new Size(240, 30),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(103, 190, 217),
                BackColor = Color.Transparent,
                UseMnemonic = false
            };
            this.Controls.Add(lbl);
        }

        private void AddItem(string text, int xOffset, int y)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(xOffset, y),
                Size = new Size(240, 25),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(230, 230, 230),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };

            lbl.MouseEnter += (s, e) => lbl.ForeColor = Color.FromArgb(0, 191, 255);
            lbl.MouseLeave += (s, e) => lbl.ForeColor = Color.FromArgb(230, 230, 230);

            // ==========================================
            // STEP 1: Add the Click event handler here
            // ==========================================
            lbl.Click += MenuItem_Click;

            this.Controls.Add(lbl);
        }

        // ==========================================
        // STEP 2: The Click Event Logic Method
        // ==========================================
        private void MenuItem_Click(object sender, EventArgs e)
        {
            // 1. Identify which label was clicked
            Label clickedLabel = sender as Label;
            if (clickedLabel == null) return;

            string selectedCategory = clickedLabel.Text;

            // Grab a reference to the main dashboard form that contains this menu
            Form mainDashboard = this.FindForm();

            // 2. Use a switch statement to open the correct form based on the text
            switch (selectedCategory)
            {
                case "Action":
                case "Action & Adventure":

                    GenreActionForm actionForm = new GenreActionForm();

                    // Hide the Main Dashboard
                    if (mainDashboard != null)
                    {
                        mainDashboard.Hide();
                    }

                    // Show the Action form
                    actionForm.Show();

                    // (Recommended) Show the main dashboard again when ActionForm is closed
                    actionForm.FormClosed += (s, args) =>
                    {
                        if (mainDashboard != null)
                        {
                            mainDashboard.Show();
                        }
                    };

                    break;

                case "Sci-Fi & Fantasy":
                    GenreSciFi sciFiForm = new GenreSciFi();

                    if (mainDashboard != null)
                    {
                        mainDashboard.Hide();
                    }
                    sciFiForm.Show();

                    break;

                case "Horror & Thriller":
                    GenreHorror horrorForm = new GenreHorror();

                    if(mainDashboard != null)
                    {
                        mainDashboard.Hide();
                    }
                    horrorForm.Show();

                    break;

                case "Coming Soon":
                    ComingsoonForm comingSoonForm = new ComingsoonForm();
                    if (mainDashboard != null)
                    {
                        mainDashboard.Hide();
                    }
                    comingSoonForm.Show();
                    break;

                case "Rated G":
                    RatedGForm ratedGForm = new RatedGForm();
                    if (mainDashboard != null)
                    {
                        mainDashboard.Hide();
                    }
                    ratedGForm.Show();
                    break;

                case "Rated PG":
                    RatedPGForm ratedPGForm = new RatedPGForm();
                    if (mainDashboard != null)
                    {
                        mainDashboard.Hide();
                    }
                    ratedPGForm.Show();
                    break;



                default:
                    // Fallback so you know the click registered for unmade forms
                    MessageBox.Show($"Coming soon: {selectedCategory} section!", "TixNova+");
                    break;
            }
        }
        // ==========================================

        // Extracted the path logic so the Parent Form can use it to clip its bounds
        // 1. Path for the Form's Region (determines the actual window shape)
        public GraphicsPath GetRegionPath()
        {
            return CreateMenuPath(0, 0, this.Width, this.Height);
        }

        // 2. Path for Drawing (inset by 1 pixel so the border isn't cut off)
        public GraphicsPath GetDrawingPath()
        {
            // Subtracting 2 from Width/Height to inset 1px on left/right and top/bottom
            return CreateMenuPath(1, 1, this.Width - 2, this.Height - 2);
        }

        // Core math for the shape
        private GraphicsPath CreateMenuPath(int x, int y, int width, int height)
        {
            int radius = 15;
            int arrowWidth = 20;
            int arrowHeight = 15;

            int right = x + width;
            int bottom = y + height;
            int arrowX = x + (width / 2) - (arrowWidth / 2);

            GraphicsPath path = new GraphicsPath();

            // Top edge with arrow
            path.AddLine(x + radius, y + arrowHeight, arrowX, y + arrowHeight);
            path.AddLine(arrowX, y + arrowHeight, arrowX + (arrowWidth / 2), y);
            path.AddLine(arrowX + (arrowWidth / 2), y, arrowX + arrowWidth, y + arrowHeight);
            path.AddLine(arrowX + arrowWidth, y + arrowHeight, right - radius, y + arrowHeight);

            // Right edge
            path.AddArc(right - (radius * 2), y + arrowHeight, radius * 2, radius * 2, 270, 90);
            path.AddLine(right, y + arrowHeight + radius, right, bottom - radius);

            // Bottom edge
            path.AddArc(right - (radius * 2), bottom - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddLine(right - radius, bottom, x + radius, bottom);

            // Left edge
            path.AddArc(x, bottom - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.AddLine(x, bottom - radius, x, y + arrowHeight + radius);

            // Top left arc
            path.AddArc(x, y + arrowHeight, radius * 2, radius * 2, 180, 90);

            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath drawPath = GetDrawingPath())
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(140, 22, 25, 35)))
                {
                    g.FillPath(brush, drawPath);
                }

                using (Pen pen = new Pen(Color.FromArgb(103, 190, 217), 1.0f))
                {
                    g.DrawPath(pen, drawPath);
                }
            }
        }
    }
}