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
    public partial class CreateFolder : Form
    {
        // 입력된 폴더명을 반환하는 프로퍼티
        public string FolderName
        {
            get { return fldNmValue.Text; }
        }

        public CreateFolder()
        {
            InitializeComponent();
            this.btnConfirm.Click += BtnConfirm_Click; // 이벤트 핸들러 연결
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fldNmValue.Text))
            {
                MessageBox.Show("폴더명을 입력해주세요.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}