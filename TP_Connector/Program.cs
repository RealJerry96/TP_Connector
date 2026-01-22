using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TP_Connector
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm login = new LoginForm();

            // ShowDialog()는 창이 닫힐 때까지 코드 실행을 여기서 대기합니다.
            // LoginForm에서 로그인이 성공하면 this.DialogResult = DialogResult.Yes; 로 설정하고 닫아야 합니다.
            if (login.ShowDialog() == DialogResult.Yes)
            {
                Application.Run(new Main());
            }
        }
    }
}