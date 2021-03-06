﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Technician : System.Web.UI.Page
{
    TimeSpan TimeWithout95(DateTime start, DateTime end)
    {
        TimeSpan diff = new TimeSpan(0,0,0);

        DateTime endOfDay = new DateTime(2000, 1, 1, 17, 0, 0);
        DateTime startOfDay = new DateTime(2000, 1, 1, 9, 0, 0);
        //if they start before the day dont chage for it
        if (start.TimeOfDay < startOfDay.TimeOfDay)
        {
            start.Add((startOfDay.TimeOfDay - start.TimeOfDay));
        }
        //if they start at the end of day dont chage for it
        if (end.TimeOfDay > endOfDay.TimeOfDay)
        {

            diff += (endOfDay.TimeOfDay - start.TimeOfDay);
            //minus one because the previous calculation added a day
            int numDays = (end.Date.Subtract(start.Date)).Days;

            diff += (new TimeSpan(numDays, 0, 0, 0));

        }
        else
        {
            if (start.TimeOfDay < end.TimeOfDay)
            {

                diff += (end.TimeOfDay - start.TimeOfDay);
                start += (end.TimeOfDay - start.TimeOfDay);

                int numDays = (end.Date.Subtract(start.Date)).Days;

                diff += (new TimeSpan(numDays, 0, 0, 0));

            }

            else
            {

                diff += (endOfDay.TimeOfDay - start.TimeOfDay) + (end.TimeOfDay - startOfDay.TimeOfDay);
 
                //minus one because the previous calculation added a day
                int numDays = (end.Date.Subtract(start.Date)).Days;
                diff += (new TimeSpan(numDays - 1, 0, 0, 0));

            }
        }

        return diff;
    }



    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {

        billtable.Visible = false;
        if (Session["CurrentUser"] == null || (int)Session["CurrentUserType"] != 1)
        {
            Response.Redirect("default.aspx");
        }
        CurrentJobLabel.InnerText = "No Current Job";
        string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "SELECT username from Users WHERE id = " + Session["CurrentUser"];
        SqlCommand cmd = new SqlCommand(select, db);
        string name = (cmd.ExecuteScalar()).ToString();
        alertDiv1.InnerText= "Welcome " + name;
        alertDiv1.Visible = true;

        string select2 = "SELECT jobId from Jobs WHERE completed = 0 and technicianId = " + Session["CurrentUser"];
        string select3 = "SELECT jobId,issuedBy,baseEnqueueTime from Jobs WHERE completed = 0 and technicianId = " + Session["CurrentUser"];
        cmd = new SqlCommand(select2, db);

        var hasJob =cmd.ExecuteScalar();
        if(hasJob == null)
        {
            CheckOutButton.Visible = false;
            CheckInButton.Visible = true;
        }
        else
        {

            cmd = new SqlCommand(select3, db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            CurrentJobLabel.InnerText = "Current Job: " + "    Job ID:  " + reader[0] +"       Issued By (User ID): "+reader[1]+ "      Enqueued:  "+ reader[2];

        }
 
        db.Close();
    }

    protected void CheckInButton_Click(object sender, EventArgs e)
    {

        billtable.Visible = false;
        CurrentJobLabel.Visible = true;

        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "SELECT TOP 1 jobID from Jobs where technicianId IS NULL order by priority DESC,enqueueTime ASC,lastUpdated ASC";
        SqlCommand cmd = new SqlCommand(select, db);
        var result = cmd.ExecuteScalar();
        if (result == null)
        {
            CurrentJobLabel.InnerText = "Queue is empty";
        }
        else
        {
            string val = result.ToString();

            CheckInButton.Visible = false;
            CheckOutButton.Visible = true;

            string select3 = "SELECT jobId,issuedBy,baseEnqueueTime from Jobs WHERE jobId = " + val;
            cmd = new SqlCommand(select3, db);

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string thisJobId = reader[0].ToString();
            string thisCustId = reader[1].ToString();
            string thisJobEnq = reader[2].ToString();
            reader.Close();

            select3 = "Select username from Users where id='" + thisCustId + "'";
            cmd = new SqlCommand(select3, db);

            string thisCust = cmd.ExecuteScalar().ToString();

            CurrentJobLabel.InnerText = "Current Job:   " +    
                "Job ID:  " + thisJobId +
                "    " + 
                " Issued By: " + thisCust +
                "    " + 
                "  Enqueued:  " + thisJobEnq;
            

            DateTime time = (DateTime.Now).AddHours(-6);
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
        var result = cmd.ExecuteScalar();
        if (result != null)
        {
            string val = result.ToString();


            DateTime time = (DateTime.Now).AddHours(-6);
            string format = "yyyy-MM-dd HH:mm:ss";
            string date = time.ToString(format);

            string update = "Update Jobs Set dequeueTime = '" + date + "', completed = 1" + "  where jobId = " + val;
            cmd = new SqlCommand(update, db);
            cmd.ExecuteNonQuery();

            //updating Users past jobs
            select = "Select issuedBy from Jobs where jobId = " + val;
            cmd = new SqlCommand(select, db);
            string by = cmd.ExecuteScalar().ToString();

            string update2 = "Update Users set pastJobs = pastJobs + 1 where id = " + by;
            cmd = new SqlCommand(update2, db);
            cmd.ExecuteScalar();

            select = "Select checkedIn from Jobs where jobId = " + val;
            cmd = new SqlCommand(select, db);

            DateTime start = (DateTime)cmd.ExecuteScalar();
            DateTime end = time;
            TimeSpan diff = TimeWithout95(start, end);

            select = "Select joinDate from Users where id = " + Session["CurrentUser"];
            cmd = new SqlCommand(select, db);

            DateTime techStart = (DateTime)cmd.ExecuteScalar();

            TimeSpan timeWorking = end.Subtract(techStart);
            int years = timeWorking.Days / 365;
            //Hours gets hours between and diff.Days * 16 accounts for the 16 hours that arent being worked each day
            int hoursWorked = (diff.Hours + diff.Days * 8);
            if (hoursWorked < 1)
            {
                hoursWorked = 1;
            }
            //and 30 + 10*years accounts for the increased pay based on experience
            int rate = 30 + 10 * years;
            int pay = hoursWorked * rate;

            string payString = string.Format("${0:00}", pay);

            CurrentJobLabel.InnerText = "Bill generated below and sent to view from Customer's Account";
            hoursbilled.Text = hoursWorked.ToString();
            payrate.Text = "$" + rate;
            totalbill.Text = payString;
            billtable.Visible = true;

            update2 = "Update Jobs set cost = " + pay + " where jobId = " + val;
            cmd = new SqlCommand(update2, db);
            cmd.ExecuteScalar();
            
        }
        else
        {

        }

        db.Close();
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

}