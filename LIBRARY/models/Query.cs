using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.models
{
    public class Query
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string Qq { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperateTime { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        public string ClientAddress { get; set; }
        /// <summary>
        /// 单位电话
        /// </summary>
        public string ClientPhone { get; set; }
        /// <summary>
        /// 主营业务
        /// </summary>
        public string ClientBusiness { get; set; }
        /// <summary>
        /// 公司网址
        /// </summary>
        public string ClientUrl { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string Nature { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 是否显示电话
        /// </summary>
        public string PhoneShow { get; set; }
        /// <summary>
        /// 是否显示职务
        /// </summary>
        public string PositionShow { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int? CustomerId { get; set; }
        /// <summary>
        /// 联系人信息逻辑删除标识
        /// </summary>
        public string IsDeleted { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public string Classify { get; set; }
        /// <summary>
        /// 联系人登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string CustomerContacts { get; set; }
        /// <summary>
        /// 客户创建时间
        /// </summary>
        public string CustomerOpenDate { get; set; }
        /// <summary>
        /// 客户信息逻辑删除标识
        /// </summary>
        public string CustomerIsDeleted { get; set; }
    }
}