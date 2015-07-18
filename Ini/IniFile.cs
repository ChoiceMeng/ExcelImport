using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ExcelImport.Ini
{
    class IniFile
    {
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="strVAlue"></param>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string strSection, string strKey, string strValue, string strFilePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string strSection, string strKey, string strDefine, StringBuilder retValue, int nSize, string strFilePath);

        /// <summary>
        /// 文件路径
        /// </summary>
        private string mFilePath = string.Empty;

        /// <summary>
        /// 文件
        /// </summary>
        public IniFile()
        {

        }

        /// <summary>
        /// 文件
        /// </summary>
        /// <param name="strFilePath"></param>
        public IniFile(string strFilePath)
        {
            SetFilePath(strFilePath);
        }

        /// <summary>
        /// 设置文件路径
        /// </summary>
        /// <param name="strFilePath"></param>
        public void SetFilePath(string strFilePath)
        {
            this.mFilePath = strFilePath;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        public string GetFilePath()
        {
            return this.mFilePath;
        }

        /// <summary>
        /// 写值
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public long WriteString(string strSection, string strKey, string strValue)
        {
            return WritePrivateProfileString(strSection, strKey, strValue, this.mFilePath);
        }

        /// <summary>
        /// 写整形
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="nValue"></param>
        /// <returns></returns>
        public long WriteInt(string strSection, string strKey, int nValue)
        {
            return WriteString(strSection, strKey, nValue.ToString());
        }

        /// <summary>
        /// 写整形
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="nValue"></param>
        /// <returns></returns>
        public long WriteLong(string strSection, string strKey, long nValue)
        {
            return WriteString(strSection, strKey, nValue.ToString());
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="strDefine"></param>
        /// <returns></returns>
        public string ReadString(string strSection, string strKey, string strDefine)
        {
            StringBuilder retValue = new StringBuilder(1024);
            int nSize = GetPrivateProfileString(strSection, strKey, strDefine, retValue, 1024, this.mFilePath);

            return retValue.ToString();
        }

        /// <summary>
        /// 读取Int
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="nDefine"></param>
        /// <returns></returns>
        public int ReadInt(string strSection, string strKey, int nDefine)
        {
            string strValue = ReadString(strSection, strKey, nDefine.ToString());
            int nValue = 0;
            int.TryParse(strValue, out nValue);

            return nValue;
        }
    }
}
