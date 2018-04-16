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
    /*
     * if(!Page.IsPostBack)
{

}*/
    TimeSpan timeWithout95(DateTime start, DateTime end)
    {
        TimeSpan diff = new TimeSpan();

        DateTime endOfDay = new DateTime(2000, 1, 1, 17, 0, 0);
        DateTime startOfDay = new DateTime(2000, 1, 1, 9, 0, 0);


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

        return diff;
    }

    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1;MultipleActiveResultSets=true; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {
       // System.Diagnostics.Debug.WriteLine(" PAGE LOADDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        DateTime time = DateTime.Now;
        DateTime lastM = (DateTime.Now).AddMonths(-1);
        string format = "yyyy-MM-dd";
        string format2 = "yyyy-MM";
        string lastMonth = lastM.ToString(format2);
        string yesterday = time.ToString(format);

        //if time time is before the end of the day show previous report
        if (time.Hour < 17)
        {
            time = time.AddDays(-1);

        }

        yesterday = time.ToString(format);

        DailyLabel.Text = "Report for " + yesterday;
        MonthlyLabel.Text = "Report for " + lastMonth;
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
        alertDiv1.InnerText = "Welcome " + name;
        alertDiv1.Visible = true;

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

        //average wait time of those fineshed yesterday
        string day = yesterday + " 00:00:00";


        select = "select SUM(DATEDIFF(minute,checkedIn,dequeueTime)) as totalWait from Jobs where year(dequeueTime) = year('" + day + "') and month(dequeueTime) = month('" + day + "') and day(dequeueTime) = day('" + day + "') and completed = 1";
        cmd = new SqlCommand(select, db);
        int totalMinutes = (int)cmd.ExecuteScalar();

        select = "select count(*) from Jobs where year(dequeueTime) = year('" + day + "') and month(dequeueTime) = month('" + day + "') and day(dequeueTime) = day('" + day + "')";
        cmd = new SqlCommand(select, db);
        int count = (int)cmd.ExecuteScalar();

        double temp = (totalMinutes / (count * 60.0));
        string result = string.Format("{0:00}", temp);
        DailyAvWaitLabel.Text = " " + result + " hours";//"count:" + count + " total:" + temp + "yesterday:" + yesterday;// result;

        //for the month
        string month = lastMonth + "-01 00:00:00";
        select = "select SUM(DATEDIFF(minute,checkedIn,dequeueTime)) as totalWait from Jobs where year(dequeueTime) = year('" + month + "') and month(dequeueTime) = month('" + month + "') and completed=1";
        cmd = new SqlCommand(select, db);
        totalMinutes = (int)cmd.ExecuteScalar();

        select = "select count(*) from Jobs where year(dequeueTime) = year('" + month + "') and month(dequeueTime) = month('" + month + "')";
        cmd = new SqlCommand(select, db);
        count = (int)cmd.ExecuteScalar();

        temp = (totalMinutes / (count * 60.0));
        result = string.Format("{0:00}", temp);
        MonthlyAvWaitLabel.Text = " " + result + " hours";// "count:" + count + " total:" + temp + "lastm:" +month ;

        //average length
        //computed as (queueLength at start of day + midday + end of day)/3
        string morning = yesterday + " 00:00:00";
        string startOfDay = yesterday + " 09:00:00";
        string endOfDay = yesterday + " 17:00:00";
        select = "select count(*) from Jobs where enqueueTime<'" + morning + "' and (checkedIn >'" + morning + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        int count1 = (int)cmd.ExecuteScalar();

        string noon = yesterday + " 12:00:00";

        select = "select count(*) from Jobs where enqueueTime<'" + noon + "' and (checkedIn >'" + noon + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        int count2 = (int)cmd.ExecuteScalar();

        string night = yesterday + " 23:59:59";

        select = "select count(*) from Jobs where enqueueTime<'" + night + "' and (checkedIn >'" + night + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        int count3 = (int)cmd.ExecuteScalar();

        temp = (count1 + count2 + count3) / (3.0);
        result = string.Format("{00:0}", temp);
        DailyAvLengthLabel.Text = " " + result;


        //for month compute as length samples from each week/5

        string week0 = lastMonth + "-01 00:00:00";

        select = "select count(*) from Jobs where enqueueTime<'" + week0 + "' and (checkedIn >'" + week0 + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        count1 = (int)cmd.ExecuteScalar();

        string week1 = lastMonth + "-08 00:00:00";

        select = "select count(*) from Jobs where enqueueTime<'" + week1 + "' and (checkedIn >'" + week1 + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        count2 = (int)cmd.ExecuteScalar();

        string week2 = lastMonth + "-15 00:00:00";

        select = "select count(*) from Jobs where enqueueTime<'" + week2 + "' and (checkedIn >'" + week2 + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        count3 = (int)cmd.ExecuteScalar();

        string week3 = lastMonth + "-22 00:00:00";

        select = "select count(*) from Jobs where enqueueTime<'" + week3 + "' and (checkedIn >'" + week3 + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        int count4 = (int)cmd.ExecuteScalar();

        string week4 = lastMonth + "-28 23:00:00";

        select = "select count(*) from Jobs where enqueueTime<'" + week4 + "' and (checkedIn >'" + week4 + "' or checkedIn IS NULL)";
        cmd = new SqlCommand(select, db);
        int count5 = (int)cmd.ExecuteScalar();


        temp = (count1 + count2 + count3 + count4 + count5) / (5.0);
        result = string.Format("{0:0}", temp);
        MonthlyAvLengthLabel.Text = " " + result;


        //jobs addressed same day


        select = "select count(jobId) as num from Jobs where year(enqueueTime) = year('" + day + "') and month(enqueueTime) = month('" + day + "') and day(enqueueTime) = day('" + day + "') and  year(enqueueTime) = year(checkedIn) and  month(enqueueTime) = month(checkedIn) and  day(enqueueTime) = day(checkedIn) ";
        cmd = new SqlCommand(select, db);

        result = cmd.ExecuteScalar().ToString();
        DailySameDayLabel.Text = result;

        //month
        select = "select count(jobId) as num from Jobs where year(enqueueTime) = year('" + month + "') and month(enqueueTime) = month('" + month + "') and  year(enqueueTime) = year(checkedIn) and  month(enqueueTime) = month(checkedIn) and  day(enqueueTime) = day(checkedIn) ";
        cmd = new SqlCommand(select, db);

        result = cmd.ExecuteScalar().ToString();
        MonthlySameDayLabel.Text = result;

        //percent time empty


        //get technician stats
        //for daily
        // get jobs that have work intervals in the day
        select = "select id,username from Users where type = 1";
        //SqlConnection db2 = new SqlConnection(con);
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("username", typeof(string));
            dt.Columns.Add("downTime", typeof(int));
            

            using (SqlDataReader dr = command.ExecuteReader())
            {
                //dt row iterator
                int i = 0;
                while (dr.Read())
                {
                    string sameDay = "((day(checkedIn)=day('" + day + "') and month(checkedIn)=month('" + day + "') and year(checkedIn)=year('" + day + "')) or (day(dequeueTime)=day('" + day + "') and month(dequeueTime)=month('" + day + "') and year(dequeueTime)=year('" + day + "')))";
                    string select4 = "select checkedIn,dequeueTime from Jobs where checkedIn IS NOT NULL and " + sameDay + " and dequeueTime > '" + startOfDay + "' and checkedIn < '" + endOfDay + "' and technicianId=" + dr[0] + " Order by checkedIn ASC";
                    //"select checkedIn,dequeueTime from Jobs where dequeueTime > '" + morning + "' and (enqueueTime<'" + night + "' and checkedIn IS NOT NULL) and (Date(checkedIn)==Date('"+day+ "') or Date(dequeueTime)==Date('" + day + "'))and technicianId=" + dr[0]+" Order by enqueueTime ASC";

                    //System.Diagnostics.Debug.WriteLine(" dr 1 start");

                    List<List<DateTime>> intervals = new List<List<DateTime>>();
                    int downTime = 0;
                    //populate intervals
                    SqlCommand cmd2 = new SqlCommand(select4, db);
                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                    {
                        //System.Diagnostics.Debug.WriteLine(" dr 2 start");
                        int j = 0;
                        while (dr2.Read())
                        {

                            intervals.Add(new List<DateTime>());
                            intervals[j].Add((DateTime)dr2[0]);
                            intervals[j].Add((DateTime)dr2[1]);
                            j += 1;

                        }

                        int k = 0;


                        if (intervals.Count == 0)
                        {
                            downTime = 8 * 60;
                            //System.Diagnostics.Debug.WriteLine(dr[1] + " empty");
                        }
                        else
                        {
                            //case where didnt have job overnight
                            if (intervals.Count > 0 && (intervals[k][0].Hour >= 9 && intervals[k][0].Day == time.Day))
                            {
                                downTime += (intervals[k][0].Hour - 9) * 60 + intervals[k][0].Minute;
                                //System.Diagnostics.Debug.WriteLine("Case 1");
                                //System.Diagnostics.Debug.WriteLine((intervals[k][0].Hour - 9) * 60 + intervals[k][0].Minute);

                            }
                            while (k < intervals.Count - 1)
                            {
                                //System.Diagnostics.Debug.WriteLine(intervals[k][0] + "    dequueue" + intervals[k][1]);
                                TimeSpan inter = (intervals[k + 1][0] - intervals[k][1]);

                                //System.Diagnostics.Debug.WriteLine("Case 2");
                                downTime += inter.Hours * 60 + inter.Minutes;
                                //System.Diagnostics.Debug.WriteLine(inter.Hours * 60 + inter.Minutes);
                                k++;
                            }
                            //case where last job isnt overnight
                            if (intervals.Count > 0 && intervals[k][1].Hour < 17)
                            {
                                //System.Diagnostics.Debug.WriteLine(intervals[k][0] + "    dequueue" + intervals[k][1]);
                                //System.Diagnostics.Debug.WriteLine("Case 3");
                                downTime += (17 - intervals[k][1].Hour) * 60 - intervals[k][1].Minute;
                                //System.Diagnostics.Debug.WriteLine((17 - intervals[k][1].Hour) * 60 - intervals[k][1].Minute);
                            }
                        }
                      
                        
                    }


                    DataRow tempRow = dt.NewRow();
                    tempRow["id"] = (int)dr[0];
                    tempRow["username"] = (string)dr[1];
                    tempRow["downTime"] = (int)(downTime);
                    dt.Rows.Add(tempRow);
                    i += 1;
                }
                DailyTechnicianGrid.DataSource = dt;
                DailyTechnicianGrid.DataBind();
            }

        }
        
        //monthly
        using (SqlCommand command = new SqlCommand(select, db))
        {
            //add parameters and their values
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("username", typeof(string));
            dt.Columns.Add("downTime", typeof(int));

            using (SqlDataReader dr3 = command.ExecuteReader())
            {
                //dt row iterator
                int i = 0;
                while (dr3.Read())
                {
                    // string sameMonth = "((month(checkedIn)=month('" + month + "') and year(checkedIn)=year('" + month + "')) or (month(dequeueTime)=month('" + month + "') and year(dequeueTime)=year('" + month + "')))";
                    //string select4 = "select checkedIn,dequeueTime from Jobs where checkedIn IS NOT NULL and " + sameMonth + " and dequeueTime > '" + week0 + "' and checkedIn < '" + week4 + "' and technicianId=" + dr[0] + " Order by checkedIn ASC";
                    //"select checkedIn,dequeueTime from Jobs where dequeueTime > '" + morning + "' and (enqueueTime<'" + night + "' and checkedIn IS NOT NULL) and (Date(checkedIn)==Date('"+day+ "') or Date(dequeueTime)==Date('" + day + "'))and technicianId=" + dr[0]+" Order by enqueueTime ASC";
                   

                    int m = 1;
                    int downTime = 0;
                    while (m <= DateTime.DaysInMonth(lastM.Year, lastM.Month))
                    {
                        //dom = day of month
                        DateTime dom = new DateTime(lastM.Year, lastM.Month, m);
                        //iterator for days
                        int k = 0;

                        List<List<DateTime>> intervals = new List<List<DateTime>>();

                        string sameDay = "(day(checkedIn)=day('" + dom + "') and month(checkedIn)=month('" + dom + "') and year(checkedIn)=year('" + dom + "'))";
                        string select4 = "select checkedIn,dequeueTime from Jobs where checkedIn IS NOT NULL and " + sameDay + " and technicianId=" + dr3[0] + " Order by checkedIn ASC";

                        //populate intervals
                        SqlCommand cmd2 = new SqlCommand(select4, db);
                        using (SqlDataReader dr4 = cmd2.ExecuteReader())
                        {
                            int j = 0;
                            while (dr4.Read())
                            {

                                intervals.Add(new List<DateTime>());
                                intervals[j].Add((DateTime)dr4[0]);
                                intervals[j].Add((DateTime)dr4[1]);
                                j += 1;

                            }

 


                            if (intervals.Count == 0)
                            {
                                downTime += 8 * 60;
                                //System.Diagnostics.Debug.WriteLine(dr3[1] + " emptyx"+m);
                            }
                            else
                            {
                                //case where didnt have job overnight
                                if (intervals.Count > 0 && (intervals[k][0].Hour >= 9 && intervals[k][0].Day == dom.Day))
                                {
                                    downTime += (intervals[k][0].Hour - 9) * 60 + intervals[k][0].Minute;
                                    m = intervals[k][1].Day;
                                    //System.Diagnostics.Debug.WriteLine("Case 1");
                                    //System.Diagnostics.Debug.WriteLine((intervals[k][0].Hour - 9) * 60 + intervals[k][0].Minute);

                                }
                                while (k < intervals.Count - 1)
                                {

                                    //System.Diagnostics.Debug.WriteLine(intervals[k][0] + "    dequueue" + intervals[k][1]);
                                    TimeSpan inter = (intervals[k + 1][0] - intervals[k][1]);

                                    //System.Diagnostics.Debug.WriteLine("Case 2");
                                    downTime += inter.Hours * 60 + inter.Minutes;
                                    System.Diagnostics.Debug.WriteLine(inter.Hours * 60 + inter.Minutes);
                                    k++;
                                }
                                //case where last job isnt overnight
                                if (intervals.Count > 0 && intervals[k][1].Hour < 17)
                                {
                                    //System.Diagnostics.Debug.WriteLine(intervals[k][0] + "    dequueue" + intervals[k][1]);
                                    //System.Diagnostics.Debug.WriteLine("Case 3");
                                    downTime += (17 - intervals[k][1].Hour) * 60 - intervals[k][1].Minute;
                                    //System.Diagnostics.Debug.WriteLine((17 - intervals[k][1].Hour) * 60 - intervals[k][1].Minute);
                                }
                            }
                        }
                        m++;
                    }

                    DataRow tempRow = dt.NewRow();
                    tempRow["id"] = (int)dr3[0];
                    tempRow["username"] = (string)dr3[1];
                    tempRow["downTime"] = (int)(downTime);
                    dt.Rows.Add(tempRow);
                    i += 1;
                }
                MonthlyTechnicianGrid.DataSource = dt;
                MonthlyTechnicianGrid.DataBind();
            }
        }
        
    //db2.Close();
    db.Close();
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