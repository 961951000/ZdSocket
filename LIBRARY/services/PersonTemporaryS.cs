using LIBRARY.controllers;
using LIBRARY.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.services
{    
    class PersonTemporaryS
    {
        PersonTemporaryC personTemporaryC = new PersonTemporaryC();
        public Paging<PersonTemporary> query(Paging<PersonTemporary> page)
        {
            return personTemporaryC.query(page);
        }
  
    }
}
