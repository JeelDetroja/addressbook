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

public partial class AdminPanel_ContactCategory_ContactCategory : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtContactCategoryName.Focus();
            if (Page.RouteData.Values["OperationName"] != null)
            {
                lblMessage.Text += Page.RouteData.Values["OperationName"].ToString().Trim();
            }
            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                lblMessage.Text += "<br/>ContactCategoryID = " + Page.RouteData.Values["ContactCategoryID"].ToString().Trim();
                FillControls(Convert.ToInt32(Decode().ToString().Trim()));
            }
            //if (Request.QueryString["ContactCategoryID"] != null)
            //{
            //    lblMessage.Text = "Edit Mode    |   ContactCategoryID = " + Request.QueryString["ContactCategoryID"].ToString();
            //    FillControls(Convert.ToInt32(Request.QueryString["ContactCategoryID"]));
            //}
            //else
            //{
            //    lblMessage.Text = "Add Mode";
            //}
        }
    }
    #endregion Load Event

    #region Button Save Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString strContactCategoryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;
        String strErrorMessage = "";

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable
        try
        {
            #region Server Side Validation
            if (txtContactCategoryName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Contact Category Name";
            }
            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (txtContactCategoryName.Text.Trim() != "")
                strContactCategoryName = txtContactCategoryName.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Command Object
            
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_ContactCategory_Insert]";
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if(strContactCategoryName != "")
                objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);
            
            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                #region Update Record
                //edit mode
                objCmd.Parameters.AddWithValue("@ModificationDate", DateTime.Now);
                objCmd.Parameters.AddWithValue("@ContactCategoryID", Convert.ToInt32(Decode()).ToString().Trim());
                objCmd.CommandText = "[dbo].[PR_ContactCategory_UpdateByPKAndUserID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_ContactCategory_Insert]";
                objCmd.ExecuteNonQuery();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Sucessfully";
                txtContactCategoryName.Text = "";
                txtContactCategoryName.Focus();
                #endregion Insert Record
            }

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
    #endregion Button Save Click

    #region Button Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/ContactCategory/List", true);
    }
    #endregion Button Cancel Click

    #region Fill Controls
    private void FillControls(SqlInt32 ContactCategoryID)
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
            objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectByPKByUserID]";
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if(ContactCategoryID.ToString().Trim() != "")
                objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
            
            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if (objSDR["ContactCategoryName"].Equals(DBNull.Value) != true)
                    {
                        txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for CountryID = " + ContactCategoryID.ToString();
            }
            #endregion Read the Value and Set the Controls

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
    #endregion Fill Controls

    #region Decode Base64
    private string Decode()
    {
        var PlainTextBytes = System.Convert.FromBase64String(Page.RouteData.Values["ContactCategoryID"].ToString().Trim());
        return Encoding.UTF8.GetString(PlainTextBytes);
    }
    #endregion Decode Base64
}