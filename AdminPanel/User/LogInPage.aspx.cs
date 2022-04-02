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

public partial class User_LogInPage : System.Web.UI.Page
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
        string strErrorMessage = "";

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {
            #region Server Side Validation
            if (txtUserName.Text.Trim() == "")
            {
                strErrorMessage += "Enter User Name <br/>";
            }
            if (txtPassword.Text.Trim() == "")
            {
                strErrorMessage += "Enter Password <br/>";
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
            
            #endregion Gather Information

            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectByUserNameAndPassword";
            objCmd.Parameters.AddWithValue("@UserName", strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);

            #endregion Set Connection & Command Object

            #region Data Reader

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                lblMessage.Text = "Log in Sucsessfully";
                while (objSDR.Read())
                {
                    if (!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }
                    if (!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                    }
                    break;
                }

                Response.Redirect("~/AdminPanel/Default/Home", true);
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Either User Name or Password is Invalid ! Login failed";
            }

            #endregion Data Reader

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    protected void lbRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/User/Registration", true);
    }
}