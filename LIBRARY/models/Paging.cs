using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Paging<T>
    {
        //pageCurrent当前页码
        public int pc
        {
            get;
            set;
        }
        //pagesize, 每页记录数
        public int ps
        {
            get;
            set;
        }
        //totalRecord, 总记录数
        public int tr
        {
            get;
            set;
        }

        //totalPage, 总页数
        public int tp
        {
            get;
            set;
        }

        //当前页记录
        public HashSet<T> datas
        {
            get;
            set;
        }

        //查询条件
        public T condition
        {
            get;
            set;
        }
    }
}