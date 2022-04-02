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

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();
            ddlCountryID.Focus();
            if (Page.RouteData.Values["OperationName"] != null)
            {
                lblMessage.Text += Page.RouteData.Values["OperationName"].ToString().Trim();
            }
            if (Page.RouteData.Values["StateID"] != null)
            {
                lblMessage.Text += "<br/>StateID = " + Page.RouteData.Values["StateID"].ToString().Trim();
                FillControls(Convert.ToInt32(Decode()));
            }
            //if (Request.QueryString["StateID"] != null)
            //{
            //    lblMessage.Text = "Edit Mode    |   StateID = " + Request.QueryString["StateID"].ToString();
            //    FillControls(Convert.ToInt32(Request.QueryString["StateID"]));
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
        SqlString strStateName = SqlString.Null;
        SqlString strStateCode = SqlString.Null;
        String strErrorMessage = "";
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable

        try
        {
            #region Server Side Validation
            if (ddlCountryID.SelectedIndex == 0)
            {
                strErrorMessage += "- Please Select Country";
            }
            else if (txtStateName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter State Name";
            }
            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (txtStateName.Text.Trim() != "")
                strStateName = txtStateName.Text.Trim();
            if (txtStateCode.Text.Trim() != "")
                strStateCode = txtStateCode.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Command Object
            
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            
            if(ddlCountryID.SelectedIndex > 0)
                objCmd.Parameters.AddWithValue("@CountryID", ddlCountryID.SelectedValue);
            
            if(strStateName != "")
                objCmd.Parameters.AddWithValue("@StateName", strStateName);
        
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["StateID"] != null)
            {
                #region Update Record
                //edit mode
                objCmd.Parameters.AddWithValue("@StateID", Convert.ToInt32(Decode()).ToString().Trim());
                objCmd.Parameters.AddWithValue("@ModificationDate", DateTime.Now);
                objCmd.CommandText = "[dbo].[PR_State_UpdateByPKAndUserID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/State/List",true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_State_Insert]";
                objCmd.ExecuteNonQuery();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Sucessfully";
                ddlCountryID.SelectedIndex = -1;
                txtStateName.Text = "";
                txtStateCode.Text = "";
                ddlCountryID.Focus();
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
        Response.Redirect("~/AdminPanel/State/List", true);
    }
    #endregion Button Cancel Click

    #region Fill Country DropDownList
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID, Convert.ToInt32(Session["UserID"]));
        //#region Local Variable
        //SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        //#endregion Local Variable
        //try
        //{
        //    #region Set Connection & Command Object
        //    if (objConn.State != ConnectionState.Open)
        //        objConn.Open();
        //    SqlCommand objCmd = objConn.CreateCommand();
        //    objCmd.CommandType = CommandType.StoredProcedure;

        //    if (Session["UserID"] != null)
        //        objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

        //    objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";
        //    #endregion Set Connection & Command Object

        //    #region Read the Value and Set the Controls
        //    SqlDataReader objSDR = objCmd.ExecuteReader();

        //    if (objSDR.HasRows == true)
        //    {
        //        ddlCountryID.DataSource = objSDR;
        //        ddlCountryID.DataValueField = "CountryID";
        //        ddlCountryID.DataTextField = "CountryName";
        //        ddlCountryID.DataBind();
        //    }
        //    ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));
        //    #endregion Read the Value and Set the Controls

        //    if (objConn.State == ConnectionState.Open)
        //        objConn.Close();
        //}
        //catch (Exception ex)
        //{
        //    lblMessage.Text = ex.Message;
        //}
        //finally
        //{
        //    if (objConn.State == ConnectionState.Open)
        //        objConn.Close();
        //}
        
    }
    #endregion Fill Country DropDownList

    #region Fill Controls
    private void FillControls(SqlInt32 StateID)
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
            objCmd.CommandText = "PR_State_SelectByPKAndUserID";

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            if (StateID.ToString().Trim() != "")
                objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());

            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if(objSDR["StateName"].Equals(DBNull.Value) != true)
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();
                    }
                    if (objSDR["CountryID"].Equals(DBNull.Value) != true)
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    if (objSDR["StateCode"].Equals(DBNull.Value) != true)
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for StateID = " + StateID.ToString();
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
        var PlainTextBytes = System.Convert.FromBase64String(Page.RouteData.Values["StateID"].ToString().Trim());
        return Encoding.UTF8.GetString(PlainTextBytes);
    }
    #endregion Decode Base64
}