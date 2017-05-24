using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class AccountRelevance
    {
        //ID唯一标识
        public string id
        {
            get;
            set;
        }

        //帐户名
        public string email
        {
            get;
            set;
        }

        //账户密码
        public string password
        {
            get;
            set;
        }

        //公司ID
        public string customerId
        {
            get;
            set;
        }

        //关联ID
        public string relevanceId
        {
            get;
            set;
        }

    }
}