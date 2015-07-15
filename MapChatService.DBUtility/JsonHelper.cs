using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace MapChatService.DBUtility
{
    /// <summary>
    /// Json操作类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string GetJson<T>(T obj)
        {
            //记住 添加引用 System.ServiceModel.Web 
            /**
             * 如果不添加上面的引用,System.Runtime.Serialization.Json; Json是出不来的哦
             * */
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, obj);
                string szJson = Encoding.UTF8.GetString(ms.ToArray());
                return szJson;
            }

        }
        private static string GetAllInfosString(string infoId, string infoCol, string infoColParent, string infoColText, DataTable table)
        {
            string[] strArray = infoId.Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            foreach (string str in strArray)
            {
                DataRow[] rowArray = table.Select(infoCol + "='" + str + "'");
                if (!builder.ToString().Contains(str) && (rowArray.Length > 0))
                {
                    builder.Append("{");
                    builder.Append("\"id\":\"");
                    builder.Append(rowArray[0][infoCol].ToString());
                    builder.Append("\",\"text\":\"");
                    builder.Append(rowArray[0][infoColText].ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”"));
                    builder.Append("\",\"state\":\"open\"");
                    builder.Append(",\"checked\":true");
                    if (!TreeUseIcon)
                    {
                        builder.Append(",\"iconCls\":\"icon-none\"");
                    }
                    builder.Append(",\"children\":[");
                    builder.Append(GetIdsString(rowArray[0][infoCol].ToString(), infoCol, infoColParent, infoColText, table));
                    builder.Append("]},");
                }
            }
            string str2 = "";
            if (builder.ToString() != "")
            {
                str2 = builder.ToString(0, builder.Length - 1);
            }
            return str2;
        }

        public static string GetAllTreeJson(string infoIds, string infoCol, string infoParentCol, string infoColText, DataTable table)
        {
            return ("[" + GetAllInfosString(infoIds, infoCol, infoParentCol, infoColText, table) + "]");
        }

        public static string GetDataGridJson(IList list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"total\":\"" + list.Count + "\",\"rows\":[");
            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append("{");
                    for (int j = 0; j < properties.Length; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append(",");
                        }
                        string str = "";
                        if (properties[j].GetValue(list[i], null) != null)
                        {
                            str = properties[j].GetValue(list[i], null).ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”");
                        }
                        builder.AppendFormat("\"" + properties[j].Name + "\":\"{0}\"", str);
                    }
                    builder.Append("}");
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }

        /// <summary>
        /// APP接口用到
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pagecount">总共多少页</param>
        /// <returns></returns>
        public static string GetDataGridJsonByHair(IList list, int pagecount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"pagetotal\":\"" + pagecount + "\",\"rows\":[");
            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append("{");
                    for (int j = 0; j < properties.Length; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append(",");
                        }
                        string str = "";
                        if (properties[j].GetValue(list[i], null) != null)
                        {
                            str = properties[j].GetValue(list[i], null).ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”");
                        }
                        builder.AppendFormat("\"" + properties[j].Name + "\":\"{0}\"", str);
                    }
                    builder.Append("}");
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }
        public static string GetDataGridJsonByHair(DataTable dt, int pagecount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"pagetotal\":\"" + pagecount + "\",\"rows\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append(",");
                        }
                        string str = "";
                        if (dt.Rows[i][dt.Columns[j].ColumnName] != null)
                        {
                            str = dt.Rows[i][dt.Columns[j].ColumnName].ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”");
                        }
                        builder.AppendFormat("\"" + dt.Columns[j].ColumnName + "\":\"{0}\"", str);
                    }
                    builder.Append("}");
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }
        public static string GetDataGridJson(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"total\":\"" + dt.Rows.Count + "\",\"rows\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append(",");
                        }
                        string str = "";
                        if (dt.Rows[i][dt.Columns[j].ColumnName] != null)
                        {
                            str = dt.Rows[i][dt.Columns[j].ColumnName].ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”");
                        }
                        builder.AppendFormat("\"" + dt.Columns[j].ColumnName + "\":\"{0}\"", str);
                    }
                    builder.Append("}");
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }

        private static string GetIdsString(string infoId, string infoCol, string infoColParent, string infoColText, DataTable table)
        {
            DataRow[] rowArray = table.Select(infoColParent + "='" + infoId + "'");
            StringBuilder builder = new StringBuilder();
            if (rowArray.Length == 0)
            {
                return string.Empty;
            }
            foreach (DataRow row in rowArray)
            {
                builder.Append("{");
                builder.Append("\"id\":\"");
                builder.Append(row[infoCol].ToString());
                builder.Append("\",\"text\":\"");
                builder.Append(row[infoColText].ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”"));
                builder.Append("\",\"state\":\"open\"");
                builder.Append(",\"checked\":true");
                if (!TreeUseIcon)
                {
                    builder.Append(",\"iconCls\":\"icon-none\"");
                }
                builder.Append(",\"children\":[");
                builder.Append(GetIdsString(row[infoCol].ToString(), infoCol, infoColParent, infoColText, table));
                builder.Append("]},");
            }
            string str = "";
            if (builder.ToString() != "")
            {
                str = builder.ToString(0, builder.Length - 1);
            }
            return str;
        }

        private static string GetInfosString(string infoId, string infoCol, string infoColParent, string infoColText, DataTable table)
        {
            DataRow[] rowArray = table.Select(infoColParent + "='" + infoId + "'");
            StringBuilder builder = new StringBuilder();
            if (rowArray.Length == 0)
            {
                return string.Empty;
            }
            foreach (DataRow row in rowArray)
            {
                builder.Append("{");
                builder.Append("\"id\":\"");
                builder.Append(row[infoCol].ToString());
                builder.Append("\",\"text\":\"");
                builder.Append(row[infoColText].ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”"));
                builder.Append("\",\"state\":\"open\"");
                builder.Append("");
                if (!TreeUseIcon)
                {
                    builder.Append(",\"iconCls\":\"icon-none\"");
                }
                builder.Append(",\"children\":[");
                builder.Append(GetInfosString(row[infoCol].ToString(), infoCol, infoColParent, infoColText, table));
                builder.Append("]},");
            }
            string str = "";
            if (builder.ToString() != "")
            {
                str = builder.ToString(0, builder.Length - 1);
            }
            return str;
        }

        public static string GetSelectJson(Type em, string selectedid, bool usePlease)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            if (usePlease)
            {
                builder.Append("{");
                builder.Append("\"id\":\"-1\",");
                builder.AppendFormat("\"text\":\"{0}\"", ToUnicode("请选择"));
                builder.Append("},");
            }
            foreach (string str in Enum.GetNames(em))
            {
                builder.Append("{");
                builder.AppendFormat("\"id\":\"{0}\",", (int)Enum.Parse(em, str));
                builder.AppendFormat("\"text\":\"{0}\"", ToUnicode(str));
                int num2 = (int)Enum.Parse(em, str);
                if (selectedid == num2.ToString())
                {
                    builder.Append(",\"selected\":\"true\"");
                }
                builder.Append("},");
            }
            builder = builder.Replace(",", "", builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public static string GetSelectJson(string id, string text, IList list, string selectedid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    foreach (PropertyInfo info in properties)
                    {
                        if (info.Name.ToLower() == id.ToLower())
                        {
                            builder.Append("{");
                            builder.AppendFormat("\"id\":\"{0}\"", info.GetValue(list[i], null));
                            string str = info.GetValue(list[i], null).ToString();
                            if (selectedid == str)
                            {
                                builder.Append(",\"selected\":\"true\"");
                            }
                            builder.Append(",");
                        }
                        if (info.Name.ToLower() == text.ToLower())
                        {
                            builder.AppendFormat("\"text\":\"{0}\"", info.GetValue(list[i], null));
                            builder.Append("}");
                        }
                    }
                }
            }
            builder.Append("]");
            return builder.ToString();
        }

        public static string GetSelectJson(string id, string text, DataTable dt, string selectedid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i][text].ToString() != "") && (dt.Rows[i][id].ToString() != ""))
                {
                    builder.Append("{");
                    builder.AppendFormat("\"id\":\"{0}\",", dt.Rows[i][id]);
                    builder.AppendFormat("\"text\":\"{0}\"", ToUnicode(dt.Rows[i][text].ToString()));
                    if (selectedid == dt.Rows[i][id].ToString())
                    {
                        builder.Append(",\"selected\":\"true\"");
                    }
                    builder.Append("},");
                }
            }
            builder = builder.Replace(",", "", builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public static string GetSelectMulJson(string id, string text, IList list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    foreach (PropertyInfo info in properties)
                    {
                        if (info.Name == id)
                        {
                            builder.Append("{");
                            builder.AppendFormat("\"value\":\"{0}\"", info.GetValue(list[i], null));
                            builder.Append(",");
                        }
                        if (info.Name == text)
                        {
                            builder.AppendFormat("\"name\":\"{0}\"", info.GetValue(list[i], null));
                            builder.Append("}");
                        }
                    }
                }
            }
            builder.Append("]");
            return builder.ToString();
        }

        public static string GetSelectMulJson(string id, string text, DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(",");
                }
                builder.Append("{");
                builder.AppendFormat("\"name\":\"{0}\"", dt.Rows[i][text]);
                builder.Append(",");
                builder.AppendFormat("\"value\":\"{0}\"", dt.Rows[i][id]);
                builder.Append("}");
            }
            builder.Append("]");
            return builder.ToString();
        }

        public static string GetTreeGridJson(string infoId, string infoCol, string infoParentCol, DataTable table)
        {
            return ("[" + GetTreeGridsString(infoId, infoCol, infoParentCol, table) + "]");
        }

        private static string GetTreeGridsString(string infoId, string infoCol, string infoColParent, DataTable table)
        {
            DataRow[] rowArray = table.Select(infoColParent + "='" + infoId + "'");
            if (rowArray.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in rowArray)
            {
                builder.Append("{\"");
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i != 0)
                    {
                        builder.Append(",\"");
                    }
                    builder.Append(row.Table.Columns[i].ColumnName);
                    builder.Append("\":\"");
                    builder.Append(row[i].ToString().Replace("\r", "<br>").Replace("\n", "<br>").Replace("\"", "”"));
                    builder.Append("\"");
                }
                if (!TreeGridUseIcon)
                {
                    builder.Append(",\"iconCls\":\"icon-none\"");
                }
                else if (TreeGridIconCss != "")
                {
                    builder.Append(",\"iconCls\":\"" + TreeGridIconCss + "\"");
                }
                builder.Append(",\"children\":[");
                builder.Append(GetTreeGridsString(row[infoCol].ToString(), infoCol, infoColParent, table));
                builder.Append("]},");
            }
            string str = "";
            if (builder.ToString() != "")
            {
                str = builder.ToString(0, builder.Length - 1);
            }
            return str;
        }

        public static string GetTreeJson(string infoId, string infoCol, string infoParentCol, string infoColText, DataTable table)
        {
            return ("[" + GetInfosString(infoId, infoCol, infoParentCol, infoColText, table) + "]");
        }

        public static string ToGb2312(string str)
        {
            string str2 = "";
            MatchCollection matchs = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bytes = new byte[2];
            foreach (Match match in matchs)
            {
                bytes[0] = (byte)int.Parse(match.Groups[2].Value, NumberStyles.HexNumber);
                bytes[1] = (byte)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
                str2 = str2 + Encoding.Unicode.GetString(bytes);
            }
            return str2;
        }

        public static string ToJson(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append(",");
                        }
                        builder.AppendFormat("\"" + dt.Columns[j].ColumnName + "\":\"{0}\"", dt.Rows[i][dt.Columns[j].ColumnName]);
                    }
                    builder.Append("}");
                }
            }
            return builder.ToString();
        }

        public static string ToJson(this object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0L;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static string ToJson(DataSet dataSet, IDictionary<string, IDictionary<string, string>> details)
        {
            string str = string.Empty;
            if (((dataSet == null) || (dataSet.Tables.Count <= 0)) || (dataSet.Tables[0].Rows.Count <= 0))
            {
                return str;
            }
            int num = 0;
            int num2 = 0;
            str = str + "[";
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (num != 0)
                {
                    str = str + ",";
                }
                num2 = 0;
                str = str + "{";
                foreach (DataColumn column in dataSet.Tables[0].Columns)
                {
                    if (num2 != 0)
                    {
                        str = str + ",";
                    }
                    if ((details != null) && details.ContainsKey(column.ColumnName))
                    {
                        IDictionary<string, string> dictionary = details[column.ColumnName];
                        if ((dictionary != null) && dictionary.ContainsKey(row[column].ToString()))
                        {
                            str = str + string.Format("'{0}':'{1}'", column.ColumnName.ToLower(), dictionary[row[column].ToString()]);
                        }
                        else
                        {
                            str = str + string.Format("'{0}':'{1}'", column.ColumnName.ToLower(), row[column]);
                        }
                    }
                    else
                    {
                        str = str + string.Format("'{0}':'{1}'", column.ColumnName.ToLower(), row[column]);
                    }
                    num2++;
                }
                str = str + "}";
                num++;
            }
            return (str + "]");
        }

        public static string ToUnicode(string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i += 2)
            {
                str2 = str2 + @"\u" + bytes[i + 1].ToString("x").PadLeft(2, '0') + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }

        public static string TreeGridIconCss
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }

        public static bool TreeGridUseIcon
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }

        public static bool TreeUseIcon
        {
            [CompilerGenerated]
            get;
            [CompilerGenerated]
            set;
        }

    }
}
