using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Services;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using static TP_Connector.common;

namespace TP_Connector
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ServerIP.Text = common.GetRegData("ServerIP");
            ServerPort.Text = common.GetRegData("ServerPort");
            UserID.Text = common.GetRegData("UserId");
            UserID.Select(0, -1);
            UserPwd.Select(0, -1);

            if (ServerIP.Text == "")
                this.ActiveControl = ServerIP;
            else if (ServerPort.Text == "")
                this.ActiveControl = ServerPort;
            else if (UserID.Text == "")
                this.ActiveControl = UserID;
            else if (UserPwd.Text == "")
                this.ActiveControl = UserPwd;
        }

        public int LoginProc(string server, string port, string userid, string pwd)
        {
            string url = "http://" + server + ":" + port + "/services/AddinIF?wsdl";

            try
            {
                common.ConnnectService(url);
                // Check User/Pwd
                bool lcheck = true;

                if (lcheck == false)
                {
                    common.DisConnectService();
                    XtraMessageBox.Show("웹서비스에 연결할수가 없습니다. " + url);
                    return -1;
                }
                else
                {
                    string localIP = common.GetLocalIPAddr();
                    AddInWebService.AddinImplService intf = common.GetService();
                    AddInWebService.userVO userInfo = intf.loginProcess(userid, pwd, "0002", localIP);

                    if (userInfo == null)
                    {
                        XtraMessageBox.Show("로그인하지 못했습니다");
                        common.DisConnectService();
                        return -1;
                    }

                    GlobalClientService.userInfo = userInfo;
                    GlobalClientService.loginIP = localIP;

                    common.SetRegData("", "ServerIP", server);
                    common.SetRegData("", "ServerPort", port);
                    common.SetRegData("", "UserId", userid);

                    return 0;
                }
            }
            catch (Exception se)
            {
                common.DisConnectService();
                XtraMessageBox.Show(se.Message.ToString());
                return -1;
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (ServerIP.Text == "")
            {
                XtraMessageBox.Show("서버 IP 또는 서버 이름을 입력하십시요");
                return;
            }

            if (ServerPort.Text == "")
            {
                XtraMessageBox.Show("서버 포트를 입력하십시요");
                return;
            }


            if (UserID.Text == "")
            {
                XtraMessageBox.Show("사용자 ID를 입력하십시요");
                return;
            }

            if (UserPwd.Text == "")
            {
                XtraMessageBox.Show("암호를 입력하십시요");
                return;
            }

            if (LoginProc(ServerIP.Text, ServerPort.Text, UserID.Text, UserPwd.Text) != 0)
                return;

            XtraMessageBox.Show("로그인 하였습니다.");

            DialogResult = DialogResult.Yes;

            this.Close();


            //이후 폼전환은 program.cs에서 진행


        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }
    }
}
