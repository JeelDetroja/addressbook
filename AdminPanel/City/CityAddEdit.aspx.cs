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

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
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
            if (Page.RouteData.Values["CityID"] != null)
            {
                lblMessage.Text += "<br/>CityID = " + Page.RouteData.Values["CityID"].ToString().Trim();
                FillControls(Convert.ToInt32(Decode().ToString().Trim()));
            }
            //if (Request.QueryString["CityID"] != null)
            //{
            //    lblMessage.Text = "Edit Mode    |   CityID = " + Request.QueryString["CityID"].ToString();
            //    FillControls(Convert.ToInt32(Request.QueryString["CityID"]));
            //}
            //else
            //{
            //    lblMessage.Text = "Add Mode";
            //} 
        }
    }
    #endregion Load Event

    #region Fill State DropDownList
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDropDownFillMethods.FillDropDownListState(ddlStateID, ddlCountryID, Convert.ToInt32(Session["UserID"]));
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
        //    objCmd.CommandText = "PR_State_SelectForDropDownListByCountryIDAndUserID";
        //    if (Session["UserID"] != null)
        //        objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
        //    if(ddlCountryID.SelectedIndex > 0)
        //        objCmd.Parameters.AddWithValue("@CountryID", ddlCountryID.SelectedValue);
        //    #endregion Set Connection & Command Object

        //    #region Read the Value and Set the Controls
        //    SqlDataReader objSDR = objCmd.ExecuteReader();

        //    if (objSDR.HasRows == true)
        //    {
        //        ddlStateID.DataSource = objSDR;
        //        ddlStateID.DataValueField = "StateID";
        //        ddlStateID.DataTextField = "StateName";
        //        ddlStateID.DataBind();
        //    }
        //    else
        //    {
        //        ddlStateID.Items.Clear();
        //        lblMessage.Text = "* Select another Country";
        //    }
        //    ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
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
    #endregion Fill State DropDownList

    #region Button Save Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString strCityName = SqlString.Null;
        SqlString strStdCode = SqlString.Null;
        SqlString strPinCode = SqlString.Null;
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
            else if (ddlStateID.SelectedIndex == 0)
            {
                strErrorMessage += "- Please Select State";
            }
            else if (txtCityName.Text.Trim() == "")
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
            if (txtCityName.Text.Trim() != "")
                strCityName = txtCityName.Text.Trim();
            if (txtStdCode.Text.Trim() != "")
                strStdCode = txtStdCode.Text.Trim();
            if (txtPinCode.Text.Trim() != "")
                strPinCode = txtPinCode.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Command Object
            
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if (ddlCountryID.SelectedIndex > 0)
                objCmd.Parameters.AddWithValue("@CountryID", ddlCountryID.SelectedValue);
            
            if (ddlStateID.SelectedIndex > 0)
                objCmd.Parameters.AddWithValue("@StateID", ddlStateID.SelectedValue);
            
            if(strCityName != "")
                objCmd.Parameters.AddWithValue("@CityName", strCityName);
            
            objCmd.Parameters.AddWithValue("@STDCode", strStdCode);
            objCmd.Parameters.AddWithValue("@PinCode", strPinCode);
            
            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["CityID"] != null)
            {
                #region Update Record
                //edit mode
                objCmd.Parameters.AddWithValue("@ModificationDate", DateTime.Now);
                objCmd.Parameters.AddWithValue("@CityID", Convert.ToInt32(Decode()).ToString().Trim());
                objCmd.CommandText = "[dbo].[PR_City_UpdateByPKAndUserID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/List", true);
                #endregion Update Record 
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_City_Insert]";
                objCmd.ExecuteNonQuery();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Sucessfully";
                ddlCountryID.SelectedIndex = -1;
                ddlStateID.SelectedIndex = -1;
                txtCityName.Text = "";
                txtStdCode.Text = "";
                txtPinCode.Text = "";
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
        //    objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";
        //    if (Session["UserID"] != null)
        //        objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
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
    private void FillControls(SqlInt32 CityID)
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
            objCmd.CommandText = "PR_City_SelectByPKAndUserID";
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if(CityID.ToString().Trim() != null)
                objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());
            
            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                while (objSDR.Read())
                {
                    if (objSDR["CountryID"].Equals(DBNull.Value) != true)
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        ddlCountryID_SelectedIndexChanged(null, null);
                    }
                    if (objSDR["StateID"].Equals(DBNull.Value) != true)
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    if (objSDR["CityName"].Equals(DBNull.Value) != true)
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (objSDR["PinCode"].Equals(DBNull.Value) != true)
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
                    }
                    if (objSDR["STDCode"].Equals(DBNull.Value) != true)
                    {
                        txtStdCode.Text = objSDR["STDCode"].ToString().Trim();
                    }
                    break;
                }
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
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
        }
    }
    #endregion Fill Controls

    #region Button Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/City/List", true);
    }
    #endregion Button Cancel Click

    #region Decode Base64
    private string Decode()
    {
        var PlainTextBytes = System.Convert.FromBase64String(Page.RouteData.Values["CityID"].ToString().Trim());
        return Encoding.UTF8.GetString(PlainTextBytes);
    }
    #endregion Decode Base64
}