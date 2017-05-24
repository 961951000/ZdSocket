using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Log
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get;
            set;
        }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get;
            set;
        }
    }
}