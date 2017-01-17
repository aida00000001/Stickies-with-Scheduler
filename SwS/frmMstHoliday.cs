using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SQLite;

namespace SwS
{
    public partial class frmMstHoliday : Form
    {
        private SQLiteAccess sqliteAccess = new SQLiteAccess();

        public frmMstHoliday()
        {
            InitializeComponent();
        }

        private void frmMstHoliday_Load(object sender, EventArgs e)
        {
            dgvHoliday.ColumnCount = 2;

            // SQL発行前の環境設定
            sqliteAccess.setEnviroment(DBManager.dbPath, "SwS.db");

            // 祝祭日マスタ読込
            readHolidayMasterData();

            // フィールド初期化
            initField();
        }

        /// <summary>
        /// 祝祭日マスタ読込
        /// </summary>
        private void readHolidayMasterData()
        {
            // SELECT文の作成
            string strSQL = "select * from holiday_master";
            SQLiteDataReader result = sqliteAccess.select(strSQL);

            dgvHoliday.RowCount = 1;
            int i = 0;
            while (result.Read())
            {
                dgvHoliday[0, i].Value = result.GetInt32(0);
                dgvHoliday[1, i].Value = result.GetString(2);
                dgvHoliday.Rows.Add();
                i++;
            }
            dgvHoliday.RowCount -= 1;

            dgvHoliday.Columns[0].Visible = false;
            dgvHoliday.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            sqliteAccess.readerClose();
        }

        private void cmbHolidayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbHolidayType.SelectedIndex)
            {
                case 0:         // 固定休日
                    label5.Text = "対象日";
                    label6.Text = "";
                    cmbDayOrWeek.Visible = true;
                    cmbDayOrWeek.Items.Clear();
                    cmbDayOrWeek.Items.Add(" 1日");
                    cmbDayOrWeek.Items.Add(" 2日");
                    cmbDayOrWeek.Items.Add(" 3日");
                    cmbDayOrWeek.Items.Add(" 4日");
                    cmbDayOrWeek.Items.Add(" 5日");
                    cmbDayOrWeek.Items.Add(" 6日");
                    cmbDayOrWeek.Items.Add(" 7日");
                    cmbDayOrWeek.Items.Add(" 8日");
                    cmbDayOrWeek.Items.Add(" 9日");
                    cmbDayOrWeek.Items.Add("10日");
                    cmbDayOrWeek.Items.Add("11日");
                    cmbDayOrWeek.Items.Add("12日");
                    cmbDayOrWeek.Items.Add("13日");
                    cmbDayOrWeek.Items.Add("14日");
                    cmbDayOrWeek.Items.Add("15日");
                    cmbDayOrWeek.Items.Add("16日");
                    cmbDayOrWeek.Items.Add("17日");
                    cmbDayOrWeek.Items.Add("18日");
                    cmbDayOrWeek.Items.Add("19日");
                    cmbDayOrWeek.Items.Add("20日");
                    cmbDayOrWeek.Items.Add("21日");
                    cmbDayOrWeek.Items.Add("22日");
                    cmbDayOrWeek.Items.Add("23日");
                    cmbDayOrWeek.Items.Add("24日");
                    cmbDayOrWeek.Items.Add("25日");
                    cmbDayOrWeek.Items.Add("26日");
                    cmbDayOrWeek.Items.Add("27日");
                    cmbDayOrWeek.Items.Add("28日");
                    cmbDayOrWeek.Items.Add("29日");
                    cmbDayOrWeek.Items.Add("30日");
                    cmbDayOrWeek.Items.Add("31日");
                    cmbDayOfWeek.Visible = false;
                    dtpYear.Visible = true;
                    cmbCompare.Visible = true;
                    break;
                case 1:         // 月・週固定休日
                    label5.Text = "対象週";
                    label6.Text = "曜日";
                    cmbDayOrWeek.Visible = true;
                    cmbDayOrWeek.Items.Clear();
                    cmbDayOrWeek.Items.Add(" 1週");
                    cmbDayOrWeek.Items.Add(" 2週");
                    cmbDayOrWeek.Items.Add(" 3週");
                    cmbDayOrWeek.Items.Add(" 4週");
                    cmbDayOrWeek.Items.Add(" 5週");
                    cmbDayOrWeek.Items.Add(" 6週");
                    cmbDayOfWeek.Visible = true;
                    dtpYear.Visible = true;
                    cmbCompare.Visible = true;
                    break;
                case 2:         // その他休日
                    label5.Text = "";
                    label6.Text = "";
                    cmbDayOrWeek.Visible = false;
                    cmbDayOfWeek.Visible = false;
                    dtpYear.Visible = true;
                    cmbCompare.Visible = true;
                    break;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string strSQL = "insert into holiday_master ("
                + "holiday_type, "
                + "holiday_name, "
                + "third, "
                + "fourth, "
                + "fifth, "
                + "sixth, "
                + "seventh"
                + ") values ("
                + cmbHolidayType.SelectedIndex + ", "
                + "'" + txtHolidayName.Text + "', "
                + cmbMonth.SelectedIndex + ", "
                + cmbDayOrWeek.SelectedIndex + ", "
                + cmbDayOfWeek.SelectedIndex + ", "
                + dtpYear.Value.ToString("yyyy") + ", "
                + cmbCompare.SelectedIndex
                + ")";

            string id = sqliteAccess.insert(strSQL, "holiday_master").ToString();
            int row = dgvHoliday.Rows.Add();
            dgvHoliday[0, row].Value = id;
            dgvHoliday[1, row].Value = txtHolidayName.Text;
            sqliteAccess.readerClose();

            // フィールド初期化
            initField();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string strSQL = "update holiday_master set "
                + "holiday_type = " + cmbHolidayType.SelectedIndex + ", "
                + "holiday_name = '" + txtHolidayName.Text + "', "
                + "third = " + cmbMonth.SelectedIndex + ", "
                + "fourth = " + cmbDayOrWeek.SelectedIndex + ", "
                + "fifth = " + cmbDayOfWeek.SelectedIndex + ", "
                + "sixth = " + dtpYear.Value.ToString("yyyy") + ", "
                + "seventh = " + cmbCompare.SelectedIndex + " "
                + " where id = " + lblId.Text + " ";

            sqliteAccess.update(strSQL);
            for (int i = 0; i < dgvHoliday.RowCount - 1; i++)
            {
                if (lblId.Text == dgvHoliday[0, i].Value.ToString())
                {
                    dgvHoliday[1, i].Value = txtHolidayName.Text;
                    break;
                }
            }
            sqliteAccess.readerClose();

            // フィールド初期化
            initField();
        }

        private void dgvHoliday_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = dgvHoliday[0, e.RowIndex].Value.ToString();

            string strSQL = "select * from holiday_master where "
                + "id = " + lblId.Text;

            SQLiteDataReader result = sqliteAccess.select(strSQL);
            if (result != null)
            {
                if (result.Read())
                {
                    cmbHolidayType.SelectedIndex = result.GetInt32(1);
                    txtHolidayName.Text = result.GetString(2);
                    cmbMonth.SelectedIndex = result.GetInt32(3);
                    cmbDayOrWeek.SelectedIndex = result.GetInt32(4);
                    cmbDayOfWeek.SelectedIndex = result.GetInt32(5);
                    dtpYear.Value = DateTime.Parse(result.GetInt32(6).ToString() + "/" + DateTime.Today.ToString("MM/dd"));
                    cmbCompare.SelectedIndex = result.GetInt32(7);
                }
            }

            sqliteAccess.readerClose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQL = "delete from holiday_master "
                + " where id = '" + lblId.Text + "' ";

            sqliteAccess.update(strSQL);
            sqliteAccess.readerClose();

            // フィールド初期化
            initField();
        }

        private void initField()
        {
            lblId.Text = "0";
            cmbHolidayType.SelectedIndex = 0;
            txtHolidayName.Text = "";
            cmbMonth.SelectedIndex = -1;
            cmbDayOrWeek.SelectedIndex = -1;
            cmbDayOfWeek.SelectedIndex = -1;
            dtpYear.Value = DateTime.Today;
            cmbCompare.SelectedIndex = -1;
        }
    }
}
