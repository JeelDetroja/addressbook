using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_UserRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtUserName.Focus();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strDisplayName = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        String strErrorMessage = "";

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable

        try
        {
            #region Server Side Validation
            if (txtUserName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter User Name";
            }
            if (txtPassword.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Password";
            }
            if (txtDisplayName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Display Name";
            }
            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            
            if (txtUserName.Text.Trim() != "")
                strUserName = txtUserName.Text.Trim();
            
            if (txtPassword.Text.Trim() != "")
                strPassword = txtPassword.Text.Trim();
            
            if (txtDisplayName.Text.Trim() != "")
                strDisplayName = txtDisplayName.Text.Trim();
           
            if (txtEmail.Text.Trim() != "")
                strEmail = txtEmail.Text.Trim();
            
            if (txtMobileNo.Text.Trim() != "")
                strMobileNo = txtMobileNo.Text.Trim();

            #endregion Gather Information

            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.AddWithValue("@UserName", strUserName);

                objCmd.Parameters.AddWithValue("@Password", strPassword);

                objCmd.Parameters.AddWithValue("@DisplayName", strDisplayName);

                objCmd.Parameters.AddWithValue("@Email", strEmail);

                objCmd.Parameters.AddWithValue("@MobileNo", strMobileNo);

            #endregion Set Connection & Command Object

            #region Insert Record
            //add mode
            objCmd.CommandText = "[dbo].[PR_User_Insert]";
            objCmd.ExecuteNonQuery();
            #endregion Insert Recoed

            Response.Redirect("~/AdminPanel/User/Login");

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/User/Login");
    }
}