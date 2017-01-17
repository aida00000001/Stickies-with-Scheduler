using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Collections;

namespace SwS
{
    public class SQLiteAccess
    {
        private string appPath = "";
        private SQLiteConnection cnn;
        private SQLiteCommand cmd;
        private SQLiteCommand cmdTran;
        private SQLiteDataReader reader;

        /// <summary>
        /// 環境設定
        /// </summary>
        /// <param name="basePath"></param>
        public void setEnviroment(string basePath, string filename)
        {
            appPath = basePath.Substring(0, basePath.LastIndexOf('\\'));
            cnn = new SQLiteConnection("Data Source=" + appPath + "\\" + filename);
            cmd = cnn.CreateCommand();

            // コネクションオープン
            cnn.Open();
        }

        /// <summary>
        /// データセットクローズ
        /// </summary>
        /// <param name="basePath"></param>
        public void readerClose()
        {
            // データセットクローズ
            if (reader != null)
            {
                reader.Close();
            }
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void disposeEnviroment()
        {
            // データセットクローズ
            if (reader != null)
            {
                reader.Close();
            }
            // コネクションクローズ
            if (cnn != null)
            {
                cnn.Close();
            }
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void beginTrans()
        {
            cmdTran = new SQLiteCommand("begin", cnn);
            cmdTran.ExecuteNonQuery();
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void endTrans()
        {
            cmdTran = new SQLiteCommand("commit", cnn);
            cmdTran.ExecuteNonQuery();
        }

        /// <summary>
        /// select処理
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public SQLiteDataReader select(string strSQL)
        {
            ArrayList result = new ArrayList();

            try
            {
                // SELECT文の実行
                cmd.CommandText = strSQL;
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return reader;
        }
        
        /// <summary>
        /// insert処理
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public Int32 insert(string strSQL, string selectTable)
        {
            Int32 iId = -1;

            try
            {
                // INSERT文の実行
                cmd.CommandText = strSQL;
                cmd.ExecuteNonQuery();

                if (selectTable != null)
                {
                    // SELECT文の実行
                    cmd.CommandText = "select max(id) from " + selectTable;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // 新規登録時は最大値を取得し管理値とする
                        iId = int.Parse(reader[0].ToString());
                    }
                    // リーダクローズ
                    readerClose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iId;
        }

        /// <summary>
        /// Update処理
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public void update(string strSQL)
        {
            try
            {
                // UPDATE文の実行
                cmd.CommandText = strSQL;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// データ定義処理
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public void execDDL(string strSQL)
        {
            update(strSQL);
        }
    }
}
