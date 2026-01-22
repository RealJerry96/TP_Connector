using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList; // TreeList 관련 네임스페이스 추가
using static TP_Connector.common;

namespace TP_Connector
{
    public partial class Main : DevExpress.XtraBars.TabForm
    {
        private string m_sCompanyUrl = "http://teamplus9.com:8080";
        private string userId = GlobalClientService.userInfo.userId;
        private string userNm = GlobalClientService.userInfo.userNm;
        private FolderSearch m_FolderSearchForm = null;

        // Fields defined to resolve compilation errors
        private DevExpress.XtraBars.BarStaticItem toolStripUserName;
        private DevExpress.XtraEditors.LabelControl LoginUserLabel;

        // 현재 검색어를 저장하는 변수
        private string _currentSearchText = string.Empty;

        public Main()
        {
            InitializeComponent();

            string url = m_sCompanyUrl + "/services/AddinIF?wsdl";
            try { common.ConnnectService(url); }
            catch (Exception se) { XtraMessageBox.Show(this, se.Message); }

            if (GlobalClientService.userInfo != null)
            {
                this.barLabel.Caption = $"{GlobalClientService.userInfo.userNm}({GlobalClientService.userInfo.userId}) 님이 로그인 하였습니다.";
            }

            // 1. 스플리터 디자인(Paint 이벤트) 연결 - 두 탭 모두 적용
            this.splitContainerControl1.Paint += SplitContainerControl_Paint;
            this.splitContainerControl2.Paint += SplitContainerControl_Paint;

            // 2. 시스템 폴더 데이터 초기화 (두 트리 모두 바인딩)
            InitSystemFolder();

            // 3. 검색어 하이라이트 이벤트 연결 - 두 트리 모두 적용
            tlFolder.CustomDrawNodeCell += TlFolder_CustomDrawNodeCell;
            tlFolder2.CustomDrawNodeCell += TlFolder_CustomDrawNodeCell;

            // (참고) 만약 tlData 클릭 이벤트 등 추가 로직이 생긴다면 여기서 tlData2에도 똑같이 연결해주면 됩니다.
        }

        #region 스플리터 디자인 (공통 이벤트 핸들러)
        private void SplitContainerControl_Paint(object sender, PaintEventArgs e)
        {
            DevExpress.XtraEditors.SplitContainerControl control = sender as DevExpress.XtraEditors.SplitContainerControl;
            if (control != null)
            {
                // 1. 스플리터의 전체 영역을 가져옵니다
                Rectangle r = control.SplitterBounds;

                // 2. 그릴 두께 설정 (4px)
                int lineThickness = 4;

                // 3. 전체 영역의 정중앙에 선이 오도록 좌표 조정
                r.X += (r.Width - lineThickness) / 2;
                r.Width = lineThickness;

                // 4. 조정된 영역에 색칠
                using (Brush brush = new SolidBrush(Color.LightGray))
                {
                    e.Graphics.FillRectangle(brush, r);
                }
            }
        }
        #endregion

        #region 폴더 트리 초기화 및 검색 기능
        private void InitSystemFolder()
        {
            var folderSearchTO = new AddInWebService.folderSearchTO();
            var pagingSearchTO = new AddInWebService.pagingSearchTO();

            pagingSearchTO.curPage = 1;
            pagingSearchTO.pageSize = 5000;
            folderSearchTO.permissionUserId = userId;

            var fldCount = common.m_intf.getFolderTotalCnt(folderSearchTO);

            if (fldCount == 0)
            {
                MessageBox.Show("폴더가 존재하지 않습니다.");
                return;
            }
            else
            {
                var folderList = common.m_intf.getFolderList(folderSearchTO, pagingSearchTO);

                // 탭1 트리 설정
                BindDataToTree(tlFolder, colFolder, folderList);

                // 탭2 트리 설정 (동일한 데이터 바인딩)
                BindDataToTree(tlFolder2, colFolder2, folderList);

                SetTreeListIcon(tlFolder);
                SetTreeListIcon(tlFolder2);
            }
        }

        // 중복 코드를 줄이기 위한 바인딩 헬퍼 함수
        private void BindDataToTree(DevExpress.XtraTreeList.TreeList tree, DevExpress.XtraTreeList.Columns.TreeListColumn col, object dataSource)
        {
            tree.BeginUpdate();
            try
            {
                tree.DataSource = dataSource;
                tree.KeyFieldName = "fldSeq";
                tree.ParentFieldName = "parFldSeq";
                col.FieldName = "fldNm";

                tree.ForceInitialize();
                tree.ExpandToLevel(0);
                tree.TopVisibleNodeIndex = 0;
            }
            finally
            {
                tree.EndUpdate();
            }
        }

        private void SetTreeListIcon(DevExpress.XtraTreeList.TreeList tree)
        {
            if (tree.SelectImageList != null) return;

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(16, 16);

            Bitmap folderIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(folderIcon))
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(Brushes.Goldenrod, 1, 2, 6, 10);
                g.FillRectangle(Brushes.Gold, 1, 4, 14, 10);
                g.DrawRectangle(Pens.DarkGoldenrod, 1, 4, 13, 9);
            }
            imgList.Images.Add(folderIcon);

            tree.SelectImageList = imgList;
            tree.GetSelectImage += (s, e) => { e.NodeImageIndex = 0; };
        }

        private void folderSearchButton_Click(object sender, EventArgs e)
        {
            if (m_FolderSearchForm != null && !m_FolderSearchForm.IsDisposed)
            {
                m_FolderSearchForm.Close();
            }

            m_FolderSearchForm = new FolderSearch();
            m_FolderSearchForm.Show(this);
        }

        /// <summary>
        /// 현재 활성화된 탭의 트리를 대상으로 검색을 수행합니다.
        /// </summary>
        public (int current, int total) SearchAndFocusFolder(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return (0, 0);

            _currentSearchText = searchText;

            // 현재 활성화된 탭에 따라 검색 대상 트리와 컬럼 결정
            DevExpress.XtraTreeList.TreeList targetTree;
            DevExpress.XtraTreeList.Columns.TreeListColumn targetCol;

            if (this.tabFormControl1.SelectedPage == this.tabFormPage2) // 신규등록 탭
            {
                targetTree = tlFolder2;
                targetCol = colFolder2;
            }
            else // 기본 탭 (체크인/체크아웃)
            {
                targetTree = tlFolder;
                targetCol = colFolder;
            }

            targetTree.Invalidate(); // 다시 그리기 (하이라이트 적용)

            // 1. 트리 내 모든 노드 수집
            var allNodes = GetAllNodesInOrder(targetTree.Nodes);

            // 2. 검색어 매칭 노드 필터링
            var matches = allNodes.Where(node =>
                node.GetDisplayText(targetCol).IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();

            if (matches.Count == 0)
            {
                XtraMessageBox.Show($"'{searchText}' 폴더를 찾을 수 없습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (0, 0);
            }

            DevExpress.XtraTreeList.Nodes.TreeListNode resultNode = null;

            // 3. 다음 찾기 로직
            if (targetTree.FocusedNode != null)
            {
                int currentMatchIndex = matches.IndexOf(targetTree.FocusedNode);

                if (currentMatchIndex >= 0)
                {
                    // 현재 노드가 매칭된 노드라면 다음 노드로 이동 (순환)
                    int nextIndex = (currentMatchIndex + 1) % matches.Count;
                    resultNode = matches[nextIndex];
                }
                else
                {
                    // 현재 노드가 매칭 노드가 아니라면, 현재 위치 이후의 첫 매칭 노드 검색
                    int currentFullIndex = allNodes.IndexOf(targetTree.FocusedNode);
                    resultNode = matches.FirstOrDefault(m => allNodes.IndexOf(m) > currentFullIndex);

                    // 없으면 처음부터
                    if (resultNode == null)
                        resultNode = matches[0];
                }
            }
            else
            {
                resultNode = matches[0];
            }

            // 4. 포커스 이동
            if (resultNode != null)
            {
                targetTree.FocusedNode = resultNode;
                targetTree.Selection.Set(resultNode);
                targetTree.MakeNodeVisible(resultNode);
            }

            return (matches.IndexOf(resultNode) + 1, matches.Count);
        }

        private List<DevExpress.XtraTreeList.Nodes.TreeListNode> GetAllNodesInOrder(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes)
        {
            var list = new List<DevExpress.XtraTreeList.Nodes.TreeListNode>();
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in nodes)
            {
                list.Add(node);
                if (node.HasChildren)
                {
                    list.AddRange(GetAllNodesInOrder(node.Nodes));
                }
            }
            return list;
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            if (m_FolderSearchForm != null && !m_FolderSearchForm.IsDisposed)
            {
                m_FolderSearchForm.Close();
            }
        }

        private void TlFolder_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentSearchText)) return;

            // 현재 그려지는 컬럼이 폴더명 컬럼인지 확인 (탭1의 컬럼 또는 탭2의 컬럼)
            if (e.Column != colFolder && e.Column != colFolder2) return;

            string text = e.CellText;
            if (string.IsNullOrEmpty(text)) return;

            int index = text.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                e.Handled = true;

                // 1. 배경 그리기
                e.Appearance.DrawBackground(e.Cache, e.Bounds);

                // 2. 하이라이트 그리기
                using (StringFormat sf = (StringFormat)e.Appearance.GetStringFormat().Clone())
                {
                    CharacterRange[] ranges = { new CharacterRange(index, _currentSearchText.Length) };
                    sf.SetMeasurableCharacterRanges(ranges);

                    Region[] regions = e.Graphics.MeasureCharacterRanges(text, e.Appearance.Font, e.Bounds, sf);
                    if (regions.Length > 0)
                    {
                        RectangleF highlightRect = regions[0].GetBounds(e.Graphics);
                        e.Graphics.FillRectangle(Brushes.Yellow, highlightRect);
                    }

                    // 3. 텍스트 그리기
                    using (Brush textBrush = new SolidBrush(e.Appearance.ForeColor))
                    {
                        e.Graphics.DrawString(text, e.Appearance.Font, textBrush, e.Bounds, sf);
                    }
                }
            }
        }
        #endregion

        // 탭 폼 기본 설정
        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            Main form = new Main();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }

        static int OpenFormCount = 1;



        #region 두번째 탭 기능 구현
        private void btnConfirm_Click(object sender, EventArgs e)
        {


        }

        #endregion
    }
}