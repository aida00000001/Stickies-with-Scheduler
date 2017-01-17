using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace SwS
{
    static class Program
    {
        private static Mutex mutex;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            mutex = new Mutex(false, "Stickies with Scheduler Debug");        // Mutex 生成 ; false = 所有権なし
#else
            mutex = new Mutex(false, "Stickies with Scheduler");        // Mutex 生成 ; false = 所有権なし
#endif
            if (!mutex.WaitOne(0, false))                               // Mutex 取得 ; false = 再取得なし
            {
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmCalendar());

                // ミューテックスを解放する
                mutex.ReleaseMutex();
                // ミューテックスを破棄する
                mutex.Close();
            }
        }
    }
}
