using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Manager : System.Web.UI.Page
{
    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime time = DateTime.Now;
        
        string format = "yyyy-MM-dd";
        string yesterday = time.ToString(format);

        //if time time is before the end of the day show previous report
        if (time.Hour < 17)
        {
            DateTime time2 = time.AddDays(-1);
            yesterday = time2.ToString(format);
        }
        
        

        DailyLabel.Text = "Report for "+yesterday;
        //get user
        if (Session["CurrentUser"] == null || (int)Session["CurrentUserType"] != 2)
        {
            Response.Redirect("default.aspx");
        }
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "SELECT username from Users WHERE id = " + Session["CurrentUser"];
        SqlCommand cmd = new SqlCommand(select, db);
        string name = (cmd.ExecuteScalar()).ToString();
        UserLabel.Text = "Welcome " + name;

        //show queue 

        //subselect counts num of jobs ahead in queue
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        string select2 = "select jobId, enqueueTime," + subSelect + " from Jobs J where checkedIn IS NULL Order By priority, jobId";
  
        using (SqlCommand command = new SqlCommand(select2, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                queueGrid.DataSource = dr;
                queueGrid.DataBind();
            }
        }

        //get technician stats
        /*
        //subselect counts num of jobs ahead in queue
        subSelect = "(select SUM(Date from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        select2 = "select id,username," + subSelect + " from Users U where U.type = 1";

        using (SqlCommand command = new SqlCommand(select2, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                technicianGrid.DataSource = dr;
                technicianGrid.DataBind();
            }
        }
        */

        //average wait time

        //average length

        //jobs addressed same day
        string day = yesterday + " 00:00:00";

        select = "select count(jobId) as num from Jobs where year(enqueueTime) = year('"+day+ "') and month(enqueueTime) = month('" + day + "') and day(enqueueTime) = day('" + day + "') and  year(enqueueTime) = year(checkedIn) and  month(enqueueTime) = month(checkedIn) and  day(enqueueTime) = day(checkedIn) ";
        cmd = new SqlCommand(select, db);

        string result = cmd.ExecuteScalar().ToString();
        DailySameDayLabel.Text = result;

        //percent time empty
        db.Close();
    }


    protected void LogOutButton_Click(object sender, EventArgs e)
    {
        Session["CurrentUser"] = null;
        Response.Redirect("Login.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

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


}