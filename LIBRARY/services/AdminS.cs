using LIBRARY.controllers;
using LIBRARY.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.services
{
    public class AdminS
    {
        AdminC adminC = new AdminC();
        public Admin login(Admin entity)
        {
            return adminC.login(entity);
        }

        public Paging<Admin> query(Paging<Admin> page)
        {
            return adminC.query(page);
        }

        public string deleteAdmin(string id)
        {
            return adminC.deleteAdmin(id);
        }

        public string modifyAdmin(Admin entity)
        {
            return adminC.modifyAdmin(entity);
        }

        public string resetPassword(Admin entity)
        {
            return adminC.resetPassword(entity);
        }
       
        public string ifAdminName(string username)
        {         
            return adminC.ifAdminName(username);
        }
        public string addOperator(Admin entity)
        {           
            return adminC.addOperator(entity);
        }
        public string modyfyPassword(Admin entity)
        {
           
            return adminC.modyfyPassword(entity);
        }
        public string modifyEmail(Admin entity)
        {
            return adminC.modyfyPassword(entity);
        }        
    }
}