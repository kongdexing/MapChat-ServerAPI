using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapChatService.Model
{
    public class RetModel
    {
        /// <summary>
        /// 返回值详见ResultType
        /// </summary>
        public string StatusCode { get; set; }
        public string RetValue { get; set; }

        public bool IsSuccess
        {
            get { return StatusCode == "1"; }
        }

        public string Message
        {
            get { return RetValue; }
        }

    }
}
