﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class bill_Display : System.Web.UI.Page
{
    #region Declaration
    connection cn;
    SqlCommand cmd;
    SqlDataReader dr;
    DataSet ds, ds1;
    SqlDataAdapter da;
    double id;
    int ptnt_id;
    string ptnt_nm;
    DateTime Fdate, Edate;
    DateTime dt = System.DateTime.Now.Date;
    int totSubTot = 0;
    double sum = 0;
    public double TotalAmount;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Name"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            cn = new connection();
            if (!IsPostBack)
            {
                #region Grid Load
                ptnt_id = 0;
                ptnt_nm = "";
                Fdate = Convert.ToDateTime(null);
                Edate = Convert.ToDateTime(null);
                String strConnString = ConfigurationManager.ConnectionStrings["sanwad"].ConnectionString;
                SqlConnection con = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "tbl_test_bill_g";
                cmd.Parameters.Add("@ptest_bill_no", SqlDbType.Int).Value = ptnt_id;
                cmd.Parameters.Add("@pSEARCH", SqlDbType.VarChar).Value = ptnt_nm;
                cmd.Parameters.Add("@pFDate", SqlDbType.Date).Value = Fdate;
                cmd.Parameters.Add("@pEDate", SqlDbType.Date).Value = Edate;
                cmd.Parameters.Add("@pCntr_id", SqlDbType.Int).Value = Convert.ToInt32(Session["Cntr_id"].ToString());
                cmd.Connection = con;
                try
                {
                    con.Open();
                    GridView1.EmptyDataText = "No Records Found";
                    GridView1.DataSource = cmd.ExecuteReader();
                    GridView1.DataBind();
                    if (GridView1.Columns.Count > 1)
                    {
                       GridView1.Columns[1].Visible = false;
                    } 
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                #endregion
            }
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("~/bill.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Bill_Id = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
        Response.Redirect("bill.aspx?Bill_Id=" + Bill_Id);
    }
    protected void btnADD_Click(object sender, EventArgs e)
    {
        int Bill_Id = 0;
        Response.Redirect("bill.aspx?Bill_Id=" + Bill_Id);
    }
    protected void btnAdSearch_Click(object sender, EventArgs e)
    {
        if (btnAdSearch.Text == "Advance Search")
        {
            Panel1.Visible = true;
            lblFr_dt.Visible = true;
            lblTo_Dt.Visible = true;
            btnAdSearch.Text = "Close";
        }
        else
        {
            Panel1.Visible = false;
            btnAdSearch.Text = "Advance Search";
        }
    }
    protected void btnSerch_Click(object sender, EventArgs e)
    {
        #region Grid Load
        ptnt_id = 0;
        ptnt_nm = txtDesc.Text;
        if ((txtFr_Dt.Text == "" && txtTo_Dt.Text == "") || (txtFr_Dt.Text == "" || txtTo_Dt.Text == ""))
        {
            Fdate = Convert.ToDateTime(null);
            Edate = Convert.ToDateTime(null);
        }
        else
        {
            Fdate = DateTime.ParseExact(txtFr_Dt.Text, "dd/MM/yyyy", null);
            Edate = DateTime.ParseExact(txtTo_Dt.Text, "dd/MM/yyyy", null);
        }
        String strConnString = ConfigurationManager.ConnectionStrings["sanwad"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "tbl_test_bill_g";
        cmd.Parameters.Add("@ptest_bill_no", SqlDbType.Int).Value = ptnt_id;
        cmd.Parameters.Add("@pSEARCH", SqlDbType.VarChar).Value = ptnt_nm;
        cmd.Parameters.Add("@pFDate", SqlDbType.VarChar).Value = Fdate;
        cmd.Parameters.Add("@pEDate", SqlDbType.VarChar).Value = Edate;
        cmd.Parameters.Add("@pCntr_id", SqlDbType.Int).Value = Convert.ToInt32(Session["Cntr_id"].ToString());
        cmd.Connection = con;
        try
        {
            con.Open();
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            if (GridView1.Columns.Count > 1)
            {
                GridView1.Columns[1].Visible = false;
            } 
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        #endregion
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //int indexOfColumn = 1;
        //if (e.Row.Cells.Count > indexOfColumn)
        //{
        //    e.Row.Cells[indexOfColumn].Visible = false;
        //}
        //if (e.Row.Cells[18].Text != "Amount")
        //{
        //    var item = e.Row.Cells[18].Text;
        //    sum += Convert.ToInt32(item);
        //}
        //txtTotal.Text = Convert.ToString(sum);

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    totSubTot += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SubTotal"));
        //    TotalAmount = Convert.ToDouble(totSubTot);
        //}
        //else if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    totSubTot += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SubTotal"));
        //    TotalAmount = Convert.ToDouble(totSubTot);

        //    e.Row.Cells[17].Text = "Grand Total";
        //    e.Row.Cells[17].Font.Bold = true;

        //    e.Row.Cells[18].Text = totSubTot.ToString();
        //    e.Row.Cells[8].Font.Bold = true;
        //}
    }
}
