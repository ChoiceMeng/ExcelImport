using ExcelImport.Ini;
using Login.Sync;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelImport
{
    public partial class ExcelImport : Form
    {
        private IniFile mIniFile = new IniFile(Environment.CurrentDirectory + "\\Config.ini");

        public ExcelImport()
        {
            InitializeComponent();

            string strPath = mIniFile.ReadString("Export", "ClientPath", "");
            SetClientPath(strPath);

            strPath = mIniFile.ReadString("Export", "ServerPath", "");
            SetServerPath(strPath);

            strPath = mIniFile.ReadString("Export", "FilePath", "");
            SetFilePath(strPath);
        }

        /// <summary>
        /// 设置客户端路径
        /// </summary>
        /// <param name="strFilePath"></param>
        private void SetClientPath(string strFilePath)
        {
            if (Directory.Exists(strFilePath))
            {
                this.mClientImportPath.Text = strFilePath;
                mIniFile.WriteString("Export", "ClientPath", strFilePath);
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="strFilePath"></param>
        private void SetFilePath(string strFilePath)
        {
            //if (Directory.Exists(strFilePath) || File.Exists(strFilePath))
            this.ImportFile.Text = strFilePath;
            mIniFile.WriteString("Export", "FilePath", strFilePath);
        }

        /// <summary>
        /// 服务器路径
        /// </summary>
        /// <param name="strFilePath"></param>
        private void SetServerPath(string strFilePath)
        {
            if (Directory.Exists(strFilePath))
            {
                this.mServerImportPath.Text = strFilePath;
                mIniFile.WriteString("Export", "ServerPath", strFilePath);
            }
        }

        private void ImportPathBtn_Click(object sender, EventArgs e)
        {
            string strOldPath = mClientImportPath.Text;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            dialog.SelectedPath = mClientImportPath.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = dialog.SelectedPath;
                SetClientPath(folderPath);
            }
        }

        private void ImportFileBtn_Click(object sender, EventArgs e)
        {
            string strOldPath = mClientImportPath.Text;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "请选择文件";
            dialog.Filter = "Excel(2000-2007)(*.xls)|*.xls|Excel(2010)(*.xlsx)|*.xlsx";
            dialog.FileName = strOldPath;
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                this.ImportFile.Text = string.Empty;
                string[] filePath = dialog.FileNames;
                bool bFirst = true;
                foreach (string strFileName in filePath)
                {
                    if (!bFirst)
                        this.ImportFile.Text += ";";
                    else
                        bFirst = false;

                    this.ImportFile.Text = this.ImportFile.Text + strFileName;
                }

                SetFilePath(this.ImportFile.Text);
            }
        }

        private void StartImport_Click(object sender, EventArgs e)
        {
            if (this.mServerImportPath.Text.Length == 0)
            {
                MessageBox.Show("没有指定服务器的路径");
                return;
            }

            if (this.mClientImportPath.Text.Length == 0)
            {
                MessageBox.Show("没有指定客户端的路径");
                return;
            }

            if (this.ImportFile.Text.Length == 0)
            {
                MessageBox.Show("没有指定要导出的文件夹或者文件");
                return;
            }

            if (this.mServerImportPath.Text.Equals(this.mClientImportPath.Text))
            {
                DialogResult result = MessageBox.Show("服务器与客户端导出路径一致, 是否确定?", "提示", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            this.StartImport.Enabled = false;
            this.LogList.Items.Clear();
            string strFiles = ImportFile.Text;
            string[] allFiles = strFiles.Split(';');
            foreach (string strFileOrPath in allFiles)
            {
                if (Directory.Exists(strFileOrPath))
                    Import_Path(strFileOrPath);
                else if (File.Exists(strFileOrPath))
                    Import_File(strFileOrPath);
            }

            this.StartImport.Enabled = true;
            this.LogList.Items.Add("转化结束");
        }

        private void ImportPath_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void ImportPath_DragDrop(object sender, DragEventArgs e)
        {
            string strPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (!Directory.Exists(strPath))
            {
                MessageBox.Show("只能拖放文件夹");
                return;
            }

            SetClientPath(strPath);
        }

        private void ServerImportPath_DragDrop(object sender, DragEventArgs e)
        {
            string strPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (!Directory.Exists(strPath))
            {
                MessageBox.Show("只能拖放文件夹");
                return;
            }

            SetServerPath(strPath);
        }

        private void ImportFile_DragDrop(object sender, DragEventArgs e)
        {
            string strPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            this.ImportFile.Text = strPath;
            SetFilePath(strPath);
        }

        private void Import_File(string strFilePath)
        {
            FileInfo fileInfo = new FileInfo(strFilePath);
            if (!fileInfo.Exists)
                return;

            if (!fileInfo.Extension.Equals(".xls") && !fileInfo.Extension.Equals(".xlsx"))
                return;

            Import_File(fileInfo);
        }

        private void Import_Path(string strFoderPath)
        {
            if (!Directory.Exists(strFoderPath))
                return;

            this.LogList.Items.Add("开始转化路径:" + strFoderPath);
            DirectoryInfo folder = new DirectoryInfo(strFoderPath);
            FileInfo[] allFiles = folder.GetFiles();
            foreach(FileInfo fileInfo in allFiles)
            {
                if (fileInfo == null)
                    continue;

                Import_File(fileInfo.FullName);
            }
        }

        private void Import_File(FileInfo fileInfo)
        {
            this.LogList.Items.Add("开始转化文件:" + fileInfo.FullName);
            int nColCount = 0;
            List<List<string>> allData = ReadExcel(fileInfo.FullName, ref nColCount);
            List<List<string>> clientData = new List<List<string>>();
            int nClientColCount = 0;
            List<List<string>> serverData = new List<List<string>>();
            int nServerColCount = 0;

            FindClientAndServer(ref allData, nColCount, ref clientData, ref nClientColCount, ref serverData, ref nServerColCount);


            int nIndex = fileInfo.Name.LastIndexOf(".") ;
            string strName = fileInfo.Name.Substring(0, nIndex);
            string strClientPath = this.mClientImportPath.Text + "\\" + strName + ".txt";
            string strServerPath = this.mServerImportPath.Text + "\\" + strName + ".txt";

            SaveToTxt(clientData, nClientColCount, strClientPath);
            SaveToTxt(serverData, nServerColCount, strServerPath);
        }

        /// <summary>
        /// 拆分服务器与客户端
        /// </summary>
        /// <param name="data"></param>
        /// <param name="clientData"></param>
        /// <param name="serverData"></param>
        private void FindClientAndServer(ref List<List<string>> data, int nColCount, ref List<List<string>> clientData, ref int clientColCount, ref List<List<string>> serverData, ref int serverColCount)
        {
            if (0 == nColCount || data == null || data.Count == 0)
                return;

            if( data.Count == 1 )
            {
                clientData = data;
                serverData = data;
                clientColCount = nColCount;
                serverColCount = nColCount;
                return ;
            }

            List<string> aMarkRow = data[1];
            if (aMarkRow.Count <= 0)
            {
                clientData = data;
                serverData = data;
                clientColCount = nColCount;
                serverColCount = nColCount;
            }

            for (int nRow = 0; nRow < data.Count; ++nRow)
            {
                List<string> aClientRow = new List<string>();
                List<string> aServerRow = new List<string>();
                List<string> aRow = data[nRow];

                // 每一列都走一遍
                for (int nCol = 0; nCol < nColCount; ++nCol)
                {
                    if (nCol >= aMarkRow.Count)
                        continue;

                    string strMark = aMarkRow[nCol].ToLower();
                    string strValue = string.Empty;
                    if (nCol < aRow.Count)
                        strValue = aRow[nCol];

                    if (strMark.Equals("server"))
                    {
                        aServerRow.Add(strValue);
                    }
                    else if (strMark.Equals("client"))
                    {
                        aClientRow.Add(strValue);
                    }
                    else if (strMark.Equals("all"))
                    {
                        aServerRow.Add(strValue);
                        aClientRow.Add(strValue);
                    }
                }

                // 放到列表中
                clientData.Add(aClientRow);
                serverData.Add(aServerRow);
                clientColCount = Math.Max(clientColCount, aClientRow.Count);
                serverColCount = Math.Max(serverColCount, aServerRow.Count);
            }
        }

        private List<List<string>> ReadExcel(string strFilePath, ref int nColCount)
        {
            List<List<string>> mAllRow = new List<List<string>>();
            FileStream stream = null;
            try
            {
                stream = new FileStream(strFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                IWorkbook mWorkBook = new XSSFWorkbook(stream);
                ISheet mSheet = mWorkBook.GetSheetAt(0);
                IEnumerator mRows = mSheet.GetRowEnumerator();
                string sValue = string.Empty;
                while (mRows.MoveNext())
                {
                    IRow mRow = (IRow)mRows.Current;
                    List<string> mRowValue = new List<string>();

                    nColCount = Math.Max(mRow.LastCellNum, nColCount);
                    int nCurCount = mRow.LastCellNum;
                    for (int nCol = 0; nCol < nCurCount; ++nCol)
                    {
                        sValue = string.Empty;
                        ICell mCell = mRow.GetCell(nCol);
                        if (mCell != null)
                        {
                            switch (mCell.CellType)
                            {
                                case CellType.Numeric:
                                    sValue = mCell.NumericCellValue.ToString();
                                    break;
                                case CellType.String:
                                    sValue = mCell.StringCellValue;
                                    break;
                                case CellType.Formula:
                                    {
                                        sValue = GetCellFormula(mCell);
                                    }
                                    break;
                                default:
                                    sValue = mCell.StringCellValue;
                                    break;
                            }
                        }

                        mRowValue.Add(sValue);
                    }

                    mAllRow.Add(mRowValue);
                }

                stream.Close();
            }
            catch (System.Exception ex)
            {
                if (stream != null)
                    stream.Close();

                MessageBox.Show(ex.ToString());
            }

            return mAllRow;
        }

        /// <summary>
        /// 公式
        /// </summary>
        /// <param name="mCell"></param>
        /// <returns></returns>
        private string GetCellFormula(ICell mCell)
        {
            if (mCell == null || mCell.CellType != CellType.Formula)
                return string.Empty;

            string strValue = string.Empty;

            try
            {
                switch (mCell.CachedFormulaResultType)
                {
                    case CellType.Numeric:
                        strValue = mCell.NumericCellValue.ToString();
                        break;
                    case CellType.String:
                        strValue = mCell.StringCellValue;
                        break;
                    default:
                        strValue = mCell.StringCellValue;
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return strValue;
        }

        private void SaveToTxt(List<List<string>> allRow, int nColCount, string strFileName)
        {
            if (nColCount == 0 || allRow.Count == 0)
                return;

            StreamWriter mWriter = null;
            try
            {
                mWriter = new StreamWriter(strFileName, false, Encoding.Unicode);

                foreach (List<string> aRow in allRow)
                {
                    string strContent = string.Empty;
                    for (int nIndex = 0; nIndex < nColCount; ++nIndex)
                    {
                        if (0 != nIndex)
                            strContent += "\t";

                        if (nIndex >= aRow.Count)
                            continue;

                        strContent += aRow[nIndex];
                    }
                    strContent += "\r\n";
                    //byte[] utf8Bytes = Encoding.Default.GetBytes(strContent);
                    ////byte[] byWriteData = Encoding.Convert(Encoding.Default, Encoding.Unicode, utf8Bytes);
                    //strContent = Encoding.Unicode.GetString(Encoding.Convert(Encoding.Default, Encoding.Unicode, Encoding.Default.GetBytes(strContent)));
                    mWriter.Write(strContent);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            finally
            {
                if (mWriter != null)
                    mWriter.Close();
            }
        }

        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTimer_Tick(object sender, EventArgs e)
        {
            SyncMgr.getInstance().HeartBeat();
        }

        private void mServerImportBtn_Click(object sender, EventArgs e)
        {
            string strOldPath = mServerImportPath.Text;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            dialog.SelectedPath = mServerImportPath.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = dialog.SelectedPath;
                SetServerPath(folderPath);
            }
        }
    }
}
