using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace SwS
{
    public partial class frmLauncherSetting : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();
        private bool isMouseLeftDown = false;
        private bool isMouseRightDown = false;
        private bool isChildItem = false;
        private Int32 yDiff = 0;
        private TreeNode tnRoot = null;
        private TreeNode tnParent = null;
        private DBManager.Launcher.data fieldData = null;

        public static DBManager.Launcher.LauncherList launcherList = new DBManager.Launcher.LauncherList();
        private FolderBrowserDialog fbd = new FolderBrowserDialog();

        public frmLauncherSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 呼び出し
        /// </summary>
        public bool showDialog()
        {
            // SQL発行後の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            yDiff = this.Height - tvLauncher.Height;

            // ランチャ設定
            readLauncherData();
            makeTreeView();

            disableField();

            this.ShowDialog();

            return true;
        }

        /// <summary>
        /// 各種データ読込
        /// </summary>
        private void readLauncherData()
        {
            launcherList.Clear();

            // SELECT文の作成
            string strSQL = "select * from launcher_list order by launcher_order";

            SQLiteDataReader result = sqliteAccess.select(strSQL);
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
            // リーダクローズ
            sqliteAccess.readerClose();
        }

        /// <summary>
        /// ランチャメニュー構造ツリービュー構築
        /// </summary>
        private void makeTreeView()
        {
            tvLauncher.Nodes.Clear();

            tnRoot = new TreeNode("Root");
            tnRoot.Tag = "0";
            tvLauncher.Nodes.Add(tnRoot);

            // ランチャメニュー構造作成
            recallMakeTreeView(launcherList.SelectChild(0), tnRoot);
            tnRoot.Expand();
        }

        /// <summary>
        /// 再起呼び出し形式　ランチャメニュー構造　子要素構築
        /// </summary>
        /// <param name="launcherList"></param>
        /// <param name="parentTsddi"></param>
        private void recallMakeTreeView(DBManager.Launcher.LauncherList launcherDirList, TreeNode parentTn)
        {
            // ランチャメニュー構造作成
            lock (launcherDirList)
            {
                foreach (DBManager.Launcher.data data in launcherDirList)
                {
                    TreeNode tn = new TreeNode();

                    tn.Text = data.launcher_name;
                    tn.Tag = data.id;
                    parentTn.Nodes.Add(tn);

                    // ランチャメニュー構造的意味合いでフォルダなら
                    if (data.launcher_type == false)
                    {
                        // ランチャメニュー構造　子要素構築
                        recallMakeTreeView(launcherList.SelectChild(data.id), tn);
                    }
                }
            }
        }

        /// <summary>
        /// フォームクローズ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLauncherSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 終了理由が×ボタン押下のとき(正しくはユーザ操作による終了処理)
            // (タスクトレイアイコン右クリックでの終了処理の場合は通常処理を行う)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 本来の終了処理ではなくフォーム非表示とする
                e.Cancel = true;                // 終了処理キャンセル
                this.Visible = false;           // フォーム非表示
            }

            // SQL終了処理
            sqliteAccess.disposeEnviroment();
        }

        /// <summary>
        /// フォームリサイズ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLauncherSetting_Resize(object sender, EventArgs e)
        {
            tvLauncher.Height = this.Height - yDiff;

        }

        /// <summary>
        /// ツリービューアイテムドラッグ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvLauncher_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            tv.SelectedNode = (TreeNode)e.Item;
            tv.Focus();

            //ノードのドラッグを開始する
            DragDropEffects dde = tv.DoDragDrop(e.Item, DragDropEffects.All);

            //移動した時は、ドラッグしたノードを削除する
            if ((dde & DragDropEffects.Move) == DragDropEffects.Move)
            {
                tv.Nodes.Remove((TreeNode)e.Item);
                batchUpdate();
            }
        }

        /// <summary>
        /// ツリービュードラッグエンター時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvLauncher_DragEnter(object sender, DragEventArgs e)
        {
            // ドラッグ中のデータ形式がファイルであれば受け付ける
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ドラッグ中のデータ一覧を取得
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (!IsFiles(files))
                {
                    return;
                }

                // コピー可能を設定
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// ツリービュードラッグオーバー時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvLauncher_DragOver(object sender, DragEventArgs e)
        {
            string[] files = { "" };

            //ドラッグされているデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    //Ctrlキーが押されていればCopy
                    //"8"はCtrlキーを表す
                    e.Effect = DragDropEffects.Copy;
                }
                else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                {
                    //何も押されていなければMove
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            //ドロップされたデータがTreeNodeか調べる
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                // ドラッグ＆ドロップされたファイル
                files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
            else
            {
                //TreeNodeでなければ受け入れない
                e.Effect = DragDropEffects.None;
            }

            //マウス下のNodeを選択する
            if (e.Effect != DragDropEffects.None)
            {
                TreeView tv = (TreeView)sender;

                //マウスのあるNodeを取得する
                TreeNode target =
                    tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));

                //ドラッグされているNodeを取得する
                TreeNode source =
                    (TreeNode)e.Data.GetData(typeof(TreeNode));

                //マウス下のNodeがドロップ先として適切か調べる
                if (target != null && target != source && !IsChildNode(source, target))
                {
                    //Nodeを選択する
                    if (target.IsSelected == false)
                    {
                        tv.SelectedNode = target;
                    }
                }
                //マウス下のNodeがドロップ先として適切か調べる
                else if (IsFiles(files))
                {
                    tv.SelectedNode = tnRoot;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        /// <summary>
        /// ツリービュードラッグドロップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvLauncher_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeView tv = (TreeView)sender;

                //ドロップされたデータ(TreeNode)を取得
                TreeNode source = (TreeNode)e.Data.GetData(typeof(TreeNode));
                
                //ドロップ先のTreeNodeを取得する
                TreeNode target = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                
                //マウス下のNodeがドロップ先として適切か調べる
                if (target != null && target != source && !IsChildNode(source, target))
                {
                    //ドロップされたNodeのコピーを作成
                    TreeNode cln = (TreeNode)source.Clone();

                    Int32 targetId = Int32.Parse(target.Tag.ToString());
                    bool type = false;
                    if (targetId != 0)
                    {
                        // ノードタイプ取得
                        type = launcherList.SelectId(targetId).launcher_type;
                    }
                    // フォルダなら
                    if (type == false)
                    {
                        //Nodeを追加
                        target.Nodes.Add(cln);

                        //ドロップ先のNodeを展開
                        target.Expand();

                        //追加されたNodeを選択
                        tv.SelectedNode = cln;
                    }
                    // 実行対象なら
                    else
                    {
                        // 移動対象を含むノード一覧を取得
                        TreeNodeCollection parentNodes = target.Parent.Nodes;

                        // ノードのインデックスを取得
                        Int32 targetNodeIndex = parentNodes.IndexOf(target);

                        // 一度削除する
                        // Me.Nodes.Remove(cln)

                        // 目的の場所に挿入
                        parentNodes.Insert(targetNodeIndex, cln);

                        //ドロップ先のNodeを展開
                        target.Expand();

                        //追加されたNodeを選択
                        tv.SelectedNode = cln;
                    }

                    // 親ノード書き換え
                    Int32 parentId = Int32.Parse(cln.Parent.Tag.ToString());
                    Int32 id = Int32.Parse(cln.Tag.ToString());
                    launcherList.SelectId(id).launcher_parent_node = parentId;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            //ドロップされたデータがFileか調べる
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ドラッグ＆ドロップされたファイル
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (IsFiles(files))
                {
                    e.Effect = DragDropEffects.Copy;

                    tvLauncher.SelectedNode = tnRoot;
                    isChildItem = true;

                    // ドロップされたファイルを配置
                    foreach (string file in files)
                    {
                        DBManager.Launcher.data data = new DBManager.Launcher.data();

                        string filename = file;
                        int sep = file.LastIndexOf('.');
                        if (sep >= 0)
                        {
                            filename = filename.Substring(0, sep);
                        }
                        sep = filename.LastIndexOf('\\');
                        if (sep >= 0)
                        {
                            filename = filename.Substring(sep + 1);
                        }
                        data.id = -1;
                        data.launcher_name = filename;
                        data.launcher_path = file;
                        data.launcher_parameter = "";
                        data.launcher_type = true;

                        applyNewElement(ref data);
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// あるTreeNodeが別のTreeNodeの子ノードか調べる
        /// </summary>
        /// <param name="parent">親ノードか調べるTreeNode</param>
        /// <param name="child">子ノードか調べるTreeNode</param>
        /// <returns>子ノードの時はTrue</returns>
        private static bool IsChildNode(TreeNode parent, TreeNode child)
        {
            if (child.Parent == parent)
            {
                return true;
            }
            else if (child.Parent != null)
            {
                return IsChildNode(parent, child.Parent);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// File がすべて存在するか調べる
        /// </summary>
        /// <param name="files">調べる対象</param>
        /// <param name="child">子ノードか調べるTreeNode</param>
        /// <returns>子ノードの時はTrue</returns>
        private static bool IsFiles(string[] files)
        {
            foreach (string file in files)
            {
                if (!(System.IO.File.Exists(file) || System.IO.Directory.Exists(file)))
                {
                    // ファイルでもフォルダでもない(んなことあるのか？)
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 一括更新
        /// </summary>
        private void batchUpdate()
        {
            tvLauncher.SuspendLayout();

            // オーダー再設定
            recallRefreshOrder(tnRoot.FirstNode, 1);

            sqliteAccess.beginTrans();

            // ランチャメニューリスト保存
            foreach (DBManager.Launcher.data data in launcherList)
            {
                effectiveUpdateOrderAndParent(data.id, data.launcher_order, data.launcher_parent_node);
            }

            sqliteAccess.endTrans();

            tvLauncher.ResumeLayout();
        }

        /// <summary>
        /// SQL接続(削除)
        /// </summary>
        /// <param name="data"></param>
        private void effectiveDelete(DBManager.Launcher.data data)
        {
            string strSQL = "delete from launcher_list "
                + " where id = '" + data.id + "' ";

            sqliteAccess.update(strSQL);

            strSQL = "delete from launcher_list "
                + " where launcher_parent_node  > 0 "
                + " and launcher_parent_node not in (select id from launcher_list )";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(更新)
        /// </summary>
        /// <param name="data"></param>
        private void effectiveUpdate(DBManager.Launcher.data data)
        {
            string strSQL = "update launcher_list set "
                + "launcher_order = " + data.launcher_order + ", "
                + "launcher_name = '" + data.launcher_name + "', "
                + "launcher_path = '" + data.launcher_path + "', "
                + "launcher_parameter = '" + data.launcher_parameter + "', "
                + "launcher_type = " + ((data.launcher_type == true) ? 1 : 0) + ", "
                + "launcher_parent_node = " + data.launcher_parent_node
                + " where id = '" + data.id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(orderのみ更新)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="parentNode"></param>
        private void effectiveUpdateOrderAndParent(Int32 id, Int32 order, Int32 parentNode)
        {
            string strSQL = "update launcher_list set "
                + "launcher_order = " + order + ", "
                + "launcher_parent_node = " + parentNode + " "
                + " where id = '" + id + "' ";

            sqliteAccess.update(strSQL);
        }

        /// <summary>
        /// SQL接続(新規)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Int32 effectiveInsert(DBManager.Launcher.data data)
        {
            string strSQL = "insert into launcher_list ("
                + "launcher_order, "
                + "launcher_name, "
                + "launcher_path, "
                + "launcher_parameter, "
                + "launcher_type, "
                + "launcher_parent_node "
                + ") values ("
                + data.launcher_order + ", "
                + "'" + data.launcher_name + "', "
                + "'" + data.launcher_path + "', "
                + "'" + data.launcher_parameter + "', "
                + ((data.launcher_type == true) ? 1 : 0) + ", "
                + data.launcher_parent_node
                + ")";

            return sqliteAccess.insert(strSQL, "launcher_list");
        }

        /// <summary>
        /// オーダー再設定
        /// </summary>
        /// <param name="nowNode"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private Int32 recallRefreshOrder(TreeNode nowNode, Int32 order)
        {
            do
            {
                // 当該ノードのオーダーを更新
                Int32 id = Int32.Parse(nowNode.Tag.ToString());
                DBManager.Launcher.data data = launcherList.SelectId(id);
                data.launcher_order = order;
                order++;

                // 子要素を持っているか
                if (nowNode.GetNodeCount(true) != 0)
                {
                    // 子要素のオーダー再設定
                    order = recallRefreshOrder(nowNode.FirstNode, order);
                }
                nowNode = nowNode.NextNode;
            } while (nowNode != null);

            return order;
        }

        /// <summary>
        /// ツリービューマウス押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvLauncher_MouseDown(object sender, MouseEventArgs e)
        {
            tvLauncher.SelectedNode = tvLauncher.GetNodeAt(e.X, e.Y);

            // マウス右押下状態
            if (e.Button == MouseButtons.Right)
            {
                isMouseRightDown = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                isMouseLeftDown = true;
            }
            disableField();
        }

        /// <summary>
        /// ツリービューマウスＵＰ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvLauncher_MouseUp(object sender, MouseEventArgs e)
        {
            // マウス押下状態でなければ
            if (!isMouseRightDown && !isMouseLeftDown)
            {
                return;
            }

            // ドラッグ状態の場合
            if (tvLauncher.SelectedNode != null)
            {
                if (isMouseRightDown == true)
                {
                    cmsRightClickMenu.Show(this, new System.Drawing.Point(e.X, e.Y));
                }
                else if (isMouseLeftDown == true)
                {
                    if (!tvLauncher.SelectedNode.Tag.Equals("0"))
                    {
                        alterLauncherMenu();
                    }
                }
            }

            isMouseRightDown = false;
            isMouseLeftDown = false;
        }

        /// <summary>
        /// 選択されたノードのランチャメニュー修正処理
        /// </summary>
        private void alterLauncherMenu()
        {
            // 選択
            TreeNode tn = tvLauncher.SelectedNode;
            Int32 id = Int32.Parse(tn.Tag.ToString());
            fieldData = launcherList.SelectId(id);
            // 修正
            enableField(fieldData);
        }

        /// <summary>
        /// 選択されたノードのランチャメニュー削除処理
        /// </summary>
        private void deleteLauncherMenu()
        {
            // 選択
            TreeNode tn = tvLauncher.SelectedNode;
            Int32 id = Int32.Parse(tn.Tag.ToString());
            DBManager.Launcher.data data = launcherList.SelectId(id);
            // 削除
            effectiveDelete(data);
            launcherList.Remove(data);
            tn.Remove();
        }

        /// <summary>
        /// 入力フィールドアクティブ化
        /// </summary>
        /// <param name="data"></param>
        private void enableField(DBManager.Launcher.data data)
        {
            txtName.Text = data.launcher_name;
            txtPath.Text = data.launcher_path;
            txtParameter.Text = data.launcher_parameter;

            btnFileSelect.Enabled = data.launcher_type;
            btnFolderSelect.Enabled = data.launcher_type;
            txtName.Enabled = true;
            txtPath.Enabled = true;
            txtParameter.Enabled = true;
            btnApply.Enabled = true;
            btnCancel.Enabled = true;

            txtName.Focus();
        }

        /// <summary>
        /// 入力フィールド非アクティブ化
        /// </summary>
        private void disableField()
        {
            txtName.Text = "";
            txtPath.Text = "";
            txtParameter.Text = "";

            btnFileSelect.Enabled = false;
            btnFolderSelect.Enabled = false;
            txtName.Enabled = false;
            txtPath.Enabled = false;
            txtParameter.Enabled = false;
            btnApply.Enabled = false;
            btnCancel.Enabled = false;

            fieldData = null;
            tnParent = null;
            tvLauncher.Focus();
        }

        /// <summary>
        /// 修正メニュー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alterLauncherMenu();
        }

        /// <summary>
        /// 削除メニュー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteLauncherMenu();
        }

        /// <summary>
        /// ツリービュー右クリックメニュー表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsRightClickMenu_Opening(object sender, CancelEventArgs e)
        {
            if (tvLauncher.SelectedNode == tnRoot)
            {
                alterToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                makeFolderToolStripMenuItem.Enabled = false;
                makeChildFolderToolStripMenuItem.Enabled = true;
                makeItemToolStripMenuItem.Enabled = false;
                makeChildItemToolStripMenuItem.Enabled = true;
            }
            else
            {
                alterToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                Int32 id = Int32.Parse(tvLauncher.SelectedNode.Tag.ToString());
                if (id > 0)
                {
                    DBManager.Launcher.data data = launcherList.SelectId(id);
                    if (data.launcher_type == true)
                    {
                        makeFolderToolStripMenuItem.Enabled = true;
                        makeChildFolderToolStripMenuItem.Enabled = false;
                        makeItemToolStripMenuItem.Enabled = true;
                        makeChildItemToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        makeFolderToolStripMenuItem.Enabled = true;
                        makeChildFolderToolStripMenuItem.Enabled = true;
                        makeItemToolStripMenuItem.Enabled = true;
                        makeChildItemToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// キャンセルボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            disableField();
        }

        /// <summary>
        /// 新規フォルダ作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (tvLauncher.SelectedNode != null)
            {
                fieldData.launcher_name = txtName.Text;
                fieldData.launcher_path = txtPath.Text;
                fieldData.launcher_parameter = txtParameter.Text;
                
                // 新規作成時
                if (fieldData.id == -1)
                {
                    DBManager.Launcher.data data = new DBManager.Launcher.data();

                    data.id = fieldData.id;
                    data.launcher_order = fieldData.launcher_order;
                    data.launcher_name = fieldData.launcher_name;
                    data.launcher_path = fieldData.launcher_path;
                    data.launcher_parameter = fieldData.launcher_parameter;
                    data.launcher_type = fieldData.launcher_type;
                    data.launcher_parent_node = fieldData.launcher_parent_node;

                    applyNewElement(ref data);
                }
                else
                {
                    tvLauncher.SelectedNode.Text = txtName.Text;
                    effectiveUpdate(fieldData);
                }
                disableField();
            }
        }

        /// <summary>
        /// 新規要素作成
        /// </summary>
        /// <param name="data"></param>
        private void applyNewElement(ref DBManager.Launcher.data data)
        {
            TreeNode tn = new TreeNode();
            tn.Text = data.launcher_name;
            tn.Tag = -1;
            // 子要素作成
            if (isChildItem == true)
            {
                // この場合は追加
                tvLauncher.SelectedNode.Nodes.Add(tn);
            }
            // 同一階層
            else
            {
                // ノードのインデックスを取得
                Int32 targetNodeIndex = tnParent.Nodes.IndexOf(tvLauncher.SelectedNode);
                // 挿入
                tnParent.Nodes.Insert(targetNodeIndex, tn);
            }

            launcherList.Add(data);

            // オーダー再設定
            recallRefreshOrder(tnRoot.FirstNode, 1);
            // 新規登録
            data.id = effectiveInsert(data);
            tn.Tag = data.id;
        }

        private void makeFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeElement(false, false);
        }

        /// <summary>
        /// 要素作成
        /// </summary>
        /// <param name="isChild">子要素か？</param>
        /// <param name="isFile">実行対象か？</param>
        private void makeElement(bool isChild, bool isFile)
        {
            Int32 id = Int32.Parse(tvLauncher.SelectedNode.Tag.ToString());
            DBManager.Launcher.data data = launcherList.SelectId(id);
            fieldData = new DBManager.Launcher.data();

            isChildItem = isChild;
            // 子要素作成
            if (isChild == true)
            {
                // 選ばれたノードを親とする
                fieldData.launcher_parent_node = data.id;
                tnParent = tvLauncher.SelectedNode;
            }
            // そうでないなら
            else
            {
                // 選ばれたノードの親ノードを渡す
                fieldData.launcher_parent_node = data.launcher_parent_node;
                tnParent = tvLauncher.SelectedNode.Parent;
            }
            // 実行対象かフォルダかを設定
            fieldData.launcher_type = isFile;

            // 初期化
            fieldData.id = -1;
            fieldData.launcher_name = "";
            fieldData.launcher_path = "";
            fieldData.launcher_parameter = "";

            // 新規
            enableField(fieldData);
        }

        /// <summary>
        /// 新規子フォルダ作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeChildFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeElement(true, false);
        }

        /// <summary>
        /// 新規項目作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeElement(false, true);
        }

        /// <summary>
        /// 新規子項目作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeChildItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeElement(true, true);
        }

        /// <summary>
        /// ファイル選択ダイアログ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            if (txtPath.Text != null && !txtPath.Text.Equals(""))
            {
                ofdFile.InitialDirectory = txtPath.Text.Substring(0, txtPath.Text.LastIndexOf('\\')) + "\\";
                ofdFile.FileName = txtPath.Text.Substring(txtPath.Text.LastIndexOf('\\') + 1);
            }
            else
            {
                ofdFile.InitialDirectory = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf('\\')) + "\\";
            }
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofdFile.FilterIndex = 2;
            //タイトルを設定する
            ofdFile.Title = "開くファイルを選択してください";
            //ダイアログを表示する
            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                string filename = ofdFile.FileName;
                int sep = filename.LastIndexOf('.');
                if (sep >= 0)
                {
                    filename = filename.Substring(0, sep);
                }
                sep = filename.LastIndexOf('\\');
                if (sep >= 0)
                {
                    filename = filename.Substring(sep + 1);
                }
                txtName.Text = filename;

                txtPath.Text = ofdFile.FileName;
            }
        }

        /// <summary>
        /// フォルダ選択ダイアログ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            if (txtPath.Text != null && !txtPath.Text.Equals(""))
            {
                fbd.SelectedPath = txtPath.Text.Substring(0, txtPath.Text.LastIndexOf('\\')) + "\\";
            }
            else
            {
                fbd.SelectedPath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf('\\')) + "\\";
            }
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                string filename = fbd.SelectedPath;
                Int32 sep = filename.LastIndexOf('\\');
                if (sep >= 0)
                {
                    filename = filename.Substring(sep + 1);
                }
                txtName.Text = filename;

                txtPath.Text = fbd.SelectedPath;
            }
        }
    }
}
