using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace shopbridge
{
    public class ReturnClass
    {
        public class ReturnDataTable
        {
            public string type { get; set; }
            public bool status { get; set; }
            public string json { get; set; }
            public string message { get; set; }
            public DataTable table { get; set; }
            public string file_name { get; set; }
            public string value { get; set; }
            public ReturnDataTable()
            {
                status = false;
                message = "";
                file_name = "";
                json = "[]";
                table = new DataTable();
                value = "";
            }
        }

        public class ReturnDataSet
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataSet dataset { get; set; }
            public string file_name { get; set; }
            public string json { get; set; }
            public string value { get; set; }
            public ReturnDataSet()
            {
                status = false;
                message = "";
                file_name = "";
                json = "[]";
                dataset = new DataSet();
                value = "";
            }
        }
        public class ReturnBool
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string number { get; set; }
            public ReturnBool()
            {
                status = false;
                message = "";
            }
        }
        public class ReturnString
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string value { get; set; }
            public ReturnString()
            {
                status = false;
                message = "";
                value = "";
            }
        }
        public class ReturnReportName
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string file_name { get; set; }
            public ReturnReportName()
            {
                status = false;
                message = "";
                file_name = "";
            }
        }

        //public class ReturnDataReader
        //{
        //    public string message { get; set; }
        //    public bool status { get; set; }
        //    public SqlDataReader DataReader { get; set; }

        //    public ReturnDataReader()
        //    {
        //        message = "";
        //        status = false;
        //        DataReader = null;
        //    }
        //}
    }
}
