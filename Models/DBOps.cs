using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class DBOps
    {
        public List<BooksDataModel> GetDataFromDB(BooksDataModel objPostedModel)
        {
            List<string> strParams = new List<string>();

            if (!string.IsNullOrEmpty(objPostedModel.BookId))
            {
                strParams.Add("@bookID~" + objPostedModel.BookId);
            }
            else
            {
                strParams.Add("@bookID~A");
            }

            if (!string.IsNullOrEmpty(objPostedModel.BookTitle))
            {
                strParams.Add("@bookTitle~" + objPostedModel.BookTitle);
            }
            else
            {
                strParams.Add("@bookTitle~A");
            }

            if (!string.IsNullOrEmpty(objPostedModel.AuthorName))
            {
                strParams.Add("@AuthorName~" + objPostedModel.AuthorName);
            }
            else
            {
                strParams.Add("@AuthorName~A");
            }

            if (!string.IsNullOrEmpty(objPostedModel.IsAvialalbe))
            {
                var parameters = "@Availability~" + (objPostedModel.IsAvialalbe == "Y" ? "1" : "0");
                strParams.Add(parameters);
            }
            else
            {
                strParams.Add("@Availability~A");
            }

            if (!string.IsNullOrEmpty(objPostedModel.Price))
            {
                strParams.Add("@Price~" + objPostedModel.Price);
            }
            else
            {
                strParams.Add("@Price~A");
            }

            if (!string.IsNullOrEmpty(objPostedModel.ShelfID))
            {
                strParams.Add("@ShelfID~" + objPostedModel.ShelfID);
            }
            else
            {
                strParams.Add("@ShelfID~A");
            }

            if (!string.IsNullOrEmpty(objPostedModel.DeleteFlag))
            {
                var parameters = "@DeleteFlag~" + (objPostedModel.DeleteFlag == "Y" ? "1" : "0");
                strParams.Add(parameters);

            }
            else
            {
                strParams.Add("@DeleteFlag~A");
            }


            if (!string.IsNullOrEmpty(objPostedModel.RackID))
            {
                strParams.Add("@RackID~" + objPostedModel.RackID);
            }
            else
            {
                strParams.Add("@RackID~A");
            }

            List<BooksDataModel> lstData = new List<BooksDataModel>();
            DataTable tblData = new DataTable();
            ExecuteSQLQuery("dbo.SearchBooks", strParams, tblData);
            if (tblData.Rows.Count > 0)
            {
                BooksDataModel objBook = new BooksDataModel();
                foreach (DataRow drItem in tblData.Rows)
                {
                    objBook.AuthorName = drItem["AuthorName"].ToString();
                    objBook.BookId = drItem["BookId"].ToString();
                    objBook.BookTitle = drItem["BookTitle"].ToString();
                    objBook.IsAvialalbe = drItem["IsAvialalbe"].ToString();
                    objBook.Price = drItem["Price"].ToString();
                    objBook.ShelfID = drItem["ShelfID"].ToString();
                    objBook.RackID = drItem["RackID"].ToString();
                    lstData.Add(objBook);
                }
            }

            return lstData;
        }



        public List<SelectListItem> GetAllShelves()
        {
            List<SelectListItem> lstData = new List<SelectListItem>();
            DataTable tblData = new DataTable();
            ExecuteSQLQuery("dbo.GetAllShelves", null, tblData);
            if (tblData.Rows.Count > 0)
            {
                SelectListItem objItem = new SelectListItem();
                foreach (DataRow drItem in tblData.Rows)
                {
                    objItem.Text = drItem["ShelfID"].ToString();
                    objItem.Value = drItem["ShelfID"].ToString();
                    lstData.Add(objItem);
                }
            }
            lstData.Insert(0, new SelectListItem { Text = "", Value = "" });
            return lstData;
        }


        public List<SelectListItem> GetAllRacks()
        {
            List<SelectListItem> lstData = new List<SelectListItem>();
            DataTable tblData = new DataTable();
            ExecuteSQLQuery("dbo.GetAllRacks", null, tblData);
            if (tblData.Rows.Count > 0)
            {

                foreach (DataRow drItem in tblData.Rows)
                {
                    SelectListItem objItem = new SelectListItem();
                    objItem.Text = drItem["RackID"].ToString();
                    objItem.Value = drItem["RackID"].ToString();
                    lstData.Add(objItem);
                }
            }

            lstData.Insert(0, new SelectListItem { Text = "", Value = "" });
            return lstData;
        }

        public void ExecuteSQLQuery(string strQuery, List<string> paramsList, DataTable tblData)
        {
            SqlConnection conn = new SqlConnection("user id=sa; password=kpsoft1;Initial Catalog= BooksDB");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = strQuery;
            cmd.CommandType = CommandType.StoredProcedure;
            List<SqlParameter> lstParamsSQL = new List<SqlParameter>();
            if (paramsList != null)
            {
                foreach (var item in paramsList)
                {
                    cmd.Parameters.Add(new SqlParameter { Value = item.Split('~')[1], ParameterName = item.Split('~')[0] });
                }

            }


            tblData.Load(cmd.ExecuteReader());
        }


        public void UpdateDelete(BooksDataModel objPostedModel)
        {
            var Query = objPostedModel.OperationType == "U" ? "dbo.UpdateBook" : "dbo.DeleteBook";
            SqlConnection conn = new SqlConnection("user id=sa; password=kpsoft1;Initial Catalog= BooksDB");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.StoredProcedure;

            objPostedModel.IsAvialalbe = objPostedModel.IsAvialalbe == "Y" ? "1" : "0";
            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.BookId, ParameterName = "@bookID" });


            if (objPostedModel.OperationType == "U")
            {
                cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.BookTitle, ParameterName = "@bookTitle" });
                cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.AuthorName, ParameterName = "@AuthorName" });
                cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.IsAvialalbe, ParameterName = "@Availability" });
                cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.Price, ParameterName = "@Price" });
                cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.ShelfID, ParameterName = "@ShelfID" });
                cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.RackID, ParameterName = "@RackID" });
            }




            //cmd.Parameters.Add(new SqlParameter { Value = item.Split('~')[1], ParameterName = item.Split('~')[0] });
            if (cmd.ExecuteNonQuery() > 0)
            {

            }


        }


        public void InsertNewBook(BooksDataModel objPostedModel)
        {
            var Query = "dbo.AddBook";
            SqlConnection conn = new SqlConnection("user id=sa; password=kpsoft1;Initial Catalog= BooksDB");
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.StoredProcedure;

            objPostedModel.IsAvialalbe = objPostedModel.IsAvialalbe == "Y" ? "1" : "0";
            


            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.BookTitle, ParameterName = "@bookTitle" });
            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.AuthorName, ParameterName = "@AuthorName" });
            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.Price, ParameterName = "@Price" });
            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.ShelfID, ParameterName = "@ShelfID" });
            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.RackID, ParameterName = "@RackID" });
            cmd.Parameters.Add(new SqlParameter { Value = objPostedModel.IsAvialalbe, ParameterName = "@Availability" });
            cmd.Parameters.Add(new SqlParameter { Value = "0", ParameterName = "@DeleteFlag" });



            //cmd.Parameters.Add(new SqlParameter { Value = item.Split('~')[1], ParameterName = item.Split('~')[0] });
            if (cmd.ExecuteNonQuery() > 0)
            {

            }


        }

    }
}
