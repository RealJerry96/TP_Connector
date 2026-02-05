using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static TP_Connector.common;

namespace TP_Connector
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //MessageBox.Show("TP Connector 프로그램이 시작됩니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //MessageBox.Show("프로그램 실행 시 전달된 인자 수: " + (args != null ? args.Length.ToString() : "0"), "디버그", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                // [CASE 1] 웹에서 호출된 경우 (파라미터가 존재함)
                if (args.Length > 0)
                {
                    // 1. 프로토콜 헤더 제거 및 정리
                    string rawData = args[0].Replace("tpconnector://", "");

                    // 브라우저에 따라 끝에 슬래시(/)가 붙을 수 있으므로 제거
                    if (rawData.EndsWith("/"))
                    {
                        rawData = rawData.Substring(0, rawData.Length - 1);
                    }
                    //MessageBox.Show("전달된 원본 데이터: " + rawData, "디버그", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rawData = Uri.UnescapeDataString(rawData);
                    // 2. 구분자 {SWS}로 분리
                    // JS: serverUrl + "{SWS}" + serverPort + "{SWS}" + userId + "{SWS}##connector##"
                    string[] parts = rawData.Split(new string[] { "{SWS}" }, StringSplitOptions.None);

                    if (parts.Length >= 3)
                    {
                        string serverIP = parts[0];
                        string serverPort = parts[1];
                        string userId = parts[2];

                        //MessageBox.Show("서버: " + serverIP + "\n포트: " + serverPort + "\n사용자ID: " + userId, "디버그", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 3. 접속 정보 및 사용자 세션 설정
                        // (Main 폼이 레지스트리나 GlobalClientService를 참조하므로 미리 값을 넣어줍니다)

                        // 서버 정보 저장 (Main() 생성자에서 GetRegData로 읽어가므로 미리 세팅)
                        common.SetRegData("", "ServerIP", serverIP);
                        common.SetRegData("", "ServerPort", serverPort);

                        if (GlobalClientService.userInfo == null)
                        {
                            // userVO 객체를 새로 생성해야 합니다. (네임스페이스 경로는 프로젝트 상황에 맞춰질 수 있음)
                            GlobalClientService.userInfo = new TP_Connector.AddInWebService.userVO();
                        }

                        GlobalClientService.userInfo.userId = userId;
                        // 이름 정보가 파라미터에 없다면 ID로 대체하거나 빈값 처리
                        GlobalClientService.userInfo.userNm = userId;

                        // 4. 로그인 폼 없이 Main 바로 실행
                        // Main 생성자에 파라미터가 없다면 new Main()으로 호출
                        Application.Run(new Main());
                    }
                    else
                    {
                        MessageBox.Show("전달된 파라미터 형식이 올바르지 않습니다.");
                    }
                }
                // [CASE 2] 실행파일 직접 실행 (파라미터 없음)
                else
                {
                    LoginForm login = new LoginForm();

                    // 로그인 성공 시(DialogResult.Yes) Main 실행
                    if (login.ShowDialog() == DialogResult.Yes)
                    {
                        Application.Run(new Main());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("실행 중 오류가 발생했습니다: " + ex.Message);
            }
        }
    }
}