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
    public partial class MainDashBoard : Form
    {
        public MainDashBoard()
        {
            InitializeComponent();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoviesForm moviesForm = new MoviesForm();

           
            moviesForm.Show();

            
            this.Hide();
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CinemasForm cinemaform = new CinemasForm();

            cinemaform.Show();

            this.Hide();
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShopForm shopform = new ShopForm();

            shopform.Show();

            this.Hide();
        }

        private void FadingImageButton2_Click(object sender, EventArgs e)
        {
            // 1. Set the scroll amount for a "Page"
            // If one poster + margin is 210, scrolling 4 at a time means 210 * 4 = 840
            int scrollAmount = 840;

            // 2. Move the track to the left
            movieTrackPanel.Left -= scrollAmount;

            // 3. Stop perfectly at the end of the list
            // Make sure 'viewportPanel' is the actual name of your outer panel!
            int maxScrollLimit = viewportPanel.Width - movieTrackPanel.Width;
            if (movieTrackPanel.Left < maxScrollLimit)
            {
                movieTrackPanel.Left = maxScrollLimit;
            }
        }

        private void FadingImageButton1_Click(object sender, EventArgs e)
        {
            // Make sure this matches the number from your Right button!
            // 1. Set the exact same scroll amount as the Right button
            int scrollAmount = 840;

            // 2. Move the track to the right
            movieTrackPanel.Left += scrollAmount;

            // 3. Stop perfectly at the beginning of the list
            if (movieTrackPanel.Left > 0)
            {
                movieTrackPanel.Left = 0;
            }
        }
    }
}
