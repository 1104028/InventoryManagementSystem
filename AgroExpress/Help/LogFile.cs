using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

namespace AgroExpress.Help
{
    public class LogFile
    {
        public DateTime logTime { get; set; }
        public string errorType { get; set; }
        public string message { get; set; }

        public void Write()
        {
            string Error = "Log time : " + this.logTime + " Error type : " + this.errorType + " Error Message : " + this.message+Environment.NewLine;
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Error.txt");            
            File.AppendAllText(path, Error);
        }

    }
}