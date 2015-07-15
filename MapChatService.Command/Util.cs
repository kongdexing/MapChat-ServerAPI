using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapChatService.Commond
{
    public class Util
    {
       
        public static string GetID()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            //得到了天和毫秒用于建立字节字符串
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));

            // 创建字节数组 
            // 服务器是精确到1/第三百毫秒，除以3.333333
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //转化字节匹以配SQL命令
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // 复制GUID的值到数组中。
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            Guid guid = new System.Guid(guidArray);
            return guid.ToString().Replace("-", "");
        }
    }
}
