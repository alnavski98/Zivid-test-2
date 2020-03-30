using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace zivid_test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void show_error_picture_Click(object sender, EventArgs e)
        {
            
        }

        public void displayPicture(Bitmap picture)
        {
            show_error_picture.Image = picture;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            show_error_picture.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}