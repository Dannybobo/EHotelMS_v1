using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EHotelMS.View.Admin
{
    public partial class Rooms : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowRooms();
            GetCategories();
        }
        private void ShowRooms()
        {
            string Query = "SELECT * FROM RoomTbl";
            RoomsGV.DataSource = Con.GetData(Query);
            RoomsGV.DataBind();
            RoomsGV.HeaderRow.Cells[1].Text = "Serial number";
            RoomsGV.HeaderRow.Cells[2].Text = "Room Name";
            RoomsGV.HeaderRow.Cells[3].Text = "Category";
            RoomsGV.HeaderRow.Cells[4].Text = "Location";
            RoomsGV.HeaderRow.Cells[5].Text = "Cost";
            RoomsGV.HeaderRow.Cells[6].Text = "Remark";
            RoomsGV.HeaderRow.Cells[7].Text = "Status";
        }
        private void GetCategories()
        {
            string Query = "SELECT * FROM CategoryTbl";
            if (!Page.IsPostBack)
            {
                CatCb.DataTextField = Con.GetData(Query).Columns["CatName"].ToString();
                CatCb.DataValueField = Con.GetData(Query).Columns["CatId"].ToString();
                CatCb.DataSource = Con.GetData(Query);
                CatCb.DataBind();
            }

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string RName = RNameTb.Value;
                string RCategory = CatCb.SelectedValue.ToString();
                string Location = LocationTb.Value;
                string Cost = CostTb.Value;
                string Remark = RemarksTb.Value;
                string Status = "Free";
                string Query = "INSERT INTO roomTbl VALUES('{0}','{1}','{2}','{3}','{4}','{5}')";
                Query = string.Format(Query, RName, RCategory, Location, Cost, Remark, Status);
                Con.setData(Query);
                ShowRooms();
                ErrMsg.InnerText = "Room added!";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }

        }
        private void CleanForm()
        {
            RNameTb.Value = "";
            CatCb.SelectedIndex = -1;
            LocationTb.Value = "";
            CostTb.Value = "";
            RemarksTb.Value = "";
        }

        int Key = 0;
        protected void RoomsGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Key = Convert.ToInt32(RoomsGV.SelectedRow.Cells[1].Text);
            RNameTb.Value = RoomsGV.SelectedRow.Cells[2].Text;
            CatCb.SelectedValue = RoomsGV.SelectedRow.Cells[3].Text;
            LocationTb.Value = RoomsGV.SelectedRow.Cells[4].Text;
            CostTb.Value = RoomsGV.SelectedRow.Cells[5].Text;
            RemarksTb.Value = RoomsGV.SelectedRow.Cells[6].Text;
            StatusCb.SelectedValue = RoomsGV.SelectedRow.Cells[7].Text;
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string RName = RNameTb.Value;
                string RCategory = CatCb.SelectedValue.ToString();
                string Location = LocationTb.Value;
                string Cost = CostTb.Value;
                string Remark = RemarksTb.Value;
                string Status = StatusCb.SelectedValue.ToString();
                string Query = "UPDATE roomTbl set RName='{0}', RCategory='{1}', RLocation='{2}', RCost='{3}', RRemarks='{4}', Status='{5}' WHERE RId='{6}'";
                Query = string.Format(Query, RName, RCategory, Location, Cost, Remark, Status, RoomsGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowRooms();
                ErrMsg.InnerText = "Room updated!";
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
                string Query = "DELETE from RoomTbl WHERE RId={0}";
                Query = string.Format(Query, RoomsGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowRooms();
                ErrMsg.InnerText = "Room deleted";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }
    }
}