using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonDropDownFillMethods
/// </summary>
public static class CommonDropDownFillMethods
{
    #region Fill DropDownList : Country
    public static void FillDropDownListCountry(DropDownList ddl, SqlInt32 UserID)
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

            if (UserID != 0)
                objCmd.Parameters.AddWithValue("@UserID", UserID);

            objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";
            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "CountryID";
                ddl.DataTextField = "CountryName";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem("Select Country", "-1"));
            #endregion Read the Value and Set the Controls

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            //lblMessage.Text = "* Select another Country";
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
	}
    #endregion Fill DropDownList : Country  

    #region Fill DropDownList : State

    public static void FillDropDownListState(DropDownList ddl, DropDownList ddlCountry, SqlInt32 UserID)
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
            objCmd.CommandText = "PR_State_SelectForDropDownListByCountryIDAndUserID";
            if (UserID != 0)
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            if (ddlCountry.SelectedIndex > 0)
                objCmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedValue);
            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "StateName";
                ddl.DataBind();
            }
            else
            {
                ddl.Items.Clear();
                //lblMessage.Text = "* Select another Country";
            }
            ddl.Items.Insert(0, new ListItem("Select State", "-1"));
            #endregion Read the Value and Set the Controls

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion Fill DropDownList : State

    #region Fill DropDownList : City

    public static void FillDropDownListCity(DropDownList ddl, DropDownList ddlState, SqlInt32 UserID)
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
            objCmd.CommandText = "PR_City_SelectForDropDownListByStateIDAndUserID";

            if (UserID != 0)
                objCmd.Parameters.AddWithValue("@UserID", UserID);

            if (ddlState.SelectedIndex > 0)
                objCmd.Parameters.AddWithValue("@StateID", ddlState.SelectedValue);
            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "CityID";
                ddl.DataTextField = "CityName";
                ddl.DataBind();
            }
            else
            {
                ddl.Items.Clear();
                //Message += "* There are no City -- Select another State";
            }
            ddl.Items.Insert(0, new ListItem("Select City", "-1"));
            #endregion Read the Value and Set the Controls

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            //Message += ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion Fill DropDownList : City

    #region Fill CheckBoxList : ContactCategory

    public static void FillCheckBoxListContactCategory(CheckBoxList cbl, SqlInt32 UserID)
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
            objCmd.CommandText = "PR_ContactCategory_SelectForDropDownListByUserID";


            if (UserID != 0)
                objCmd.Parameters.AddWithValue("@UserID", UserID);

            #endregion Set Connection & Command Object

            #region Read the Value and Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                cbl.DataValueField = "ContactCategoryID";
                cbl.DataTextField = "ContactCategoryName";
                cbl.DataSource = objSDR;
                cbl.DataBind();
            }
            objSDR.Close();
            #endregion Read the Value and Set the Controls

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            //Message = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion Fill DropDownList : ContactCategory
}