using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Technician : System.Web.UI.Page
{
    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUser"] == null)
        {
            Response.Redirect("default.aspx");
        }
        string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "SELECT username from Users WHERE id = " + Session["CurrentUser"];
        SqlCommand cmd = new SqlCommand(select, db);
        string name = (cmd.ExecuteScalar()).ToString();
        UserLabel.Text = "Welcome " + name;

        string select2 = "SELECT jobId from Jobs WHERE completed = 0 and technicianId = " + Session["CurrentUser"];
        string select3 = "SELECT jobId,issuedBy,enqueueTime from Jobs WHERE completed = 0 and technicianId = " + Session["CurrentUser"];
        cmd = new SqlCommand(select2, db);

        var hasJob =cmd.ExecuteScalar();
        if(hasJob == null)
        {
            CheckOutButton.Visible = false;
            CurrentJobLabel.Visible = false;
        }
        else
        {
            CheckInButton.Visible = false;
            cmd = new SqlCommand(select3, db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            CurrentJobLabel.Text = "Current Job: Job ID" + reader[0] +" Issued By: "+reader[1]+ "  Enqueue:"+ reader[2];

        }
 
        db.Close();
    }

    protected void CheckInButton_Click(object sender, EventArgs e)
    {
        CurrentJobLabel.Visible = true;

        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "SELECT TOP 1 jobID from Jobs where technicianId IS NULL order by priority DESC,enqueueTime ASC";
        SqlCommand cmd = new SqlCommand(select, db);
        var result = cmd.ExecuteScalar();
        if (result == null)
        {
            CurrentJobLabel.Text = "Queue is empty";
        }
        else
        {
            string val = result.ToString();

            CheckInButton.Visible = false;
            CheckOutButton.Visible = true;

            string select3 = "SELECT jobId,issuedBy,enqueueTime from Jobs WHERE jobId = " + val;
            cmd = new SqlCommand(select3, db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            CurrentJobLabel.Text = "Current Job: Job ID " + reader[0] + " Issued By: " + reader[1] + "  Enqueue:" + reader[2];
            reader.Close();

            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            string date = time.ToString(format);

            string update = "Update Jobs Set technicianId = (select id from Users where id = " + Session["CurrentUser"] + "), checkedIn = '" + date + "'  where jobId = " + val;
            cmd = new SqlCommand(update, db);
            cmd.ExecuteNonQuery();
        }
        db.Close();
    }

    protected void CheckOutButton_Click(object sender, EventArgs e)
    {
        CheckInButton.Visible = true;
        CheckOutButton.Visible = false;
        SqlConnection db = new SqlConnection(con);
        db.Open();

        string select = "SELECT jobId from Jobs where completed = 0 and technicianId = "+Session["CurrentUser"];
        SqlCommand cmd = new SqlCommand(select, db);

        string val = cmd.ExecuteScalar().ToString();

        DateTime time = DateTime.Now;
        string format = "yyyy-MM-dd HH:mm:ss";
        string date = time.ToString(format);

        string update = "Update Jobs Set dequeueTime = '"+date+"', completed = 1" + "  where jobId = " + val;
        cmd = new SqlCommand(update, db);
        cmd.ExecuteNonQuery();

        //updating Users past jobs
        select = "Select issuedBy from Jobs where jobId = " + val;
        cmd = new SqlCommand(select, db);
        string by = cmd.ExecuteScalar().ToString();

        string update2 = "Update Users set pastJobs = pastJobs + 1 where id = " + by;

        select = "Select enqueueTime from Jobs where jobId = " + val;
        cmd = new SqlCommand(select, db);

        DateTime start = (DateTime)cmd.ExecuteScalar();
        TimeSpan startTime = start.TimeOfDay;
        TimeSpan endTime = time.TimeOfDay;
        TimeSpan diff = endTime.Subtract(startTime);

        select = "Select joinDate from Users where id = " + Session["CurrentUser"];
        cmd = new SqlCommand(select, db);

        DateTime techStart = (DateTime)cmd.ExecuteScalar();

        TimeSpan timeWorking = endTime.Subtract(techStart.TimeOfDay);
        int years = timeWorking.Days / 365;
        //Hours gets hours between and diff.Days * 16 accounts for the 16 hours that arent being worked each day
        int hoursWorked = (diff.Hours - diff.Days * 16);
        if (hoursWorked < 1)
        {
            hoursWorked = 1;
        }
        //and 30 + 10*years accounts for the increased pay based on experience
        float pay = hoursWorked * (30 + 10 * years);
        string payString = string.Format("{0:00}",pay);
        CurrentJobLabel.Text = "Hours worked = " + hoursWorked + " cost = " + payString;
        //debug string (Timespan seems buggy) "days :" + diff.Days + "Hours :" + diff.Hours + "Seconds :" + diff.Seconds +"Milli  :" + diff.Milliseconds;
        db.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "select * from Jobs Order By priority DESC, enqueueTime ASC";
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
    protected void LogOutButton_Click(object sender, EventArgs e)
    {
        Session["CurrentUser"] = null;
        Response.Redirect("Login.aspx");
    }

}