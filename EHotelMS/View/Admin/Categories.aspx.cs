using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EHotelMS.View.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowCategories();
        }

        private void ShowCategories()
        {
            string Query = "SELECT CatId AS Id, CatName AS Categories, CatRemarks FROM CategoryTbl";
            CategoriesGV.DataSource = Con.GetData(Query);
            CategoriesGV.DataBind();
            CategoriesGV.HeaderRow.Cells[1].Text = "NO.";
            CategoriesGV.HeaderRow.Cells[2].Text = "Category";
            CategoriesGV.HeaderRow.Cells[3].Text = "Remark";
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string CatName = CatNameTb.Value;
                string Rem = RemarkTb.Value;
                string Query = "INSERT INTO CategoryTbl VALUES('{0}','{1}')";
                Query = string.Format(Query, CatName, Rem);
                Con.setData(Query);
                ShowCategories();
                ErrMsg.InnerText = "RoomCategory added!";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }

        int Key = 0;
        protected void CategoriesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Key = Convert.ToInt32(CategoriesGV.SelectedRow.Cells[1].Text);
            CatNameTb.Value = CategoriesGV.SelectedRow.Cells[2].Text;
            RemarkTb.Value = CategoriesGV.SelectedRow.Cells[3].Text;
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string CatName = CatNameTb.Value;
                string Rem = RemarkTb.Value;
                string Query = "UPDATE CategoryTbl SET CatName='{0}', CatRemarks='{1}' WHERE CatId={2}";
                Query = string.Format(Query, CatName, Rem, CategoriesGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowCategories();
                ErrMsg.InnerText = "Room Category updated!";
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
                string Query = "DELETE from CategoryTbl WHERE CatId={0}";
                Query = string.Format(Query, CategoriesGV.SelectedRow.Cells[1].Text);
                Con.setData(Query);
                ShowCategories();
                ErrMsg.InnerText = "Room Category deleted";
                CleanForm();
            }
            catch (Exception ex)
            {
                ErrMsg.InnerText = ex.Message;
            }
        }
        private void CleanForm()
        {
            CatNameTb.Value = "";
            RemarkTb.Value = "";
        }
    }
}