﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PrototypeAddJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string test = "Test";
        string insert = "insert into Jobs (enqueueTime,dequeueTime,priority,complexity,issuedBy,technicianId) values ('" + test + "','" + test + "',0,0,0,0)";
        SqlCommand cmd = new SqlCommand(insert, db);
        int m = cmd.ExecuteNonQuery();
        if (m != 0)
        {
            Response.Write("< script > alert('Data Inserted !!') </ script >");
        }
        else
        {
            Response.Write("< script > alert('Data Inserted !!') </ script >");
        }
        db.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "select * from Jobs Order By priority ASC";
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

    protected void UserButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrototypeAddUser.aspx");
    }
}