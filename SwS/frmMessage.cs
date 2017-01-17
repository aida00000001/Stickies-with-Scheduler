using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SwS
{
    public partial class frmMessage : Form
    {
        public frmMessage()
        {
            InitializeComponent();
        }

        public void showWithMessage(string message, string title)
        {
            this.Text = title;

            int fontHeight = this.Font.Height;
            // Graphics オブジェクトの取得
            this.BackgroundImage = new Bitmap(this.Width - 40, this.Height - 40);
            Graphics grfx = Graphics.FromImage(this.BackgroundImage);

            Font font = this.Font;
            SolidBrush brushDG = new SolidBrush(Color.DarkGreen);
            grfx.DrawString(message, font, brushDG, 20, 20);
            SizeF sizef = grfx.MeasureString(message, this.Font);
            this.Size = new Size((int)sizef.Width + 40, (int)sizef.Height + 40);

            Pen penGreen = new Pen(Color.Green);
            Pen penBlack = new Pen(Color.Black);
            Pen penWhite = new Pen(Color.White);
            Pen penLightGreen = new Pen(Color.LightGreen);

            grfx.DrawLine(penGreen, 0, 0, this.Width - 1, 0);
            grfx.DrawLine(penBlack, this.Width - 1, 0, this.Width - 1, this.Height - 1);
            grfx.DrawLine(penBlack, this.Width - 1, this.Height - 1, 0, this.Height - 1);
            grfx.DrawLine(penGreen, 0, this.Height - 1, 0, 0);

            grfx.DrawLine(penWhite, 1, 1, this.Width - 2, 1);
            grfx.DrawLine(penGreen, this.Width - 2, 1, this.Width - 2, this.Height - 2);
            grfx.DrawLine(penGreen, this.Width - 2, this.Height - 2, 1, this.Height - 2);
            grfx.DrawLine(penWhite, 1, this.Height - 2, 1, 1);

            grfx.DrawLine(penLightGreen, 2, 2, this.Width - 3, 2);
            grfx.DrawLine(penLightGreen, this.Width - 3, 2, this.Width - 3, this.Height - 3);
            grfx.DrawLine(penLightGreen, this.Width - 3, this.Height - 3, 2, this.Height - 3);
            grfx.DrawLine(penLightGreen, 2, this.Height - 3, 2, 2);

            grfx.DrawLine(penGreen, 3, 3, this.Width - 4, 3);
            grfx.DrawLine(penWhite, this.Width - 4, 3, this.Width - 4, this.Height - 4);
            grfx.DrawLine(penWhite, this.Width - 4, this.Height - 4, 3, this.Height - 4);
            grfx.DrawLine(penGreen, 3, this.Height - 4, 3, 3);

            grfx.DrawLine(penBlack, 4, 4, this.Width - 5, 4);
            grfx.DrawLine(penGreen, this.Width - 5, 4, this.Width - 5, this.Height - 5);
            grfx.DrawLine(penGreen, this.Width - 5, this.Height - 5, 4, this.Height - 5);
            grfx.DrawLine(penBlack, 4, this.Height - 5, 4, 4);

            this.Show();
        }

        private void frmMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
