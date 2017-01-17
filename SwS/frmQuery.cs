using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;

namespace SwS
{
    public partial class frmQuery : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();
        public static DBManager.Contents.ContentsList contentsList = new DBManager.Contents.ContentsList();
        private Hashtable dataType = new Hashtable();
        private frmCalendar parentForm;

        public frmQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 起動用メソッド
        /// </summary>
        /// <param name="frm"></param>
        public void query(frmCalendar frm)
        {
            parentForm = frm;
            dataType.Clear();
            dataType.Add(0, "スケジュール");
            dataType.Add(1, "ToDo");
            dataType.Add(2, "メモ");
            dataType.Add(3, "付箋");
            dataType.Add(4, "アラーム");
            dataType.Add("スケジュール", 0);
            dataType.Add("ToDo", 1);
            dataType.Add("メモ", 2);
            dataType.Add("付箋", 3);
            dataType.Add("アラーム", 4);

            // SQL発行後の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // クエリ検索文字列
            char[] sep = {','};
            string[] queryString = DBManager.Setting.main_query_string.Split(sep);
            cmbQueryString.Items.Clear();
            for (int i = 0; i < queryString.Length; i++)
            {
                cmbQueryString.Items.Add(queryString[i]);
            }

            // クエリ検索対象
            string[] queryObject = DBManager.Setting.main_query_object.Split(sep);
            if (queryObject.Length == 5)
            {
                cbSchedule.Checked = (int.Parse(queryObject[0]) == 1) ? true : false;
                cbTodo.Checked = (int.Parse(queryObject[1]) == 1) ? true : false;
                cbMemo.Checked = (int.Parse(queryObject[2]) == 1) ? true : false;
                cbSticky.Checked = (int.Parse(queryObject[3]) == 1) ? true : false;
                cbAlarm.Checked = (int.Parse(queryObject[4]) == 1) ? true : false;
            }

            // クエリ検索方法
            if (DBManager.Setting.main_query_expr == 0)
            {
                rbComplete.Checked = true;
            }
            else if (DBManager.Setting.main_query_expr == 1)
            {
                rbAnd.Checked = true;
            }
            else if (DBManager.Setting.main_query_expr == 2)
            {
                rbOr.Checked = true;
            }

            this.Show();
            this.Activate();
        }

        private void frmQuery_Load(object sender, EventArgs e)
        {
        }

        private void frmQuery_Resize(object sender, EventArgs e)
        {
            cmbQueryString.Width = this.Width - cmbQueryString.Left - 11;
            btnQuery.Left = this.Width - btnQuery.Width - 12;
            splitContainer1.Size = new Size(this.Width - splitContainer1.Left - 11, this.Height - splitContainer1.Top - 30);
        }

        private void frmQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 終了理由が×ボタン押下のとき(正しくはユーザ操作による終了処理)
            // (タスクトレイアイコン右クリックでの終了処理の場合は通常処理を行う)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 本来の終了処理ではなくフォーム非表示とする
                e.Cancel = true;                // 終了処理キャンセル
                this.Visible = false;           // フォーム非表示
                return;
            }

            // SQL終了処理
            sqliteAccess.disposeEnviroment();
        }

        /// <summary>
        /// 検索開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            // 検索対象に一つもチェックされていない場合は無処理でリターン
            if (cbSchedule.Checked == false && cbTodo.Checked == false && cbMemo.Checked == false && cbSticky.Checked == false && cbAlarm.Checked == false)
                return;
            // 検索文字列に有効な文字が入っていない場合は無処理でリターン
            if (cmbQueryString.Text.Trim().Length == 0)
                return;
            textBox1.Text = "";
            listView1.Items.Clear();

            // クエリ検索
            readContentsData();
            // クエリ検索データ生成
            makeQueryOrder();
            // クエリ検索データ保存
            DBManager.Setting.save(sqliteAccess);

        }

        /// <summary>
        /// 各種データ読込
        /// </summary>
        private void readContentsData()
        {
            // SELECT文の作成
            string strSQLCheckBox = "";
            ArrayList exprList = new ArrayList();
            if (cbSchedule.Checked == true)
                exprList.Add(0);
            if (cbTodo.Checked == true)
                exprList.Add(1);
            if (cbMemo.Checked == true)
                exprList.Add(2);
            if (cbSticky.Checked == true)
                exprList.Add(3);
            if (cbAlarm.Checked == true)
                exprList.Add(4);

            string strSQL = "select * from contents_list ";
            strSQL = strSQL + "where data_type in (";
            foreach (int type in exprList)
            {
                if (strSQLCheckBox.Equals("") == false)
                {
                    strSQLCheckBox = strSQLCheckBox + ", ";
                }
                strSQLCheckBox = strSQLCheckBox + type.ToString();
            }
            strSQL = strSQL + strSQLCheckBox;
            strSQL = strSQL + ") and";

            if (rbComplete.Checked == true)
            {
                strSQL = strSQL + " contents like '%" + cmbQueryString.Text + "%'";
            }
            else
            {
                ArrayList queryList = new ArrayList();
                char[] sep = { ' ' };
                string[] query = cmbQueryString.Text.Split(sep);
                for (int i = 0; i < query.Length; i++)
                {
                    if (i == 0)
                    {
                        strSQL = strSQL + " (";
                    }
                    else
                    {
                        if (rbAnd.Checked == true)
                        {
                            strSQL = strSQL + " and";
                        }
                        else if (rbOr.Checked == true)
                        {
                            strSQL = strSQL + " or";
                        }
                    }
                    strSQL = strSQL + " contents like '%" + query[i] + "%'";
                }
                strSQL = strSQL + ")";
            }

            SQLiteDataReader result = sqliteAccess.select(strSQL);
            while (result.Read())
            {
                DBManager.Contents.data data = new DBManager.Contents.data();

                data.id = result.GetInt32((Int32)DBManager.Contents.enum_data.id);
                data.effective = result.GetBoolean((Int32)DBManager.Contents.enum_data.effective);
                data.data_type = result.GetInt32((Int32)DBManager.Contents.enum_data.data_type);
                data.date_time = result.GetDateTime((Int32)DBManager.Contents.enum_data.date_time);
                data.contents = result.GetString((Int32)DBManager.Contents.enum_data.contents);
                data.show_stickies = result.GetBoolean((Int32)DBManager.Contents.enum_data.show_stickies);
                data.backcolor_stickies = result.GetString((Int32)DBManager.Contents.enum_data.backcolor_stickies);
                data.point_stickies = result.GetString((Int32)DBManager.Contents.enum_data.point_stickies);
                data.time_trigger = result.GetBoolean((Int32)DBManager.Contents.enum_data.time_trigger);
                data.time_trigger_type = result.GetInt32((Int32)DBManager.Contents.enum_data.time_trigger_type);
                if (data.time_trigger == true)
                {
                    data.time_trigger_datetime = result.GetDateTime((Int32)DBManager.Contents.enum_data.time_trigger_datetime);
                }
                data.time_trigger_dayofweek = result.GetInt32((Int32)DBManager.Contents.enum_data.time_trigger_dayofweek);
                data.attach_trigger = result.GetBoolean((Int32)DBManager.Contents.enum_data.attach_trigger);
                if (data.attach_trigger == true)
                {
                    data.attach_trigger_title = result.GetString((Int32)DBManager.Contents.enum_data.attach_trigger_title);
                }
                data.display_backimage_stickies = result.GetBoolean((Int32)DBManager.Contents.enum_data.display_backimage_stickies);
                data.display_backimage_type_stickies = result.GetInt32((Int32)DBManager.Contents.enum_data.display_backimage_type_stickies);
                if (data.display_backimage_stickies == true)
                {
                    data.display_backimage_path_stickies = result.GetString((Int32)DBManager.Contents.enum_data.display_backimage_path_stickies);
                }
                data.display_backimage_region_stickies = result.GetBoolean((Int32)DBManager.Contents.enum_data.display_backimage_region_stickies);
                data.alpha_stickies = result.GetInt32((Int32)DBManager.Contents.enum_data.alpha_stickies);

                object objFontSerializeble = result.GetValue((Int32)DBManager.Contents.enum_data.display_font_stickies);
                if (objFontSerializeble != null)
                {
                    string fontSerializeble = objFontSerializeble.ToString();
                    if (!fontSerializeble.Trim().Equals(""))
                    {
                        FontConverter fc = new FontConverter();
                        data.display_font_stickies = (Font)fc.ConvertFromString(fontSerializeble);
                    }
                    else
                    {
                        data.display_font_stickies = this.Font;
                    }
                }
                else
                {
                    data.display_font_stickies = this.Font;
                }

                // データ格納
                contentsList.Add(data);

                // リストビューへデータ移行
                string[] queryResult = {
                                           (string)dataType[(int)data.data_type]
                                           , data.date_time.ToString()
                                           , data.contents
                                       };
                ListViewItem lvi = new ListViewItem(queryResult);
                lvi.ToolTipText = data.contents;
                lvi.Tag = data.id.ToString();
                listView1.Items.Add(lvi);

            }
            // リーダクローズ
            sqliteAccess.readerClose();
        }

        /// <summary>
        /// リストビューからテキストボックスへ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListView lv = (sender as ListView);
            ListViewItem lvi = lv.SelectedItems[0];

            textBox1.Text = lvi.ToolTipText;
        }

        /// <summary>
        /// クエリ検索データ生成
        /// </summary>
        private void makeQueryOrder()
        {
            if (cmbQueryString.Items.Contains(cmbQueryString.Text) == false)
            {
                if (DBManager.Setting.main_query_string.Equals(""))
                {
                    DBManager.Setting.main_query_string = cmbQueryString.Text;
                }
                else
                {
                    DBManager.Setting.main_query_string = cmbQueryString.Text + "," + DBManager.Setting.main_query_string;
                }
                char[] sep = { ',' };
                string[] queryString = DBManager.Setting.main_query_string.Split(sep);
                cmbQueryString.Items.Clear();
                for (int i = 0; i < queryString.Length; i++)
                {
                    cmbQueryString.Items.Add(queryString[i]);
                }
            }

            DBManager.Setting.main_query_object = (cbSchedule.Checked == true) ? "1" : "0";
            DBManager.Setting.main_query_object += (cbTodo.Checked == true) ? ",1" : ",0";
            DBManager.Setting.main_query_object += (cbMemo.Checked == true) ? ",1" : ",0";
            DBManager.Setting.main_query_object += (cbSticky.Checked == true) ? ",1" : ",0";
            DBManager.Setting.main_query_object += (cbAlarm.Checked == true) ? ",1" : ",0";

            if (rbComplete.Checked == true)
            {
                DBManager.Setting.main_query_expr = 0;
            }
            else if (rbAnd.Checked == true)
            {
                DBManager.Setting.main_query_expr = 1;
            }
            else if (rbOr.Checked == true)
            {
                DBManager.Setting.main_query_expr = 2;
            }
        }
    }
}
