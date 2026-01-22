using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_Connector
{
    public partial class FolderSearch : DevExpress.XtraEditors.XtraForm
    {
        // 부모 폼에서 입력된 폴더명을 가져갈 수 있도록 프로퍼티 정의
        public string SearchTerm { get; private set; }

        public FolderSearch()
        {
            InitializeComponent();
            this.AcceptButton = btnConfirm;

        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            // 텍스트박스(txtFolderName)에서 입력값 가져오기
            string inputText = txtFolderName.Text.Trim();

            // 유효성 검사
            if (string.IsNullOrEmpty(inputText))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("폴더명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFolderName.Focus();
                return;
            }

            // 입력값을 프로퍼티에 저장
            this.SearchTerm = inputText;

            // Owner를 Main으로 캐스팅하여 검색 메서드 호출
            if (this.Owner is Main mainForm)
            {
                // 변경된 메서드로부터 (현재순번, 전체개수)를 받아옵니다.
                var (current, total) = mainForm.SearchAndFocusFolder(inputText);

                // 라벨에 (현재 순번/전체 개수) 형식으로 텍스트 설정
                if (total > 0)
                {
                    searchCnt.Text = $"({current}/{total})";
                }
                else
                {
                    searchCnt.Text = "(0/0)";
                }
            }
        }


    }
}