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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtCountryName.Focus();
            if (Page.RouteData.Values["OperationName"] != null)
            {
                lblMessage.Text += Page.RouteData.Values["OperationName"].ToString().Trim();
            }
            if (Page.RouteData.Values["CountryID"] != null)
            {
                lblMessage.Text += "<br/>CountryID = " + Page.RouteData.Values["CountryID"].ToString().Trim();
                FillControls(Convert.ToInt32(Decode().ToString().Trim()));
            }
            //if (Request.QueryString["CountryID"] != null)
            //{
            //    lblMessage.Text = "Edit Mode    |   CountryID = " + Request.QueryString["CountryID"].ToString();
            //    FillControls(Convert.ToInt32(Request.QueryString["CountryID"]));
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
        SqlString strCountryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;
        String strErrorMessage = "";

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable

        try
        {
            #region Server Side Validation
            if (txtCountryName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Country Name";
            }
            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if(txtCountryName.Text.Trim() != "")
                strCountryName = txtCountryName.Text.Trim();
            if (txtCountryCode.Text.Trim() != "")
                strCountryCode = txtCountryCode.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if (strCountryName != "")
                objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
            
            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);

            #endregion Set Connection & Command Object

            //if (Request.QueryString["CountryID"] != null)
            if (Page.RouteData.Values["CountryID"] != null)
            {
                #region Update Record
                //edit mode
                objCmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(Decode()).ToString().Trim());
                objCmd.Parameters.AddWithValue("@ModificationDate", DateTime.Now);
                objCmd.CommandText = "[dbo].[PR_Country_UpdateByPKAndUserID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_Country_Insert]";
                objCmd.ExecuteNonQuery();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Sucessfully";               
                txtCountryName.Text = "";
                txtCountryCode.Text = "";
                txtCountryName.Focus();
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

    #region Fill Controls
    private void FillControls(SqlInt32 CountryID)
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
            objCmd.CommandText = "PR_Country_SelectByPKAndUserID";
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if(CountryID.ToString().Trim() != "")
                objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());

            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if (objSDR["CountryName"].Equals(DBNull.Value) != true)
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (objSDR["CountryCode"].Equals(DBNull.Value) != true)
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for CountryID = " + CountryID.ToString();
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

    #region Button Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Country/List", true);
    }
    #endregion Button Cancel Click

    #region Decode Base64
    private string Decode()
    {
        var PlainTextBytes = System.Convert.FromBase64String(Page.RouteData.Values["CountryID"].ToString().Trim());
        return Encoding.UTF8.GetString(PlainTextBytes);
    }
    #endregion Decode Base64
}
