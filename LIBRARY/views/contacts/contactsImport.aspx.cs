using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Log;
using Log = LIBRARY.models.Log;

namespace LIBRARY.views.contacts
{
    public partial class contactsImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["username"] == null)
                {
                    Response.Redirect("../home/login.aspx");
                }
                try
                {
                    HttpFileCollection files = HttpContext.Current.Request.Files;//得到所有的FILE控件
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile postedFile = files[i];//取得单个文件
                        Stream fileStream = postedFile.InputStream;
                        StreamReader sr = new StreamReader(fileStream, Encoding.Default);

                        string path = System.AppDomain.CurrentDomain.BaseDirectory;
                        path = path + ConfigurationManager.AppSettings["sourceFiles"].ToString();
                        if (System.IO.Directory.Exists(path) == false)
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }

                        string fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);
                        string filePos = path + "\\" + fileName;
                        if (File.Exists(filePos))
                        {
                            File.Delete(filePos);
                        }
                        postedFile.SaveAs(filePos);
                        DataTable dt = getExcel(filePos, Path.GetExtension(Request.Files[0].FileName));
                        List<Contacts> list = new List<Contacts>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            //if (dt.Rows.IndexOf(dr) == dt.Rows.Count-1)
                            //{
                            //    break;
                            //}
                            Contacts contacts = new Contacts();
                            try { contacts.name = Convert.ToString(dr[0]); } catch { contacts.name = ""; }
                            try { contacts.birthday = Convert.ToString(dr[1]); } catch { contacts.birthday = ""; }
                            try { contacts.clientName = Convert.ToString(dr[2]); } catch { contacts.clientName = ""; }
                            try { contacts.position = Convert.ToString(dr[3]); } catch { contacts.position = ""; }
                            try { contacts.clientAddress = Convert.ToString(dr[4]); } catch { contacts.clientAddress = ""; }
                            try { contacts.zip = Convert.ToString(dr[5]); } catch { contacts.zip = ""; }
                            try { contacts.clientPhone = Convert.ToString(dr[6]); } catch { contacts.clientPhone = ""; }
                            try { contacts.phone = Convert.ToString(dr[7]); } catch { contacts.phone = ""; }
                            try { contacts.email = Convert.ToString(dr[8]); } catch { contacts.email = ""; }
                            try { contacts.img = Convert.ToString(dr[9]); } catch { contacts.img = ""; }
                            try { contacts.qq = Convert.ToString(dr[10]); } catch { contacts.qq = ""; }
                            try { contacts.fax = Convert.ToString(dr[11]); } catch { contacts.fax = ""; }
                            try { contacts.nature = Convert.ToString(dr[12]); } catch { contacts.nature = ""; }
                            try { contacts.legalPerson = Convert.ToString(dr[13]); } catch { contacts.legalPerson = ""; }
                            try { contacts.clientUrl = Convert.ToString(dr[14]); } catch { contacts.clientUrl = ""; }
                            try { contacts.classify = Convert.ToString(dr[15]); } catch { contacts.classify = ""; }
                            try { contacts.clientBusiness = Convert.ToString(dr[16]); } catch { contacts.clientBusiness = ""; }
                            contacts.phoneShow = "1";
                            contacts.positionShow = "1";
                            contacts.customerId = Session["customerId"].ToString();
                            list.Add(contacts);
                        }
                        string count = new ContactsS().addList(list);
                    }
                    Response.Redirect("../prompt/success.aspx", false);
                }
                catch (Exception ex)
                {
                    Loger.WriteError(ex);
                    Response.Redirect("../prompt/error.aspx");
                }
            }
        }
        //加载Excel 
        public static DataTable getExcel(string filePos, string fileExt)
        {
            /* string connstr = "";
             //判断xls文件还是xlsx文件
             if (fileExt == ".xls")
             {
                 //HDR = Yes表示将excel中第一行做为列名
                 connstr = "Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + filePos + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
             }
             else
             {
                 connstr = "Provider=Microsoft.ACE.OLEDB.12.0 ;Data Source=" + filePos + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";
                 //未在本地计算机上注册"Microsoft.ACE.OLEDB.12.0"  下载安装在 http://download.microsoft.com/download/7/0/3/703ffbcb-dc0c-4e19-b0da-1463960fdcdb/AccessDatabaseEngine.exe
             }*/
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0 ;Data Source=" + filePos + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";
            DataSet ds = new DataSet();
            string tableName;
            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connstr))
            {
                connection.Open();
                DataTable table = connection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                tableName = table.Rows[0]["Table_Name"].ToString();
                string strExcel = "select * from " + "[" + tableName + "]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, connstr);
                adapter.Fill(ds, tableName);
                connection.Close();
            }
            return ds.Tables[tableName];
        }
    }
}