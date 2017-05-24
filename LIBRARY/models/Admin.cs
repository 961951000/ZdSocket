using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Admin
    {
        public string id
        {
            get;
            set;
        }

        public string username
        {

            get;
            set;
        }

        public string password
        {
            get;
            set;
        }
        public string newPassword
        {
            get;
            set;
        }
        public string serial
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }

        public string role
        {
            get;
            set;
        }

        public string customerId//客户
        {
            get;
            set;
        }
    }
}