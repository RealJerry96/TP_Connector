using System;
using System.Windows.Forms;

namespace TP_Connector
{
    public partial class FolderSearch : Form
    {
        public string SearchTerm { get; private set; }

        public FolderSearch()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            folderSearchButton(sender,e);
        }

        private void folderSearchButton(object sender, EventArgs e)
        {
            string inputText = txtFolderName.Text.Trim();
            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("폴더명을 입력해주세요.");
                txtFolderName.Focus();
                return;
            }

            if (this.Owner is Main mainForm)
            {
                var (current, total) = mainForm.SearchAndFocusFolder(inputText);
                if (total > 0)
                    searchCnt.Text = $"({current}/{total})";
                else
                    searchCnt.Text = "(0/0)";
            }
        }

        private void FolderSearch_Enter(object sender, EventArgs e)
        {
            folderSearchButton(sender,e);
        }
    }
}