using LIBRARY.controllers;
using LIBRARY.models;
using LIBRARY.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.services
{
    public class ContactsS
    {
        ContactsC contactsC = new ContactsC();
        public List<Query> Query(Query entity)
        {
            return contactsC.Query(entity);
        }
        public Contacts login(Contacts entity)
        {
            return contactsC.login(entity);
        }

        //添加账号
        public Contacts loginRele(Contacts entity,string firstPhone)
        {
            return contactsC.loginRele(entity, firstPhone);
        }

        //取消账号关联
        public object cancelRele(string phone) {
            return contactsC.cancelRele(phone);
        }

        public string addList(List<Contacts> list)
        {
            return contactsC.addList(list);
        }
        public Contacts prepareAdd(string customer_id)
        {
            return contactsC.prepareAdd(customer_id);
        }
        public string informationConfimNo(string id)
        {
            return new PersonTemporaryC().no(id);
        }
        public string informationConfim(string id)
        {
            PersonTemporaryC personTemporaryC = new PersonTemporaryC();
            string result = string.Empty;
            if (contactsC.informationConfim(personTemporaryC.query(id)) > 0)
            {
                result = personTemporaryC.no(id);
            }
            return result;
        }
        public string addContacts(Contacts entity)
        {
            return contactsC.addContacts(entity);
        }
        public string modifyImg(string id, string img)
        {
            return contactsC.modifyImg(id, img);
        }
        public Paging<Contacts> query(Paging<Contacts> page)
        {
            return contactsC.query(page);
        }

        public string delSureContacts(Contacts entity)
        {
            return contactsC.delSureContacts(entity);
        }

        public string modifyContacts(Contacts entity)
        {
            return contactsC.modifyContacts(entity);
        }

        public HashSet<Contacts> queryAll(Contacts entity)
        {
            return contactsC.queryAll(entity);
        }
        public List<Contacts> queryPhone()
        {
            return contactsC.queryAll();
        }
        //App查询所有Contacts
        public List<Contacts> getContacts(Contacts entity)
        {
            return contactsC.getContacts(entity);
        }

        public string upload(Contacts entity)
        {
            string result = "0";
            //if (Convert.ToInt32(Basic.getTr("person_temporary where person_id='" + entity.personId + "'")) > 0)
            //{
            //    if (Convert.ToInt32(ifEmail(entity.email, "person_temporary")) > 0)
            //    {
            //        result = "-1";
            //    }
            //    else
            //    {
            //        result = contactsC.modifyContactsTemporary(entity);
            //    }
            //}
            //else
            //{
                result = contactsC.addContactsTemporary(entity);
            //}
            return result;
        }

        //判断有些是否存在
        public string ifPhone(string phone, string table)
        {
            return Basic.getTr(table + " where phone='" + phone + "'");
        }

        //查询全部sendPhone
        public List<Contacts> queryAll()
        {
            return contactsC.queryAll();
        }

        //模糊查询Contacts表中的name属性 sendPhone
        public List<Contacts> queryName(string name)
        {
            return contactsC.queryName(name);
        }

        //查询关联账号
        public List<dynamic> selectRele(Contacts entity)
        {
            return contactsC.selectRele(entity);
        }

        //查询同一个手机号的所有组织，公司
        public List<Contacts> queryAllPhone(Contacts entity)
        {
            return contactsC.queryAllPhone(entity);
        }
    }
}