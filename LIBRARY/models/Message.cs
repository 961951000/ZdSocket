using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Message
    {
        public string id
        {
            get;
            set;
        }
        public string title
        {
            get;
            set;
        }
        public string content
        {
            get;
            set;
        }
        public string customerId
        {
            get;
            set;
        }
    }
}