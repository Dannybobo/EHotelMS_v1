using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EHotelMS.View
{
    public partial class Login : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            Session["UserName"] = "";
            Session["UId"] = "";
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (AdminCb.Checked)
            {
                if (UserTb.Value == "Admin" && PasswordTb.Value == "password")
                {
                    Session["UserName"] = "Admin";
                    Response.Redirect("Admin/Rooms.aspx");
                }
                else
                {
                    ErrMsg.InnerText = "Invalid Admin";
                }
            }
            else
            {
                string Query = "SELECT UId, UName FROM UserTbl WHERE UName = '{0}' AND UPass = '{1}'";
                Query = string.Format(Query, UserTb.Value, PasswordTb.Value);
                DataTable dt = Con.GetData(Query);
                if(dt.Rows.Count == 0)
                {
                    ErrMsg.InnerText = "Invalid User or Password";
                }
                else
                {
                    Session["UserName"] = dt.Rows[0][1].ToString();
                    Session["UId"] = Convert.ToInt32(dt.Rows[0][0].ToString());
                    Response.Redirect("Admin/Categories.aspx");
                }
            }
        }
    }
}