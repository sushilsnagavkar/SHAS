using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

public partial class Master_Doctor_Directory : System.Web.UI.Page
{
    connection cn;
    #region Declaration
    SqlCommand cmd;
    SqlDataReader dr;
    DataSet ds, ds1;
    SqlDataAdapter da;
    double id;
    int ptnt_id;
    string ptnt_nm;
    DateTime Fdate, Edate;
   // public SqlConnection con = new SqlConnection();
    DateTime dt = System.DateTime.Now.Date;    
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
            cmd.CommandText = "tbl_doc_master_g";
            cmd.Parameters.Add("@pDoc_id", SqlDbType.Int).Value = ptnt_id;
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
                throw ex;
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Doc_Id = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
        Response.Redirect("~/DoctorMaster.aspx?Doc_Id=" + Doc_Id);
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int Doc_Id = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
        Response.Redirect("~/DoctorMaster.aspx?Doc_Id=" + Doc_Id);
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
    protected void btnADD_Click(object sender, EventArgs e)
    {
        int Doc_Id = 0;
        Response.Redirect("DoctorMaster.aspx?Doc_Id=" + Doc_Id);
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
            //Fdate = Convert.ToDateTime(txtFr_Dt.Text);
            //Edate = Convert.ToDateTime(txtTo_Dt.Text);
            Fdate = DateTime.ParseExact(txtFr_Dt.Text, "dd/MM/yyyy", null);
            Edate = DateTime.ParseExact(txtTo_Dt.Text, "dd/MM/yyyy", null);
        }
        String strConnString = ConfigurationManager.ConnectionStrings["sanwad"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "tbl_doc_master_g";
        cmd.Parameters.Add("@pDoc_id", SqlDbType.Int).Value = ptnt_id;
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
        }
        catch (Exception ex)
        {
            throw ex;
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
        int indexOfColumn = 1;
        if (e.Row.Cells.Count > indexOfColumn)
        {
            e.Row.Cells[indexOfColumn].Visible = false;
        }
    }
}
