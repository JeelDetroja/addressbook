using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_City_CityList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridView();
        }
    }
    #endregion Load Event

    #region gvCity : RowCommand
    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete City
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != "")
            {
                DeleteCity(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete City

        #region Edit Country
        if (e.CommandName == "EditRecord")
        {
            if (e.CommandArgument != "")
            {
                var PlainTextBytes = Encoding.UTF8.GetBytes(e.CommandArgument.ToString().Trim());
                Response.Redirect("~/AdminPanel/City/Edit/" + Convert.ToBase64String(PlainTextBytes), true);
            }
        }
        #endregion Edit Country
    }
    #endregion gvCity : RowCommand

    #region Fill GridView
    private void FillGridView()
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectByUserID";

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            #endregion Set Connection & Command Object

            #region Data Reader
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                gvCity.DataSource = objSDR;
                gvCity.DataBind();
            }
            #endregion Data Reader

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }  
    }
    #endregion Fill GridView

    #region Delete City Record
    private void DeleteCity(SqlInt32 CityID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_City_DeleteByPKAndUserID]";
            
            if(CityID.ToString() != null)
                objCmd.Parameters.AddWithValue("@CityID", CityID.ToString());

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            objCmd.ExecuteNonQuery();
            #endregion Set Connection & Command Object


            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        FillGridView();
    }
    #endregion Delete City Record
}