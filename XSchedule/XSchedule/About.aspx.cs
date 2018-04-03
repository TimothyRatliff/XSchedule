using XServer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mytest one = new mytest();
        one.Name = "Test of XServer Class Library";
        Label1.Text = "Name: " + one.Name;

    }
}