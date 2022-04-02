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

public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
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
            if (Page.RouteData.Values["ContactID"] != null)
            {
                lblMessage.Text += "<br/>ContactID = " + Page.RouteData.Values["ContactID"].ToString().Trim();
                FillControls(Convert.ToInt32(Decode().ToString().Trim()));
            }
            //if (Request.QueryString["ContactID"] != null)
            //{
            //    lblMessage.Text = "Edit Mode    |   ContactID = " + Request.QueryString["ContactID"].ToString();
            //    FillControls(Convert.ToInt32(Request.QueryString["ContactID"]));
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
        SqlString strContactName = SqlString.Null;
        SqlString strContactNo = SqlString.Null;
        SqlString strWhatsappNo = SqlString.Null;
        SqlString strBirthDate = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strAge = SqlString.Null;
        SqlString strAddtess = SqlString.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strFaceBookID = SqlString.Null;
        SqlString strLinkedINID = SqlString.Null;
        String    strErrorMessage = "";

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
            else if (ddlCityID.SelectedIndex == 0)
            {
                strErrorMessage += "- Please Select City";
            }
            else if (txtContactName.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Name";
            }
            else if (txtContactNo.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Contact No.";
            }
            else if (txtEmail.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Email Id";
            }
            else if (txtareaAddress.InnerText.Trim() == "")
            {
                strErrorMessage += "- Enter Address";
            }
            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            
            if (txtContactName.Text.Trim() != "")
                strContactName = txtContactName.Text.Trim();

            if (txtContactNo.Text.Trim() != "")
                strContactNo = txtContactNo.Text.Trim();
            
            if (txtEmail.Text.Trim() != "")
                strEmail = txtEmail.Text.Trim();            
            
            if (txtareaAddress.InnerText.Trim() != "")
                strAddtess = txtareaAddress.InnerText.Trim();
            
            if (txtWhatsAppNo.Text.Trim() != "")
                strWhatsappNo = txtWhatsAppNo.Text.Trim();
            
            if (txtBirthDate.Text.Trim() != "")
                strBirthDate = txtBirthDate.Text.Trim();
            
            if (txtAge.Text.Trim() != "")
                strAge = txtAge.Text.Trim();
            
            if (txtBloodGroup.Text.Trim() != "")
                strBloodGroup = txtBloodGroup.Text.Trim();
            
            if (txtFacebookID.Text.Trim() != "")
                strFaceBookID = txtFacebookID.Text.Trim();
            
            if (txtLinkedINID.Text.Trim() != "")
                strLinkedINID = txtLinkedINID.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

            objCmd.Parameters.AddWithValue("@CountryID", ddlCountryID.SelectedValue);
            objCmd.Parameters.AddWithValue("@StateID", ddlStateID.SelectedValue);
            objCmd.Parameters.AddWithValue("@CityID", ddlCityID.SelectedValue);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@WhatsappNo", strWhatsappNo);
            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            objCmd.Parameters.AddWithValue("@Address", strAddtess);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
            objCmd.Parameters.AddWithValue("@FacebookID", strFaceBookID);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedINID);
            

            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["ContactID"] != null)
            {

                #region Update Record
                SqlInt32 strContactID = Convert.ToInt32(Decode());

                #region Delete old Value of cblContactCategory in Table

                SqlCommand objDeleteCategory = objConn.CreateCommand();
                objDeleteCategory.CommandType = CommandType.StoredProcedure;
                objDeleteCategory.CommandText = "PR_ContactWiseContactCategory_DeleteContactCategoryByContactIDAndUserID";

                if (Session["UserID"] != null)
                    objDeleteCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);
                objDeleteCategory.Parameters.AddWithValue("ContactID", strContactID.ToString().Trim());

                objDeleteCategory.ExecuteNonQuery();

                #endregion Delete old Value of cblContactCategory in Table

                //edit mode
                objCmd.Parameters.AddWithValue("@ModificationDate", DateTime.Now);
                objCmd.Parameters.AddWithValue("@ContactID", strContactID.ToString().Trim());
                objCmd.CommandText = "[dbo].[PR_Contact_UpdateByPKAndUserID]";
                objCmd.ExecuteNonQuery();

                #region Insert ContactCategory

                foreach (ListItem liContactCategoryID in cblContactCategory.Items)
                {
                    if (liContactCategoryID.Selected)
                    {
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                        objCmdContactCategory.CommandText = "[dbo].[PR_ContactWiseContactCategory_Insert]";
                        if (Session["UserID"] != null)
                            objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        objCmdContactCategory.Parameters.AddWithValue("ContactID", strContactID.ToString().Trim());
                        objCmdContactCategory.Parameters.AddWithValue("ContactCategoryID", liContactCategoryID.Value.ToString().Trim());
                        objCmdContactCategory.ExecuteNonQuery();
                    }
                }
                #endregion Insert ContactCategory


                Response.Redirect("~/AdminPanel/Contact/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_Contact_Insert]";

                //Out Parameter
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();

                #region Insert ContactCategory

                SqlInt32 ContactID = 0;
                ContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);

                foreach (ListItem liContactCategoryID in cblContactCategory.Items)
                {
                    if (liContactCategoryID.Selected)
                    {
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                        objCmdContactCategory.CommandText = "[dbo].[PR_ContactWiseContactCategory_Insert]";
                        if (Session["UserID"] != null)
                            objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        objCmdContactCategory.Parameters.AddWithValue("ContactID", ContactID.ToString().Trim());
                        objCmdContactCategory.Parameters.AddWithValue("ContactCategoryID", liContactCategoryID.Value.ToString().Trim());
                        objCmdContactCategory.ExecuteNonQuery();
                    }
                }
                #endregion Insert ContactCategory
                
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Data Inserted Sucessfully";
                ddlCountryID.SelectedIndex = -1;
                ddlStateID.SelectedIndex = -1;
                ddlCityID.SelectedIndex = -1;
                txtContactName.Text = "";
                txtContactNo.Text = "";
                txtWhatsAppNo.Text = "";
                txtBirthDate.Text = "";
                txtEmail.Text = "";
                txtAge.Text = "";
                txtareaAddress.InnerText = "";
                txtBloodGroup.Text = "";
                txtFacebookID.Text = "";
                txtLinkedINID.Text = "";
                foreach (ListItem item in cblContactCategory.Items)
                {
                    if (item.Selected)
                    {
                        item.Selected = false;
                    }         
                }
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

    #region Fill State DropDownList
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCityID.Items.Clear();
        ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));

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
        //        lblMessage.Text = "* There are no States -- Select another Country";
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

    #region Fill City DropDownList
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonDropDownFillMethods.FillDropDownListCity(ddlCityID, ddlStateID, Convert.ToInt32(Session["UserID"]));
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
        //    objCmd.CommandText = "PR_City_SelectForDropDownListByStateIDAndUserID";
            
        //    if (Session["UserID"] != null)
        //        objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
        //    if(ddlStateID.SelectedIndex > 0)
        //        objCmd.Parameters.AddWithValue("@StateID", ddlStateID.SelectedValue);
        //    #endregion Set Connection & Command Object

        //    #region Read the Value and Set the Controls
        //    SqlDataReader objSDR = objCmd.ExecuteReader();

        //    if (objSDR.HasRows == true)
        //    {
        //        ddlCityID.DataSource = objSDR;
        //        ddlCityID.DataValueField = "CityID";
        //        ddlCityID.DataTextField = "CityName";
        //        ddlCityID.DataBind();
        //    }
        //    else
        //    {
        //        ddlCityID.Items.Clear();
        //        lblMessage.Text = "* There are no City -- Select another State";
        //    }
        //    ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
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
    #endregion Fill City DropDownList

    #region Button Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Contact/List", true);
    }
    #endregion Button Cancel Click

    #region Fill Country DropDownList & ContactCategory CheckBoxList
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID, Convert.ToInt32(Session["UserID"]));
        CommonDropDownFillMethods.FillCheckBoxListContactCategory(cblContactCategory, Convert.ToInt32(Session["UserID"]));
        //#region Local Variable
        //SqlConnection objConnCountry = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        //#endregion Local Variable
        //try
        //{
        //    #region Set Connection & Command Object
        //    if (objConnCountry.State != ConnectionState.Open)
        //        objConnCountry.Open();

        //    SqlCommand objCmdCountry = objConnCountry.CreateCommand();
        //    objCmdCountry.CommandType = CommandType.StoredProcedure;
        //    objCmdCountry.CommandText = "PR_Country_SelectForDropDownListByUserID";
            
        //    if (Session["UserID"] != null)
        //        objCmdCountry.Parameters.AddWithValue("@UserID", Session["UserID"]);
        //    #endregion Set Connection & Command Object

        //    #region Read the Value and Set the Controls
        //    SqlDataReader objSDRCountry = objCmdCountry.ExecuteReader();

        //    if (objSDRCountry.HasRows == true)
        //    {
        //        ddlCountryID.DataSource = objSDRCountry;
        //        ddlCountryID.DataValueField = "CountryID";
        //        ddlCountryID.DataTextField = "CountryName";
        //        ddlCountryID.DataBind();
        //    }
        //    ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));
        //    #endregion Read the Value and Set the Controls

        //    if (objConnCountry.State == ConnectionState.Open)
        //        objConnCountry.Close();
        //}
        //catch (Exception ex)
        //{
        //    lblMessage.Text = ex.Message;
        //}
        //finally
        //{
        //    if (objConnCountry.State == ConnectionState.Open)
        //        objConnCountry.Close();
        //}

        //#region Local Variable
        //SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        //#endregion Local Variable
        //try
        //{
        //    #region Set Connection & Command Object
            
        //    if (objConn.State != ConnectionState.Open)
        //        objConn.Open();
            
        //    SqlCommand objCmdContactCategory = objConn.CreateCommand();
        //    objCmdContactCategory.CommandType = CommandType.StoredProcedure;
        //    objCmdContactCategory.CommandText = "PR_ContactCategory_SelectForDropDownListByUserID";

            
        //    if (Session["UserID"] != null)
        //        objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
        //    #endregion Set Connection & Command Object

        //    #region Read the Value and Set the Controls
        //    SqlDataReader objSDRContactCategory = objCmdContactCategory.ExecuteReader();
        //    if (objSDRContactCategory.HasRows)
        //    {        
        //        cblContactCategory.DataValueField = "ContactCategoryID";
        //        cblContactCategory.DataTextField = "ContactCategoryName";
        //        cblContactCategory.DataSource = objSDRContactCategory;
        //        cblContactCategory.DataBind();
        //    }
        //    objSDRContactCategory.Close();
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
    #endregion Fill Country DropDownList & ContactCategory CheckBoxList

    #region Fill Controls
    private void FillControls(SqlInt32 ContactID)
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
            objCmd.CommandText = "PR_Contact_SelectByPKAndUserID";
            
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if (ContactID.ToString().Trim() != "")
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            
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
                        ddlStateID_SelectedIndexChanged(null , null);
                    }
                    if (objSDR["CityID"].Equals(DBNull.Value) != true)
                    {
                        ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    //if (objSDR["ContactCategoryID"].Equals(DBNull.Value) != true)
                    //{
                    //    cblContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                    //}
                    if (objSDR["ContactName"].Equals(DBNull.Value) != true)
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (objSDR["ContactNo"].Equals(DBNull.Value) != true)
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (objSDR["WhatsAppNo"].Equals(DBNull.Value) != true)
                    {
                        txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }
                    if (objSDR["BirthDate"].Equals(DBNull.Value) != true)
                    {
                        txtBirthDate.Text = Convert.ToDateTime(objSDR["BirthDate"]).ToString("yyyy-MM-dd").Trim();
                    }
                    if (objSDR["Email"].Equals(DBNull.Value) != true)
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (objSDR["Age"].Equals(DBNull.Value) != true)
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (objSDR["Address"].Equals(DBNull.Value) != true)
                    {
                        txtareaAddress.InnerText = objSDR["Address"].ToString().Trim();
                    }
                    if (objSDR["BloodGroup"].Equals(DBNull.Value) != true)
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (objSDR["FacebookID"].Equals(DBNull.Value) != true)
                    {
                        txtFacebookID.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (objSDR["LinkedINID"].Equals(DBNull.Value) != true)
                    {
                        txtLinkedINID.Text = objSDR["LinkedINID"].ToString().Trim();
                    }
                    break;
                }
            }
            objSDR.Close();

            SqlCommand objCategory = objConn.CreateCommand();
            objCategory.CommandType = CommandType.StoredProcedure;
            objCategory.CommandText = "PR_ContactWiseContactCategory_SelectContactCategoryByContactIDAndUserID";
            
            if (Session["UserID"] != null)
                objCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);
            
            if (ContactID.ToString().Trim() != "")
                objCategory.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            
            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls

            SqlDataReader objSDRforCategory = objCategory.ExecuteReader();

            if (objSDRforCategory.HasRows == true)
            {
                while (objSDRforCategory.Read())
                {
                    foreach(ListItem item in cblContactCategory.Items){
                        if (objSDRforCategory.GetValue(0).ToString().Trim() == item.Text.Trim())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            objSDRforCategory.Close();

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
        var PlainTextBytes = System.Convert.FromBase64String(Page.RouteData.Values["ContactID"].ToString().Trim());
        return Encoding.UTF8.GetString(PlainTextBytes);
    }
    #endregion Decode Base64
}