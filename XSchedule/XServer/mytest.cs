/* This is an example class to check that it works with the XSchedule Web Project
 * It follows the tutorial here: https://msdn.microsoft.com/en-us/library/cc668164.aspx
 * There is a Label on the About.aspx page that uses the mytest class */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XServer
{
    public class mytest
    {
        private int _age;
        private string _name;

        public mytest()
        {
            Age = 0;
            Name = "Not available";
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}