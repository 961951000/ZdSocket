using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Customer
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string customerCode
        {
            get;
            set;
        }
        public string address
        {
            get;
            set;
        }

        public string contacts
        {
            get;
            set;
        }
        public string openDate
        {
            get;
            set;
        }
        public string isDeleted
        {
            get;
            set;
        }
    }
}