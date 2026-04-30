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
    public partial class CinemasForm : Form
    {
        public CinemasForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainDashBoard mainDashBoard = new MainDashBoard();

            mainDashBoard.Show();

            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoviesForm moviesForm = new MoviesForm();

            moviesForm.Show();

            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShopForm shopForm = new ShopForm();

            shopForm.Show();

            this.Hide();
        }
    }
}
