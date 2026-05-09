using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TixNova__Final
{
    public partial class SearchTopFamily : Form
    {
        public SearchTopFamily()
        {
            InitializeComponent();
        }

        private void FadingImageButton1_Click(object sender, EventArgs e)
        {
            // 1. Set the exact same scroll amount as the Right button
            int scrollAmount = 840;

            // 2. Move the track to the right (reveals movies on the left)
            movieTrackPanel.Left += scrollAmount;

            // 3. Stop perfectly at the beginning of the list
            if (movieTrackPanel.Left > 0)
            {
                movieTrackPanel.Left = 0;
            }
        }

        private void FadingImageButton2_Click(object sender, EventArgs e)
        {
            // 1. Set the scroll amount for a "Page"
            // Change 840 if your pictures in this form are a different size!
            int scrollAmount = 840;

            // 2. Move the track to the left (reveals movies on the right)
            movieTrackPanel.Left -= scrollAmount;

            // 3. Stop perfectly at the end of the list
            int maxScrollLimit = viewportPanel.Width - movieTrackPanel.Width;
            if (movieTrackPanel.Left < maxScrollLimit)
            {
                movieTrackPanel.Left = maxScrollLimit;
            }
        }

        private void RoundedPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BlueGradientLabel1_Click(object sender, EventArgs e)
        {

        }

        private void GradientLabel2_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void GradientLabel1_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void RoundedPictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void MovieTrackPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ViewportPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
