﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "select * from Users";
        SqlCommand cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                testGV.DataSource = dr;
                testGV.DataBind();
            }
        }
        db.Close();
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }

    protected void LogInButton_Click(object sender, EventArgs e)
    {
       
        SqlConnection db = new SqlConnection(con);
        db.Open();

        string select = "select id from Users where username ='" + usernameField.Text + "' and password ='" + passwordField.Text+"'";
        SqlCommand cmd = new SqlCommand(select, db);
        var result = cmd.ExecuteScalar();
        if (result == null)
        {
            Response.Write("< script > alert('Invalid Login') </ script >");
        }
        else
        {
            Session["CurrentUser"] = result;
            string select2 = "select type from Users where id =" + Session["CurrentUser"];
            SqlCommand cmd2 = new SqlCommand(select2, db);
            int type = (int)cmd2.ExecuteScalar();
            if (type == 0)
            {
                Response.Redirect("Customer.aspx");
            }
            if (type == 1)
            {
                Response.Redirect("Technician.aspx");
            }
            if (type == 2)
            {
                Response.Redirect("Manager.aspx");
            }


        }
        db.Close();
    }
}