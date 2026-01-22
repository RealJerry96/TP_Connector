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







            // 스플리터를 얇게 그리기 위한 Paint 이벤트 처리기 추가
            this.splitContainerControl1.Paint += (s, e) =>
            {
                DevExpress.XtraEditors.SplitContainerControl control = s as DevExpress.XtraEditors.SplitContainerControl;
                if (control != null)
                {
                    // 1. 스플리터의 전체 영역을 가져옵니다 (보통 5~6px 정도 됨)
                    Rectangle r = control.SplitterBounds;

                    // 2. 그릴 두께 설정 (1px)
                    int lineThickness = 4;

                    // 3. 전체 영역의 정중앙에 1px 짜리 선이 오도록 좌표(X)와 너비(Width)를 조정
                    r.X += (r.Width - lineThickness) / 2;
                    r.Width = lineThickness;

                    // 4. 조정된 얇은 영역에만 색칠
                    // (Color.Silver나 Color.LightGray 추천)
                    using (Brush brush = new SolidBrush(Color.LightGray))
                    {
                        e.Graphics.FillRectangle(brush, r);
                    }
                }
            };

            InitSystemFolder();

            // 검색어 하이라이트를 위한 이벤트 등록
            tlFolder.CustomDrawNodeCell += TlFolder_CustomDrawNodeCell;
        }

        



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

                tlFolder.BeginUpdate();
                try
                {
                    tlFolder.DataSource = folderList;
                    tlFolder.KeyFieldName = "fldSeq";
                    tlFolder.ParentFieldName = "parFldSeq";
                    colFolder.FieldName = "fldNm";

                    SetTreeListIcon();

                    tlFolder.ForceInitialize();
                    tlFolder.ExpandToLevel(0);
                }
                finally
                {
                    tlFolder.EndUpdate();
                    //스크롤 가장 위로가게끔
                    tlFolder.TopVisibleNodeIndex = 0;
                }
            }
        }





        private void SetTreeListIcon()
        {
            if (tlFolder.SelectImageList != null) return;

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

            tlFolder.SelectImageList = imgList;
            tlFolder.GetSelectImage += (s, e) => { e.NodeImageIndex = 0; };
        }





        

        private void folderSearchButton_Click(object sender, EventArgs e)
        {
            // 이미 열려 있다면 닫고 새로 열거나, 혹은 그냥 포커스만 줄 수도 있음
            if (m_FolderSearchForm != null && !m_FolderSearchForm.IsDisposed)
            {
                m_FolderSearchForm.Close();
            }

            m_FolderSearchForm = new FolderSearch();

            // 메인 폼 위에 뜨도록 Owner 설정
            m_FolderSearchForm.Show(this);
        }

        /// <summary>
        /// 폴더명으로 트리의 노드를 검색하고 포커스 및 선택 처리합니다.
        /// (다음 찾기 기능 포함: 계속 호출 시 순환하며 다음 항목을 찾습니다.)
        /// </summary>
        /// <param name="searchText">검색할 폴더명</param>
        // void 에서 (int current, int total) 튜플 반환형으로 변경
        public (int current, int total) SearchAndFocusFolder(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return (0, 0);

            _currentSearchText = searchText; // 현재 검색어 저장
            tlFolder.Invalidate(); // 검색어가 변경되었으므로 즉시 다시 그리기 요청

            // 1. 트리 내 모든 노드를 순서대로 가져오기 (탐색 순서 정렬)
            var allNodes = GetAllNodesInOrder(tlFolder.Nodes);

            // 2. 검색어가 포함된 모든 노드를 필터링
            var matches = allNodes.Where(node =>
                node.GetDisplayText(colFolder).IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();

            if (matches.Count == 0)
            {
                XtraMessageBox.Show($"'{searchText}' 폴더를 찾을 수 없습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (0, 0); // 검색 결과 없음 반환
            }

            DevExpress.XtraTreeList.Nodes.TreeListNode targetNode = null;

            // 3. 현재 포커스된 노드 기준으로 '다음' 매칭 노드 찾기
            if (tlFolder.FocusedNode != null)
            {
                // 현재 포커스된 노드가 매칭 리스트에 있는지 확인
                int currentMatchIndex = matches.IndexOf(tlFolder.FocusedNode);

                if (currentMatchIndex >= 0)
                {
                    // (A) 현재 노드가 검색 결과 중 하나라면 -> 다음 인덱스 노드 선택 (마지막이면 처음으로 순환)
                    int nextIndex = (currentMatchIndex + 1) % matches.Count;
                    targetNode = matches[nextIndex];
                }
                else
                {
                    // (B) 현재 노드가 검색 결과가 아니라면 (사용자가 임의로 다른 곳을 클릭한 경우)
                    //     전체 노드 순서상 현재 노드보다 '뒤에' 있는 첫 번째 매칭 노드를 찾음
                    int currentFullIndex = allNodes.IndexOf(tlFolder.FocusedNode);

                    // 현재 위치보다 뒤에 있는 매칭 결과 찾기
                    targetNode = matches.FirstOrDefault(m => allNodes.IndexOf(m) > currentFullIndex);

                    // 뒤에 없으면 다시 처음부터 찾음 (순환)
                    if (targetNode == null)
                        targetNode = matches[0];
                }
            }
            else
            {
                // 4. 아무것도 선택되어 있지 않다면 첫 번째 결과 선택
                targetNode = matches[0];
            }

            // 5. 노드 포커싱 및 화면 표시
            if (targetNode != null)
            {
                tlFolder.FocusedNode = targetNode;
                tlFolder.Selection.Set(targetNode);
                tlFolder.MakeNodeVisible(targetNode);
            }

            // 현재 찾은 항목의 인덱스(1부터 시작)와 전체 검색 개수를 반환합니다.
            return (matches.IndexOf(targetNode) + 1, matches.Count);
        }

        /// <summary>
        /// 트리 리스트의 모든 노드를 화면 표시 순서(논리적 순서)대로 수집합니다.
        /// </summary>
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

        private string _currentSearchText = string.Empty;

        private void TlFolder_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentSearchText)) return;
            // colFolder 컬럼에서만 하이라이트 (필요 시 다른 컬럼도 포함 가능)
            if (e.Column != colFolder) return;

            string text = e.CellText;
            if (string.IsNullOrEmpty(text)) return;

            int index = text.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                e.Handled = true;

                // 1. 배경 그리기 (선택, 포커스 등 기본 배경)
                e.Appearance.DrawBackground(e.Cache, e.Bounds);

                // 2. 하이라이트 영역 계산 및 그리기
                // GetStringFormat()으로 가져온 객체의 원본을 보호하기 위해 Clone()을 사용합니다.
                using (StringFormat sf = (StringFormat)e.Appearance.GetStringFormat().Clone())
                {
                    CharacterRange[] ranges = { new CharacterRange(index, _currentSearchText.Length) };
                    sf.SetMeasurableCharacterRanges(ranges);

                    // GDI+ 측정
                    Region[] regions = e.Graphics.MeasureCharacterRanges(text, e.Appearance.Font, e.Bounds, sf);
                    if (regions.Length > 0)
                    {
                        RectangleF highlightRect = regions[0].GetBounds(e.Graphics);
                        // 노란색 배경 칠하기
                        e.Graphics.FillRectangle(Brushes.Yellow, highlightRect);
                    }

                    // 3. 텍스트 그리기 
                    // 수정됨: e.Appearance.DrawString 대신 e.Graphics.DrawString을 사용하여
                    // 측정에 사용된 StringFormat(sf)과 동일한 기준으로 텍스트를 그립니다.
                    // 이렇게 해야 배경 위치와 글자 위치가 정확히 일치합니다.
                    using (Brush textBrush = new SolidBrush(e.Appearance.ForeColor))
                    {
                        e.Graphics.DrawString(text, e.Appearance.Font, textBrush, e.Bounds, sf);
                        
                        
                    }
                }
            }
        }
        #endregion

        //탭관련 기본임
        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            Main form = new Main();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }

        static int OpenFormCount = 1;
    }
}
