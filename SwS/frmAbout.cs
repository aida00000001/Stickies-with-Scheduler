using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SwS
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        public void showDialog()
        {
            if (this.Visible == true)
            {
                this.Visible = false;
            }
            this.ShowDialog();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            // バージョンセット
            lblVers.Text = DBManager.Setting.version;

            // 更新履歴読み込み
            string strHistory = "";

            using (StreamReader sr = new StreamReader("ReleaseNote.txt", Encoding.GetEncoding("Shift_JIS")))
            {
                strHistory = sr.ReadToEnd();
            }

            textBox1.Text = strHistory;
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }
    }
}
