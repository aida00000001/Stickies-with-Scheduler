using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.SqlServer.Server;
using System.Net.Sockets;

namespace SwS
{
    // NtpClient 管理クラス
    public class NtpClient
    {
        /// <summary>
        /// NTPパケットを格納する構造体
        /// </summary>
        private struct NTP_Packet
        {
            public int Control_Word;
            public int root_delay;
            public int root_dispersion;
            public int reference_identifier;
            public Int64 reference_timestamp;
            public Int64 originate_timestamp;
            public Int64 receive_timestamp;
            public int transmit_timestamp_seconds;
            public int transmit_timestamp_fractions;
        };

        /// <summary>
        /// 時刻問い合わせ先サーバ名(FQDN)
        /// </summary>
        private string _server;

        /// <summary>
        /// コンストラクタ: インスタンス生成時にNTPサーバ(FQDN)を設定します
        /// </summary>
        /// <param name="server">NTPサーバ名(FQDN)</param>
        public NtpClient(string server)
        {
            _server = server;
        }

        /// <summary>
        /// NTP_Packet内のデータ全てをByte配列として取得します
        /// </summary>
        /// <param name="strct">NTPサーバから取得したデータを格納しているNTP_Packet</param>
        /// <returns>NTPパケットデータをByte配列に変換したデータ</returns>
        private byte[] convertStruct2Bytes(NTP_Packet strct)
        {
            // 各項目をbyteデータに変換します。
            byte[] dat1 = BitConverter.GetBytes(strct.Control_Word);
            byte[] dat2 = BitConverter.GetBytes(strct.root_delay);
            byte[] dat3 = BitConverter.GetBytes(strct.root_dispersion);
            byte[] dat4 = BitConverter.GetBytes(strct.reference_identifier);
            byte[] dat5 = BitConverter.GetBytes(strct.reference_timestamp);
            byte[] dat6 = BitConverter.GetBytes(strct.originate_timestamp);
            byte[] dat7 = BitConverter.GetBytes(strct.receive_timestamp);
            byte[] dat8 = BitConverter.GetBytes(strct.transmit_timestamp_seconds);
            byte[] dat9 = BitConverter.GetBytes(strct.transmit_timestamp_fractions);

            // 全項目を連結してByte列を生成します。
            byte[] data = appendBytes(dat1, dat2);
            data = appendBytes(data, dat3);
            data = appendBytes(data, dat4);
            data = appendBytes(data, dat5);
            data = appendBytes(data, dat6);
            data = appendBytes(data, dat7);
            data = appendBytes(data, dat8);
            data = appendBytes(data, dat9);
            return data;
        }

        /// <summary>
        /// byte配列の後ろに、byte配列を追加します
        /// </summary>
        /// <param name="data">追加先byte配列</param>
        /// <param name="add">追加用byte配列</param>
        /// <returns>新しい配列データ</returns>
        private byte[] appendBytes(byte[] data, byte[] add)
        {
            int len = data.Length + add.Length;
            byte[] newdata = new byte[len];
            int idx = 0;
            for (int i = 0; i < data.Length; i++)
            {
                newdata[idx++] = data[i];
            }
            for (int i = 0; i < add.Length; i++)
            {
                newdata[idx++] = add[i];
            }
            return newdata;
        }
        /// <summary>
        /// NTPサーバから現在の時刻を取得します
        /// </summary>
        /// <param name="newTime">取得した現在の時刻</param>
        /// <returns>true:処理成功時、false:処理失敗時</returns>
        public bool GetCurrentTime(ref DateTime newTime)
        {
            bool ret = false;
            DateTime dtTime = DateTime.Now;
            try
            {
                // IPAddress、IPEndPointは、リクエストを受け取る為のEND-POINTを表します。
                // DNSから指定したNTPサーバのIPアドレスを取得します。
                IPHostEntry list = System.Net.Dns.GetHostEntry(_server);
                IPAddress remoteAddr = list.AddressList[0];
                WriteLog("Server IP " + remoteAddr.ToString());
                IPEndPoint remotePoint = new IPEndPoint(remoteAddr, 123);
                EndPoint remoteEnd = (EndPoint)remotePoint;

                // ＋αTips
                // WindowsXPが使用している 123のポートから投げて123で取得する方法。
                // サービスの「Windows Time」を停止しておきます。123使用中のPGを停止します。
                // 停止していないと、エラー「"通常、各ソケット アドレスに対してプロトコル、ネットワーク アドレス、またはポートのどれか 1 つのみを使用できます。"」となります。
                // 因みに2005でも2002でも同じ。
                string localIpStr = "127.0.0.1";
                IPAddress localAddr = System.Net.Dns.GetHostEntry(localIpStr).AddressList[0];
                WriteLog("Local IP " + localAddr.ToString());
                IPEndPoint localPoint = new IPEndPoint(localAddr, 0);
                WriteLog("Local IP " + localPoint.ToString());
                EndPoint senderPoint = (EndPoint)localPoint;

                // UDPを使用して通信します。（ソケット生成オプション：UDPの場合はSocketType.Dgram）
                Socket s = new Socket(remotePoint.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                // タイムアウトを設定します（ミリ秒単位で最小500ミリ秒から。0,-1は無限）
                s.SendTimeout = 1000 * 10;
                s.ReceiveTimeout = 1000 * 10;
                // ローカルIPポートをソケットにバインド （→ ただし、開発環境が無いPCではエラーになってしまうのでbindしません...）
                //s.Bind(localPoint);

                // NTPサーバに時刻取得用コマンドリクエストを送信します。
                NTP_Packet dat;
                dat.Control_Word = 0xb;
                dat.root_delay = 0;
                dat.root_dispersion = 0;
                dat.reference_identifier = 0;
                dat.reference_timestamp = 0;
                dat.originate_timestamp = 0;
                dat.receive_timestamp = 0;
                dat.transmit_timestamp_seconds = 0;
                dat.transmit_timestamp_fractions = 0;
                byte[] data = convertStruct2Bytes(dat);
                int i = s.SendTo(data, SocketFlags.None, remotePoint);
                WriteLog("Sent " + i + " bytes.");

                // NTPサーバから時刻データを取得します。
                Int32 bytes = 0;
                Byte[] recvBytes = new Byte[50];
                bytes = s.ReceiveFrom(recvBytes, SocketFlags.None, ref senderPoint);
                s.Close();
                WriteLog("Receive: " + bytes + " bytes.");

                // ----------------------------------------------------------------------
                // NTPサーバから取得したデータから現在日時を計算します。
                // ----------------------------------------------------------------------
                if (bytes > 0)
                {
                    // 1970/1/1からの日数
                    double cuDays;
                    // 現在の時刻（何時か）
                    double cuHours;
                    // 現在の分
                    double cuMinutes;
                    // 時間算出用
                    double cuTempSecs;
                    //
                    double cuTimeStamp;

                    // 世界協定時刻時間に変換します。40-43バイト目で判別、ms単位は無視
                    double d1 = double.Parse("" + recvBytes[40]);
                    double d2 = double.Parse("" + recvBytes[41]);
                    double d3 = double.Parse("" + recvBytes[42]);
                    double d4 = double.Parse("" + recvBytes[43]);
                    cuTimeStamp = (double)(d1 * Math.Pow(2, 8 * 3) + d2 * Math.Pow(2, 8 * 2) + d3 * Math.Pow(2, 8) + d4);

                    // NTPサーバから取得した時刻: 1900/1/1からの時間を1970/1/1からの基準に合わせます。
                    cuTimeStamp -= 2208988800L;

                    // 日付: 24hours x 60minutes x 60seconds
                    cuDays = cuTimeStamp / (24 * (60 * 60));
                    // 日付のあまり: 本日経過した秒数
                    cuTempSecs = cuTimeStamp % (24 * (60 * 60));

                    // 時間: 60m x 60s
                    cuHours = cuTempSecs / (60 * 60);
                    // 時間のあまり秒: 残りの'分'以下
                    cuTempSecs = cuTempSecs % (60 * 60);

                    // 何分かの計算
                    cuMinutes = cuTempSecs / 60;
                    // その残りが秒数
                    cuTempSecs = cuTempSecs % 60;

                    // 1970/1/1に算出した日数を加算し、時/分/秒を連結して現在日時とします。
                    dtTime = DateTime.Parse(string.Format("{0:yyyy/MM/dd} {1:00}:{2:00}:{3:00}", new DateTime(1970, 01, 01).AddDays((double)cuDays), (int)cuHours, (int)cuMinutes, (int)cuTempSecs));

                    // 日本時刻へ変更します。 GMT+9時間
                    newTime = dtTime = dtTime.AddHours(9);

                    string tm = dtTime.ToString("yyyy/MM/dd hh:mm:ss");

                    DateTime tmpTime = DateTime.Now;
                    string aaaa = tmpTime.ToString("yyyy/MM/dd hh:mm:ss");

                    WriteLog("時刻: " + tm);

                    ret = true;
                }
                else
                {
                    WriteLog("時刻 修正なし");
                }
            }
            catch (Exception e)
            {
                string tm = e.Message + "\rHttp\n";
                tm += e.StackTrace;
                WriteLog(tm);
            }
            return ret;
        }

        // ログ出力メソッド
        private void WriteLog(string p)
        {
            // throw new NotImplementedException();
        }
    }
}
