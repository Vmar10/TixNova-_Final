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
            
            int scrollAmount = 840;

            
            movieTrackPanel.Left += scrollAmount;

           
            if (movieTrackPanel.Left > 0)
            {
                movieTrackPanel.Left = 0;
            }
        }

        private void FadingImageButton2_Click(object sender, EventArgs e)
        {
            
            int scrollAmount = 840;

            
            movieTrackPanel.Left -= scrollAmount;

            
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
