using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TixNova__Final
{
    public partial class TicketDetails : Form
    {
        
        private readonly BookingData _bookingData;
        private readonly List<string> _selectedSeats;

        
        private Form _dropDownForm;
        private DateTime _menuLastClosedTime = DateTime.MinValue;
        private TixNovaMenuControl _menuContent;
        private CustomSearchMenu _searchMenu;
        private CustomMenu _sideMenu;

        
        public TicketDetails(BookingData bookingData = null, List<string> selectedSeats = null)
        {
            InitializeComponent();

            _bookingData = bookingData;
            _selectedSeats = selectedSeats ?? new List<string>();

           
            MakeRoundedGradientButton(MenuButton, Color.FromArgb(78, 199, 220), Color.FromArgb(7, 89, 179), 30);
            MakeRoundedGradientButton(SearchButton, Color.FromArgb(78, 199, 220), Color.FromArgb(7, 89, 179), 35);
            SetupAllLinkLabelsGlow();
            SetupMenu();

            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            SetupUI();
        }

        private void SetupUI()
        {
            
            string movieTitle = _bookingData?.MovieName ?? "Unknown Movie";
            string cinemaName = _bookingData?.Cinema ?? "Unknown Cinema";
            string showTime = _bookingData?.Schedule ?? "Unknown Time";
            string seats = _selectedSeats != null && _selectedSeats.Count > 0 ? string.Join(", ", _selectedSeats) : "None";
            int ticketCount = _selectedSeats?.Count ?? 1;

            
            decimal ticketTotal = (_bookingData?.TicketPrice ?? 250) * ticketCount;
            decimal snacksTotal = _bookingData?.TotalSnacksPrice ?? 0;
            decimal subtotal = ticketTotal + snacksTotal;
            decimal tax = subtotal * 0.12m;
            decimal serviceFee = 50.00m;
            decimal grandTotal = subtotal + tax + serviceFee;

            int panelWidth = 550;
            int panelHeight = 720;

            
            Panel ticketPanel = new Panel
            {
                Size = new Size(panelWidth, panelHeight),
                Location = new Point((this.ClientSize.Width - panelWidth) / 2, (this.ClientSize.Height - panelHeight) / 2),
                BackColor = Color.FromArgb(20, 255, 255, 255)
            };

            
            ticketPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = ticketPanel.ClientRectangle;
                int radius = 30;

                using (GraphicsPath path = GetRoundedPath(rect, radius))
                {
                    ticketPanel.Region = new Region(path);

                    
                    Rectangle headerRect = new Rectangle(0, 0, rect.Width, 80);
                    using (LinearGradientBrush headerBrush = new LinearGradientBrush(headerRect, Color.FromArgb(0, 180, 216), Color.FromArgb(7, 89, 179), LinearGradientMode.Horizontal))
                    {
                        e.Graphics.FillRectangle(headerBrush, headerRect);
                    }

                   
                    using (Pen cyanPen = new Pen(Color.FromArgb(0, 255, 255), 3))
                    {
                        e.Graphics.DrawPath(cyanPen, path);
                    }
                }
            };

            
            AddDetailLabel(ticketPanel, "TICKET DETAILS", new Point(25, 25), true, 18);
            AddDetailLabel(ticketPanel, $"Title : {movieTitle}", new Point(35, 110));
            AddDetailLabel(ticketPanel, $"Cinema : {cinemaName}", new Point(35, 150));
            AddDetailLabel(ticketPanel, $"Time : {showTime}", new Point(35, 190));
            AddDetailLabel(ticketPanel, $"Seats : {seats}", new Point(35, 230));

            
            if (_bookingData?.Snacks != null && _bookingData.Snacks.Count > 0)
            {
                string snacksText = string.Join(", ", _bookingData.Snacks.Select(s => $"{s.Name} x{s.Quantity}"));
                AddDetailLabel(ticketPanel, $"Snacks : {snacksText}", new Point(35, 270));
                AddDetailLabel(ticketPanel, $"Snacks Total : ₱{snacksTotal:N2}", new Point(35, 310));
                
                AddDetailLabel(ticketPanel, $"Tickets : {ticketCount}", new Point(35, 350));

                
                Panel line = new Panel { Size = new Size(panelWidth - 70, 2), Location = new Point(35, 390), BackColor = Color.FromArgb(0, 180, 216) };
                ticketPanel.Controls.Add(line);

                
                AddDetailLabel(ticketPanel, $"Ticket Total : ₱{ticketTotal:N2}", new Point(35, 430));
                AddDetailLabel(ticketPanel, $"Subtotal : ₱{subtotal:N2}", new Point(35, 465));
                AddDetailLabel(ticketPanel, $"Tax (12% VAT) : ₱{tax:N2}", new Point(35, 500));
                AddDetailLabel(ticketPanel, $"Service Fee : ₱{serviceFee:N2}", new Point(35, 535));

                
                Panel line2 = new Panel { Size = new Size(panelWidth - 70, 1), Location = new Point(35, 570), BackColor = Color.FromArgb(100, 255, 255, 255) };
                ticketPanel.Controls.Add(line2);

                AddDetailLabel(ticketPanel, $"TOTAL AMOUNT : ₱{grandTotal:N2}", new Point(35, 600), true, 18);
            }
            else
            {
                
                AddDetailLabel(ticketPanel, $"Tickets : {ticketCount}", new Point(35, 310));

                
                Panel line = new Panel { Size = new Size(panelWidth - 70, 2), Location = new Point(35, 360), BackColor = Color.FromArgb(0, 180, 216) };
                ticketPanel.Controls.Add(line);

                
                AddDetailLabel(ticketPanel, $"Ticket Total : ₱{ticketTotal:N2}", new Point(35, 400));
                AddDetailLabel(ticketPanel, $"Subtotal : ₱{subtotal:N2}", new Point(35, 435));
                AddDetailLabel(ticketPanel, $"Tax (12% VAT) : ₱{tax:N2}", new Point(35, 470));
                AddDetailLabel(ticketPanel, $"Service Fee : ₱{serviceFee:N2}", new Point(35, 505));

                
                Panel line2 = new Panel { Size = new Size(panelWidth - 70, 1), Location = new Point(35, 540), BackColor = Color.FromArgb(100, 255, 255, 255) };
                ticketPanel.Controls.Add(line2);

                AddDetailLabel(ticketPanel, $"TOTAL AMOUNT : ₱{grandTotal:N2}", new Point(35, 575), true, 18);
            }

            
            AddBottomButtons(ticketPanel, panelWidth, grandTotal);

            this.Controls.Add(ticketPanel);
        }

        private void AddBottomButtons(Panel parent, int panelWidth, decimal grandTotal)
        {
            int btnWidth = 150;
            int btnHeight = 45;
            int btnY = parent.Height - 70;
            int spacing = 20;

            int totalButtonsWidth = (btnWidth * 2) + spacing;
            int startX = (panelWidth - totalButtonsWidth) / 2;

            
            Button btnConfirm = new Button
            {
                Text = "CONFIRM PAYMENT",
                Size = new Size(btnWidth, btnHeight),
                Location = new Point(startX, btnY),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 180, 216),
                FlatStyle = FlatStyle.Flat
            };

            
            Button btnCancel = new Button
            {
                Text = "CANCEL",
                Size = new Size(btnWidth, btnHeight),
                Location = new Point(startX + btnWidth + spacing, btnY),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(70, 70, 70),
                FlatStyle = FlatStyle.Flat
            };

            btnConfirm.Click += (s, e) =>
            {
                string seats = _selectedSeats != null && _selectedSeats.Count > 0 ? string.Join(", ", _selectedSeats) : "None";
                DialogResult result = MessageBox.Show(
                    $"Confirm payment of ₱{grandTotal:N2}?\n\nTickets: {seats}",
                    "Confirm Payment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Payment confirmed! Enjoy your movie! 🎬", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MainDashBoard mainForm = new MainDashBoard();
                    mainForm.Show();
                    this.Hide();
                }
            };

            btnCancel.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel Booking",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    
                    BookingSeats seatPicker = new BookingSeats();
                    seatPicker.Show();
                    this.Hide();
                }
            };

            
            MakeRoundedGradientButton(btnConfirm, Color.FromArgb(0, 180, 216), Color.FromArgb(7, 89, 179), 25);
            MakeRoundedGradientButton(btnCancel, Color.FromArgb(80, 80, 85), Color.FromArgb(50, 50, 55), 25);

            parent.Controls.Add(btnConfirm);
            parent.Controls.Add(btnCancel);
        }

        private void AddDetailLabel(Panel parent, string text, Point loc, bool isBold = false, int size = 14)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = loc,
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", size, isBold ? FontStyle.Bold : FontStyle.Regular)
            };
            parent.Controls.Add(lbl);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        internal enum AccentState
        {
            ACCENT_ENABLE_BLURBEHIND = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        private void EnableBlur(IntPtr hwnd)
        {
            var accent = new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND };
            int accentStructSize = Marshal.SizeOf(accent);
            IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            SetWindowCompositionAttribute(hwnd, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        private void SetupMenu()
        {
            _menuContent = new TixNovaMenuControl { Location = new Point(0, 0) };
            _dropDownForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                Size = _menuContent.Size,
                BackColor = Color.Black,
                Region = new Region(_menuContent.GetRegionPath())
            };
            _dropDownForm.Controls.Add(_menuContent);
            _dropDownForm.HandleCreated += (s, e) => EnableBlur(_dropDownForm.Handle);
            _dropDownForm.Deactivate += (s, e) =>
            {
                _dropDownForm.Hide();
                _menuLastClosedTime = DateTime.Now;
            };
        }

        private void SetupAllLinkLabelsGlow()
        {
            foreach (Control control in this.Controls)
            {
                if (control is LinkLabel linkLabel)
                {
                    var originalColor = linkLabel.LinkColor;
                    Timer pulseTimer = null;
                    int pulseValue = 0;
                    bool increasing = true;

                    linkLabel.MouseEnter += (sender, e) =>
                    {
                        LinkLabel lbl = sender as LinkLabel;
                        pulseTimer = new Timer { Interval = 50 };
                        pulseTimer.Tick += (ts, te) =>
                        {
                            if (increasing)
                            {
                                pulseValue += 15;
                                if (pulseValue >= 255) increasing = false;
                            }
                            else
                            {
                                pulseValue -= 15;
                                if (pulseValue <= 100) increasing = true;
                            }
                            lbl.LinkColor = Color.FromArgb(255, 0, pulseValue, 255);
                        };
                        pulseTimer.Start();
                    };

                    linkLabel.MouseLeave += (sender, e) =>
                    {
                        LinkLabel lbl = sender as LinkLabel;
                        pulseTimer?.Stop();
                        pulseTimer?.Dispose();
                        lbl.LinkColor = originalColor;
                    };
                }
            }
        }

        private void MakeRoundedGradientButton(Button btn, Color startColor, Color endColor, int radius = 20)
        {
            btn.FlatStyle = FlatStyle.Popup;
            btn.FlatAppearance.BorderSize = 0;
            btn.Tag = new GradientInfo { StartColor = startColor, EndColor = endColor };

            var originalSize = btn.Size;
            var originalLocation = btn.Location;

            btn.MouseEnter += (sender, e) =>
            {
                btn.Size = new Size(btn.Width + 5, btn.Height + 5);
                btn.Location = new Point(btn.Location.X - 2, btn.Location.Y - 2);
                btn.Cursor = Cursors.Hand;
            };

            btn.MouseLeave += (sender, e) =>
            {
                btn.Size = originalSize;
                btn.Location = originalLocation;
                btn.Cursor = Cursors.Default;
            };

            btn.Paint += (sender, e) =>
            {
                Button b = sender as Button;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = new GraphicsPath())
                {
                    Rectangle rect = new Rectangle(0, 0, b.Width - 1, b.Height - 1);
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    b.Region = new Region(path);

                    GradientInfo gradient = (GradientInfo)b.Tag;
                    using (var brush = new LinearGradientBrush(rect, gradient.StartColor, gradient.EndColor, LinearGradientMode.Vertical))
                        e.Graphics.FillPath(brush, path);

                    TextRenderer.DrawText(e.Graphics, b.Text, b.Font, rect, b.ForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            };
            btn.Resize += (sender, e) => btn.Invalidate();
        }

        private class GradientInfo
        {
            public Color StartColor { get; set; }
            public Color EndColor { get; set; }
        }

        
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainDashBoard mainForm = new MainDashBoard();
            mainForm.Show();
            this.Hide();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoviesForm moviesForm = new MoviesForm();
            moviesForm.Show();
            this.Hide();
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CinemasForm cinemasForm = new CinemasForm();
            cinemasForm.Show();
            this.Hide();
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShopForm shopForm = new ShopForm();
            shopForm.Show();
            this.Hide();
        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((DateTime.Now - _menuLastClosedTime).TotalMilliseconds < 100) return;

            if (_dropDownForm.Visible) _dropDownForm.Hide();
            else
            {
                int xOffset = (linkLabel5.Width - _menuContent.Width) / 2;
                Point screenLocation = linkLabel5.PointToScreen(new Point(xOffset, linkLabel5.Height + 5));
                _dropDownForm.Location = screenLocation;
                _dropDownForm.Show();
                _dropDownForm.BringToFront();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (_searchMenu == null || _searchMenu.IsDisposed)
            {
                _searchMenu = new CustomSearchMenu(this);
                Point screenPos = SearchButton.PointToScreen(new Point(0, SearchButton.Height));
                _searchMenu.Location = new Point(screenPos.X - (_searchMenu.Width / 2) + (SearchButton.Width / 2), screenPos.Y + 10);
                _searchMenu.Show();
            }
            else
            {
                if (_searchMenu.Visible) _searchMenu.Hide();
                else _searchMenu.Show();
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (_sideMenu == null || _sideMenu.IsDisposed)
            {
                _sideMenu = new CustomMenu();
                Point screenPos = this.PointToScreen(new Point(this.Width - _sideMenu.Width - 20, 50));
                _sideMenu.Location = screenPos;
                _sideMenu.Show();
            }
            else
            {
                _sideMenu.Visible = !_sideMenu.Visible;
            }
        }
    }
}