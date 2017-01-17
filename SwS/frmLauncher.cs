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
    public partial class frmLauncher : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();

        public static DBManager.Launcher.LauncherList launcherList = new DBManager.Launcher.LauncherList();

        private bool bLoaded = false;

        public frmLauncher()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ランチャ隠し画面ロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLauncher_Load(object sender, EventArgs e)
        {
            // SQL発行後の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // ランチャ設定
            readLauncherData();

            // ホットキーの登録
            RegisterHotKey();

            this.Show();
        }

        /// <summary>
        /// Window Process override
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // ホットキー制御
            if (m.Msg == Win32APIs.HotKey.WM_HOTKEY)
            {
                switch ((int)m.WParam)
                {
                    case Win32APIs.HotKey.HOTKEY_ID_0000:
                        /* ホットキーの処理 */
                        this.Activate();
                        cmsLaunchMenu.Show(Control.MousePosition);
                        this.Activate();
                        if (!bLoaded)
                        {
                            // 初回起動がうまくいかないので無理やりかけてみる
                            cmsLaunchMenu.Show(Control.MousePosition);
                        }
                        break;
                    case Win32APIs.HotKey.HOTKEY_ID_0001:
                        /* ホットキーの処理 */
                        break;
                }
            }
        }

        /// <summary>
        /// ランチャメニュー表示
        /// </summary>
        public void showLauncherMenu()
        {
            this.Activate();
            cmsLaunchMenu.Show(Control.MousePosition);
            this.Activate();
        }

        /// <summary>
        /// ランチャ隠し画面アンロード前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLauncher_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 終了理由が×ボタン押下のとき(正しくはユーザ操作による終了処理)
            // (タスクトレイアイコン右クリックでの終了処理の場合は通常処理を行う)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 本来の終了処理ではなくフォーム非表示とする
                e.Cancel = true;                // 終了処理キャンセル
                this.Visible = false;           // フォーム非表示
            }

            // ホットキーの登録抹消
            UnregisterHotKey();

            // SQL終了処理
            sqliteAccess.disposeEnviroment();
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

                    // データ格納
                    launcherList.Add(data);
                }
            }
            // リーダクローズ
            sqliteAccess.readerClose();
        }

        /// <summary>
        /// ランチャメニュー消去処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLauncher_Deactivate(object sender, EventArgs e)
        {
            cmsLaunchMenu.Close();
        }

        /// <summary>
        /// ランチャメニュー消去処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLauncher_Leave(object sender, EventArgs e)
        {
            cmsLaunchMenu.Close();
        }

        /// <summary>
        /// ランチャメニュー表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsLaunchMenu_Opening(object sender, CancelEventArgs e)
        {
            // 初期化
            cmsLaunchMenu.Items.Clear();
            readLauncherData();

            // ランチャメニュー構造作成
            lock (launcherList)
            {
                foreach (DBManager.Launcher.data data in launcherList.SelectChild(0))
                {
                    // 対象が実行対象なら
                    if (data.launcher_type == true)
                    {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem();

                        tsmi.Text = data.launcher_name;
                        tsmi.Tag = data.id;
                        tsmi.Click += cmsLaunchMenu_Click;
                        cmsLaunchMenu.Items.Add(tsmi);
                    }
                    // ランチャメニュー構造的意味合いでフォルダなら
                    else
                    {
                        ToolStripDropDownItem tsddi = new ToolStripMenuItem();

                        tsddi.Text = data.launcher_name;
                        tsddi.Tag = data.id;
                        cmsLaunchMenu.Items.Add(tsddi);
                        // ランチャメニュー構造　子要素構築
                        recallMakeMenuStructure(launcherList.SelectChild(data.id), tsddi);
                    }
                }
            }
        }

        /// <summary>
        /// 再起呼び出し形式　ランチャメニュー構造　子要素構築
        /// </summary>
        /// <param name="launcherList"></param>
        /// <param name="parentTsddi"></param>
        private void recallMakeMenuStructure(DBManager.Launcher.LauncherList launcherDirList, ToolStripDropDownItem parentTsddi)
        {
            // ランチャメニュー構造作成
            lock (launcherList)
            {
                foreach (DBManager.Launcher.data data in launcherDirList)
                {
                    // 対象が実行対象なら
                    if (data.launcher_type == true)
                    {
                        ToolStripMenuItem tsmi = new ToolStripMenuItem();

                        tsmi.Text = data.launcher_name;
                        tsmi.Tag = data.id;
                        tsmi.Click += cmsLaunchMenu_Click;
                        parentTsddi.DropDownItems.Add(tsmi);
                    }
                    // ランチャメニュー構造的意味合いでフォルダなら
                    else
                    {
                        ToolStripDropDownItem tsddi = new ToolStripMenuItem();

                        tsddi.Text = data.launcher_name;
                        tsddi.Tag = data.id;
                        parentTsddi.DropDownItems.Add(tsddi);
                        // ランチャメニュー構造　子要素構築
                        recallMakeMenuStructure(launcherList.SelectChild(data.id), tsddi);
                    }
                }
            }
        }

        private void cmsLaunchMenu_Click(object sender, EventArgs e)
        {
            // イベントが起きたコントロールは sender 引数から取得する
            ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);

            if (tsmi != null)
            {
                lock (launcherList)
                {
                    // ランチャメニューリストから対象付箋データ検索
                    DBManager.Launcher.data data = launcherList.SelectId((int)tsmi.Tag);
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
                    }
                }
            }
        }

        /// <summary>
        /// ホットキー登録
        /// </summary>
        public void RegisterHotKey()
        {
            // ランチャメニュー
            if (!DBManager.Setting.main_hotkey_000.Equals(""))
            {
                Win32APIs.HotKey.RegisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0000, Win32APIs.HotKey.GetControlKey(DBManager.Setting.main_hotkey_000), Win32APIs.HotKey.GetNormalKey(DBManager.Setting.main_hotkey_000));
            }
        }

        /// <summary>
        /// ホットキー解除
        /// </summary>
        public void UnregisterHotKey()
        {
            // ランチャメニュー
            Win32APIs.HotKey.UnregisterHotKey(this.Handle, Win32APIs.HotKey.HOTKEY_ID_0000);
        }
    }
}
