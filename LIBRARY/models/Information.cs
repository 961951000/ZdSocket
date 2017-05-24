using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Information
    {
        public string id { 
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string customerId//客户
        {
            get;
            set;
        }
        public bool is_select
        {
            get;
            set;
        }
        public bool isDeleted
        {
            get;
            set;
        }

    }
}