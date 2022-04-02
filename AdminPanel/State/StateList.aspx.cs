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

public partial class AdminPanel_State_StateList : System.Web.UI.Page
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

    #region gvState : RowCommand
    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete State
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != "")
            {
                DeleteState(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete State

        #region Edit Country
        if (e.CommandName == "EditRecord")
        {
            if (e.CommandArgument != "")
            {
                var PlainTextBytes = Encoding.UTF8.GetBytes(e.CommandArgument.ToString().Trim());
                Response.Redirect("~/AdminPanel/State/Edit/" + Convert.ToBase64String(PlainTextBytes), true);
            }
        }
        #endregion Edit Country
    }
    #endregion gvCountry : RowCommand

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
            objCmd.CommandText = "PR_State_SelectByUserID";

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            #endregion Set Connection & Command Object

            #region Data Reader
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                gvState.DataSource = objSDR;
                gvState.DataBind();
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

    #region Delete State Record
    private void DeleteState(SqlInt32 StateID)
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
            objCmd.CommandText = "PR_State_DeleteByPKAndUserID";

            if(StateID.ToString() != null)
                objCmd.Parameters.AddWithValue("@StateID", StateID.ToString());

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
    #endregion Delete State Record
}