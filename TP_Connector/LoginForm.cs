using System;
using System.Windows.Forms;
using static TP_Connector.common; // 기존 common 클래스 참조

namespace TP_Connector
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 레지스트리에서 정보 가져오기
            ServerIP.Text = common.GetRegData("ServerIP");
            ServerPort.Text = common.GetRegData("ServerPort");
            UserID.Text = common.GetRegData("UserId");

            // 텍스트 전체 선택 (표준 WinForms 방식)
            UserID.SelectAll();
            UserPwd.SelectAll();

            // 포커스 설정
            if (string.IsNullOrEmpty(ServerIP.Text))
                this.ActiveControl = ServerIP;
            else if (string.IsNullOrEmpty(ServerPort.Text))
                this.ActiveControl = ServerPort;
            else if (string.IsNullOrEmpty(UserID.Text))
                this.ActiveControl = UserID;
            else if (string.IsNullOrEmpty(UserPwd.Text))
                this.ActiveControl = UserPwd;
        }

        public int LoginProc(string server, string port, string userid, string pwd)
        {
            string url = "http://" + server + ":" + port + "/services/AddinIF?wsdl";

            try
            {
                common.ConnnectService(url);

                // 로그인 체크 로직 (기존 유지)
                bool lcheck = true;

                if (lcheck == false)
                {
                    common.DisConnectService();
                    MessageBox.Show("웹서비스에 연결할수가 없습니다.\n" + url, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else
                {
                    string localIP = common.GetLocalIPAddr();
                    // 웹서비스 호출 (기존 참조 유지)
                    AddInWebService.AddinImplService intf = common.GetService();
                    AddInWebService.userVO userInfo = intf.loginProcess(userid, pwd, "0002", localIP);

                    if (userInfo == null)
                    {
                        MessageBox.Show("로그인하지 못했습니다", "실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        common.DisConnectService();
                        return -1;
                    }

                    // 전역 변수 설정
                    GlobalClientService.userInfo = userInfo;
                    GlobalClientService.loginIP = localIP;

                    // 성공 시 정보 저장
                    common.SetRegData("", "ServerIP", server);
                    common.SetRegData("", "ServerPort", port);
                    common.SetRegData("", "UserId", userid);

                    return 0;
                }
            }
            catch (Exception se)
            {
                common.DisConnectService();
                MessageBox.Show(se.Message, "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            // 유효성 검사
            if (string.IsNullOrEmpty(ServerIP.Text))
            {
                MessageBox.Show("서버 IP 또는 서버 이름을 입력하십시요", "알림");
                ServerIP.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ServerPort.Text))
            {
                MessageBox.Show("서버 포트를 입력하십시요", "알림");
                ServerPort.Focus();
                return;
            }

            if (string.IsNullOrEmpty(UserID.Text))
            {
                MessageBox.Show("사용자 ID를 입력하십시요", "알림");
                UserID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(UserPwd.Text))
            {
                MessageBox.Show("암호를 입력하십시요", "알림");
                UserPwd.Focus();
                return;
            }

            // 로그인 시도
            if (LoginProc(ServerIP.Text, ServerPort.Text, UserID.Text, UserPwd.Text) != 0)
                return;

            MessageBox.Show("로그인 하였습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}