using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tsumugi
{

    public partial class frmTaskTray : Form
    {
        frmCalendar frm = null;

        public frmTaskTray()
        {
            InitializeComponent();
        }

        private void tTimer_Tick(object sender, EventArgs e)
        {

        }

        // フォーム非表示タスクトレイ起動処理
        private void frmTaskTray_Load(object sender, EventArgs e)
        {
            DateTime newTime = DateTime.MaxValue;
            this.niTsumugi.ShowBalloonTip(500);
            this.Hide();
            NtpClient ntp = new NtpClient("time.windows.com");
            Console.WriteLine("現在時刻: " + ntp.GetCurrentTime(ref newTime));
            Win32APIs.SysDateTime.SetNowDateTime(newTime);

        }

        // 終了処理
        private void termToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // タスクトレイからアイコンを取り除く
            this.niTsumugi.Visible = false;
            // アプリケーション終了
            Application.Exit();
        }

        // カレンダ画面起動処理
        private void niTsumugi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //フォーム(frm)のインスタンスを作成
            //frmをモードレスで表示する
            if (frm == null)
            {
                frm = new frmCalendar();
                frm.Show();
            }
        }
    }
}
