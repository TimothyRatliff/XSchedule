using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

public partial class Manager : System.Web.UI.Page
{
    TimeSpan TimeWithout95(DateTime start, DateTime end)
    {
        TimeSpan diff = new TimeSpan(0, 0, 0);

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


    string con = "Data Source=den1.mssql3.gear.host;Initial Catalog=TestDBXSCHEDULE1;User Id=testdbxschedule1;MultipleActiveResultSets=true; Password=By2up3~f6!Wy";

    protected void Page_Load(object sender, EventArgs e)
    {
       // System.Diagnostics.Debug.WriteLine(" PAGE LOADDDDDDDDDDDDDDDDDDDDDDDDDDDD");
       // compensate for server time -ghetto
        DateTime time = (DateTime.Now).AddHours(-6);
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
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)or (X.priority = J.priority and X.enqueueTime=J.enqueueTime and X.lastUpdated<J.lastUpdated)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        string select2 = "select jobId, enqueueTime,baseEnqueueTime,priority," + subSelect + " from Jobs J where checkedIn IS NULL Order By position ASC ,enqueueTime ASC,lastUpdated ASC";

        using (SqlCommand command = new SqlCommand(select2, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                queueGrid.DataSource = dr;
                queueGrid.DataBind();
                
            }
        }

         emptyQueueDiv.Visible = (queueGrid.Rows.Count == 0);

        //average wait time of those fineshed yesterday
        string day = yesterday + " 00:00:00.000";


        select = "select SUM(DATEDIFF(minute,checkedIn,dequeueTime)) as totalWait from Jobs where year(dequeueTime) = year('" + day + "') and month(dequeueTime) = month('" + day + "') and day(dequeueTime) = day('" + day + "') and completed = 1";
        cmd = new SqlCommand(select, db);
        object results = cmd.ExecuteScalar();
        int totalMinutes = 0;
        if (results.ToString() != "")
        {
            totalMinutes = (int)results;
        }
        System.Diagnostics.Debug.WriteLine(totalMinutes);
        select = "select count(*) from Jobs where year(dequeueTime) = year('" + day + "') and month(dequeueTime) = month('" + day + "') and day(dequeueTime) = day('" + day + "')";
        cmd = new SqlCommand(select, db);
        int count = (int)cmd.ExecuteScalar();
        if (count == 0)
            count = 1; 

        double temp = (totalMinutes / (count * 60.0));
        string result = string.Format("{00:0}", temp);
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
        string noon = yesterday + " 12:00:00";
        string night = yesterday + " 23:59:59";

        string startOfDay = yesterday + " 09:00:00";
        string endOfDay = yesterday + " 17:00:00";

        select = "select AVG(Cast(qCount as Float)) from Jobs where baseEnqueueTime >'" + morning + "' and baseEnqueueTime <'" + endOfDay + "'";
        cmd = new SqlCommand(select, db);

        double av = 0;
        var value = cmd.ExecuteScalar();
        if (value != DBNull.Value)
        {
            av = (double)value;

        }
        result = string.Format("{0:0}", av);
        DailyAvLengthLabel.Text = " " + result;


        string week0 = lastMonth + "-01 00:00:00";
        string week1 = lastMonth + "-08 00:00:00";
        string week2 = lastMonth + "-15 00:00:00";
        string week4 = lastMonth + "-28 23:00:00";

        select = "select AVG(Cast(qCount as Float)) as aver from Jobs where baseEnqueueTime >'" + week0 + "' and baseEnqueueTime<'" + week4 + "'";
        cmd = new SqlCommand(select, db);
        av = 0;
        value = cmd.ExecuteScalar();
        if (value != DBNull.Value)
            av = (double)value;

        result = string.Format("{0:0}", av);
        MonthlyAvLengthLabel.Text = " " + result;


        //jobs addressed same day


        select = "select count(jobId) as num from Jobs where year(baseEnqueueTime) = year('" + day + "') and month(baseEnqueueTime) = month('" + day + "') and day(baseEnqueueTime) = day('" + day + "') and  year(baseEnqueueTime) = year(checkedIn) and  month(baseEnqueueTime) = month(checkedIn) and  day(baseEnqueueTime) != day(checkedIn) ";
        cmd = new SqlCommand(select, db);

        result = cmd.ExecuteScalar().ToString();
        DailySameDayLabel.Text = result;

        //month
        select = "select count(jobId) as num from Jobs where year(baseEnqueueTime) = year('" + month + "') and month(baseEnqueueTime) = month('" + month + "') and  year(baseEnqueueTime) = year(checkedIn) and  month(baseEnqueueTime) = month(checkedIn) and  day(baseEnqueueTime) != day(checkedIn) ";
        cmd = new SqlCommand(select, db);

        result = cmd.ExecuteScalar().ToString();
        MonthlySameDayLabel.Text = result;

        //percent time empty

        //daily
        //get first job of day
        long firstMinPrev = -1;
        long lastMinPrev = -1;

        double empty;

        //last job before day started
        select = "select top 1 minPrevEmpty from Jobs where baseEnqueueTime <'" + morning + "' order by baseEnqueueTime DESC";
        cmd = new SqlCommand(select, db);
        results = cmd.ExecuteScalar();
        if (results != null)
        {
            firstMinPrev = (long)results;
        }


        //get last job of day

        //first job  after day ended
        select = "select top 1 minPrevEmpty from Jobs where baseEnqueueTime >'" + night + "' order by baseEnqueueTime ASC";
        cmd = new SqlCommand(select, db);
        results = cmd.ExecuteScalar();
        if(results == null)
        {
            select = "select top 1 minPrevEmpty from Jobs where baseEnqueueTime <'" + night + "' order by baseEnqueueTime DESC";
            cmd = new SqlCommand(select, db);
            results = cmd.ExecuteScalar();
            
        }

        lastMinPrev = (long)results;



        empty = 100*(lastMinPrev - firstMinPrev) / (8.0 * 60);


        result = string.Format("{00:0}", empty);
        DailyTimeEmptyLabel.Text = " "+result+"%";

        //monthly

        //last job before day started
        select = "select top 1 minPrevEmpty from Jobs where baseEnqueueTime <'" + week0 + "' order by baseEnqueueTime DESC";
        cmd = new SqlCommand(select, db);
        results = cmd.ExecuteScalar();
        if (results != null)
        {
            firstMinPrev = (long)results;
        }


        //get last job of day
        
        //first job  after day ended
        select = "select top 1 minPrevEmpty from Jobs where baseEnqueueTime >'" + week4 + "' order by baseEnqueueTime ASC";
        cmd = new SqlCommand(select, db);
        results = cmd.ExecuteScalar();
        if (results == null)
        {
            select = "select top 1 minPrevEmpty from Jobs where baseEnqueueTime <'" + week4 + "' order by baseEnqueueTime DESC";
            cmd = new SqlCommand(select, db);
            results = cmd.ExecuteScalar();

        }

        lastMinPrev = (long)results;



        empty = 100 * (lastMinPrev - firstMinPrev) / (29*8.0 * 60);


        result = string.Format("{00:0}", empty);
        MonthlyTimeEmptyLabel.Text = " " + result + "%";

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


                    List<List<DateTime>> intervals = new List<List<DateTime>>();
                    int downTime = 0;
                    //populate intervals
                    SqlCommand cmd2 = new SqlCommand(select4, db);
                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                    {

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
                        }
                        else
                        {
                            //case where didnt have job overnight
                            if (intervals.Count > 0 && (intervals[k][0].Hour >= 9 && intervals[k][0].Day == time.Day))
                            {
                                downTime += (intervals[k][0].Hour - 9) * 60 + intervals[k][0].Minute;

                            }
                            while (k < intervals.Count - 1)
                            {
                                if (intervals[k + 1][0] > intervals[k][1])
                                {
                                    TimeSpan inter = (intervals[k + 1][0] - intervals[k][1]);
                                    downTime += inter.Hours * 60 + inter.Minutes;

                                }

                                k++;
                            }
                            //case where last job isnt overnight
                            if (intervals.Count > 0 && intervals[k][1].Hour < 17)
                            {

                                downTime += (17 - intervals[k][1].Hour) * 60 - intervals[k][1].Minute;
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
                   
                    string sameMonth = "(month(checkedIn)=month('" + lastM + "') and year(checkedIn)=year('" + lastM + "'))";
                        string select4 = "select checkedIn,dequeueTime from Jobs where dequeueTime IS NOT NULL and " + sameMonth + " and technicianId=" + dr3[0] + " Order by checkedIn ASC";

                        //populate intervals
                        SqlCommand cmd2 = new SqlCommand(select4, db);
                        using (SqlDataReader dr4 = cmd2.ExecuteReader())
                        {

                        int downTime = 0;

                            List<List<DateTime>> intervals = new List<List<DateTime>>();

                            int j = 0;
                            while (dr4.Read() )
                            {
                                intervals.Add(new List<DateTime>());
                                intervals[j].Add((DateTime)dr4[0]);
                                intervals[j].Add((DateTime)dr4[1]);
                                j += 1;

                            }



                        if (intervals.Count == 0)
                            {
                             downTime+=8 * 60*DateTime.DaysInMonth(lastM.Year,lastM.Month);
                                
                            }
                        else
                        {
                            int k = 0;
                                    
                            if ( intervals[k][0].Hour >= 9)
                            {
                                downTime += (intervals[k][0].Hour - 9) * 60 + intervals[k][0].Minute;


                            }
                            while (k < intervals.Count - 1)
                            {

                                TimeSpan diff = TimeWithout95(intervals[k][1], intervals[k + 1][0]);
                                downTime += diff.Days * 8 * 60 + diff.Hours * 60 + diff.Minutes;
                                    k++;

                            }
                            //case where last job isnt overnight
                            if (intervals.Count > 0 && intervals[k][1].Hour < 17)
                            {

                                downTime += (17 - intervals[k][1].Hour) * 60 - intervals[k][1].Minute;
                            }

                        //adding downtime for days without a job until end of the month
                        downTime += (DateTime.DaysInMonth(lastM.Year, lastM.Month) - intervals[k][1].Day) * 8 * 60;

                        }
                            
                        DataRow tempRow = dt.NewRow();
                        tempRow["id"] = (int)dr3[0];
                        tempRow["username"] = (string)dr3[1];
                        tempRow["downTime"] = (int)(downTime);
                        dt.Rows.Add(tempRow);
                        i += 1;
                    }



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

    protected void JobEditButton_Click(object sender, EventArgs e)
    {

        SqlConnection db = new SqlConnection(con);
        db.Open();


        //crappy way to find key
        int i = 1;
        int.TryParse(newPositionField.Text, out i);
        string select = "SELECT enqueueTime,priority from jobs where jobId="+queueGrid.DataKeys[i]["jobId"];
        System.Diagnostics.Debug.WriteLine(i);

        //get index of job to change
        int jobInd = -1;
        int j = 0;
        while (j < queueGrid.Rows.Count)
        {
            if (queueGrid.Rows[j].Cells[0].Text == jobEditField.Text)
            {
                jobInd = j;
                j = queueGrid.Rows.Count;
            }
            j++;
        }
        DateTime baseTime = (DateTime)queueGrid.DataKeys[i]["enqueueTime"];
        System.Diagnostics.Debug.WriteLine(baseTime);

        DateTime newTime = new DateTime();
        DateTime time = (DateTime.Now).AddHours(-6);
        if (jobInd > i)
            newTime = baseTime.AddSeconds(-1);
        else if (jobInd == i)
        {
            newTime = baseTime;
        }
        else
            newTime = baseTime.AddSeconds(1);

        System.Diagnostics.Debug.WriteLine("id" + queueGrid.Rows[i].Cells[0].Text);
        System.Diagnostics.Debug.WriteLine(newTime);

        string format = "yyyy-MM-dd HH:mm:ss";
        string timeString = newTime.ToString(format);
        string update = "Update Jobs set enqueueTime='" + timeString + "', lastUpdated = '"+time+"', priority=" + queueGrid.DataKeys[i]["priority"].ToString() + " where jobId =" + jobEditField.Text + ";";
 
        SqlCommand updateCmd = new SqlCommand(update, db);
        updateCmd.ExecuteNonQuery();
        alertDiv1.InnerText = "Job Edited";
        //force query reload
        string subSelect = "(select count(*) from Jobs X  where (X.priority>J.priority or (X.priority = J.priority and X.enqueueTime<J.enqueueTime)or (X.priority = J.priority and X.enqueueTime=J.enqueueTime and X.lastUpdated<J.lastUpdated)) and X.technicianId IS NULL and J.technicianID IS NULL) as position";
        string select2 = "select jobId, enqueueTime,baseEnqueueTime,priority," + subSelect + " from Jobs J where checkedIn IS NULL Order By position ASC ,enqueueTime ASC,lastUpdated ASC";

        using (SqlCommand command = new SqlCommand(select2, db))
        {
            //add parameters and their values

            using (SqlDataReader dr = command.ExecuteReader())
            {
                queueGrid.DataSource = dr;
                queueGrid.DataBind();

            }
        }


        db.Close();
    }


}