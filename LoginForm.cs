using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TixNova__Final;

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

            // --- BULLETPROOF PLACEHOLDER SETUP ---

            // 1. Set the initial text and color
            roundedTextBox1.Text = "Enter username...";
            roundedTextBox1.ForeColor = Color.Gray;

            roundedTextBox2.Text = "Enter password...";
            roundedTextBox2.ForeColor = Color.Gray;

            // 2. Force the password masks completely OFF when the app starts
            roundedTextBox2.UsePasswordChar = false;
            roundedTextBox2.PasswordChar = '\0';
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
            ControlPaint.DrawGrid(e.Graphics, this.ClientRectangle, new Size(20, 20), Color.FromArgb(5, 255, 255, 255));
        }

        private void roundedButton1_Click(object sender, System.EventArgs e)
        {
            string typedusername = roundedTextBox1.Text;
            string typedpassword = roundedTextBox2.Text;

            // --- SAFEGUARD: If the text is the placeholder, treat it as empty! ---
            if (typedusername == "Enter username...") typedusername = "";
            if (typedpassword == "Enter password...") typedpassword = "";

            // 1. Check if BOTH fields are empty
            if (string.IsNullOrWhiteSpace(typedusername) && string.IsNullOrWhiteSpace(typedpassword))
            {
                MessageBox.Show("Please fill out the fields.", "Fields Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                roundedTextBox1.Focus(); // Put the blinking cursor inside the username box
                return; // Stop here so it doesn't try to log in
            }

            // 2. Check if ONLY the password is missing
            if (string.IsNullOrWhiteSpace(typedpassword))
            {
                MessageBox.Show("Password is missing.", "Missing Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Clear the fields, reset the mask, and start over at the username
                roundedTextBox1.Text = "";
                roundedTextBox2.Text = "";
                roundedTextBox2.PasswordChar = '\0'; // Ensure mask is off if they tabbed through
                roundedTextBox1.Focus();
                return;
            }

            // 3. Check if ONLY the username is missing
            if (string.IsNullOrWhiteSpace(typedusername))
            {
                MessageBox.Show("Username is missing.", "Missing Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                roundedTextBox1.Focus();
                return;
            }

            // --- IF WE GET PAST THE CHECKS ABOVE, WE PROCEED WITH LOGIN ---

            string validUsername = "vistoedmar";
            string validPassword = "password123";

            // 4. Check if they match
            if (typedusername == validUsername && typedpassword == validPassword)
            {
                // SUCCESS! Store the user's name in our global memory bank
                UserSession.CurrentUsername = typedusername;

                // Hide the login screen
                this.Hide();

                // Open your main dashboard
                MainDashBoard dashboard = new MainDashBoard();
                dashboard.ShowDialog();

                // Close the application entirely when the dashboard is closed
                this.Close();
            }
            else
            {
                // FAILED LOGIN (Wrong username or password)
                MessageBox.Show("Incorrect username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear the password box so they can try typing it again
                roundedTextBox2.Text = "";
                roundedTextBox2.Focus();
            }
        }

        private void LoginForm_Load(object sender, System.EventArgs e)
        {
            // 1. Set the initial placeholders when the app starts
            roundedTextBox1.Text = "Enter username...";
            roundedTextBox1.ForeColor = Color.Gray;

            roundedTextBox2.Text = "Enter password...";
            roundedTextBox2.ForeColor = Color.Gray;

            // 2. Turn OFF the password mask initially so you can actually read "Enter password..."
            // ('\0' is a special code that means "no masking character")
            roundedTextBox2.PasswordChar = '\0';

            // 3. Prevent the first text box from auto-selecting and instantly clearing its placeholder
            this.ActiveControl = null;
        }

        private void roundedTextBox1_Enter(object sender, System.EventArgs e)
        {
            if (roundedTextBox1.Text == "Enter username...")
            {
                roundedTextBox1.Text = "";
                // Change text color back to your normal color (e.g., White)
                roundedTextBox1.ForeColor = Color.White;
            }
        }

        private void roundedTextBox1_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(roundedTextBox1.Text))
            {
                roundedTextBox1.Text = "Enter username...";
                // Change text color to a faded gray to look like a placeholder
                roundedTextBox1.ForeColor = Color.Gray;
            }
        }

        private void roundedTextBox2_Enter(object sender, System.EventArgs e)
        {
            if (roundedTextBox2.Text == "Enter password...")
            {
                roundedTextBox2.Text = "";
                roundedTextBox2.ForeColor = Color.White;

                // Turn ON the password mask since they are about to type a real password
                roundedTextBox2.PasswordChar = '●';
            }
        }

        private void roundedTextBox2_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(roundedTextBox2.Text))
            {
                // Turn OFF the password mask so the placeholder is readable again
                roundedTextBox2.PasswordChar = '\0';

                roundedTextBox2.Text = "Enter password...";
                roundedTextBox2.ForeColor = Color.Gray;
            }
        }
    }
}