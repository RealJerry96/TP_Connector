using BrightIdeasSoftware; // ObjectListView 필수
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TP_Connector.AddInWebService;
using static System.Net.WebRequestMethods;
using static TP_Connector.common;
using File = System.IO.File;
using TextBox = System.Windows.Forms.TextBox;

namespace TP_Connector
{
    public partial class Main : Form
    {
        // 검색용 변수
        private string _currentSearchText = string.Empty;
        private FolderSearch m_FolderSearchForm = null;

        private string m_sCompanyUrl;

        private string userId = GlobalClientService.userInfo.userId;
        private string userPwd = GlobalClientService.userInfo.userNm;

        private string userNm;

        private int g_clsSeq = 0;

        private List<AddInWebService.attributeVO> m_AttributeList;

        public string CHECKOUT_ROOT = @"c:\teamplus\tpclient\checkedoutfile\";
        string DOWNLOAD_ROOT = @"c:\teamplus\tpclient\downloadedfile\";

        private ImageList _folderImageList;
        // 파일 아이콘용 이미지 리스트
        private ImageList _fileImageList;

        // 자식 노드 관리를 위한 클래스 멤버 변수
        private Dictionary<int, List<AddInWebService.objectVO>> m_ChildMap = new Dictionary<int, List<AddInWebService.objectVO>>();
        // 로컬에서 추가된 새 항목의 임시 ID (중복 방지를 위해 음수 사용)
        private int _tempIdCounter = -1;

        public Main()
        {

            string serverIP = common.GetRegData("ServerIP");
            string serverPort = common.GetRegData("ServerPort");

            if (!string.IsNullOrEmpty(serverIP) && !string.IsNullOrEmpty(serverPort))
            {
                m_sCompanyUrl = "http://" + serverIP + ":" + serverPort;
            }

            // 폴더 이미지 리스트 초기화
            InitializeFolderImageList();
            InitializeComponent(); // Designer.cs에 정의된 메서드 호출


            string url = m_sCompanyUrl + "/services/AddinIF?wsdl";
            try { common.ConnnectService(url); }
            catch (Exception se) { MessageBox.Show(this, se.Message); }


            statusLabel.Text = $"{GlobalClientService.userInfo.userNm}({GlobalClientService.userInfo.userId}) 님이 로그인 하였습니다. || " + m_sCompanyUrl;

            InitializeTreeListViews(); // 트리 설정 초기화

            //신규등록할때 사용함/////
            InitializeNewAddGrid();
            InitializeLocalGrid();
            InitializeTlvDataDelete();
            //////////////////////

            // 우측 그리드 컬럼 설정 (필요에 따라 실제 필드명으로 수정 필요)
            SetColumn(tlvData, "도면명", "objNm", 300);
            OLVColumn versionCol = new OLVColumn("버전", "version");
            versionCol.Width = 80;
            versionCol.TextAlign = HorizontalAlignment.Center;
            versionCol.AspectGetter = delegate (object row)
            {
                if (row is objectVO obj)
                {
                    // "revision.version" 형식으로 문자열 반환 (예: 1.0)
                    return $"{obj.revision}.{obj.version}";
                }
                return "";
            };
            tlvData.Columns.Add(versionCol);
            SetColumn(tlvData, "상태", "objStateCommNm", 80);
            SetColumn(tlvData, "최종수정자", "updId", 80);
            SetColumn(tlvData, "최종수정일", "regDt", 180);

            // 파일 다운로드 유무 컬럼
            OLVColumn downloadStateCol = new OLVColumn("파일다운로드유무", "isDownloaded");
            downloadStateCol.Width = 120;
            downloadStateCol.TextAlign = HorizontalAlignment.Center;
            downloadStateCol.AspectGetter = delegate (object row)
            {
                if (row is objectVO obj)
                {
                    // 1. 체크아웃 루트 폴더가 없으면 무조건 X
                    if (!System.IO.Directory.Exists(CHECKOUT_ROOT))
                        return "X";

                    try
                    {
                        // 2. 해당 폴더에서 objSeq로 시작하는 파일 검색
                        // (정확한 파일명을 모르므로 패턴 매칭 사용)
                        string searchPattern = $"{obj.objSeq}_*";
                        var files = System.IO.Directory.GetFiles(CHECKOUT_ROOT, searchPattern);

                        return (files.Length > 0) ? "O" : "X";
                    }
                    catch
                    {
                        return "X";
                    }
                }
                return "";
            };
            tlvData.Columns.Add(downloadStateCol);

            // tlvData 편집 설정 (newAddGrid와 동일하게 적용)
            tlvData.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
            tlvData.CellEditUseWholeCell = true;

            // 에디터 위치 및 스타일 조정 + [중요] 편집 불가 컬럼 방어 로직 추가
            tlvData.CellEditStarting += (s, e) =>
            {
                // 1. 해당 컬럼이 편집 가능한지 확인 (IsEditable이 false면 편집 취소)
                if (!e.Column.IsEditable)
                {
                    e.Cancel = true;
                    return;
                }

                // 2. 편집 가능한 경우, 컨트롤 스타일 설정
                if (e.Control != null)
                {
                    e.Control.Bounds = e.CellBounds;
                    if (e.Control is TextBox tb)
                    {
                        tb.TextAlign = HorizontalAlignment.Center;
                        if (e.Value != null)
                            tb.Text = e.Value.ToString();
                    }
                }
            };

            // 편집 완료 후 갱신
            tlvData.CellEditFinished += (s, e) =>
            {
                if (!e.Cancel)
                {
                    tlvData.RefreshItem(e.ListViewItem);
                }
            };

            tlvData.RowFormatter = delegate (OLVListItem item)
            {
                // 바인딩된 데이터 객체 가져오기 (objectVO라고 가정)
                if (item.RowObject is objectVO obj)
                {
                    // 작성하신 SetListColor 함수를 이용하여 색상 결정
                    item.ForeColor = SetListColor(obj.objStateCommCd);
                }
            };

            tlvData.UseCellFormatEvents = true;

            //InitCols로 추가된 동적 컬럼(속성 데이터)은 무조건 검은색으로 표시
            tlvData.FormatCell += (sender, e) =>
            {
                // m_AttributeList가 로드되었고 컬럼 정보가 유효한지 확인
                if (m_AttributeList != null && e.Column != null)
                {
                    // 현재 렌더링 중인 컬럼이 InitCols에서 가져온 속성(attrId)인지 확인
                    // 동적 컬럼 생성 시 AspectName을 attrId로 설정했으므로 이를 비교
                    bool isDynamicColumn = m_AttributeList.Any(attr => attr.attrId == e.Column.AspectName);

                    if (isDynamicColumn)
                    {
                        // 행의 색상(RowFormatter)을 무시하고 해당 셀만 검은색으로 설정
                        e.SubItem.ForeColor = Color.Black;
                    }
                }
            };

            //신규등록
            SetColumn(newAddGrid, "도면명", "objNm", 400);

            // 1. 3D 클래스의 속성 정보 가져오기
            List<AddInWebService.attributeVO> attrList;
            int clsSeq;
            InitCols(out attrList, out clsSeq);

            m_AttributeList = attrList;

            // 2. 가져온 속성만큼 컬럼 동적 추가
            if (attrList != null && attrList.Count > 0)
            {
                foreach (var att in attrList)
                {
                    // attrNm(화면표시명)을 헤더로 사용, 없으면 attrId 사용
                    string headerName = !string.IsNullOrEmpty(att.attrNm) ? att.attrNm : att.attrId;
                    string key = att.attrId; // 클로저 캡처 방지용 로컬 변수

                    // -------------------------------------------------------------
                    // [newAddGrid] 컬럼 생성 (기존 유지)
                    // -------------------------------------------------------------
                    OLVColumn col = new OLVColumn(headerName, key);
                    col.Width = 100;
                    col.TextAlign = HorizontalAlignment.Center;
                    col.IsEditable = true; // 편집 가능 설정

                    // [Getter] 딕셔너리에서 값 가져오기
                    col.AspectGetter = delegate (object row)
                    {
                        if (row is FileNode node && node.Attributes != null && node.Attributes.ContainsKey(key))
                        {
                            return node.Attributes[key];
                        }
                        return "";
                    };

                    // [Putter] 딕셔너리에 값 저장하기
                    col.AspectPutter = delegate (object row, object value)
                    {
                        if (row is FileNode node)
                        {
                            if (node.Attributes == null) node.Attributes = new Dictionary<string, string>();
                            string val = value?.ToString() ?? "";
                            if (node.Attributes.ContainsKey(key)) node.Attributes[key] = val;
                            else node.Attributes.Add(key, val);
                        }
                    };
                    newAddGrid.Columns.Add(col);


                    // -------------------------------------------------------------
                    // [tlvData] 컬럼 생성 (추가)
                    // objectVO를 사용하므로 배열(arrStrKey/arrStrValue) 접근 방식 사용
                    // -------------------------------------------------------------
                    OLVColumn tlvCol = new OLVColumn(headerName, key);
                    tlvCol.Width = 100;
                    tlvCol.TextAlign = HorizontalAlignment.Center;
                    tlvCol.IsEditable = true;

                    // [Getter] objectVO 배열에서 값 찾기
                    tlvCol.AspectGetter = delegate (object row)
                    {
                        if (row is AddInWebService.objectVO obj)
                        {
                            if (obj.arrStrKey != null && obj.arrStrValue != null)
                            {
                                int idx = Array.IndexOf(obj.arrStrKey, key);
                                if (idx >= 0 && idx < obj.arrStrValue.Length)
                                {
                                    return obj.arrStrValue[idx];
                                }
                            }
                        }
                        return "";
                    };

                    // [Putter] objectVO 배열에 값 저장
                    tlvCol.AspectPutter = delegate (object row, object value)
                    {
                        if (row is AddInWebService.objectVO obj)
                        {
                            string val = value?.ToString() ?? "";

                            // 배열 초기화
                            if (obj.arrStrKey == null) obj.arrStrKey = new string[0];
                            if (obj.arrStrValue == null) obj.arrStrValue = new string[0];

                            int idx = Array.IndexOf(obj.arrStrKey, key);
                            if (idx >= 0)
                            {
                                // 기존 값 업데이트
                                obj.arrStrValue[idx] = val;
                            }
                            else
                            {
                                // 신규 값 추가 (배열 크기 증가)
                                List<string> kList = obj.arrStrKey.ToList();
                                List<string> vList = obj.arrStrValue.ToList();
                                kList.Add(key);
                                vList.Add(val);
                                obj.arrStrKey = kList.ToArray();
                                obj.arrStrValue = vList.ToArray();
                            }
                        }
                    };
                    tlvData.Columns.Add(tlvCol);
                }
            }

        }

        private void InitCols(out List<AddInWebService.attributeVO> attributeList, out int iClsSeq)
        {
            attributeList = new List<AddInWebService.attributeVO>();
            iClsSeq = -1;

            try
            {
                // 클래스 목록 조회
                AddInWebService.classSearchTO schTo = new AddInWebService.classSearchTO();
                AddInWebService.classVO[] clsInfo = common.m_intf.getClassList(schTo);

                if (clsInfo == null) return;

                // "3D" 클래스 찾기
                for (int i = 0; i < clsInfo.Length; i++)
                {
                    if (clsInfo[i].clsNm == "3D")
                    {
                        iClsSeq = clsInfo[i].clsSeq;
                        break;
                    }
                }

                if (iClsSeq == -1) return; // 3D 클래스 없음

                g_clsSeq = iClsSeq; // 전역 변수 업데이트

                // 해당 클래스의 속성 조회
                AddInWebService.clsAttrRelSearchTO attSchTo = new AddInWebService.clsAttrRelSearchTO();
                attSchTo.clsSeq = iClsSeq;
                attSchTo.clsSeqSpecified = true;

                AddInWebService.attributeVO[] atts = common.m_intf.getAttributeIncludeClassList(attSchTo);

                if (atts != null)
                {
                    foreach (AddInWebService.attributeVO att in atts)
                    {
                        // 중복 방지 (attrId 기준)
                        if (!attributeList.Any(a => a.attrId == att.attrId))
                        {
                            attributeList.Add(att);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 통신 오류 등이 발생할 경우 처리
                Console.WriteLine($"InitCols Error: {ex.Message}");
            }
        }

        // 폴더 아이콘 생성 및 ImageList 설정
        private void InitializeFolderImageList()
        {
            _folderImageList = new ImageList();
            _folderImageList.ImageSize = new Size(16, 16); // 아이콘 크기
            _folderImageList.ColorDepth = ColorDepth.Depth32Bit;

            // 리소스 파일이 없을 경우를 대비해 코드로 간단한 폴더 아이콘(노란색) 생성
            Bitmap folderIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(folderIcon))
            {
                g.Clear(Color.Transparent);
                // 폴더 탭 (좌측 상단)
                g.FillRectangle(Brushes.Goldenrod, 1, 1, 7, 3);
                // 폴더 본체
                g.FillRectangle(Brushes.Gold, 1, 3, 14, 11);
                // 테두리
                g.DrawRectangle(Pens.DarkGoldenrod, 1, 3, 13, 10);
            }

            // "folder"라는 키로 이미지 추가
            _folderImageList.Images.Add("folder", folderIcon);
        }


        private void InitializeNewAddGrid()
        {
            // 자식 노드가 있으면 [+] 버튼 활성화
            newAddGrid.CanExpandGetter = x => ((FileNode)x).Children.Count > 0;
            // 자식 노드 리스트 반환
            newAddGrid.ChildrenGetter = x => ((FileNode)x).Children;

            // [설정 1] 더블클릭 시 편집 모드 활성화 및 셀 전체 사용
            newAddGrid.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
            newAddGrid.CellEditUseWholeCell = true;

            // [설정 2] 에디터(TextBox) 크기 및 위치 조정
            newAddGrid.CellEditStarting += (s, e) =>
            {
                if (e.Control != null)
                {
                    // 에디터의 Bounds를 셀 전체 바운즈로 설정
                    e.Control.Bounds = e.CellBounds;

                    if (e.Control is TextBox tb)
                    {
                        // 텍스트 박스 높이가 셀 높이와 맞지 않을 때 수직 가운데 정렬 효과
                        tb.TextAlign = HorizontalAlignment.Center;

                        // [중요] 값이 사라지는 문제 방지: 초기값 설정
                        if (e.Value != null)
                            tb.Text = e.Value.ToString();
                    }
                }
            };

            // 편집 완료 후 UI 갱신 (값이 안 바뀌고 사라지는 현상 방지)
            newAddGrid.CellEditFinished += (s, e) =>
            {
                // 편집이 취소되지 않았다면 화면 강제 갱신
                if (!e.Cancel)
                {
                    newAddGrid.RefreshItem(e.ListViewItem);
                }
            };

            // [설정 4] ItemActivate를 사용하여 첫 번째 컬럼일 때만 확장/축소
            newAddGrid.Activation = ItemActivation.Standard;
            newAddGrid.ItemActivate += (s, e) =>
            {
                // [수정] OlvHitTest는 (int x, int y)를 인자로 받습니다.
                Point clientPoint = newAddGrid.PointToClient(Cursor.Position);
                var hitTest = newAddGrid.OlvHitTest(clientPoint.X, clientPoint.Y);

                // 컬럼 인덱스가 0보다 크면(속성 컬럼이면) 트리의 확장/축소 로직을 타지 않고 리턴
                if (hitTest.ColumnIndex > 0)
                {
                    return;
                }

                // 첫 번째 컬럼 클릭 시에만 확장/축소 수행
                object selectedObj = newAddGrid.SelectedObject;
                if (selectedObj != null)
                {
                    if (newAddGrid.IsExpanded(selectedObj))
                        newAddGrid.Collapse(selectedObj);
                    else
                        newAddGrid.Expand(selectedObj);
                }
            };

            //드래그 앤 드롭 초기화 함수 호출
            InitializeNewAddGridDragDrop();

            // 삭제 기능 초기화 (키보드 및 우클릭 메뉴)
            InitializeNewAddGridDelete();
        }

        private void InitializeNewAddGridDragDrop()
        {
            // 기존 WinForms 이벤트 핸들러 제거 (충돌 방지 및 OLV 내장 기능 사용을 위해)
            newAddGrid.DragEnter -= newAddGrid_DragEnter;
            newAddGrid.DragDrop -= newAddGrid_DragDrop;

            // 1. 드래그 소스 설정
            newAddGrid.DragSource = new SimpleDragSource();

            // 2. 드롭 싱크 설정 (비주얼 및 로직 처리 담당)
            var sink = new SimpleDropSink();

            // 기본 설정: 아이템 위(자식 추가)와 빈 공간(루트 추가) 드롭 허용
            sink.CanDropOnItem = true;
            sink.CanDropOnBackground = true;
            sink.CanDropBetween = false;     // 순서 변경(끼워넣기) 비활성화

            // [스타일 설정] 드롭 위치에 따른 시각적 피드백(색상, 메시지) 처리
            sink.CanDrop += (s, e) =>
            {
                // 소스 확인: 파일 드롭이거나 로컬 폴더 그리드에서 선택된 경우
                bool isFileDrop = e.DataObject is IDataObject d && d.GetDataPresent(DataFormats.FileDrop);
                bool isLocalDrag = (tlvLocalFolder.SelectedObjects.Count > 0);

                if (isFileDrop || isLocalDrag)
                {
                    e.Effect = DragDropEffects.Copy;

                    // [핵심] 위치별 스타일 분기
                    if (e.DropTargetLocation == DropTargetLocation.Item)
                    {
                        // Row 위에 올렸을 때: 파란색 계열 강조 + 메시지
                        sink.FeedbackColor = Color.DodgerBlue;
                        e.InfoMessage = "하위 레벨에 추가";
                    }
                    else
                    {
                        // 빈 공간(Background)에 올렸을 때: 초록색 계열 강조 + 메시지
                        sink.FeedbackColor = Color.LimeGreen;
                        e.InfoMessage = "최상위 레벨에 추가";
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    e.InfoMessage = null; // 불가능할 때는 메시지 제거
                }
            };

            // 3. 실제 데이터 드롭 처리
            sink.Dropped += (s, e) =>
            {
                FileNode targetNode = e.DropTargetItem?.RowObject as FileNode;
                List<FileNode> nodesToAdd = new List<FileNode>();

                // A. Windows 탐색기 파일 처리
                if (e.DataObject is IDataObject dataObj && dataObj.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])dataObj.GetData(DataFormats.FileDrop);
                    if (files != null)
                    {
                        foreach (string filePath in files)
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                            nodesToAdd.Add(new FileNode
                            {
                                objNm = fi.Name,
                                fullPath = fi.FullName,
                                version = "1.0",
                                objStateCommCd = "New",
                                fileSize = FormatFileSize(fi.Length)
                            });
                        }
                    }
                }
                // B. 로컬 폴더 그리드 아이템 처리
                else if (tlvLocalFolder.SelectedObjects.Count > 0)
                {
                    foreach (var item in tlvLocalFolder.SelectedObjects)
                    {
                        if (item is FileNode srcNode)
                        {
                            nodesToAdd.Add(new FileNode
                            {
                                objNm = srcNode.objNm,
                                fullPath = srcNode.fullPath,
                                version = "1.0",
                                objStateCommCd = "New",
                                fileSize = srcNode.fileSize
                            });
                        }
                    }
                }

                if (nodesToAdd.Count == 0) return;

                // 데이터 추가 로직
                if (targetNode != null)
                {
                    // 아이템 위 -> 자식으로 추가
                    targetNode.Children.AddRange(nodesToAdd);
                    newAddGrid.RefreshObject(targetNode);
                    newAddGrid.Expand(targetNode);
                }
                else
                {
                    // 빈 공간 -> 최상위 루트에 추가
                    newAddGrid.AddObjects(nodesToAdd);
                }

                // 추가된 항목 자동 선택
                newAddGrid.SelectObjects(nodesToAdd);
            };

            // 싱크 적용
            newAddGrid.DropSink = sink;
        }

        // 로컬 폴더 그리드 초기화
        private void InitializeLocalGrid()
        {
            if (splitContainer3 != null)
            {
                splitContainer3.FixedPanel = FixedPanel.Panel1;
            }

            // 파일 아이콘 이미지 리스트 생성 및 설정
            if (_fileImageList == null)
            {
                _fileImageList = new ImageList();
                _fileImageList.ImageSize = new Size(16, 16);
                _fileImageList.ColorDepth = ColorDepth.Depth32Bit;
            }
            tlvLocalFolder.SmallImageList = _fileImageList;

            // 컬럼 설정
            var nameCol = new OLVColumn("파일명", "objNm");
            //nameCol.Width = 250; // 고정 너비 제거
            nameCol.FillsFreeSpace = true; // 남은 공간을 모두 채우도록 설정
            nameCol.TextAlign = HorizontalAlignment.Left;

            // 파일명 컬럼에 확장자별 아이콘 표시 설정
            nameCol.ImageGetter = delegate (object row)
            {
                if (row is FileNode node)
                {
                    // 파일명에서 확장자 추출 (소문자로 통일)
                    string ext = Path.GetExtension(node.objNm).ToLower();

                    // 이미지 리스트에 해당 확장자 키가 있으면 그 키를 반환
                    if (_fileImageList.Images.ContainsKey(ext))
                        return ext;
                }
                return null; // 아이콘 없음
            };

            var sizeCol = new OLVColumn("크기", "fileSize");
            sizeCol.Width = 80;
            sizeCol.TextAlign = HorizontalAlignment.Right;
            sizeCol.IsVisible = true; // 항상 보이도록 명시

            // 중복 추가 방지 (혹시 모를 재호출 대비)
            if (tlvLocalFolder.Columns.Count == 0)
            {
                tlvLocalFolder.Columns.Add(nameCol);
                tlvLocalFolder.Columns.Add(sizeCol);
            }

            //　tlvLocalFolder에서 드래그 시작 활성화
            tlvLocalFolder.DragSource = new SimpleDragSource();

            if (this.btnLocalFolderSelect != null)
            {
                // 이벤트 중복 연결 방지
                this.btnLocalFolderSelect.Click -= BtnLocalFolderSelect_Click;
                this.btnLocalFolderSelect.Click += BtnLocalFolderSelect_Click;
            }
        }

        // 로컬 폴더 선택 버튼 이벤트
        private void BtnLocalFolderSelect_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                // fbd.Description = "작업할 폴더를 선택하세요.";
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    LoadLocalFiles(fbd.SelectedPath);
                }
            }
        }

        // 로컬 파일 로드 및 UI 업데이트
        // 로컬 파일 로드 및 UI 업데이트
        private void LoadLocalFiles(string folderPath)
        {
            try
            {
                lblLocalTitle.Text = folderPath;
                lblLocalTitle.BackColor = Color.Chocolate;

                var dirInfo = new System.IO.DirectoryInfo(folderPath);
                var files = dirInfo.GetFiles();

                // [안전장치] 이미지 리스트가 없으면 초기화
                if (_fileImageList == null) InitializeLocalGrid();

                var nodes = new List<FileNode>();
                foreach (var file in files)
                {
                    // [추가] 확장자별 아이콘 추출 및 등록
                    string ext = file.Extension.ToLower();

                    // 이미 등록된 확장자가 아닐 경우에만 추출 시도 (성능 최적화)
                    if (!_fileImageList.Images.ContainsKey(ext))
                    {
                        try
                        {
                            // 파일 전체 경로를 통해 연결된 아이콘 추출
                            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                            if (icon != null)
                            {
                                // 확장자를 키(Key)로 사용하여 이미지 추가
                                _fileImageList.Images.Add(ext, icon);
                                icon.Dispose(); // 리소스 해제
                            }
                        }
                        catch
                        {
                            // 권한 문제 등으로 추출 실패 시 무시 (아이콘 없이 표시됨)
                        }
                    }

                    nodes.Add(new FileNode
                    {
                        objNm = file.Name,
                        fullPath = file.FullName, // [중요] 전체 경로 저장
                        fileSize = FormatFileSize(file.Length),
                        objStateCommCd = "Local",
                        version = "-"
                    });
                }

                tlvLocalFolder.SetObjects(nodes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 목록을 불러오는 중 오류가 발생했습니다.\n{ex.Message}");
            }
        }

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024) return bytes + " B";
            double kb = bytes / 1024.0;
            if (kb < 1024) return kb.ToString("F1") + " KB";
            double mb = kb / 1024.0;
            return mb.ToString("F1") + " MB";
        }

        // 폴더 노드 데이터 모델
        public class FolderNode
        {
            public string fldSeq { get; set; }
            public string fldNm { get; set; }
            public List<FolderNode> Children { get; set; } = new List<FolderNode>();
        }

        // [Serializable]을 추가하여 드래그 앤 드롭 시 데이터 유실 방지
        [Serializable]
        public class FileNode
        {
            public string objNm { get; set; }
            public string version { get; set; }
            public string objStateCommCd { get; set; }
            public string fileSize { get; set; } // [추가] 파일 크기 속성
            public List<FileNode> Children { get; set; } = new List<FileNode>();
            // 동적 생성되는 속성(컬럼)들의 값을 저장하기 위한 딕셔너리
            public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
            public string fullPath { get; set; }
        }

        // --- 초기화 로직 ---
        private void InitializeTreeListViews()
        {
            try
            {
                var folderSearchTO = new AddInWebService.folderSearchTO();
                var pagingSearchTO = new AddInWebService.pagingSearchTO();

                // 페이지 설정 (전체 조회를 위해 넉넉하게 설정)
                pagingSearchTO.curPage = 1;
                pagingSearchTO.pageSize = 5000;

                folderSearchTO.permissionUserId = userId;
                folderSearchTO.fldType = "3D"; // 3D 폴더만 조회

                // 1. 서버에서 폴더 목록 가져오기
                var fldCount = common.m_intf.getFolderTotalCnt(folderSearchTO);
                if (fldCount == 0)
                {
                    return; // 폴더가 없으면 종료
                }

                var folderList = common.m_intf.getFolderListCatia(folderSearchTO, pagingSearchTO);
                if (folderList == null || folderList.Length == 0) return;

                // 2. FolderNode 객체로 변환 및 ID 매핑 생성
                var nodeMap = new Dictionary<string, FolderNode>();
                var roots = new List<FolderNode>();

                foreach (var item in folderList)
                {
                    // 웹 서비스 리턴 객체(item)의 속성을 FolderNode에 매핑
                    // fldSeq: 폴더ID, fldNm: 폴더명, parFldSeq: 부모폴더ID
                    var node = new FolderNode
                    {
                        fldSeq = item.fldSeq.ToString(),
                        fldNm = item.fldNm,
                        Children = new List<FolderNode>()
                    };

                    if (!nodeMap.ContainsKey(node.fldSeq))
                    {
                        nodeMap.Add(node.fldSeq, node);
                    }
                }

                // 3. 부모-자식 관계 연결 (트리 구조 형성)
                foreach (var item in folderList)
                {
                    string myId = item.fldSeq.ToString();
                    string parentId = item.parFldSeq.ToString(); // 부모 ID (최상위는 0 가정)

                    if (nodeMap.ContainsKey(myId))
                    {
                        var myNode = nodeMap[myId];

                        // 부모 ID가 0이거나 매핑된 부모가 없으면 루트("Roots")로 간주
                        if (parentId == "0" || !nodeMap.ContainsKey(parentId))
                        {
                            roots.Add(myNode);
                        }
                        else
                        {
                            var parentNode = nodeMap[parentId];
                            parentNode.Children.Add(myNode);
                        }
                    }
                }

                // 4. TreeListView 컨트롤 설정 및 데이터 바인딩
                SetupTreeListView(tlvFolder, roots);  // 첫 번째 탭 트리
                SetupTreeListView(tlvFolder2, roots); // 두 번째 탭 트리 (검색 기능 등에서 사용됨)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"시스템 폴더 목록을 불러오는 중 오류가 발생했습니다.\n{ex.Message}");
            }
        }

        private Color SetListColor(string sCommcd)
        {
            switch (sCommcd)
            {
                case "0001":
                    return Color.Black;
                case "0002":
                    return Color.Blue;
                case "0003":
                    return Color.Red;
                case "0004":
                    return Color.Orange;
                case "0005":
                    return Color.Green;
            }
            return Color.Black;
        }

        /// <summary>
        /// TreeListView에 대한 공통 설정 및 데이터 바인딩을 수행합니다.
        /// </summary>
        private void SetupTreeListView(TreeListView tlv, List<FolderNode> roots)
        {
            if (tlv == null) return;

            // 이미지 리스트 연결
            tlv.SmallImageList = _folderImageList;

            // [+] 버튼 활성화 조건: 자식 노드가 있을 때
            tlv.CanExpandGetter = x => ((FolderNode)x).Children.Count > 0;

            // 자식 노드 리스트 반환 로직
            tlv.ChildrenGetter = x => ((FolderNode)x).Children;

            // 컬럼 설정 (디자이너에 컬럼이 있으면 첫 번째 컬럼에 바인딩, 없으면 새로 추가)
            if (tlv.Columns.Count > 0)
            {
                var col = tlv.Columns[0] as OLVColumn;
                if (col != null)
                {
                    col.AspectName = "fldNm"; // 표시할 속성명
                    // 폴더 아이콘 이미지 키 반환
                    col.ImageGetter = delegate (object x) { return "folder"; };
                }
            }
            else
            {
                var col = new OLVColumn("폴더명", "fldNm");
                col.Width = 300;
                // 폴더 아이콘 이미지 키 반환
                col.ImageGetter = delegate (object x) { return "folder"; };
                tlv.Columns.Add(col);
            }

            // 트리 뿌리(Root) 데이터 설정
            tlv.Roots = roots;

            // 데이터가 존재하면 첫 번째 루트 노드를 기본적으로 확장
            if (roots.Count > 0)
            {
                tlv.Expand(roots[0]);
            }
        }




        // 2. 폴더 검색 버튼 (돋보기)
        private void folderSearchButton_Click(object sender, EventArgs e)
        {
            if (m_FolderSearchForm != null && !m_FolderSearchForm.IsDisposed)
            {
                m_FolderSearchForm.Close();
            }

            m_FolderSearchForm = new FolderSearch();
            m_FolderSearchForm.Owner = this; // Main을 부모로 설정
            m_FolderSearchForm.Show();
        }

        // 3. 폴더 열기 버튼 (탭2)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                System.Diagnostics.Process.Start("explorer.exe", folderPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}");
            }
        }

        // 4. 드래그 앤 드롭 (탭2 그리드)
        private void newAddGrid_DragEnter(object sender, DragEventArgs e)
        {
            // [수정] 데이터 형식을 엄격하게 체크하는 대신,
            // 윈도우 파일 드롭이거나, 로컬 폴더 그리드에서 선택된 항목이 있을 경우 허용
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            // 같은 어플리케이션 내의 tlvLocalFolder에서 드래그 중인지 판단
            else if (tlvLocalFolder != null && tlvLocalFolder.SelectedObjects.Count > 0)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // [수정] DragDrop: 드래그된 소스(외부/내부)에 따라 처리 분기
        private void newAddGrid_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // 드롭 위치 파악 (특정 행 위인지, 빈 공간인지)
                Point clientPoint = newAddGrid.PointToClient(new Point(e.X, e.Y));
                OLVListItem targetItem = newAddGrid.GetItemAt(clientPoint.X, clientPoint.Y) as OLVListItem;
                FileNode targetNode = targetItem?.RowObject as FileNode;

                List<FileNode> nodesToAdd = new List<FileNode>();

                // Case 1: 윈도우 탐색기에서 파일 드롭
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files != null)
                    {
                        foreach (string filePath in files)
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                            nodesToAdd.Add(new FileNode
                            {
                                objNm = fi.Name,
                                version = "1.0",
                                objStateCommCd = "New",
                                fileSize = FormatFileSize(fi.Length)
                            });
                        }
                    }
                }
                
                // Case 2: tlvLocalFolder(내부 그리드)에서 아이템 드롭 [수정됨]
                // DataObject에서 타입을 꺼내는 것이 실패할 수 있으므로, 직접 tlvLocalFolder의 선택 항목을 참조
                else if (tlvLocalFolder.SelectedObjects.Count > 0)
                {
                    foreach (var item in tlvLocalFolder.SelectedObjects)
                    {
                        if (item is FileNode srcNode)
                        {
                            // 기존 노드를 복제하여 add
                            nodesToAdd.Add(new FileNode
                            {
                                objNm = srcNode.objNm,
                                version = "1.0",
                                objStateCommCd = "New",
                                fileSize = srcNode.fileSize
                            });
                        }
                    }
                }

                // 그리드에 노드 추가 처리
                if (nodesToAdd.Count > 0)
                {
                    if (targetNode != null)
                    {
                        // 특정 행 위에 드롭 -> 자식 노드로 추가 (하위 레벨)
                        targetNode.Children.AddRange(nodesToAdd);
                        newAddGrid.RefreshObject(targetNode);
                        newAddGrid.Expand(targetNode);
                    }
                    else
                    {
                        // 빈 공간에 드롭 -> 최상위 노드로 추가 (0레벨)
                        newAddGrid.AddObjects(nodesToAdd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 추가 중 오류: {ex.Message}");
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            // 폼 활성화 시 처리 로직 (필요시 구현)
        }

        //검색기능
        public (int current, int total) SearchAndFocusFolder(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return (0, 0);

            // 현재 선택된 탭에 따라 검색 대상 트리 결정
            TreeListView targetTree = tabControl1.SelectedTab == tabPage2 ? tlvFolder2 : tlvFolder;

            // [핵심 수정] EnumerableAllObjects 대신 Roots(데이터 원본)에서 전체 노드를 가져옵니다.
            // 이렇게 해야 접혀있는(Collapsed) 폴더 안의 내용까지 모두 찾을 수 있습니다.
            var roots = targetTree.Roots as System.Collections.IEnumerable;
            var allNodes = GetAllNodes(roots); // 아래에 정의한 헬퍼 메서드 사용

            // 검색어 매칭 (대소문자 무시)
            var matches = allNodes.Where(n => n.fldNm.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (matches.Count == 0)
            {
                MessageBox.Show($"'{searchText}' 폴더를 찾을 수 없습니다.");
                return (0, 0);
            }

            // --- [순환 검색 로직] ---
            // 계속 버튼을 누르면 다음 항목으로 이동하는 기능입니다.
            FolderNode resultNode = matches[0];

            // 현재 선택된 노드가 검색 결과 중에 있다면, 그 다음 순서의 노드를 선택
            if (targetTree.SelectedObject is FolderNode currentSelection)
            {
                int currentIndex = matches.IndexOf(currentSelection);
                if (currentIndex >= 0)
                {
                    // 다음 인덱스 계산 (마지막이면 다시 0번으로)
                    int nextIndex = (currentIndex + 1) % matches.Count;
                    resultNode = matches[nextIndex];
                }
            }

            // 해당 노드로 이동 및 선택
            targetTree.Reveal(resultNode, true);       // 부모 노드를 펼쳐서 보이게 함
            targetTree.SelectObject(resultNode, true); // 해당 노드 선택 및 스크롤 이동
            targetTree.Focus();

            // (현재 순번, 전체 개수) 반환
            return (matches.IndexOf(resultNode) + 1, matches.Count);
        }
        private List<FolderNode> GetAllNodes(System.Collections.IEnumerable roots)
        {
            var list = new List<FolderNode>();
            if (roots == null) return list;

            foreach (var obj in roots)
            {
                if (obj is FolderNode node)
                {
                    list.Add(node);

                    // 자식 노드가 있으면 재귀적으로 파고들어서 리스트에 추가
                    if (node.Children != null && node.Children.Count > 0)
                    {
                        list.AddRange(GetAllNodes(node.Children));
                    }
                }
            }
            return list;
        }



        private void SetColumn(ObjectListView olv, string title, string aspect, int width)
        {
            OLVColumn col = new OLVColumn(title, aspect);
            col.Width = width;
            col.TextAlign = HorizontalAlignment.Center;
            if (title.Contains("도면명")) col.TextAlign = HorizontalAlignment.Left;
            olv.Columns.Add(col);
            col.IsEditable = false;
        }

        private void doubleClickToExpand(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListView tree = (TreeListView)sender;
                object selectedObj = tree.SelectedObject;

                if (selectedObj != null)
                {
                    // 현재 아이템이 확장되어 있으면 축소, 아니면 확장
                    if (tree.IsExpanded(selectedObj))
                    {
                        tree.Collapse(selectedObj);
                    }
                    else
                    {
                        tree.Expand(selectedObj);
                    }
                }
            }
            catch (Exception ex)
            {
                // 오류 처리가 필요하다면 여기에 추가
                // MessageBox.Show(ex.Message);
            }
        }


        //더블클릭이벤트 추가
        private void tlvFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            doubleClickToExpand(sender, e);
        }

        //private void newAddGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    doubleClickToExpand(sender, e);
        //}

        private void tlvFolder2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            doubleClickToExpand(sender, e);
        }

        private void tlvFolder_CellClick(object sender, CellClickEventArgs e)
        {
            // e.Model이 null이거나 FolderNode가 아닌 경우 처리를 중단하는 예외 처리 추가
            if (e.Model == null || !(e.Model is FolderNode folderNode))
            {
                return;
            }

            var objectSearchTO = new AddInWebService.objectSearchTO();

            // 안전하게 캐스팅된 folderNode 사용
            if (!int.TryParse(folderNode.fldSeq, out int fldSeq))
            {
                // fldSeq가 유효한 숫자가 아니면 중단
                return;
            }

            objectSearchTO.fldSeq = fldSeq;
            objectSearchTO.objStateCommGrpCd = "0009";
            objectSearchTO.objInitStateCommGrpCd = "0018";
            objectSearchTO.fldSeqSpecified = true;
            objectSearchTO.deleteYn = "N";

            var pagingSearchTO = new AddInWebService.pagingSearchTO();

            // 페이지 설정 (전체 조회를 위해 넉넉하게 설정)
            pagingSearchTO.curPage = 1;
            pagingSearchTO.pageSize = 5000;

            // 1. 웹서비스에서 평면 리스트(Flat List) 가져오기
            var objectList = common.m_intf.getSearchObjectListTpConnect(objectSearchTO, pagingSearchTO);

            if (objectList == null || objectList.Length == 0)
            {
                tlvData.ClearObjects();
                return;
            }

            // 2. 평면 리스트를 트리 구조로 변환
            // 자식 맵 초기화 (지역 변수 childrenMap 제거하고 멤버 변수 m_ChildMap 사용)
            m_ChildMap = new Dictionary<int, List<AddInWebService.objectVO>>();
            _tempIdCounter = -1; // 폴더 이동 시 임시 ID 리셋

            var roots = new List<AddInWebService.objectVO>();

            foreach (var obj in objectList)
            {
                bool isRoot = (!obj.parObjSeqSpecified || obj.parObjSeq == 0);

                if (isRoot)
                {
                    roots.Add(obj);
                }
                else
                {
                    // [수정] m_ChildMap 사용
                    if (!m_ChildMap.ContainsKey(obj.parObjSeq))
                    {
                        m_ChildMap[obj.parObjSeq] = new List<AddInWebService.objectVO>();
                    }
                    m_ChildMap[obj.parObjSeq].Add(obj);
                }
            }

            // 3. TreeListView 설정
            // 자식 여부 확인 델리게이트
            tlvData.CanExpandGetter = delegate (object x)
            {
                if (x is AddInWebService.objectVO obj)
                {
                    return m_ChildMap.ContainsKey(obj.objSeq);
                }
                return false;
            };

            // 자식 리스트 반환 델리게이트
            tlvData.ChildrenGetter = delegate (object x)
            {
                if (x is AddInWebService.objectVO obj)
                {
                    if (m_ChildMap.TryGetValue(obj.objSeq, out var children))
                    {
                        return children;
                    }
                }
                return new List<AddInWebService.objectVO>(); // 빈 리스트
            };

            // 4. 데이터 바인딩 (Roots 사용)
            tlvData.Roots = roots;

            // 필요 시 전체 펼치기
            tlvData.ExpandAll();

            // 상세 속성 정보 로드 및 바인딩
            LoadObjectAttributes();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // tlvFolder2(신규등록 탭의 좌측 트리)에서 선택된 폴더 ID 사용 (부모 폴더가 됨)
            int parentFldSeq = 0;

            if (tlvFolder2.SelectedObject is FolderNode selFolder)
            {
                if (int.TryParse(selFolder.fldSeq, out int fldSeq))
                {
                    parentFldSeq = fldSeq;
                }
            }

            if (parentFldSeq == 0)
            {
                MessageBox.Show("업로드할 상위 폴더를 선택해주세요 (좌측 폴더 목록).");
                return;
            }

            // 등록할 파일이 있는지 확인
            if (newAddGrid.GetItemCount() == 0)
            {
                MessageBox.Show("등록할 파일이 없습니다.");
                return;
            }

            // CreateFolder 폼을 띄워 하위 폴더명 입력 받기
            using (CreateFolder dlg = new CreateFolder())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string newFolderName = dlg.FolderName;

                    // 폴더 생성 VO 설정
                    AddInWebService.folderVO4Addin addInFolderVO = new AddInWebService.folderVO4Addin();
                    addInFolderVO.parFldSeq = parentFldSeq; // 선택한 폴더를 부모로 설정
                    addInFolderVO.parFldSeqSpecified = true;
                    addInFolderVO.fldNm = newFolderName;
                    addInFolderVO.fldId = newFolderName; // fldId에도 동일한 값 설정
                    addInFolderVO.clsSeq = g_clsSeq;
                    addInFolderVO.clsSeqSpecified = true;
                    addInFolderVO.regId = userId;

                    int iNewFldSeq = 0;
                    bool bReturn = false;

                    // 커서 변경
                    Cursor.Current = Cursors.WaitCursor;

                    try
                    {
                        // 1. 하위 폴더 생성 요청
                        common.m_intf.createFolderTpConnect(addInFolderVO, out iNewFldSeq, out bReturn);

                        if (bReturn && iNewFldSeq > 0)
                        {
                            // 2. 생성된 하위 폴더 ID(iNewFldSeq)로 파일 업로드 진행
                            // 최상위 노드들의 부모 ID는 null, parentVerSeq는 0(없음)으로 시작
                            UploadRecursive(newAddGrid.Roots, null, 0, iNewFldSeq);

                            MessageBox.Show("폴더 생성 및 업로드가 완료되었습니다.");

                            // 완료 후 그리드 비우기 (필요시 주석 해제)
                            newAddGrid.ClearObjects();

                            // 참고: 좌측 폴더 트리 갱신이 필요할 수 있습니다.
                            InitializeTreeListViews();
                        }
                        else
                        {
                            MessageBox.Show("폴더 생성에 실패했습니다.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"작업 중 오류가 발생했습니다.\n{ex.Message}");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// 트리 구조를 순회하며 순차적으로 데이터를 업로드하는 재귀 함수
        /// </summary>
        /// <param name="nodes">업로드할 노드 목록</param>
        /// <param name="parObjSeq">상위 객체 ID (최상위는 null)</param>
        /// <param name="parentVerSeq">상위 객체 버전 ID (최상위는 0)</param>
        /// <param name="targetFldSeq">업로드 대상 폴더 ID</param>
        private void UploadRecursive(System.Collections.IEnumerable nodes, string parObjSeq, int parentVerSeq, int targetFldSeq)
        {
            if (nodes == null) return;

            foreach (FileNode node in nodes)
            {
                AddInWebService.objectVO objectVO = new AddInWebService.objectVO();

                if (m_AttributeList != null && m_AttributeList.Count > 0)
                {
                    objectVO.arrStrKey = new string[m_AttributeList.Count];
                    objectVO.arrStrValue = new string[m_AttributeList.Count];

                    for (int i = 0; i < m_AttributeList.Count; i++)
                    {
                        string key = m_AttributeList[i].attrId;
                        objectVO.arrStrKey[i] = key;

                        // 사용자가 입력한 값(Attributes 딕셔너리)이 있으면 설정, 없으면 빈 값
                        if (node.Attributes != null && node.Attributes.ContainsKey(key))
                        {
                            objectVO.arrStrValue[i] = node.Attributes[key];
                        }
                        else
                        {
                            objectVO.arrStrValue[i] = "";
                        }
                    }
                }

                objectVO.fldSeq = targetFldSeq;
                objectVO.fldSeqSpecified = true;
                objectVO.clsSeq = g_clsSeq;
                objectVO.clsSeqSpecified = true;
                objectVO.objNm = node.objNm;
                objectVO.objStateCommCd = "0001";
                objectVO.objTypeCommCd = "0001";
                objectVO.objInitStateCommCd = "0018";
                objectVO.securityLvl = 3;
                objectVO.deleteYn = "N";
                objectVO.regId = userId;
                objectVO.updId = userId;
                objectVO.ownerUserId = userId;
                objectVO.objNumber = GetAutoNo(targetFldSeq);

                if (!string.IsNullOrEmpty(parObjSeq) && int.TryParse(parObjSeq.Trim(), out int pSeq))
                {
                    objectVO.parObjSeq = pSeq;
                    objectVO.parObjSeqSpecified = true;
                }
                else
                {
                    objectVO.parObjSeqSpecified = false;
                }

                string result = common.m_intf.createObjectTpConnect(objectVO);

                string createdObjSeq = null;
                if (!string.IsNullOrEmpty(result))
                {
                    string[] parts = result.Split('_');
                    if (parts.Length > 0)
                    {
                        createdObjSeq = parts[0].Trim();
                    }
                }

                int iVerSeq = 0;

                //파일 업로드 로직 통합
                if (!string.IsNullOrEmpty(createdObjSeq) && int.TryParse(createdObjSeq, out int iObjSeq))
                {
                    // fullPath가 유효할 경우 파일 업로드 수행
                    if (!string.IsNullOrEmpty(node.fullPath) && System.IO.File.Exists(node.fullPath))
                    {
                        UploadFileProcess(node.fullPath, iObjSeq);
                    }

                    // 버전 생성 (iVerSeq 획득)
                    iVerSeq = common.m_intf.createObjectVersion(Convert.ToInt32(createdObjSeq), true, userId);

                    // 부모 버전 정보가 있을 경우 3DRef 관계 생성
                    if (parentVerSeq > 0)
                    {
                        AddInWebService.object3DRefVO obj3DRefVOs = new AddInWebService.object3DRefVO();

                        obj3DRefVOs.parentVerSeq = parentVerSeq;
                        obj3DRefVOs.parentVerSeqSpecified = true;
                        obj3DRefVOs.childVerSeq = iVerSeq;
                        obj3DRefVOs.childVerSeqSpecified = true;
                        obj3DRefVOs.regId = userId;

                        common.m_intf.createObject3DRef(obj3DRefVOs);
                    }
                }

                // 자식 노드가 있으면 재귀 호출 (현재 생성된 createdObjSeq와 iVerSeq 전달)
                if (node.Children != null && node.Children.Count > 0 && !string.IsNullOrEmpty(createdObjSeq))
                {
                    // iVerSeq가 0이 아니어야 유효한 부모 버전 정보를 전달할 수 있음
                    if (iVerSeq > 0)
                    {
                        UploadRecursive(node.Children, createdObjSeq, iVerSeq, targetFldSeq);
                    }
                }
            }
        }

        private string GetAutoNo(int fldSeq)
        {
            try
            {
                AddInWebService.autonumSearchTO schTo = new AddInWebService.autonumSearchTO();
                schTo.autonumTypeGrpCd = "0019";
                schTo.clsSeq = g_clsSeq;
                schTo.fldSeq = fldSeq;
                schTo.fldSeqSpecified = true;

                // 채번 서비스 호출
                string sAutoNo = common.m_intf.getAutonum4Preview(schTo);
                return sAutoNo;
            }
            catch
            {
                return ""; // 오류 시 빈 값 처리
            }
        }

        // [추가] 삭제 관련 이벤트 및 컨텍스트 메뉴 설정
        private void InitializeNewAddGridDelete()
        {
            // 1. Delete 키 이벤트 연결
            newAddGrid.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteSelectedNodes();
                }
            };

            // 2. 우클릭 컨텍스트 메뉴 생성
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("삭제");
            deleteItem.Click += (s, e) => DeleteSelectedNodes();

            // 메뉴 아이템 추가
            menu.Items.Add(deleteItem);

            // 그리드에 메뉴 연결
            newAddGrid.ContextMenuStrip = menu;
        }

        // [추가] 선택된 노드 삭제 처리 로직
        private void DeleteSelectedNodes()
        {
            if (newAddGrid.SelectedObjects.Count == 0) return;

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // 선택된 객체들을 리스트로 복사 (반복문 중 컬렉션 수정 오류 방지)
            var selectedItems = newAddGrid.SelectedObjects.Cast<FileNode>().ToList();

            newAddGrid.BeginUpdate(); // 화면 갱신 일시 중지 (성능 최적화)
            try
            {
                foreach (var item in selectedItems)
                {
                    // 부모 노드를 찾아서 모델(Children 리스트)에서도 제거해야 함
                    var parent = newAddGrid.GetParent(item);

                    if (parent is FileNode parentNode)
                    {
                        // 자식 목록에서 제거
                        parentNode.Children.Remove(item);
                        // 부모 노드 UI 갱신 (자식이 없어지면 [+] 버튼 사라지도록)
                        newAddGrid.RefreshObject(parentNode);
                    }
                    // parent가 null이면 루트 노드이므로, 아래 RemoveObjects가 처리함
                }

                // OLV 내부 데이터 및 뷰에서 제거
                newAddGrid.RemoveObjects(selectedItems);
            }
            finally
            {
                newAddGrid.EndUpdate(); // 화면 갱신 재개
            }
        }


        // 파일 등록
        private void UploadFileProcess(string sUploadFilePath, int iSeq)
        {
            // 파일 정보 확인
            FileInfo finfo = new FileInfo(sUploadFilePath);
            if (!finfo.Exists) return;

            AddInWebService.objectFileVO fvo = new AddInWebService.objectFileVO();
            fvo.objSeq = iSeq;
            fvo.objSeqSpecified = true;
            fvo.orgFileNm = finfo.FullName; // 원본 전체 경로
            fvo.fileNm = finfo.Name;        // 파일 이름
            fvo.regId = userId;
            fvo.updId = GlobalClientService.userInfo.userId;
            fvo.masterYn = "Y";

            // 파일 바이너리 읽기
            using (FileStream fs = File.Open(finfo.FullName, FileMode.Open, FileAccess.Read))
            {
                fvo.file = new byte[fs.Length];
                fs.Read(fvo.file, 0, (int)fs.Length);
                fvo.fileSize = fs.Length;
            }

            // 웹 서비스 호출
            int iRet = common.m_intf.uploadFileProcessTpConnect(fvo);

            if (iRet != 1)
            {
                throw new Exception($"파일 등록 실패: {finfo.Name}");
            }
        }

        // 1. 프로그램 실행 버튼
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (tlvData.SelectedObject is AddInWebService.objectVO selectedObj)
            {
                // 체크아웃 상태('0003')인지 확인
                if (selectedObj.objStateCommCd != "0003")
                {
                    MessageBox.Show("체크아웃된 파일만 실행할 수 있습니다.");
                    return;
                }

                string sResult = common.m_intf.checkObjectTpConnect(selectedObj.objSeq, true, userId, "0003", "Y");
                if (sResult != "SUCCESS")
                {
                    MessageBox.Show("권한이 없습니다.");
                    return;
                }

                if (selectedObj.updId != userId)
                {
                    MessageBox.Show("다른 사용자가 체크아웃 한 자료는 실행할 수 없습니다.");
                    return;
                }

                try
                {
                    // 1. 서버에서 해당 객체의 파일 정보 가져오기 (정확한 파일명 및 fileSeq 확인용)
                    AddInWebService.objectSearchTO schTo = new AddInWebService.objectSearchTO();
                    schTo.objSeq = selectedObj.objSeq;

                    var fileList = common.m_intf.getObjectFileList(schTo);

                    if (fileList == null || fileList.Length == 0)
                    {
                        MessageBox.Show("파일 정보를 찾을 수 없습니다.");
                        return;
                    }

                    // 2. 대표 파일(첫 번째 파일) 경로 구성
                    // 규칙: CHECKOUT_ROOT + objSeq + "_" + fileSeq + "_" + orgFileNm
                    var mainFile = fileList[0];
                    string localFileName = $"{selectedObj.objSeq}_{mainFile.objFileSeq}_{mainFile.orgFileNm}";
                    string fullPath = Path.Combine(CHECKOUT_ROOT, localFileName);

                    // 3. 실행
                    ExecuteProgram(fullPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"실행 중 오류가 발생했습니다.\n{ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("실행할 항목을 선택해주세요.");
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            // 1. 체크인 대상 선택 확인
            if (tlvData.SelectedObject == null)
            {
                MessageBox.Show("체크인할 항목을 선택해주세요.");
                return;
            }

            var rootObj = tlvData.SelectedObject as AddInWebService.objectVO;
            if (rootObj == null) return;

            // 2. 전체 계층 노드 수집 (본인 + 하위 자식들)
            List<AddInWebService.objectVO> allNodes = new List<AddInWebService.objectVO>();
            CollectAllNodes(rootObj, allNodes);

            // 3. [검증 단계] 체크인 불가능한 조건 확인
            // 조건: 하위 포함 모든 항목의 최종수정자가 본인이어야 함 (체크아웃 상태인 것만 검사)
            foreach (var node in allNodes)
            {
                // 체크아웃 상태('0003')인데 수정자가 내가 아닌 경우
                if (node.objStateCommCd == "0003" && node.updId != userId)
                {
                    MessageBox.Show($"하위 항목 중 다른 사용자({node.updId})가 작업 중인 파일이 있어 체크인할 수 없습니다.\n파일명: {node.objNm}",
                                    "체크인 불가", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // [추가] 실제 체크인 대상 필터링 (내가 체크아웃한 '0003' 상태인 항목만)
            var targetNodes = allNodes.Where(n => n.objStateCommCd == "0003" && n.updId == userId).ToList();

            if (targetNodes.Count == 0)
            {
                MessageBox.Show("체크인할 대상(본인이 체크아웃한 파일)이 없습니다.");
                return;
            }

            // 4. 권한 체크 및 수정사항 입력
            try
            {
                // 대표 항목(루트)에 대해 먼저 권한 체크
                bool checkYn = common.m_intf.checkPermission(rootObj.fldSeq, true, userId, rootObj.objStateCommCd, rootObj.objSeq, true);
                if (!checkYn)
                {
                    MessageBox.Show("폴더 권한이 없습니다. (루트 항목 기준)\n관리자에게 문의 바랍니다.");
                    return;
                }

                // 5. 수정사항 입력 창 띄우기 (일괄 적용)
                string modificationDetails = string.Empty;

                using (ModifyDesc dlg = new ModifyDesc())
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                    {
                        return; // 취소
                    }
                    modificationDetails = dlg.DescriptionText;
                }

                //체크인 대상이 되는 모든 노드(하위 포함)에 대해 코멘트 업데이트
                foreach (var node in targetNodes)
                {
                    AddInWebService.objectVO modifyObjVO = new AddInWebService.objectVO();

                    modifyObjVO.workCommnet = modificationDetails;
                    modifyObjVO.objSeq = node.objSeq;
                    modifyObjVO.objSeqSpecified = true;
                    modifyObjVO.updId = userId;

                    if (node.arrStrKey != null && node.arrStrValue != null)
                    {
                        modifyObjVO.arrStrKey = node.arrStrKey;
                        modifyObjVO.arrStrValue = node.arrStrValue;
                    }

                    common.m_intf.modifyObjectTpConnect(modifyObjVO);
                }

                Cursor.Current = Cursors.WaitCursor;

                // [ObjSeq -> NewVerSeq] 매핑을 저장할 딕셔너리 (참조 생성용)
                Dictionary<int, int> objSeqToVerSeqMap = new Dictionary<int, int>();

                // 6. [일괄 실행 단계] 대상 항목 순회하며 체크인 처리
                foreach (var selectedObj in targetNodes)
                {
                    // [로직 시작] 파일삭제 및 새파일 업로드 -> 새 버전 업로드 -> 상태 변경

                    // A. 조회용 TO 설정
                    AddInWebService.objectSearchTO schTo = new AddInWebService.objectSearchTO();
                    schTo.objSeq = selectedObj.objSeq;

                    // B. 서버 파일 리스트 가져오기
                    AddInWebService.objectFileVO[] fileList = common.m_intf.getObjectFileList(schTo);

                    if (fileList == null || fileList.Length == 0)
                    {
                        Console.WriteLine($"[체크인 건너뜀] 서버 파일 정보 없음: {selectedObj.objNm}");
                        continue;
                    }

                    string downloadFolder = Path.Combine(Application.StartupPath, "Download");
                    if (!Directory.Exists(downloadFolder)) Directory.CreateDirectory(downloadFolder);

                    // C. 파일 처리 반복 (기존 삭제 후 재업로드)
                    for (int i = 0; i < fileList.Length; i++)
                    {
                        // 로컬 파일 경로 구성
                        string localFileName = $"{selectedObj.objSeq}_{fileList[i].orgFileNm}";
                        string localFilePath = Path.Combine(downloadFolder, localFileName);

                        // 체크아웃 루트 확인
                        if (!File.Exists(localFilePath))
                        {
                            localFilePath = Path.Combine(CHECKOUT_ROOT, localFileName);
                        }

                        // 로컬에 파일이 없으면 업데이트 스킵
                        if (!File.Exists(localFilePath)) continue;

                        // 업로드할 파일 정보 구성
                        AddInWebService.objectFileVO filevo = new AddInWebService.objectFileVO();
                        filevo.objSeq = selectedObj.objSeq;
                        filevo.objSeqSpecified = true;
                        filevo.orgFileNm = fileList[i].orgFileNm;
                        filevo.fileNm = fileList[i].orgFileNm;
                        filevo.regId = userId;
                        filevo.masterYn = fileList[i].masterYn;

                        // 삭제 대상 정보 구성
                        AddInWebService.objectFileVO delFileVO = new AddInWebService.objectFileVO();
                        delFileVO.objSeq = selectedObj.objSeq;
                        delFileVO.objSeqSpecified = true;
                        delFileVO.objFileSeq = fileList[i].objFileSeq;
                        delFileVO.objFileSeqSpecified = true;

                        // 파일 읽기
                        using (FileStream fs = File.Open(localFilePath, FileMode.Open, FileAccess.Read))
                        {
                            filevo.file = new byte[fs.Length];
                            fs.Read(filevo.file, 0, (int)fs.Length);
                            filevo.fileSize = fs.Length;
                        }

                        // [Step 1] 기존 파일 삭제
                        common.m_intf.removeObjectFile(delFileVO);

                        // [Step 2] 로컬 파일 재업로드
                        common.m_intf.uploadFileProcessTpConnect(filevo);
                    }

                    // [Step 3] 새 버전 업로드 및 상태 변경 (Check-In)
                    AddInWebService.objectFileVO objectFileVO = new AddInWebService.objectFileVO();
                    AddInWebService.objectVO objectVO = new AddInWebService.objectVO();

                    objectVO.objSeq = selectedObj.objSeq;
                    objectVO.objSeqSpecified = true;
                    objectVO.objStateCommCd = "0002"; // Check-In 상태
                    objectVO.updId = userId;

                    // modifyObjectStateTpConnect 반환값은 새로운 verSeq
                    int iReturn = common.m_intf.modifyObjectStateTpConnect(objectVO, objectFileVO);

                    // 딕셔너리에 저장 (ObjSeq : New VerSeq)
                    if (!objSeqToVerSeqMap.ContainsKey(selectedObj.objSeq))
                    {
                        objSeqToVerSeqMap.Add(selectedObj.objSeq, iReturn);
                    }
                }

                // 7. [관계 생성 단계] 부모-자식 간 3DRef 생성
                // [수정] 체크인된 대상(targetNodes)뿐만 아니라, 전체 트리구조(allNodes)를 확인하여
                // 부모가 새로 체크인 되었다면(새 버전 존재), 자식이 체크인되지 않았더라도(기존 버전) 관계를 맺어줍니다.
                foreach (var node in allNodes)
                {
                    // 1. 부모가 이번 체크인으로 새 버전이 생성되었는지 확인
                    if (node.parObjSeqSpecified && node.parObjSeq > 0 &&
                        objSeqToVerSeqMap.ContainsKey(node.parObjSeq))
                    {
                        int parentVerSeq = objSeqToVerSeqMap[node.parObjSeq];
                        int childVerSeq = 0;

                        // 2. 자식(현재 노드)의 버전 확인
                        if (objSeqToVerSeqMap.ContainsKey(node.objSeq))
                        {
                            // 같이 체크인 된 경우 -> 새 버전 사용
                            childVerSeq = objSeqToVerSeqMap[node.objSeq];
                        }
                        else
                        {
                            // 체크인 되지 않은 경우 -> 기존 버전 사용
                            childVerSeq = node.verSeq;
                        }

                        // 3. 관계 생성 (버전 정보가 유효할 때만)
                        if (parentVerSeq > 0 && childVerSeq > 0)
                        {
                            AddInWebService.object3DRefVO obj3DRefVOs = new AddInWebService.object3DRefVO();
                            obj3DRefVOs.parentVerSeq = parentVerSeq;
                            obj3DRefVOs.parentVerSeqSpecified = true;
                            obj3DRefVOs.childVerSeq = childVerSeq;
                            obj3DRefVOs.childVerSeqSpecified = true;
                            obj3DRefVOs.regId = userId;

                            common.m_intf.createObject3DRef(obj3DRefVOs);
                        }
                    }
                }

                MessageBox.Show("체크인이 완료되었습니다."); // 일괄 완료 메시지

                RefreshGrid(); // 그리드 갱신
            }
            catch (Exception ex)
            {
                MessageBox.Show($"체크인 프로세스 중 오류가 발생했습니다.\n{ex.Message}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            // 1. 선택된 항목 확인
            if (tlvData.SelectedObject == null)
            {
                MessageBox.Show("체크아웃할 항목을 선택해주세요.");
                return;
            }

            var rootObj = tlvData.SelectedObject as AddInWebService.objectVO;
            if (rootObj == null) return;

            // 2. 전체 계층 노드 수집 (본인 + 하위 자식들)
            // TreeListView의 구조를 활용하여 모든 자식을 가져옵니다.
            List<AddInWebService.objectVO> allNodes = new List<AddInWebService.objectVO>();
            CollectAllNodes(rootObj, allNodes);

            // 3. [검증 단계] 체크아웃 불가능한 조건 확인 (Case 2: 하위 레벨 중 타인이 체크아웃한 것이 있는지)
            foreach (var node in allNodes)
            {
                // '0003'이 체크아웃 상태라고 가정
                if (node.objStateCommCd == "0003" && node.updId != userId)
                {
                    MessageBox.Show($"하위 항목 중 다른 사용자({node.updId})에 의해 체크아웃된 파일이 있습니다.\n파일명: {node.objNm}",
                                    "체크아웃 불가", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // 4. [실행 단계] 권한 체크 및 체크아웃/다운로드 진행
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                bool isAnyUpdated = false;

                foreach (var node in allNodes)
                {
                    bool needStateChange = true;

                    // 상태 확인
                    if (node.objStateCommCd == "0003")
                    {
                        // Case 3: 이미 체크아웃 상태이고, 내가 소유자인 경우
                        if (node.updId == userId)
                        {
                            needStateChange = false; // 상태 변경 API 호출 생략
                        }
                    }

                    // 상태 변경이 필요한 경우 (체크인 상태 -> 체크아웃 시도)
                    if (needStateChange)
                    {
                        // 권한 체크
                        bool checkYn = common.m_intf.checkPermission(node.fldSeq, true, userId, node.objStateCommCd, node.objSeq, true);
                        if (!checkYn)
                        {
                            MessageBox.Show("폴더 권한이 없습니다. (루트 항목 기준)\n관리자에게 문의 바랍니다.");
                            // 권한이 없으면 이 파일은 건너뛰거나 에러 처리 (여기선 로그만 남기고 진행)
                            Console.WriteLine($"폴더 권한 없음: {node.objNm}");
                            continue;
                        }

                        // 상태 업데이트 (Check-Out: '0003')
                        AddInWebService.objectFileVO fileParam = new AddInWebService.objectFileVO();
                        AddInWebService.objectVO objParam = new AddInWebService.objectVO();
                        objParam.objSeq = node.objSeq;
                        objParam.objSeqSpecified = true;
                        objParam.objStateCommCd = "0003"; // 체크아웃 상태 코드
                        objParam.updId = userId;

                        common.m_intf.modifyObjectStateTpConnect(objParam, fileParam);

                        // 메모리 상의 객체 상태 업데이트 (UI 즉시 반영용)
                        node.objStateCommCd = "0003";
                        node.updId = userId;
                        isAnyUpdated = true;
                    }

                    // 5. 파일 다운로드 (무조건 수행하되, 내부에 로컬 존재 여부 체크)
                    // node 정보를 기반으로 서버 파일 리스트를 가져와 다운로드 함수 호출
                    DownloadFileIfNeeded(node);
                }

                if (isAnyUpdated)
                {
                    tlvData.RefreshObjects(allNodes); // 그리드 갱신
                }

                RefreshGrid();
                //MessageBox.Show("체크아웃 되었습");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"체크아웃 진행 중 오류가 발생했습니다.\n{ex.Message}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // [헬퍼] 트리 구조 재귀 순회하여 리스트에 담기
        private void CollectAllNodes(AddInWebService.objectVO parentNode, List<AddInWebService.objectVO> list)
        {
            list.Add(parentNode);

            // TreeListView(tlvData)에서 자식 목록 가져오기
            System.Collections.IEnumerable children = tlvData.GetChildren(parentNode);
            if (children != null)
            {
                foreach (var child in children)
                {
                    if (child is AddInWebService.objectVO childNode)
                    {
                        CollectAllNodes(childNode, list);
                    }
                }
            }
        }

        // [헬퍼] 파일 로컬 존재 여부 확인 후 다운로드
        private void DownloadFileIfNeeded(AddInWebService.objectVO node)
        {
            try
            {
                // 해당 객체의 파일 목록 조회
                AddInWebService.objectSearchTO schTo = new AddInWebService.objectSearchTO();
                schTo.objSeq = node.objSeq;
                //schTo.objSeqSpecified = true; // 필요시 주석 해제

                var fileList = common.m_intf.getObjectFileList(schTo);

                if (fileList == null || fileList.Length == 0) return;

                // 다운로드 루트 폴더 생성 확인
                if (!Directory.Exists(CHECKOUT_ROOT))
                {
                    Directory.CreateDirectory(CHECKOUT_ROOT);
                }

                foreach (var fileInfo in fileList)
                {
                    // 로컬 저장 경로 생성 규칙 (규칙에 맞게 수정 가능)
                    // 예: CHECKOUT_ROOT + objSeq + "_" + fileSeq + "_" + orgFileNm
                    string localFileName = $"{node.objSeq}_{fileInfo.objFileSeq}_{fileInfo.orgFileNm}";
                    string fullPath = Path.Combine(CHECKOUT_ROOT, localFileName);

                    // [중요 로직] 이미 파일이 있는지 확인
                    if (File.Exists(fullPath))
                    {
                        // 이미 존재하면 다운로드 건너뜀 (사용자 요구사항)
                        Console.WriteLine($"파일 이미 존재함(Skip): {localFileName}");
                        continue;
                    }

                    // 파일이 없으면 다운로드 수행
                    // 실제 파일 바이너리를 가져오기 위해 별도 호출이 필요할 수 있음
                    // getObjectFile 등 바이트 배열을 주는 메서드가 필요합니다. 
                    // 여기서는 기존 코드 Context를 기반으로 로직을 구성합니다.

                    // 주의: getObjectFileList는 메타데이터만 주는 경우가 많으므로
                    // 실제 다운로드는 common.m_intf.downloadFileProcessTpConnect 등을 사용해야 할 것입니다.
                    // 아래는 예시입니다. 사용 가능한 다운로드 API를 사용하세요.
                    AddInWebService.objectSearchTO downParam = new AddInWebService.objectSearchTO();
                    downParam.objSeq = node.objSeq;
                    //downParam.objSeqSpecified = true;
                    downParam.objFileSeq = fileInfo.objFileSeq;
                    downParam.objFileSeqSpecified = true;

                    // 웹서비스 호출 (메타데이터 조회: 파일 경로 획득용)
                    var resultFileVOArray = common.m_intf.getObjectFileListTpConnect(downParam);

                    if (resultFileVOArray != null && resultFileVOArray.Length > 0)
                    {
                        var targetFileVO = resultFileVOArray[0];
                        string serverFilePath = targetFileVO.fileNm; // 예: "/edm/6/filename.jpg"

                        // 만약 파일 데이터(byte[])가 있으면 그걸 우선 사용
                        if (targetFileVO.file != null && targetFileVO.file.Length > 0)
                        {
                            File.WriteAllBytes(fullPath, targetFileVO.file);
                        }
                        // 파일 데이터는 없고 URL 경로만 있는 경우 -> 웹 다운로드 시도
                        else if (!string.IsNullOrEmpty(serverFilePath))
                        {
                            try
                            {
                                // 서버 기본 주소와 파일 경로 조합 (m_sCompanyUrl은 Main의 멤버 변수)
                                string downloadUrl = m_sCompanyUrl + "/upload" + serverFilePath;

                                using (System.Net.WebClient wc = new System.Net.WebClient())
                                {
                                    wc.DownloadFile(downloadUrl, fullPath);
                                }
                            }
                            catch (Exception downloadEx)
                            {
                                Console.WriteLine($"웹 다운로드 실패: {downloadEx.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 개별 파일 다운로드 실패 시 전체 로직을 멈출지, 로그만 남길지 판단
                Console.WriteLine($"파일 다운로드 실패({node.objNm}): {ex.Message}");
                // throw; // 필요시 에러를 상위로 던짐
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oseq"></param>
        /// <param name="fseq"></param>
        /// <returns></returns>
        public int CheckOutProc(string oseq, string fseq)
        {
            int objseq = Convert.ToInt32(oseq);
            int fileseq = Convert.ToInt32(fseq);

            AddInWebService.objectFileVO filevo = GetFileInfo(objseq, fileseq, null);

            if (filevo == null)
            {
                System.Windows.Forms.MessageBox.Show("다운로드 할 파일의 정보가 없습니다");
                return -1;
            }

            string filename = GetLocalFileName(true, oseq, fseq, filevo.orgFileNm);

            bool islock = IsFileLocked(filename);

            if (islock == true)
            {
                MessageBox.Show("파일이 이미 실행되어있습니다.");
                return -2;
            }


            ExecuteProgram(filename);
            return 0;
        }

        private void ExecuteProgram(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show(this, "파일이 없습니다.");
                return;
            }

            Process myProcess = new Process();
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            myProcess.StartInfo.FileName = path;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isCheckOut"></param>
        /// <param name="objseq"></param>
        /// <param name="fileseq"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLocalFileName(bool isCheckOut, string objseq, string fileseq, string filename)
        {
            if (isCheckOut == true)
                return CHECKOUT_ROOT + objseq + "_" + fileseq + "_" + filename;
            else
                return DOWNLOAD_ROOT + objseq + "_" + fileseq + "_" + filename;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objseq"></param>
        /// <param name="fileseq"></param>
        /// <returns></returns>
        AddInWebService.objectFileVO GetFileInfo(int objseq, int fileseq, string verseq)
        {

            AddInWebService.objectSearchTO schTo = new AddInWebService.objectSearchTO();

            schTo.objSeq = (int)objseq;
            //schTo.objSeqSpecified = true;

            if (verseq != null)
            {
                schTo.verSeq = Convert.ToInt32(verseq);
                //schTo.verSeqSpecified = true;
            }
            else
                schTo.verSeq = 0;

            AddInWebService.objectFileVO[] fileList;

            fileList = (AddInWebService.objectFileVO[])common.m_intf.getObjectFileList(schTo);

            if (fileList.Length == 0 || fileList[0] == null)
                return null;

            if (fileList.Length == 1)
                return fileList[0];

            for (int i = 0; i < fileList.Length; i++)
            {
                if ((int)fileList[i].objFileSeq == fileseq)
                    return fileList[i];
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsFileLocked(string fname)
        {
            if (File.Exists(fname) == false)
                return false;

            FileInfo file = new FileInfo(fname);

            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// 현재 그리드에 있는 모든 객체(자식 포함)에 대해 상세 속성 정보를 조회하여 바인딩합니다.
        /// </summary>
        private void LoadObjectAttributes()
        {
            // 그리드에 데이터가 없으면 중단
            // 주의: tlvData.Objects는 루트만 반환할 수 있으므로, Roots를 체크하거나 GetItemCount 등을 확인
            if (tlvData.Roots == null) return;

            // 작업 중 UI가 멈춘 것처럼 보이지 않도록 커서 변경
            Cursor.Current = Cursors.WaitCursor;

            // 대량 업데이트 시 깜빡임 방지를 위해 UI 갱신 일시 중지
            tlvData.BeginUpdate();

            try
            {
                // 자식 노드까지 모두 순회하기 위해 TreeListView의 모든 항목을 가져오는 로직 사용
                // 방법 1: UI에 바인딩된 모든 노드를 순회 (확장된 것만 나올 수도 있음) -> 비추전
                // 방법 2: 바인딩 원본 데이터를 알면 좋으나, 여기선 Roots에서부터 재귀 탐색하거나
                //        TreeListView가 제공하는 전체 순회 기능을 사용

                // TreeListView는 내부적으로 전체 객체를 관리하는 리스트를 직접 노출하지 않을 수 있습니다.
                // 따라서 Roots를 기반으로 전체 노드를 수집하는 헬퍼 메서드(GetAllNodes 같은 것)를 사용하거나
                // IEnumerable<object>로 변환하여 처리해야 합니다.

                // 기존 코드는 tlvData.Objects를 썼는데 이는 루트만 가져옵니다.
                // 모든 노드를 리스트로 모읍니다.
                IEnumerable<object> allObjects = GetAllObjectsRecursive(tlvData.Roots);

                foreach (var item in allObjects)
                {
                    if (item is AddInWebService.objectVO obj)
                    {
                        try
                        {
                            // 1. 웹서비스 호출 (각 행의 objSeq로 조회)
                            string[] result = common.m_intf.getObjectInfo(obj.objSeq);

                            if (result != null && result.Length > 0)
                            {
                                List<string> keys = new List<string>();
                                List<string> values = new List<string>();

                                // 2. 데이터 파싱
                                foreach (string s in result)
                                {
                                    if (string.IsNullOrEmpty(s)) continue;

                                    // "!@#" 구분자로 분리
                                    string[] parts = s.Split(new string[] { "!@#" }, StringSplitOptions.None);

                                    string key = "";
                                    string val = "";

                                    if (parts.Length >= 2)
                                    {
                                        key = parts[0];
                                        val = parts[1];
                                    }
                                    else if (parts.Length == 1)
                                    {
                                        key = parts[0];
                                        val = "";
                                    }

                                    keys.Add(key);
                                    values.Add(val);
                                }

                                // 3. objectVO 객체에 배열 할당
                                obj.arrStrKey = keys.ToArray();
                                obj.arrStrValue = values.ToArray();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"속성 정보 조회 실패 (ObjSeq: {obj.objSeq}): {ex.Message}");
                        }
                    }
                }
            }
            finally
            {
                // UI 갱신 재개
                tlvData.EndUpdate();
                // 변경된 데이터 강제 반영 (특히 하위 토드)
                tlvData.Refresh();
                Cursor.Current = Cursors.Default;
            }
        }

        // 트리 구조의 모든 노드를 평면 리스트로 반환하는 헬퍼 메서드
        private IEnumerable<object> GetAllObjectsRecursive(System.Collections.IEnumerable roots)
        {
            if (roots == null) yield break;

            foreach (var item in roots)
            {
                yield return item;

                // tlvData의 ChildrenGetter를 활용하여 자식들을 가져옴
                // (주의: Cast 문제 방지를 위해 일반 object로 처리)
                System.Collections.IEnumerable children = tlvData.GetChildren(item);
                if (children != null)
                {
                    foreach (var child in GetAllObjectsRecursive(children))
                    {
                        yield return child;
                    }
                }
            }
        }

        // 그리드 새로고침 메서드
        private void RefreshGrid()
        {
            // 현재 폴더 트리에서 선택된 폴더가 있는지 확인
            if (tlvFolder.SelectedObject is FolderNode selectedFolder)
            {
                var objectSearchTO = new AddInWebService.objectSearchTO();
                if (int.TryParse(selectedFolder.fldSeq, out int fldSeq))
                {
                    objectSearchTO.fldSeq = fldSeq;
                    objectSearchTO.objStateCommGrpCd = "0009";
                    objectSearchTO.objInitStateCommGrpCd = "0018";
                    objectSearchTO.fldSeqSpecified = true;
                    objectSearchTO.deleteYn = "N";

                    var pagingSearchTO = new AddInWebService.pagingSearchTO();
                    // 페이지 설정 (전체 조회를 위해 넉넉하게 설정)
                    pagingSearchTO.curPage = 1;
                    pagingSearchTO.pageSize = 5000;

                    // 1. 웹서비스에서 평면 리스트(Flat List) 가져오기
                    var objectList = common.m_intf.getSearchObjectListTpConnect(objectSearchTO, pagingSearchTO);

                    if (objectList == null || objectList.Length == 0)
                    {
                        tlvData.ClearObjects();
                        return;
                    }

                    // 2. 트리 구조 구성 전에 속성 정보(Attribute) 미리 로드
                    // 여기서 objectList 전체에 대해 속성을 채워 넣습니다.
                    // (별도의 LoadObjectAttributes 호출 없이 데이터 자체를 완성)
                    LoadAttributesForList(objectList);

                    // 3. 평면 리스트를 트리 구조로 변환
                    m_ChildMap = new Dictionary<int, List<AddInWebService.objectVO>>();
                    _tempIdCounter = -1;

                    var roots = new List<AddInWebService.objectVO>();

                    foreach (var obj in objectList)
                    {
                        bool isRoot = (!obj.parObjSeqSpecified || obj.parObjSeq == 0);
                        if (isRoot)
                        {
                            roots.Add(obj);
                        }
                        else
                        {
                            if (!m_ChildMap.ContainsKey(obj.parObjSeq))
                            {
                                m_ChildMap[obj.parObjSeq] = new List<AddInWebService.objectVO>();
                            }
                            m_ChildMap[obj.parObjSeq].Add(obj);
                        }
                    }

                    // [수정] Getter 델리게이트 갱신 (m_ChildMap 참조)
                    tlvData.CanExpandGetter = delegate (object x)
                    {
                        if (x is AddInWebService.objectVO obj) return m_ChildMap.ContainsKey(obj.objSeq);
                        return false;
                    };
                    tlvData.ChildrenGetter = delegate (object x)
                    {
                        if (x is AddInWebService.objectVO obj && m_ChildMap.TryGetValue(obj.objSeq, out var children))
                            return children;
                        return new List<AddInWebService.objectVO>();
                    };

                    tlvData.Roots = roots;

                    // 5. UI 갱신
                    tlvData.Refresh();
                    tlvData.ExpandAll();
                }
            }
        }

        // 리스트 단위로 속성 정보를 로드하는 헬퍼 메서드 (기존 LoadObjectAttributes 대체/보조)
        private void LoadAttributesForList(IEnumerable<AddInWebService.objectVO> objects)
        {
            if (objects == null) return;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                foreach (var obj in objects)
                {
                    try
                    {
                        string[] result = common.m_intf.getObjectInfo(obj.objSeq);
                        if (result != null && result.Length > 0)
                        {
                            List<string> keys = new List<string>();
                            List<string> values = new List<string>();

                            foreach (string s in result)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] parts = s.Split(new string[] { "!@#" }, StringSplitOptions.None);

                                string key = parts.Length >= 1 ? parts[0] : "";
                                string val = parts.Length >= 2 ? parts[1] : "";

                                keys.Add(key);
                                values.Add(val);
                            }
                            obj.arrStrKey = keys.ToArray();
                            obj.arrStrValue = values.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"속성 로드 실패(ObjSeq:{obj.objSeq}): {ex.Message}");
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void resetGrid_Click(object sender, EventArgs e)
        {
            // 사용자가 실수로 누르는 경우를 대비해 확인 메시지 표시
            if (MessageBox.Show("등록 목록을 초기화 하시겠습니까?", "초기화 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                newAddGrid.ClearObjects();
            }
        }

        // tlvData 삭제 관련 이벤트 및 컨텍스트 메뉴 설정
        private void InitializeTlvDataDelete()
        {
            // 1. Delete 키 이벤트 연결
            tlvData.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteTlvDataNodes();
                }
            };

            // 2. 우클릭 컨텍스트 메뉴 생성
            ContextMenuStrip menu = tlvData.ContextMenuStrip;
            if (menu == null)
            {
                menu = new ContextMenuStrip();
                tlvData.ContextMenuStrip = menu;
            }

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("삭제");
            deleteItem.Click += (s, e) => DeleteTlvDataNodes();

            menu.Items.Add(deleteItem);
        }

        // tlvData 선택된 노드 및 하위 노드 삭제(deleteYn=Y) 처리 로직
        private void DeleteTlvDataNodes()
        {
            if (tlvData.SelectedObjects.Count == 0) return;


            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 삭제 대상 수집 (중복 방지)
                List<AddInWebService.objectVO> allTargets = new List<AddInWebService.objectVO>();

                // 선택된 노드 각각에 대해 하위 포함 수집
                foreach (var item in tlvData.SelectedObjects)
                {
                    if (item is AddInWebService.objectVO obj)
                    {
                        CollectAllNodes(obj, allTargets);
                    }
                }

                // objSeq 기준으로 중복 제거 (부모, 자식이 같이 선택되었을 경우 대비)
                var uniqueTargets = allTargets.GroupBy(n => n.objSeq).Select(g => g.First()).ToList();

                // 하위 레벨 포함 objStateCommCd가 "0003"인 것이 있는지 확인
                if (uniqueTargets.Any(n => n.objStateCommCd == "0003"))
                {
                    Cursor.Current = Cursors.Default; // 메시지 박스 표시 전 커서 복구
                    MessageBox.Show("체크아웃 된 자료는 삭제할 수 없습니다.\n 하위 항목도 확인해주세요.");
                    return;
                }
                if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?\n(하위 항목도 모두 삭제됩니다.)", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                // 수집된 모든 노드에 대해 deleteYn="Y" 처리
                foreach (var node in uniqueTargets)
                {
                    AddInWebService.objectVO modifyObjVO = new AddInWebService.objectVO();
                    AddInWebService.objectFileVO objectFileVO = new AddInWebService.objectFileVO();

                    modifyObjVO.objSeq = node.objSeq;
                    modifyObjVO.objSeqSpecified = true;
                    modifyObjVO.updId = userId; // Main 전역 변수 userId
                    modifyObjVO.deleteYn = "Y";
                    // 필요한 경우 userNm 등 추가 정보 설정

                    common.m_intf.modifyObjectStateTpConnect(modifyObjVO, objectFileVO);
                }

                MessageBox.Show("삭제되었습니다.");

                // 그리드 갱신
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 중 오류가 발생했습니다.\n{ex.Message}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void rowAdd_Click(object sender, EventArgs e)
        {
            // 1. 선택된 Parent Row 확인
            var selectedObj = tlvData.SelectedObject as AddInWebService.objectVO;
            if (selectedObj == null)
            {
                MessageBox.Show("하위 항목을 추가할 상위 도면(항목)을 선택해주세요.");
                return;
            }

            // 선택된 객체(부모가 될 객체)의 폴더 권한 등을 확인합니다.
            bool checkYn = common.m_intf.checkPermission(selectedObj.fldSeq, true, userId, selectedObj.objStateCommCd, selectedObj.objSeq, true);
            if (!checkYn)
            {
                MessageBox.Show("폴더 권한이 없습니다. (선택 항목 기준)\n관리자에게 문의 바랍니다.");
                return;
            }

            // 2. 파일 Browse 창 띄우기
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false; // 다중 선택 X
                openFileDialog.Title = "하위 항목으로 추가할 파일 선택";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        // 3. 선택한 파일들을 순회하며 서버에 생성
                        foreach (string fileName in openFileDialog.FileNames)
                        {
                            FileInfo fi = new FileInfo(fileName);

                            AddInWebService.objectVO newObj = new AddInWebService.objectVO();
                            newObj.objNm = fi.Name; // 파일명을 도면명으로 사용

                            // 기본 속성 설정
                            newObj.objStateCommCd = "0001"; // 작업중(Check-In 등 초기상태)
                            newObj.objTypeCommCd = "0001";
                            newObj.objInitStateCommCd = "0018";
                            newObj.securityLvl = 3;
                            newObj.deleteYn = "N";
                            newObj.regId = userId;
                            newObj.updId = userId;
                            newObj.ownerUserId = userId;

                            // 상위 객체(selectedObj) 정보 상속
                            newObj.fldSeq = selectedObj.fldSeq;
                            newObj.fldSeqSpecified = true;
                            newObj.clsSeq = g_clsSeq;
                            newObj.clsSeqSpecified = true;
                            newObj.parObjSeq = selectedObj.objSeq; // 부모 ID 설정
                            newObj.parObjSeqSpecified = true;

                            // 채번 (필요시)
                            newObj.objNumber = GetAutoNo(selectedObj.fldSeq);

                            // [API 1] Object 생성
                            string result = common.m_intf.createObjectTpConnect(newObj);

                            string createdObjSeqStr = null;
                            if (!string.IsNullOrEmpty(result))
                            {
                                string[] parts = result.Split('_');
                                if (parts.Length > 0) createdObjSeqStr = parts[0].Trim();
                            }

                            if (!string.IsNullOrEmpty(createdObjSeqStr) && int.TryParse(createdObjSeqStr, out int iObjSeq))
                            {
                                UploadFileProcess(fi.FullName, iObjSeq);

                                int iVerSeq = common.m_intf.createObjectVersion(iObjSeq, true, userId);

                            }
                        }

                        MessageBox.Show("파일이 등록되었습니다.");

                        // 4. UI 갱신 (서버 데이터를 다시 불러와 화면에 반영)
                        RefreshGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"파일 등록 중 오류가 발생했습니다.\n{ex.Message}");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
    }

}