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

public partial class AdminPanel_Contact_ContactList : System.Web.UI.Page
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

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Contact
        if (e.CommandName == "DeleteRecord")
        {
           if (e.CommandArgument != "")
           {
                DeleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
           }
        }
        #endregion Delete Contact

        #region Edit Contact
        if (e.CommandName == "EditRecord")
        {
            if (e.CommandArgument != "")
            {
                var PlainTextBytes = Encoding.UTF8.GetBytes(e.CommandArgument.ToString().Trim());
                Response.Redirect("~/AdminPanel/Contact/Edit/" + Convert.ToBase64String(PlainTextBytes), true);
            }
        }
        #endregion Edit Contact
    }
    #endregion gvContact : RowCommand

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
            objCmd.CommandText = "PR_Contact_SelectByUserID";

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            #endregion Set Connection & Command Object

            #region Data Reader
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
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

    #region Delete Country Record
    private void DeleteContact(SqlInt32 ContactID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            //ContactCategory Delete
            SqlCommand objCmdContactCategory = objConn.CreateCommand();
            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
            objCmdContactCategory.CommandText = "[dbo].[PR_ContactWiseContactCategory_DeleteByPKAndUserID]";
            
            if (ContactID.ToString() != null)
                objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID.ToString());

            if (Session["UserID"] != null)
                objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);         
            objCmdContactCategory.ExecuteNonQuery();


            //Contact Delete
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_Contact_DeleteByPKAndUserID]";

            if (ContactID.ToString() != null)
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());

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
    #endregion Delete Country Record
}