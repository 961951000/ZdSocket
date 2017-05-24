using LIBRARY.controllers;
using LIBRARY.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.services
{
    public class MessageS
    {
        MessageC messageC = new MessageC();
        public string addFw(Message entity)
        {
            return messageC.addFw(entity);
        }

        //查询全部发文Title
        public List<Message> getTitle()
        {
            return messageC.getTitle();
        }
        //查询全部message
        public HashSet<Message> queryAll(Message entity)
        {
            return messageC.queryAll(entity);
        }
        //删除发文内容
        public string del(string id) {
            return messageC.del(id);
        }
        //修改（编辑）发文内容
        public string updateFw(Message entity) {
            return messageC.updateFw(entity);
        }
    }
}