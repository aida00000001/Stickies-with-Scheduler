using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SwS
{
    public partial class frmStickiesSetting : Form
    {
        DBManager.Contents.data stickyData;
        private Hashtable dayOfWeekToNumber = new Hashtable();

        private bool bSave = false;

        public frmStickiesSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool showDialog(ref DBManager.Contents.data data)
        {
            stickyData = data;
            lblFilename.Text = data.display_backimage_path_stickies;
            lblAlpha.Text = data.alpha_stickies.ToString() + "%";
            tbAlpha.Value = data.alpha_stickies;
            cmbLayout.SelectedIndex = data.display_backimage_type_stickies;
            string[] rgb = data.backcolor_stickies.Split(',');
            lblColor.BackColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            cbRegion.Checked = data.display_backimage_region_stickies;

            cbTimeTrigger.Checked = data.time_trigger;
            uttTimeTrigger.init();
            uttTimeTrigger.DayDivide = true;
            if (data.time_trigger == true)
            {
                uttTimeTrigger.Visible = true;
                uatAttachTrigger.Visible = false;
                uatSnapWindow.Visible = false;
                uttTimeTrigger.TriggerType = stickyData.time_trigger_type;
                uttTimeTrigger.Date = stickyData.time_trigger_datetime;
                uttTimeTrigger.Time = stickyData.time_trigger_datetime;
                uttTimeTrigger.DayOfWeek = stickyData.time_trigger_dayofweek;
            }
            else
            {
                uttTimeTrigger.TriggerType = 0;
                uttTimeTrigger.Date = DateTime.Now;
                uttTimeTrigger.Time = DateTime.Now;
                uttTimeTrigger.DayOfWeek = 0;
            }

            cbAttachTrigger.Checked = data.attach_trigger;
            if (data.attach_trigger == true)
            {
                uttTimeTrigger.Visible = false;
                uatSnapWindow.Visible = false;
                uatAttachTrigger.Visible = true;
                uatAttachTrigger.AttachedTitle = data.attach_trigger_title;
            }

            cbSnapWindow.Checked = data.attach_trigger;
            if (data.attach_trigger == true)
            {
                uttTimeTrigger.Visible = false;
                uatAttachTrigger.Visible = false;
                uatSnapWindow.Visible = true;
                uatSnapWindow.AttachedTitle = data.attach_trigger_title;
            }

            lblFont.Font = stickyData.display_font_stickies;

            dayOfWeekToNumber.Add(DayOfWeek.Sunday, 0);
            dayOfWeekToNumber.Add(DayOfWeek.Monday, 1);
            dayOfWeekToNumber.Add(DayOfWeek.Tuesday, 2);
            dayOfWeekToNumber.Add(DayOfWeek.Wednesday, 3);
            dayOfWeekToNumber.Add(DayOfWeek.Thursday, 4);
            dayOfWeekToNumber.Add(DayOfWeek.Friday, 5);
            dayOfWeekToNumber.Add(DayOfWeek.Saturday, 6);

            this.ShowDialog();

            data = stickyData;
            return bSave;
        }

        private void btnSelectDialog_Click(object sender, EventArgs e)
        {
            if (lblFilename.Text != null && !lblFilename.Text.Equals(""))
            {
                ofdBackImage.InitialDirectory = lblFilename.Text.Substring(0, lblFilename.Text.LastIndexOf('\\')) + "\\";
                ofdBackImage.FileName = lblFilename.Text.Substring(lblFilename.Text.LastIndexOf('\\') + 1);
            }
            else
            {
                ofdBackImage.InitialDirectory = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf('\\')) + "\\";
            }
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofdBackImage.FilterIndex = 2;
            //タイトルを設定する
            ofdBackImage.Title = "開くファイルを選択してください";
            //ダイアログを表示する
            if (ofdBackImage.ShowDialog() == DialogResult.OK)
            {
                lblFilename.Text = ofdBackImage.FileName;
            }
        }

        /// <summary>
        /// 透明度設定バースクロールイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAlpha_Scroll(object sender, EventArgs e)
        {
            lblAlpha.Text = tbAlpha.Value.ToString() + "%";
        }

        /// <summary>
        /// クリアボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblFilename.Text = "";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            stickyData.alpha_stickies = tbAlpha.Value;

            stickyData.display_backimage_stickies = lblFilename.Text.Equals("") ? false : true;
            stickyData.display_backimage_path_stickies = lblFilename.Text;
            stickyData.display_backimage_type_stickies = cmbLayout.SelectedIndex;
            stickyData.display_backimage_region_stickies = cbRegion.Checked;
            stickyData.backcolor_stickies = lblColor.BackColor.R.ToString() + ", " + lblColor.BackColor.G.ToString() + ", " + lblColor.BackColor.B.ToString();
            // 時間起動チェックあり
            if (cbTimeTrigger.Checked == true)
            {
                stickyData.displayed = false;
                stickyData.time_trigger = true;
                stickyData.time_trigger_type = uttTimeTrigger.TriggerType;
                stickyData.time_trigger_dayofweek = uttTimeTrigger.DayOfWeek;
                // 種別が毎週なら
                if (stickyData.time_trigger_type == 3)
                {
                    // 設定した日過去曜日なら次週の該当曜日に移動
                    DateTime tempDate = DateTime.Now;
                    for (int i = 0; i < 7; i++)
                    {
                        if ((int)dayOfWeekToNumber[tempDate.DayOfWeek] == stickyData.time_trigger_dayofweek)
                        {
                            break;
                        }
                        tempDate.AddDays(1);
                    }
                    stickyData.time_trigger_datetime = DateTime.Parse(tempDate.ToShortDateString() + " " + uttTimeTrigger.Time.ToShortTimeString());
                    // 指定日時が現在日時より前なら
                    if (stickyData.time_trigger_datetime.CompareTo(DateTime.Now) <= 0)
                    {
                        stickyData.time_trigger_datetime.AddDays(7);
                    }
                }
                else
                {
                    stickyData.time_trigger_datetime = DateTime.Parse(uttTimeTrigger.Date.ToShortDateString() + " " + uttTimeTrigger.Time.ToShortTimeString());
                }
            }
            else {
                stickyData.time_trigger = false;
            }
            // 連想起動にチェックありなら
            if (cbAttachTrigger.Checked == true)
            {
                // 連想起動対象タイトルバー名称が設定あり
                if (uatAttachTrigger.AttachedTitle != null && !uatAttachTrigger.AttachedTitle.Equals(""))
                {
                    stickyData.displayed = false;
                    stickyData.attach_trigger = true;
                    stickyData.attach_trigger_title = uatAttachTrigger.AttachedTitle;
                }
                else
                {
                    stickyData.displayed = true;
                    stickyData.attach_trigger = false;
                    stickyData.attach_trigger_title = "";
                }
            }
            else {
                stickyData.attach_trigger = false;
            }
            // ウィンドウ貼付にチェックありなら
            if (cbSnapWindow.Checked == true)
            {
                // ウィンドウ貼付対象タイトルバー名称が設定あり
                if (uatSnapWindow.AttachedTitle != null && !uatSnapWindow.AttachedTitle.Equals(""))
                {
                    stickyData.displayed = true;
                    stickyData.snap_window = true;
                    stickyData.snap_window_title = uatSnapWindow.AttachedTitle;
                    stickyData.point_snap_window = uatSnapWindow.AttachedPoint.X + "," + uatSnapWindow.AttachedPoint.Y;
                }
                else
                {
                    stickyData.displayed = true;
                    stickyData.snap_window = false;
                    stickyData.snap_window_title = "";
                    stickyData.point_snap_window = "0,0";
                }
            }
            else
            {
                stickyData.snap_window = false;
            }
            stickyData.display_font_stickies = lblFont.Font;
            bSave = true;
            this.Close();
        }

        /// <summary>
        /// 保存せずに終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 色選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            cdColorDialog.AnyColor = true;
            cdColorDialog.Color = lblColor.BackColor;
            cdColorDialog.ShowDialog();
            lblColor.BackColor = cdColorDialog.Color;
        }

        /// <summary>
        /// 時限起動有効化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTimeTrigger_MouseClick(object sender, MouseEventArgs e)
        {

            if (cbTimeTrigger.Checked == true)
            {
                cbTimeTrigger.Checked = false;
                uttTimeTrigger.Visible = false;
            }
            else
            {
                cbTimeTrigger.Checked = true;
                uttTimeTrigger.Visible = true;
            }

            if (cbAttachTrigger.Checked == true)
            {
                cbAttachTrigger.Checked = false;
                uatAttachTrigger.Visible = false;
            }

            if (cbSnapWindow.Checked == true)
            {
                cbSnapWindow.Checked = false;
                uatSnapWindow.Visible = false;
            }
        }

        /// <summary>
        /// 連想起動有効化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAttachTrigger_MouseClick(object sender, MouseEventArgs e)
        {

            if (cbAttachTrigger.Checked == true)
            {
                cbAttachTrigger.Checked = false;
                uatAttachTrigger.Visible = false;
            }
            else
            {
                cbAttachTrigger.Checked = true;
                uatAttachTrigger.Visible = true;
            }

            if (cbTimeTrigger.Checked == true)
            {
                cbTimeTrigger.Checked = false;
                uttTimeTrigger.Visible = false;
            }

            if (cbSnapWindow.Checked == true)
            {
                cbSnapWindow.Checked = false;
                uatSnapWindow.Visible = false;
            }
        }

        /// <summary>
        /// ウィンドウ貼付有効化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSnapWindow_MouseClick(object sender, MouseEventArgs e)
        {

            if (cbSnapWindow.Checked == true)
            {
                cbSnapWindow.Checked = false;
                uatSnapWindow.Visible = false;
            }
            else
            {
                cbSnapWindow.Checked = true;
                uatSnapWindow.Visible = true;
            }

            if (cbTimeTrigger.Checked == true)
            {
                cbTimeTrigger.Checked = false;
                uttTimeTrigger.Visible = false;
            }

            if (cbAttachTrigger.Checked == true)
            {
                cbAttachTrigger.Checked = false;
                uatAttachTrigger.Visible = false;
            }
        }

        /// <summary>
        /// font選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = lblFont.Font;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                lblFont.Font = fd.Font;
            }
        }
    }
}
