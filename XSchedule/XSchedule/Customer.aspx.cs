using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Customer : System.Web.UI.Page
{
    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUser"] == null || (int)Session["CurrentUserType"] != 0)
        {
            Response.Redirect("default.aspx");
        }
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "SELECT username from Users WHERE id = " + Session["CurrentUser"];
        SqlCommand cmd = new SqlCommand(select, db);
        string name = (cmd.ExecuteScalar()).ToString();

        alertDiv1.Visible = true;
        alertDiv1.InnerText = "Welcome " + name;

        //load completed
        select = "select jobId, enqueueTime,checkedIn,dequeueTime,(select username from Users U where U.id = J.technicianId) as technician from Jobs J where issuedBy = " + Session["CurrentUser"] + " and completed = 1";
        cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                completedJobs.DataSource = dr;
                completedJobs.DataBind();
            }
        }
      
        noCompletedDiv.Visible = (completedJobs.Rows.Count == 0);
        //load started
        select = "select jobId,enqueueTime, checkedIn,(select username from Users U where U.id = J.technicianId) as technician  from Jobs J where issuedBy = " + Session["CurrentUser"] + " and technicianId IS NOT NULL and completed = 0";
        cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                startedJobs.DataSource = dr;
                startedJobs.DataBind();
            }
        }
        noStartedDiv.Visible = (startedJobs.Rows.Count == 0);
        //load unstarted
        //subselect counts num of jobs ahead in queue
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        select = "select jobId, enqueueTime," + subSelect + " from Jobs J where J.issuedBy = " + Session["CurrentUser"] + "and J.technicianID IS NULL Order By priority, jobId";
        cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                unstartedJobs.DataSource = dr;
                unstartedJobs.DataBind();
            }
        }
        noUnstartedDiv.Visible = (unstartedJobs.Rows.Count == 0);

    }


    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        
        SqlConnection db = new SqlConnection(con);
        db.Open();

        string select = "select pastJobs from Users where id =" + Session["CurrentUser"];
        SqlCommand cmd = new SqlCommand(select, db);
        int numJobs = (int)cmd.ExecuteScalar();

        //leaving gaps in priority levels so managers have room to change priorities
        int priority = 0;

        if (numJobs > 4)
        {
            priority = 6;
        }
        else if(numJobs > 2)
        {
            priority = 4;
        }
        else if(numJobs > 0)
        {
            priority = 2;
        }

        int complexity = 1;
        if (radio1.Checked)
        {
            complexity = 1;
        }

        if (radio2.Checked)
        {
            complexity = 2;
        }

        if (radio3.Checked)
        {
            complexity = 3;
        }
        DateTime time = DateTime.Now;
        string format = "yyyy-MM-dd HH:mm:ss";
        string date = time.ToString(format);
        Console.WriteLine(date);

        string insert = "insert into Jobs (enqueueTime,complexity,priority,issuedBy) values ('" + date + "'," + complexity + "," + priority + ", (select id from Users where id ="+Session["CurrentUser"]+"))";
        cmd = new SqlCommand(insert, db);
        int m = cmd.ExecuteNonQuery();

        //load unstarted
        //subselect counts num of jobs ahead in queue
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        select = "select jobId, enqueueTime," + subSelect + " from Jobs J where J.issuedBy = " + Session["CurrentUser"] + "and J.technicianID IS NULL Order By priority, jobId";
        cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                unstartedJobs.DataSource = dr;
                unstartedJobs.DataBind();
            }
        }

        db.Close();

        alertDiv1.InnerText = "Job Created!";
        noUnstartedDiv.Visible = false;
    }
    /*
    protected void ViewCompleted_Click(object sender, EventArgs e)
    {
        
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "select jobId, enqueueTime,checkedIn,dequeueTime,(select username from Users U where U.id = J.technicianId) as technician from Jobs J where issuedBy = " + Session["CurrentUser"] + " and completed = 1";
        SqlCommand cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                completedJobs.DataSource = dr;
                completedJobs.DataBind();
            }
        }

        db.Close();
    }
    protected void ViewStarted_Click(object sender, EventArgs e)
    {
      
        SqlConnection db = new SqlConnection(con);
        db.Open();
        string select = "select jobId,enqueueTime, checkedIn,(select username from Users U where U.id = J.technicianId) as technician  from Jobs J where issuedBy = " + Session["CurrentUser"] + " and technicianId IS NOT NULL and completed = 0";
        SqlCommand cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                startedJobs.DataSource = dr;
                startedJobs.DataBind();
            }
        }

        db.Close();
    }
    protected void ViewUnstarted_Click(object sender, EventArgs e)
    {
        
        SqlConnection db = new SqlConnection(con);
        db.Open();
        //subselect counts num of jobs ahead in queue
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        string select = "select jobId, enqueueTime,"+subSelect+" from Jobs J where J.issuedBy = " + Session["CurrentUser"] + "and J.technicianID IS NULL Order By priority, jobId";
        SqlCommand cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                unstartedJobs.DataSource = dr;
                unstartedJobs.DataBind();
            }
        }

        db.Close();
    }
    */
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

    protected void unstartedJobs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        SqlConnection db = new SqlConnection(con);
        db.Open();
   
        string delete = "delete from Jobs where jobId = " + Convert.ToInt32(unstartedJobs.Rows[e.RowIndex].Cells[0].Text);
        SqlCommand cmd = new SqlCommand(delete, db);
        cmd.ExecuteNonQuery();

        //load unstarted
        //subselect counts num of jobs ahead in queue
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        string select = "select jobId, enqueueTime," + subSelect + " from Jobs J where J.issuedBy = " + Session["CurrentUser"] + "and J.technicianID IS NULL Order By priority, jobId";
        cmd = new SqlCommand(select, db);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                unstartedJobs.DataSource = dr;
                unstartedJobs.DataBind();
            }
        }

        noUnstartedDiv.Visible = (unstartedJobs.Rows.Count == 0);
        //alertDiv1.InnerText = unstartedJobs.Rows[e.RowIndex].Cells[0].Text;


    }

}