using EHotelMS.View.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EHotelMS.View.Users
{
    public partial class Booking : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowRooms();
            ShowBooks();
        }

        private void ShowRooms()
        {
            string Query = "SELECT RId as Id, RName as Name, RCategory as Catrgory, RCost as Cost, Status as Status FROM RoomTbl WHERE Status = 'Free'";

            DataTable dt = Con.GetData(Query);

            if (dt.Rows.Count == 0)
            {
                RoomGV.DataSource = null;
                RoomGV.DataBind();
                return;
            }

            RoomGV.DataSource = dt;
            RoomGV.DataBind();
            RoomGV.HeaderRow.Cells[1].Text = "No.";
            RoomGV.HeaderRow.Cells[2].Text = "Room Name";
            RoomGV.HeaderRow.Cells[3].Text = "Type";
            RoomGV.HeaderRow.Cells[4].Text = "Cost";
            RoomGV.HeaderRow.Cells[5].Text = "Status";
        }
        private void ShowBooks()
        {
            string Query = "SELECT * FROM BookingTbl";
            DataTable dt = Con.GetData(Query);

            if (dt.Rows.Count == 0)
            {
                BookingGV.DataSource = null;
                BookingGV.DataBind();
                return;
            }

            BookingGV.DataSource = dt;
            BookingGV.DataBind();
            BookingGV.HeaderRow.Cells[1].Text = "Order No.";
            BookingGV.HeaderRow.Cells[2].Text = "Now";
            BookingGV.HeaderRow.Cells[3].Text = "Room";
            BookingGV.HeaderRow.Cells[4].Text = "UId";
            BookingGV.HeaderRow.Cells[5].Text = "Check in";
            BookingGV.HeaderRow.Cells[6].Text = "Check out";
            BookingGV.HeaderRow.Cells[7].Text = "Cost";
        }

        int Key = 0;
        int Days = 1;
        protected void RoomGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Key = Convert.ToInt32(RoomGV.SelectedRow.Cells[1].Text);
            RoomTb.Value = RoomGV.SelectedRow.Cells[2].Text;
            int Cost = Days * Convert.ToInt32(RoomGV.SelectedRow.Cells[4].Text);
            AmountTb.Value = Cost.ToString();
        }
        private void UpdateRoom()
        {
            try
            {
                string Query = "UPDATE RoomTbl set Status='Booked' WHERE RId='{0}'";
                Query = string.Format(Query, RoomGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowRooms();
                ErrMsg.InnerText = "Rooms Information Updated!";
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }


        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string RId = RoomGV.SelectedRow.Cells[1].Text;
                string BDate = System.DateTime.Today.Date.ToString("yyyy-MM-dd");
                string InDate = DateInTb.Value;
                string OutDate = DateOutTb.Value;
                string Agent = Session["UId"] as string;

                
                int Amount = Convert.ToInt32(GetCost().ToString());
                string Query = "INSERT INTO BookingTbl VALUES('{0}', {1}, '{2}', '{3}', '{4}', {5})";
                Query = string.Format(Query, BDate, RId, Agent, InDate, OutDate, Amount);
                Con.setData(Query);
                UpdateRoom();
                ShowRooms();
                ShowBooks();
                ErrMsg.InnerText = "Booking success!";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message + " " + ex.StackTrace;
            }

        }
        private void CleanForm()
        {
            RoomTb.Value = "";
            AmountTb.Value = "";
        }

        private int GetCost()
        {
            DateTime DIn = Convert.ToDateTime(DateInTb.Value);
            DateTime DOut = Convert.ToDateTime(DateOutTb.Value);
            TimeSpan Value = DOut.Subtract(DIn);
            int Days = Convert.ToInt32(Value.TotalDays);
            return Days * Convert.ToInt32(RoomGV.SelectedRow.Cells[4].Text);

        }

        protected void BookingGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}