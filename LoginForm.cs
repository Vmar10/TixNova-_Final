using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TixNova_Final
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Essential for smooth custom drawing when resizing
            this.ResizeRedraw = true;

            // Prevents screen flickering when the gradient and grid are drawn
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Let the base class draw first
            base.OnPaint(e);

            // 1. Define your HTML Hex Colors here
            Color topColor = ColorTranslator.FromHtml("#0A2538");    // Lighter Navy/Teal
            Color bottomColor = ColorTranslator.FromHtml("#040F17"); // Very Dark Navy

            // 2. Draw the smooth gradient background
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                topColor,
                bottomColor,
                45F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // 3. Draw the subtle premium grid texture over the gradient
            // (Keeping FromArgb here because we need the '5' for transparency, which hex doesn't handle easily)
            ControlPaint.DrawGrid(e.Graphics, this.ClientRectangle, new Size(20, 20), Color.FromArgb(5, 255, 255, 255));
        }

      
    }
}