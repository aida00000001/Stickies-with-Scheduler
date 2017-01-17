using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace SwS
{
    public partial class frmCommandlineLauncher : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();

        public static DBManager.Launcher.LauncherList launcherList = new DBManager.Launcher.LauncherList();

        private Point mousePoint;

        public frmCommandlineLauncher()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ランチャメニュー表示
        /// </summary>
        public void showLauncherMenu()
        {
            // SQL発行後の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // ランチャ設定
            readLauncherData();

            this.txtCommandline.Focus();
            this.txtCommandline.SelectAll();
            this.Show();
            this.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            this.Activate();
        }

        private void frmCommandlineLauncher_Load(object sender, EventArgs e)
        {
            // 表示制御
            // label1の親コントロールをpictureBox1とする
            pictureBox1.Controls.Add(label1);
            // Label1の位置をPictureBox1内の位置に変更する
            //label1.Top = label1.Top - pictureBox1.Top;
            //label1.Left = label1.Left - pictureBox1.Left;
        }

        /// <summary>
        /// 各種データ読込
        /// </summary>
        public void readLauncherData()
        {
            launcherList.Clear();

            // SELECT文の作成
            string strSQL = "select * from launcher_list order by launcher_order";

            SQLiteDataReader result = sqliteAccess.select(strSQL);
            if (result != null)
            {
                while (result.Read())
                {
                    DBManager.Launcher.data data = new DBManager.Launcher.data();

                    data.id = result.GetInt32((Int32)DBManager.Launcher.enum_data.id);
                    data.launcher_order = result.GetInt32((Int32)DBManager.Launcher.enum_data.launcher_order);
                    data.launcher_name = result.GetString((Int32)DBManager.Launcher.enum_data.launcher_name);
                    data.launcher_path = result.GetString((Int32)DBManager.Launcher.enum_data.launcher_path);
                    data.launcher_parameter = result.GetString((Int32)DBManager.Launcher.enum_data.launcher_parameter);
                    data.launcher_type = result.GetBoolean((Int32)DBManager.Launcher.enum_data.launcher_type);
                    data.launcher_parent_node = result.GetInt32((Int32)DBManager.Launcher.enum_data.launcher_parent_node);

                    txtCommandline.AutoCompleteCustomSource.Add(data.launcher_name);

                    // データ格納
                    launcherList.Add(data);
                }
            }
            // リーダクローズ
            sqliteAccess.readerClose();
        }

        /// <summary>
        /// キーダウン処理(Enter,Escapeキー処理用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCommandline_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String cl = txtCommandline.Text;
                if (cl.Length != 0)
                {
                    lock (launcherList)
                    {
                        // ランチャメニューリストから対象付箋データ検索
                        DBManager.Launcher.data data = launcherList.SelectName(txtCommandline.Text);
                        if (data != null)
                        {
                            // 定義内容に従い実行
                            string execCmd = data.launcher_path;
                            // システム標準のブラウザ起動
                            if (data.launcher_parameter == null || "".Equals(data.launcher_parameter.Trim()))
                            {
                                Process.Start(execCmd);
                            }
                            else
                            {
                                Process.Start(execCmd, "\"" + data.launcher_parameter + "\"");
                            }
                            this.Hide();
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// コマンドラインランチャ終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ulsbClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // 左ボタン押下時
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.SuspendLayout();

            // 左ボタン押下ドラッグ時
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                int moveX = e.X - mousePoint.X;
                int moveY = e.Y - mousePoint.Y;

                // デスクトップの四辺にスナップする場合
                if (DBManager.Setting.main_desktop_snapping == true)
                {
                    int h, w;
                    //ディスプレイの作業領域の高さ
                    h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
                    //ディスプレイの作業領域の幅
                    w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;
                    int band = DBManager.Setting.main_desktop_snapping_band;

                    if (this.Left + moveX >= 0 && this.Left + moveX <= band)
                    {
                        this.Left = 0;
                    }
                    else if (this.Left + this.Width + moveX <= w && this.Left + this.Width + moveX >= w - band)
                    {
                        this.Left = w - this.Width;
                    }
                    else
                    {
                        this.Left += moveX;
                    }
                    if (this.Top + moveY >= 0 && this.Top + moveY <= band)
                    {
                        this.Top = 0;
                    }
                    else if (this.Top + this.Height + moveY <= h && this.Top + this.Height + moveY >= h - band)
                    {
                        this.Top = h - this.Height;
                    }
                    else
                    {
                        this.Top += moveY;
                    }
                }
                else
                {
                    this.Left += moveX;
                    this.Top += moveY;
                }
            }

            this.ResumeLayout();
        }
    }
}
