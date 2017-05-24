using LIBRARY.controllers;
using LIBRARY.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.services
{
    public class CustomerS
    {
        CustomerC customerC = new CustomerC();
        public Paging<Customer> query(Paging<Customer> page)
        {
            return customerC.query(page);
        }
        //App查询所有Customer
        public List<Customer> getCustomer(Customer entity)
        {
            return customerC.getCustomer(entity);
        }
    }
}