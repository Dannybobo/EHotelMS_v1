using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EHotelMS.View.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowUsers();

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string UName = UNameTb.Value;
                string UPhone = PhoneTb.Value;
                string UGen = GenCb.SelectedValue;
                string UAdd = AddressTb.Value;
                string UPass = PasswordTb.Value;

                string Query = "INSERT INTO UserTbl VALUES('{0}','{1}','{2}','{3}','{4}')";
                Query = string.Format(Query, UName, UPhone, UGen, UAdd, UPass);
                Con.setData(Query);
                ShowUsers();
                ErrMsg.InnerText = "User added!";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }

        private void ShowUsers()
        {
            string Query = "SELECT * FROM UserTbl";
            UserGV.DataSource = Con.GetData(Query);
            UserGV.DataBind();
            UserGV.HeaderRow.Cells[1].Text = "NO.";
            UserGV.HeaderRow.Cells[2].Text = "User Name";
            UserGV.HeaderRow.Cells[3].Text = "Phone";
            UserGV.HeaderRow.Cells[4].Text = "Gender";
            UserGV.HeaderRow.Cells[5].Text = "Address";
            UserGV.HeaderRow.Cells[6].Text = "Password";
        }

        private void CleanForm()
        {
            UNameTb.Value = "";
            PhoneTb.Value = "";
            GenCb.SelectedIndex = -1;
            AddressTb.Value = "";
            PasswordTb.Value = "";
        }

        int Key = 0;
        protected void UserGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Key = Convert.ToInt32(UserGV.SelectedRow.Cells[1].Text);
            UNameTb.Value = UserGV.SelectedRow.Cells[2].Text;
            PhoneTb.Value = UserGV.SelectedRow.Cells[3].Text;
            GenCb.SelectedValue = UserGV.SelectedRow.Cells[4].Text;
            AddressTb.Value = UserGV.SelectedRow.Cells[5].Text;
            PasswordTb.Value = UserGV.SelectedRow.Cells[6].Text;
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string UName = UNameTb.Value;
                string UPhone = PhoneTb.Value;
                string UGen = GenCb.SelectedValue.ToString();
                string UAddress = AddressTb.Value;
                string UPass = PasswordTb.Value;
                string Query = "UPDATE UserTbl set UName='{0}', UPhone='{1}', UGen='{2}', UAdd='{3}', UPass='{4}' WHERE UId='{5}'";
                Query = string.Format(Query, UName, UPhone, UGen, UAddress, UPass, UserGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowUsers();
                ErrMsg.InnerText = "User updated!";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "DELETE from UserTbl WHERE UId={0}";
                Query = string.Format(Query, UserGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowUsers();
                ErrMsg.InnerText = "User deleted";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }
    }
}