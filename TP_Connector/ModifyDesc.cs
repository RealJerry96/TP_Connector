using System;
using System.Windows.Forms;

namespace TP_Connector
{
    // [변경] UserControl -> Form 상속으로 변경
    public partial class ModifyDesc : Form
    {
        // 입력된 수정사항을 외부에서 가져갈 속성
        public string DescriptionText { get; private set; } = string.Empty;

        public ModifyDesc()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 입력값 저장 후 다이얼로그 OK 결과 반환
            DescriptionText = txtDescription.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 다이얼로그 취소 결과 반환
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}