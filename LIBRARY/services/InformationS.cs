using LIBRARY.controllers;
using LIBRARY.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.services
{
    public class InformationS
    {
        //查询全部  and  App查询所有Information
        InformationC informationC = new InformationC();
        public List<Information> queryAll(string customer_id)
        {
            return informationC.queryAll(customer_id); ;
        }
        public List<Information> queryAllIsSelect(string customer_id)
        {
            return informationC.queryAllIsSelect(customer_id); ;
        }
        //修改栏目信息
        public string setTemplate(Information entity)
        {           
            return informationC.setTemplate(entity);
        }

    }
}