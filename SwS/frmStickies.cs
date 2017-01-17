using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SwS
{
    public partial class frmStickies : Form
    {
        // スクロール処理実現用
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        internal static extern int
            SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        private const int EM_LINESCROLL = 0xB6;
        
        private Point mousePoint;
        private DBManager.Contents.data stickyData;
        private SQLiteAccess sqliteAccess = new SQLiteAccess();
        private Color transparencyKey = Color.White;

        int iHitFlag = 0;
        private bool bIconSelect = false;
        private bool bDragMode = false;
        private bool bEditMode = false;
        private bool bEdited = false;
        private Int32 displayRow = 0;
        private Int32 displayHeight = 0;
        private double fontHeight = 0;
        private DateTime dtCheckTime = DateTime.Now;

        public frmStickies()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 定義済み付箋表示
        /// </summary>
        /// <param name="data"></param>
        public void showSticky(ref DBManager.Contents.data data)
        {
            llblDisplay.TabStop = false;

            stickyData = data;

            transparencyKey = this.TransparencyKey;

            // 新規付箋かどうか
            if (stickyData.id == -1)
            {
                bEditMode = true;
                string[] rgb = stickyData.backcolor_stickies.Split(',');
                Color backColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
                // 背景色設定
                this.BackColor = backColor;
            }
            else
            {
                bEditMode = false;
                // 位置と大きさを設定
                string[] pos_size = stickyData.point_stickies.Split(',');
                this.StartPosition = FormStartPosition.Manual;
                int x = int.Parse(pos_size[0]);
                int y = int.Parse(pos_size[1]);
                this.Location = new Point(x, y);
                int width = int.Parse(pos_size[2]);
                int height = int.Parse(pos_size[3]);
                this.Size = new Size(width, height);
                this.llblDisplay.Font = stickyData.display_font_stickies;
            }

            // テキストボックススクロールイベント追記
            txtSticky.MouseWheel += new MouseEventHandler(txtSticky_MouseWheel);

            // ラベル調整用フォント高さ
            fontHeight = llblDisplay.Font.GetHeight();

            // 背景関連設定
            setBackground();
        }

        /// <summary>
        /// テキストボックススクロール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSticky_MouseWheel(object sender, MouseEventArgs e)
        {
            // こちらでもスクロールイベントは記述可能だが、OnMouseWheelに統一して記述することとする
            // if (bEditMode == true)
            // {
            //     SendMessage(txtSticky.Handle.ToInt32(), EM_LINESCROLL, 0, (e.Delta * -1 / 120));
            // }
            OnMouseWheel(e);
        }

        /// <summary>
        /// フォーム表示時一回のみ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_Shown(object sender, EventArgs e)
        {
            // SQL発行後の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // コンテンツ設定
            this.Text = stickyData.contents;
            txtSticky.Text = stickyData.contents;

            // 新規付箋かどうか
            if (stickyData.id == -1)
            {
                this.Text = "新規付箋";
                stickyData.point_stickies = this.Location.X + ", " + this.Location.Y + ", " + this.Size.Width + ", " + this.Size.Height;
                llblDisplay.ContextMenuStrip.Enabled = false;

                stickyData.contents = "";
                this.WindowState = FormWindowState.Normal;
                this.Visible = true;
                txtSticky.Visible = true;
                this.ActiveControl = txtSticky;
            }
            else
            {
                setLinkLabel(stickyData.contents);

                this.WindowState = FormWindowState.Normal;
                this.Visible = true;
            }
            displayHeight = calcDisplayHeight();
        }

        /// <summary>
        /// 表示用ラベル高さ算出
        /// </summary>
        /// <returns></returns>
        private Int32 calcDisplayHeight()
        {
            Int32 height = 0;

            Graphics g = llblDisplay.CreateGraphics();
            SizeF size = g.MeasureString(llblDisplay.Text, llblDisplay.Font, llblDisplay.Size.Width - (llblDisplay.Padding.Top * 2));

            height = (int)Math.Ceiling(size.Height) + (llblDisplay.Padding.Top * 2);

            //height = ((Int32)(fontHeight * (txtSticky.GetLineFromCharIndex(txtSticky.Text.Length - 1) + 1) + llblDisplay.Padding.Top * 2));

            return height;
        }
        
        /// <summary>
        /// フォームクローズ時の動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_FormClosing(object sender, FormClosingEventArgs e)
        {
            stickyData.displayed = false;
        }

        /// <summary>
        /// フォームリサイズ時の動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_Resize(object sender, EventArgs e)
        {
            this.Opacity = 1;

            llblDisplay.Size = new Size(this.Size.Width, this.Size.Height + (Int32)(fontHeight * (displayRow)));
            displayHeight = calcDisplayHeight();
            txtSticky.Size = new Size(this.Size.Width - 8, (this.Size.Height - 28));
            ulsbClose.Location = new Point(this.Width - 4 - ulsbClose.Size.Width, ulsbClose.Location.Y);
            stickyData.point_stickies = this.Location.X + ", " + this.Location.Y + ", " + this.Size.Width + ", " + this.Size.Height;

            this.Opacity = ((double)stickyData.alpha_stickies / 100);
            this.Refresh();

            bIconSelect = false;
            bDragMode = false;

            // 編集モードでなければ保存
            if (bEditMode == false && stickyData.id != -1)
            {
                // データベースに保存
                update();
            }
        }

        /// <summary>
        /// 付箋情報保存及び表示
        /// </summary>
        private void stickySaveAndDisplay()
        {
            // 編集ありなら登録処理実行
            if (bEdited == true)
            {
                stickyData.contents = txtSticky.Text;
                this.Text = txtSticky.Text;

                // 付箋データ保存
                if (stickyData.id == -1)
                {
                    // 新規登録
                    insert();
                    stickyData.displayed = true;
                }
                else
                {
                    // 修正登録
                    update();
                }
            }
            sqliteAccess.readerClose();

            txtSticky.Visible = false;

            // リンクラベルに編集内容設定
            setLinkLabel(stickyData.contents);
            displayHeight = calcDisplayHeight();
            displayRow = 0;
            llblDisplay.SetBounds(0, 0, this.Size.Width, this.Size.Height);
        }

        /// <summary>
        /// リンクラベルに編集内容設定
        /// (リンク情報作成)
        /// </summary>
        /// <param name="contents"></param>
        private void setLinkLabel(string contents)
        {
            llblDisplay.Text = escapeToLabel(contents);

            //HTTPアドレス取得用Regexオブジェクトを作成
            System.Text.RegularExpressions.Regex rHttp =
                new System.Text.RegularExpressions.Regex(
                    @"((http|https)://[\w,_,\.,/,\-,\?,=,#,\&,\%]+)",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //メールアドレス取得用Regexオブジェクトを作成
            System.Text.RegularExpressions.Regex rMail =
                new System.Text.RegularExpressions.Regex(
                    @"(mailto:[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4})",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //ローカルパス取得用Regexオブジェクトを作成
            //string path = "([a-z]:|\\\\|){1}[^ \t*?|:,;<>\"][^ \t*?|:,;<>\"]*(?=$)*)";
            // TODO 完璧じゃないけどねぇ～。。。
            System.Text.RegularExpressions.Regex rDirectory =
                new System.Text.RegularExpressions.Regex(
                    @"([a-z]:[\\])([^\t\f\r\n<>:" + "\"*?/|" + "]*)",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //ローカルパス取得用Regexオブジェクトを作成
            //string path = "([a-z]:|\\\\|){1}[^ \t*?|:,;<>\"][^ \t*?|:,;<>\"]*(?=$)*)";
            // TODO 完璧じゃないけどねぇ～。。。
            System.Text.RegularExpressions.Regex rDirectory2 =
                new System.Text.RegularExpressions.Regex(
                    @"([\\][\\])([^\t\f\r\n<>:" + "\"*?/|" + "]*)",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // リンクラベルのリンク領域クリア
            llblDisplay.Links.Clear();

            //テキストボックス内でHTTPアドレス正規表現と一致する対象を1つ検索
            System.Text.RegularExpressions.Match m = rHttp.Match(llblDisplay.Text);
            // テキストボックスの内容を正規表現にて検索しリンク作成
            regexSearchAndSet(ref m);

            //テキストボックス内でメールアドレス正規表現と一致する対象を1つ検索
            m = rMail.Match(llblDisplay.Text);
            // テキストボックスの内容を正規表現にて検索しリンク作成
            regexSearchAndSet(ref m);

            //テキストボックス内でローカルパス正規表現と一致する対象を1つ検索
            m = rDirectory2.Match(llblDisplay.Text);
            // テキストボックスの内容を正規表現にて検索しリンク作成
            regexSearchAndSet(ref m);
            
            //テキストボックス内でローカルパス正規表現と一致する対象を1つ検索
            m = rDirectory.Match(llblDisplay.Text);
            // テキストボックスの内容を正規表現にて検索しリンク作成
            regexSearchAndSet(ref m);
        }

        /// <summary>
        /// LinkLabel用エスケープ処理
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        private string escapeToLabel(string contents)
        {
            string buffer = "";
            buffer = contents.Replace("&", "&&");

            return buffer;
        }

        /// <summary>
        /// テキストボックスの内容を正規表現にて検索しリンク作成
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private int regexSearchAndSet(ref System.Text.RegularExpressions.Match m)
        {
            int index = 0;
            while (m.Success)
            {
                // リンクラベルにリンク設定
                index = llblDisplay.Text.IndexOf(m.Value, index);
                llblDisplay.Links.Add(index, m.Value.Length, m.Value);
                index = index + m.Value.Length;

                //次に一致する対象を検索
                m = m.NextMatch();
            }
            return index;
        }

        /// <summary>
        /// マウス押下イベントオーバーライド
        /// </summary>
        /// <param name="me"></param>
        protected override void OnMouseDown(MouseEventArgs me)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                if (me.Button == MouseButtons.Left)
                {
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        int iSendToMsg = 0;
                        iSendToMsg = RectHitTest(this.Size.Width, this.Size.Height, me.X, me.Y);
                        if (iSendToMsg != 0)
                        {
                            Win32APIs.Resize.ReleaseCaptureAPI();
                            Win32APIs.Resize.SendMessageAPI(this.Handle, Win32APIs.Resize.WM_NCLBUTTONDOWN, iSendToMsg, 0);
                        }
                        iHitFlag = iSendToMsg;
                    }
                }
            }

            if (iHitFlag == 0)
            {
                // 左ボタン押下時
                if ((me.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    //位置を記憶する
                    mousePoint = new Point(me.X, me.Y);
                }
            }

            base.OnMouseDown(me);
        }

        /// <summary>
        /// マウス移動イベントオーバーライド
        /// </summary>
        /// <param name="me"></param>
        protected override void OnMouseMove(MouseEventArgs me)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                int iSendToMsg = 0;
                iSendToMsg = RectHitTest(this.Size.Width, this.Size.Height, me.X, me.Y);
                switch (iSendToMsg)
                {
                    case Win32APIs.Resize.HTLEFT:
                    case Win32APIs.Resize.HTRIGHT:
                        this.Cursor = Cursors.SizeWE;
                        break;

                    case Win32APIs.Resize.HTTOP:
                    case Win32APIs.Resize.HTBOTTOM:
                        this.Cursor = Cursors.SizeNS;
                        break;

                    case Win32APIs.Resize.HTTOPLEFT:
                    case Win32APIs.Resize.HTBOTTOMRIGHT:
                        this.Cursor = Cursors.SizeNWSE;
                        break;

                    case Win32APIs.Resize.HTTOPRIGHT:
                    case Win32APIs.Resize.HTBOTTOMLEFT:
                        this.Cursor = Cursors.SizeNESW;
                        break;

                    default:
                        this.Cursor = Cursors.Default;
                        break;

                }
                iHitFlag = iSendToMsg;
            }

            // 左ボタン押下ドラッグ時
            if ((me.Button & MouseButtons.Left) == MouseButtons.Left && bDragMode == true && bIconSelect == false)
            {
                int moveX = me.X - mousePoint.X;
                int moveY = me.Y - mousePoint.Y;

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

            base.OnMouseMove(me);
        }

        /// <summary>
        /// マウス開放イベントオーバーライド
        /// </summary>
        /// <param name="me"></param>
        protected override void OnMouseUp(MouseEventArgs me)
        {
            if (bIconSelect == true)
            {
                bIconSelect = false;
                Win32APIs.ReleaseCapture();
                DesktopHook.ListViewAccessor.PostMessageMouseLUp(screenToDesktop(me));
            }
            bDragMode = false;

            base.OnMouseUp(me);
        }

        // Noneの場合、Borderはないが、0だとシビアなので、遊び幅設定
        private const int BORDER_WIDTH = 5;
        /// <summary>
        /// HitTestメソッド
        /// </summary>
        /// <param name="FormWidth"></param>
        /// <param name="FromHeight"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        private int RectHitTest(int FormWidth, int FromHeight, int X, int Y)
        {
            int retValue = 0;

            bool[] point = new bool[4];
            point[0] = X < BORDER_WIDTH;
            point[1] = Y < (fontHeight * (displayRow)) + BORDER_WIDTH;
            point[2] = X > FormWidth - BORDER_WIDTH - 2;
            point[3] = Y > (fontHeight * (displayRow)) + FromHeight - BORDER_WIDTH - 2;

            if ((point[0] == true) && ((point[1] == false) || (point[3] == false)))
            {
                retValue = Win32APIs.Resize.HTLEFT;
            }

            if ((point[1] == true) && ((point[0] == false) || (point[2] == false)))
            {
                retValue = Win32APIs.Resize.HTTOP;
            }

            if ((point[2] == true) && ((point[1] == false) || (point[3] == false)))
            {
                retValue = Win32APIs.Resize.HTRIGHT;
            }

            if ((point[3] == true) && ((point[0] == false) || (point[2] == false)))
            {
                retValue = Win32APIs.Resize.HTBOTTOM;
            }

            if ((point[0] == true) && (point[1] == true))
            {
                retValue = Win32APIs.Resize.HTTOPLEFT;
            }

            if ((point[0] == true) && (point[3] == true))
            {
                retValue = Win32APIs.Resize.HTBOTTOMLEFT;
            }

            if ((point[2] == true) && (point[1] == true))
            {
                retValue = Win32APIs.Resize.HTTOPRIGHT;
            }

            if ((point[2] == true) && (point[3] == true))
            {
                retValue = Win32APIs.Resize.HTBOTTOMRIGHT;
            }

            return retValue;
        }

        /// <summary>
        /// 付箋ダブルクリック時、編集状態へ遷移、再度ダブルクリックで表示状態へ遷移
        /// (デスクトップアイコン上の場合はアイコンを操作する)
        /// (現在はカスタムイベントから呼び出される形に変更)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 左クリックのみ
            if (e.Button == MouseButtons.Left)
            {
                this.SuspendLayout();
                if (bEditMode == false)
                {
                    // 透過処理中かつデスクトップアイコンにHITしたら透過アイコンへの処理を優先
                    if (desktopIconHitTest(e) == true)
                    {
                        // 透過アイコンへの処理
                        DesktopHook.ListViewAccessor.PostMessageMouseLDblClick(screenToDesktop(e));
                    }
                    else
                    {
                        // コピーモードでないとき
                        if (txtSticky.ReadOnly == false)
                        {
                            /// FIXME これでVista以降でクリップボードがつぶされる件解消できるか？の暫定対応
#if true
                            // 編集状態へ
                            bEditMode = true;
                            // ラベル非表示設定
                            llblDisplay.Text = "";
                            llblDisplay.ContextMenuStrip.Enabled = false;
                            // 非透過設定
                            this.Opacity = 1;
                            dtCheckTime = DateTime.Now;
                            this.Refresh();
                            // 編集領域設定
                            txtSticky.Visible = true;
                            this.ActiveControl = txtSticky;
                            txtSticky.BringToFront();
#else
                            // 編集状態へ
                            bEditMode = true;
                            // ラベル非表示設定
                            llblDisplay.Visible = false;
                            llblDisplay.ContextMenuStrip.Enabled = false;
                            // 非透過設定
                            this.Opacity = 1;
                            dtCheckTime = DateTime.Now;
                            this.Refresh();
                            // 編集領域設定
                            // 編集領域表示設定
                            txtSticky.Visible = true;
                            // ラベル非表示設定
                            llblDisplay.Visible = false;
                            txtSticky.Focus();
#endif
                        }
                    }
                }
                else
                {
                    // 全開と今回で内容が異なっていれば編集と判定し登録処理実行
                    if (!stickyData.contents.Equals(txtSticky.Text))
                    {
                        bEdited = true;
                    }
                    bEditMode = false;
                    llblDisplay.ContextMenuStrip.Enabled = true;
                    // ラベル表示設定
                    //llblDisplay.Visible = true;
                    // 付箋情報保存及び表示
                    stickySaveAndDisplay();
                }
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// 透過中のデスクトップアイコンHITTEST
        /// </summary>
        /// <returns></returns>
        private bool desktopIconHitTest(MouseEventArgs e)
        {
            // 透過　かつ　編集中でなく　かつ　デスクトップアイコンにHIT
            return (DBManager.Setting.main_desktop_access == true
                    && stickyData.alpha_stickies != 100
                    && bEditMode == false
                    && DesktopHook.ListViewAccessor.HitTest(screenToDesktop(e))
                    == true);
        }

        /// <summary>
        /// LinkLabelクリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            // 左クリックのみ
            if (e.Button == MouseButtons.Left)
            {
                this.SuspendLayout();
                if (bEditMode == false)
                {
                    // 透過処理中かつデスクトップアイコンにHITしたら透過アイコンへの処理を優先
                    if (desktopIconHitTest(e) == true)
                    {
                        // 透過アイコンへの処理
                        DesktopHook.ListViewAccessor.PostMessageMouseLDblClick(screenToDesktop(e));
                    }
                    else
                    {
                        // 半透明処理中かつコピーモードでないとき
//                        if (DBManager.Setting.main_desktop_access == true
                        if (stickyData.alpha_stickies != 100
                            && bEditMode == false
                            && txtSticky.ReadOnly == false)
                        {
                            // 非透過処理用意
                            this.Opacity = 1;
                            dtCheckTime = DateTime.Now.AddSeconds(5);   // 最終作業以降５秒後に透過設定復帰
                            tTimer.Enabled = true;
                        }
                    }
                }
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// ウィンドウからデスクトップ座標へ変換
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Point screenToDesktop(MouseEventArgs e)
        {
            Point cursorPos = new Point();

            cursorPos.X = this.Left + e.X;
            cursorPos.Y = this.Top + e.Y + llblDisplay.Top;

            return cursorPos;
        }

        private void llblDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            // 左クリックのみ
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                // 透過処理中かつデスクトップアイコンにHITしたら
                if (desktopIconHitTest(e) == true)
                {
                    // デスクトップアイコン選択
                    if (bIconSelect == false)
                    {
                        bIconSelect = true;
                        DesktopHook.ListViewAccessor.PostMessageMouseLDown(screenToDesktop(e));
                        Win32APIs.SetCapture(this.Handle);
                    }
                }
                else
                {
                    bIconSelect = false;
                    bDragMode = true;
                    OnMouseDown(e);
                }
            }
            // 右クリックのみ
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                // 透過処理中かつデスクトップアイコンにHITしたら
                if (desktopIconHitTest(e) == true)
                {
                    // デスクトップアイコンコンテキストメニュー表示
                    if (bIconSelect == false)
                    {
                        bIconSelect = true;
                        DesktopHook.ListViewAccessor.PostMessageMouseRDown(screenToDesktop(e));
                        Win32APIs.SetCapture(this.Handle);
                    }
                }
            }

        }

        private void llblDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ulsbClose.Visible == false)
            {
                this.ulsbClose.Visible = true;
            }

            // drag中でなく透過処理中かつデスクトップアイコンにHITしたら
            if (bDragMode == false && desktopIconHitTest(e) == true)
            {
                ttTips.SetToolTip(llblDisplay, DesktopHook.ListViewAccessor.GetIconText(screenToDesktop(e)));
                this.ContextMenuStrip = null;
                llblDisplay.ContextMenuStrip = null;
            }
            else
            {
                this.ContextMenuStrip = cmsMenu;
                llblDisplay.ContextMenuStrip = cmsMenu;
                ttTips.RemoveAll();
            }
            if (bDragMode == true || bIconSelect == false)
            {
                if (txtSticky.ReadOnly == true)
                {
                    this.ActiveControl = llblDisplay;
                }
                OnMouseMove(e);
            }
        }

        private void llblDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            stickyData.point_stickies = this.Location.X + ", " + this.Location.Y + ", " + this.Size.Width + ", " + this.Size.Height;

            if (bIconSelect == true)
            {
                bIconSelect = false;
                Win32APIs.ReleaseCapture();
                DesktopHook.ListViewAccessor.PostMessageMouseLUp(screenToDesktop(e));
            }
            bDragMode = false;

            // データベースに保存
            update();
        }

        /// <summary>
        /// SQL接続(insert)
        /// </summary>
        private void insert()
        {
            FontConverter fc = new FontConverter();

            string strSQL = "insert into contents_list ("
                + "data_type"
                + ", date_time"
                + ", contents"
                + ", show_stickies"
                + ", backcolor_stickies"
                + ", point_stickies"
                + ", time_trigger"
                + ", time_trigger_type"
                + ", time_trigger_datetime"
                + ", time_trigger_dayofweek"
                + ", attach_trigger"
                + ", attach_trigger_title"
                + ", display_backimage_stickies"
                + ", display_backimage_type_stickies"
                + ", display_backimage_path_stickies"
                + ", display_backimage_region_stickies"
                + ", alpha_stickies"
                + ", task_to_calendar"
                + ", task_trigger_type"
                + ", task_trigger_datetime"
                + ", task_trigger_dayofweek"
                + ", task_trigger_span"
                + ", task_range"
                + ", task_range_startdate"
                + ", task_range_enddate"
                + ", task_type"
                + ", display_fontcolor_stickies"
                + ", display_font_stickies"
                + ") values ("
                + "3"
                + ", datetime('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", '" + stickyData.contents + "'"
                + ", 1"
                + ", '" + stickyData.backcolor_stickies + "'"
                + ", '" + stickyData.point_stickies + "'"
                + ", " + (stickyData.time_trigger == true ? 1 : 0)
                + ", " + stickyData.time_trigger_type
                + ", datetime('" + stickyData.time_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", " + stickyData.time_trigger_dayofweek
                + ", " + (stickyData.attach_trigger == true ? 1 : 0)
                + ", '" + stickyData.attach_trigger_title + "'"
                + ", " + (stickyData.display_backimage_stickies == true ? 1 : 0)
                + ", " + stickyData.display_backimage_type_stickies
                + ", '" + stickyData.display_backimage_path_stickies + "'"
                + ", " + (stickyData.display_backimage_region_stickies == true ? 1 : 0)
                + ", " + stickyData.alpha_stickies + ""
                + ", " + ((stickyData.task_to_calendar == true) ? 1 : 0)
                + ", " + stickyData.task_trigger_type
                + ", datetime('" + stickyData.task_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", " + stickyData.task_trigger_dayofweek
                + ", " + stickyData.task_trigger_span
                + ", " + ((stickyData.task_range == true) ? 1 : 0)
                + ", datetime('" + stickyData.task_range_startdate.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", datetime('" + stickyData.task_range_enddate.ToString("yyyy-MM-dd 00:00:00") + "')"
                + ", " + stickyData.task_type
                + ", ''"
                + ", '" + fc.ConvertToString(stickyData.display_font_stickies) + "'"
                + ")";

            stickyData.id = sqliteAccess.insert(strSQL, "contents_list");
        }

        /// <summary>
        /// SQL接続(update)
        /// </summary>
        private void update()
        {
            FontConverter fc = new FontConverter();

            string strSQL = "update contents_list set "

                // 登録日は変更しないよう修正
                // (予定データの対象日にも使っているため。付箋側で初回登録時の日付を保持し続けても特に問題はない)
                // + "date_time = datetime('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                // + ", contents = '" + stickyData.contents + "'"   // こっちは先頭のカンマ保持のためコピーしたものをコメントアウト

                + "contents = '" + stickyData.contents + "'"
                + ", backcolor_stickies = '" + stickyData.backcolor_stickies + "'"
                + ", point_stickies = '" + stickyData.point_stickies + "'"
                + ", time_trigger = " + (stickyData.time_trigger == true ? 1 : 0)
                + ", time_trigger_type = " + stickyData.time_trigger_type
                + ", time_trigger_datetime = datetime('" + stickyData.time_trigger_datetime.ToString("yyyy-MM-dd HH:mm:ss") + "')"
                + ", time_trigger_dayofweek = " + stickyData.time_trigger_dayofweek
                + ", attach_trigger = " + (stickyData.attach_trigger == true ? 1 : 0)
                + ", attach_trigger_title = '" + stickyData.attach_trigger_title + "'"
                + ", display_backimage_stickies = " + (stickyData.display_backimage_stickies == true ? 1 : 0)
                + ", display_backimage_type_stickies = " + stickyData.display_backimage_type_stickies
                + ", display_backimage_path_stickies = '" + stickyData.display_backimage_path_stickies + "'"
                + ", display_backimage_region_stickies = " + (stickyData.display_backimage_region_stickies == true ? 1 : 0)
                + ", alpha_stickies = " + stickyData.alpha_stickies
                + ", display_fontcolor_stickies = '" + "'"
                + ", display_font_stickies = '" + fc.ConvertToString(stickyData.display_font_stickies) + "'"
                + " where id = '" + stickyData.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(delete)
        /// </summary>
        private void delete()
        {
            string strSQL = "update contents_list set "
                + "effective = 0"
                + " where id = '" + stickyData.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(destroy)
        /// </summary>
        private void destroy()
        {
            string strSQL = "delete from contents_list"
                + " where id = '" + stickyData.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// 付箋個別設定ダイアログ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stickiesSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 編集モードでない場合
            if (bEditMode == false)
            {
                frmStickiesSetting frm = new frmStickiesSetting();
                // 付箋個別設定ダイアログ実行後の戻り値がtrue(save)なら
                if (frm.showDialog(ref stickyData) == true)
                {
                    // フォント設定
                    llblDisplay.Font = stickyData.display_font_stickies;
                    fontHeight = llblDisplay.Font.GetHeight();
                    displayHeight = calcDisplayHeight();
                    // 付箋情報保存及び表示
                    bEdited = true;
                    stickySaveAndDisplay();
                    // 背景関連設定
                    setBackground();
                }
            }
        }

        /// <summary>
        /// 背景関連設定
        /// </summary>
        private void setBackground()
        {
            // 背景画像が指定されている場合は合成
            if (stickyData.display_backimage_stickies == true)
            {
                Bitmap bmp = new Bitmap(@stickyData.display_backimage_path_stickies);

                // 透明にする色
                Color backColor;

                // リージョン指定があるならリンクラベル消去
                if (stickyData.display_backimage_region_stickies)
                {
                    llblDisplay.Enabled = false;
                    llblDisplay.Visible = false;
                    // 型紙の透明色を設定
                    // 透明にする色
                    backColor = bmp.GetPixel(0, 0);
                    // 画像を透明にする
                    bmp.MakeTransparent(backColor);
                    // 透明を指定する
                    this.TransparencyKey = backColor;
                    // 背景色設定
                    this.BackColor = backColor;
                }
                else
                {
                    llblDisplay.Enabled = true;
                    llblDisplay.Visible = true;
                    // 透明を指定する
                    this.TransparencyKey = transparencyKey;
                    // 背景色設定
                    string[] rgb = stickyData.backcolor_stickies.Split(',');
                    backColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
                }
                // 背景画像を指定する
                this.BackgroundImage = bmp;
                // 背景色設定
                this.BackColor = backColor;
            }
            else
            {
                llblDisplay.Visible = true;
                // 背景画像クリア
                this.BackgroundImage = null;
                // 背景色設定
                string[] rgb = stickyData.backcolor_stickies.Split(',');
                this.BackColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            }
            // 背景画像配置
            this.BackgroundImageLayout = converterLayout(stickyData.display_backimage_type_stickies);
            // 背景色設定
            ulsbClose.BackColor = this.BackColor;
            txtSticky.BackColor = this.BackColor;
            // 透過率設定
            this.Opacity = ((double)stickyData.alpha_stickies / 100);
            if (stickyData.displayed == true)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 背景画像の描画設定内容をImageLayout型に変換し返却
        /// </summary>
        /// <param name="layoutIndex"></param>
        /// <returns></returns>
        private ImageLayout converterLayout(Int32 layoutIndex)
        {
            switch (layoutIndex)
            {
                case 0:
                    return ImageLayout.None;
                case 1:
                    return ImageLayout.Tile;
                case 2:
                    return ImageLayout.Center;
                case 3:
                    return ImageLayout.Stretch;
                case 4:
                    return ImageLayout.Zoom;
            }
            return ImageLayout.None;
        }

        /// <summary>
        /// 付箋を閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stickiesCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stickyData.displayed = false;
            this.Close();
        }

        /// <summary>
        /// 付箋解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stickiesDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 付箋解除
            stickiesDelete();
        }

        /// <summary>
        /// 付箋・内容共に破棄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stickiesDestroyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 付箋解除
            stickiesDestroy();
        }

        /// <summary>
        /// 付箋解除
        /// </summary>
        private void stickiesDelete()
        {
            if (MessageBox.Show("付箋を解除しますか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                stickyData.effective = false;
                delete();
                this.Close();
            }
        }

        /// <summary>
        /// 付箋・内容共に破棄
        /// </summary>
        private void stickiesDestroy()
        {
            if (MessageBox.Show("付箋・内容を共に破棄しますか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                stickyData.effective = false;
                destroy();
                this.Close();
            }
        }

        /// <summary>
        /// 付箋解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ulsbClose_MouseUp(object sender, MouseEventArgs e)
        {
            // 付箋解除
            stickiesDelete();
        }

        /// <summary>
        /// 付箋透過率設定(100%)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphaSetting100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 透過率設定
            setAlpha(100);
        }

        /// <summary>
        /// 付箋透過率設定(80%)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphaSetting080ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 透過率設定
            setAlpha(80);
        }

        /// <summary>
        /// 付箋透過率設定(60%)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphaSetting060ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 透過率設定
            setAlpha(60);
        }

        /// <summary>
        /// 付箋透過率設定(50%)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphaSetting050ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 透過率設定
            setAlpha(50);
        }

        /// <summary>
        /// 付箋透過率設定(40%)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphaSetting040ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 透過率設定
            setAlpha(40);
        }

        /// <summary>
        /// 付箋透過率設定(20%)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphaSetting020ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 透過率設定
            setAlpha(20);
        }

        /// <summary>
        /// 付箋透過率設定
        /// </summary>
        /// <param name="alpha"></param>
        private void setAlpha(Int32 alpha)
        {
            stickyData.alpha_stickies = alpha;
            this.Opacity = ((double)stickyData.alpha_stickies / 100);
            bEdited = true;
            // 付箋情報保存及び表示
            stickySaveAndDisplay();
            this.Show();
        }

        /// <summary>
        /// 背景画像配置方法設定(なし)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutSettingNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLayout(0);
        }

        /// <summary>
        /// 背景画像配置方法設定(並べて表示)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutSettingTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLayout(1);
        }

        /// <summary>
        /// 背景画像配置方法設定(中央に表示)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutSettingCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLayout(2);
        }

        /// <summary>
        /// 背景画像配置方法設定(併せて表示)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutSettingStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLayout(3);
        }

        /// <summary>
        /// 背景画像配置方法設定(同比率拡縮)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutSettingZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setLayout(4);
        }

        /// <summary>
        /// 背景画像配置方法設定
        /// </summary>
        /// <param name="type"></param>
        private void setLayout(Int32 type)
        {
            // 背景画像配置
            stickyData.display_backimage_type_stickies = type;
            this.BackgroundImageLayout = converterLayout(stickyData.display_backimage_type_stickies);
            bEdited = true;
            // 付箋情報保存及び表示
            stickySaveAndDisplay();
            this.Show();
        }

        /// <summary>
        /// 最前面処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = topMostToolStripMenuItem.Checked == true ? false : true;
        }

        /// <summary>
        /// コンテキストメニューオープン時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsMenu_Opening(object sender, CancelEventArgs e)
        {
            // 最前面
            topMostToolStripMenuItem.Checked = this.TopMost;
        }

        /// <summary>
        /// リンククリック時動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblDisplay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string startArgs = e.Link.LinkData.ToString();

            try
            {
                if (startArgs.Contains("http://") == true)
                {
                    // システム標準のブラウザ起動
                    Process.Start(startArgs);
                }
                else if (startArgs.Contains("@") == true)
                {
                    if (startArgs.Contains("mailto:") == false)
                    {
                        startArgs = "mailto:" + startArgs;
                    }
                    // システム標準のメーラ起動
                    Process.Start(startArgs);
                }
                else if (startArgs.Contains("\\\\") == true)
                {
                    // システム標準のブラウザ起動
                    Process.Start("EXPLORER.EXE", startArgs);
                }
                else
                {
                    // システム標準のブラウザ起動
                    Process.Start(startArgs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// ショートカットキー追加(ctrl+s, ctrl+a)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSticky_KeyDown(object sender, KeyEventArgs e)
        {
            if (bEditMode == true)
            {
                // ctrl+s 実現
                if (e.Control && e.KeyCode.ToString().Equals("S"))
                {
                    // 全開と今回で内容が異なっていれば編集と判定し登録処理実行
                    if (!stickyData.contents.Equals(txtSticky.Text))
                    {
                        bEdited = true;
                    }
                    bEditMode = false;
                    llblDisplay.ContextMenuStrip.Enabled = true;
                    // ラベル表示設定
                    llblDisplay.Visible = true;
                    // 付箋情報保存及び表示
                    stickySaveAndDisplay();
                }
                // ctrl+a 実現
                if (e.Control && e.KeyCode.ToString().Equals("A"))
                {
                    txtSticky.SelectAll();
                }
            }
        }

        /// <summary>
        /// テキストボックスがアクティブコントロールでなくなったら
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSticky_Leave(object sender, EventArgs e)
        {
            leaveTxtSticky();
        }

        /// <summary>
        /// テキストボックス非アクティブ化
        /// </summary>
        private void leaveTxtSticky()
        {
            // リードオンリーのとき(コピーモードのとき)
            if (txtSticky.ReadOnly == true)
            {
                txtSticky.ReadOnly = false;
                txtSticky.Visible = false;
                txtSticky.BorderStyle = BorderStyle.FixedSingle;
                if (this.ContextMenuStrip != null)
                {
                    llblDisplay.ContextMenuStrip.Enabled = true;
                }
                this.ActiveControl = llblDisplay;
                // リンクラベルに編集内容設定
                setLinkLabel(stickyData.contents);
            }
            else if (bEditMode == true)
            {
                // 全開と今回で内容が異なっていれば編集と判定し登録処理実行
                if (!stickyData.contents.Equals(txtSticky.Text))
                {
                    bEdited = true;
                }
                bEditMode = false;
                if (this.ContextMenuStrip != null)
                {
                    llblDisplay.ContextMenuStrip.Enabled = true;
                }
                // ラベル表示設定
                llblDisplay.Visible = true;
                this.ActiveControl = llblDisplay;
                // 付箋情報保存及び表示
                stickySaveAndDisplay();
            }
        }

        /// <summary>
        /// スクロールイベント捕捉用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (bEditMode == false)
            {
                // リンクラベルスクロール用

                // Delta はホイールが1回カチッとなると、+120/-120という値になる。
                // よって、120で割ってあげれば解りやすい値になる。
                int direction = (e.Delta * -1 / 120);
                if ((displayRow + direction) >= 0)
                {
                    this.SuspendLayout();
                    displayRow = displayRow + direction;
                    if ((this.Size.Height + fontHeight * (displayRow)) <= displayHeight + fontHeight)
                    {
                        llblDisplay.SetBounds(0, (Int32)(fontHeight * displayRow * -1), 0, this.Size.Height + (Int32)(fontHeight * displayRow), BoundsSpecified.Y | BoundsSpecified.Height);
                    }
                    else
                    {
                        if (direction > 0)
                        {
                            displayRow = displayRow - direction;
                        }
                    }
                    dtCheckTime = DateTime.Now.AddSeconds(5);   // 最終作業以降５秒後に透過設定復帰
                    this.ResumeLayout();
                }
            }
            else
            {
                // テキストボックススクロールは専用イベントでトラップし、ここでスクロールを行うこととする
                // (専用イベント内でも記述できるけど、こちらに統一)
                SendMessage(txtSticky.Handle.ToInt32(), EM_LINESCROLL, 0, (e.Delta * -1 / 120));
            }
        }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 編集モードでない場合のみ処理
            if (bEditMode == false)
            {
                // シフトキー押下でコピーモードへ
                if (e.KeyCode == Keys.ShiftKey)
                {
                    llblDisplay.Text = "";
                    if (this.ContextMenuStrip != null)
                    {
                        llblDisplay.ContextMenuStrip.Enabled = false;
                    }
                    this.Refresh();
                    txtSticky.ReadOnly = true;
                    txtSticky.Visible = true;
                    txtSticky.BorderStyle = BorderStyle.None;
                    this.ActiveControl = txtSticky;
                }
                if (e.KeyCode == Keys.Escape)
                {
                    leaveTxtSticky();
                }
            }
        }

        /// <summary>
        /// フォームが非アクティブになったとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_Deactivate(object sender, EventArgs e)
        {
            leaveTxtSticky();
        }

        /// <summary>
        /// タイマ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tTimer_Tick(object sender, EventArgs e)
        {
            if (bEditMode == false)
            {
                if (dtCheckTime.CompareTo(DateTime.Now) < 0)
                {
                    tTimer.Enabled = false;
                    this.Opacity = ((double)stickyData.alpha_stickies / 100);
                }
            }
        }

        /// <summary>
        /// カレンダより強制移動させられた場合の保存処理
        /// </summary>
        public void saveForceMoved() {
            // 位置変更
            stickyData.point_stickies = this.Location.X + ", " + this.Location.Y + ", " + this.Size.Width + ", " + this.Size.Height;
            // データベースに保存
            update();
        }

        /// <summary>
        /// FIXME これでVista以降でクリップボードがつぶされる件解消できるか？の暫定対応
        /// フォームダブルクリックで編集状態終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 左クリックのみ
            if (e.Button == MouseButtons.Left)
            {
                this.SuspendLayout();
                if (bEditMode == true)
                {
                    // 全開と今回で内容が異なっていれば編集と判定し登録処理実行
                    if (!stickyData.contents.Equals(txtSticky.Text))
                    {
                        bEdited = true;
                    }
                    bEditMode = false;
                    if (this.ContextMenuStrip != null)
                    {
                        llblDisplay.ContextMenuStrip.Enabled = true;
                    }
                    llblDisplay.Visible = true;
                    // 付箋情報保存及び表示
                    stickySaveAndDisplay();
                }
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// フォームへのOnMouse時、閉じるボタン表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_MouseMove(object sender, MouseEventArgs e)
        {
            //this.ulsbClose.Visible = true;
            //this.ulsbClose.BringToFront();
        }

        /// <summary>
        /// フォームからLeaveMouse時、閉じるボタン非表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStickies_MouseLeave(object sender, EventArgs e)
        {
            //this.ulsbClose.Visible = false;
            //this.ulsbClose.BringToFront();
        }

        /// <summary>
        /// リンクラベルからLeaveMouse時、閉じるボタン非表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblDisplay_MouseLeave(object sender, EventArgs e)
        {
            //this.ulsbClose.Visible = false;
            //this.ulsbClose.BringToFront();
        }
    }
}
