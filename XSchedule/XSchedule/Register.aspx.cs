using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Register : System.Web.UI.Page
{
    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void SubmitButton_Click(object sender, EventArgs e)
    { 

        Regex r = new Regex("^[a-zA-Z0-9]*$");

        if(r.IsMatch(usernameField.Text) && r.IsMatch(passwordField.Text))
            {

            SqlConnection db = new SqlConnection(con);
            db.Open();
            int type = 0;
            if (optionField.SelectedValue == "1")
                type = 1;
            if (optionField.SelectedValue == "2")
                type = 2;
            string select = "SELECT id from Users WHERE username = '" + usernameField.Text +"'";
            SqlCommand cmd = new SqlCommand(select, db);
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                alertDiv2.Visible = false;
                alertDiv1.Visible = true;
            }
            else
            {
                DateTime time = (DateTime.Now).AddHours(-6);
                string format = "yyyy-MM-dd";
                string date = time.ToString(format);

                string insert = "insert into Users (username,password,type,joinDate) values ('" + usernameField.Text + "','" + passwordField.Text + "'," + type + ",'"+date+"')";
                SqlCommand cmd2 = new SqlCommand(insert, db);
                int m = cmd2.ExecuteNonQuery();

                Session["CurrentUser"] = cmd.ExecuteScalar();

                if (type == 0)
                {
                    Session["CurrentUserType"] = 0;
                    Response.Redirect("Customer.aspx");
                }
                if (type == 1)
                {
                    Session["CurrentUserType"] = 1;
                    Response.Redirect("Technician.aspx");
                }
                if (type == 2)
                {
                    Session["CurrentUserType"] = 2;
                    Response.Redirect("Manager.aspx");
                }
            }
            db.Close();
        }
        else
        {
            alertDiv2.Visible = true;
            alertDiv1.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        /* for debugging
       
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
        */
    }

    protected void LogInButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

}