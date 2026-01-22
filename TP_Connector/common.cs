using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
// DevExpress
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AddInWebService = TP_Connector.AddInWebService;

namespace TP_Connector
{
    class common
    {
        public struct HeadName
        {
            public static string sFILEFULLPATH = "FILEFULLPATH";        // 로컬 경로
            public static string sFilePath = "filePath";                // 업로드경로
            public static string sObjNumber = "objNumber";              // 관리번호
            public static string sObjNm = "objNm";                      // 자료명
            public static string sObjDesc = "objDesc";                  // 설명
            public static string sSecurityLvl = "securityLvl";          // 보안등급
            public static string sObjStateCommCd = "objStatecommCd";    // 초기상태 
        }

        public static AddInWebService.AddinImplService m_intf = null;

        // DELETE: DataGridView용 사전 제거
        // public static Dictionary<string, DataGridView> dicDocs;

        // KEEP: DevExpress GridControl 보관용
        public static Dictionary<string, GridControl> dicDocsDX;

        public static string m_sCompanyUrl = "";
        public static string[] m_DB_OpenStr;

        public struct GlobalClientService
        {
            public static AddInWebService.AddinImplService addInWebService = null;
            public static AddInWebService.userVO userInfo = null;
            public static string loginIP = "";
        }

        public static string GetLocalIPAddr()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostname);
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                if (addr[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return addr[i].ToString();
            }
            return addr[0].ToString();
        }

        public static int ConnnectService(string serverUrl)
        {
            GlobalClientService.addInWebService = new AddInWebService.AddinImplService();
            if (GlobalClientService.addInWebService == null) return -1;
            GlobalClientService.addInWebService.Url = serverUrl;
            m_intf = GlobalClientService.addInWebService;
            return 0;
        }

        public static AddInWebService.AddinImplService GetService()
        {
            return GlobalClientService.addInWebService;
        }

        public static AddInWebService.folderVO4Addin[] GetChildFolder(int pseq)
        {
            try
            {
                AddInWebService.AddinImplService intf = m_intf;

                AddInWebService.codeConstants codeConst = intf.getCodeConstants();
                AddInWebService.folderSearchTO schTo = new AddInWebService.folderSearchTO();
                schTo.permissionUserId = "sysadm";
                schTo.permissionType = codeConst.permissionFolderView;
                schTo.parFldSeq = pseq;
                schTo.parFldSeqSpecified = true;

                int nReturn = intf.getFolderTotalCnt(schTo);
                if (nReturn == 0) return null;

                return intf.getFolderList(schTo, null);
            }
            catch (System.Exception se)
            {
                MessageBox.Show(se.Message);
                return null;
            }
        }


        //레지스트리에 등록된 정보들 가져오는거임
        public static string GetRegData(string val)
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("Software\\HKDMS");
            try
            {
                if (rkey == null)
                {
                    return "";
                }
                string str = (string)rkey.GetValue(val);
                rkey.Close();
                return str;
            }
            catch (Exception se)
            {
                System.Windows.Forms.MessageBox.Show(se.Message.ToString());

                if (rkey != null)
                    rkey.Close();
                return "";
            }
        }

        public static int SetRegData(string ky, string name, string val)
        {
            RegistryKey rootkey = null;
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("Software\\HKDMS", true);

            if (rkey == null)
            {
                rootkey = Registry.CurrentUser.OpenSubKey("Software", true);
                rkey = rootkey.CreateSubKey("HKDMS");
            }

            try
            {
                rkey.SetValue(name, (string)val);
                rkey.Close();

                if (rootkey != null)
                    rootkey.Close();

                return 0;
            }
            catch (Exception se)
            {
                System.Windows.Forms.MessageBox.Show(se.Message.ToString());
                rkey.Close();

                if (rootkey != null)
                    rootkey.Close();
                return -1;
            }
        }

        public static void DisConnectService()
        {
            GlobalClientService.addInWebService.Dispose();
            GlobalClientService.addInWebService = null;
        }


    }
}