using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace SmashNetwork.Areas.Develop.Models
{
    public class DataViewModel
    {
        public string id;
        public string tableName { get; set; }
        public DataTable table;
        public DataTable valuesTable;
        public List<string> columnList;
        public List<string> errorStrings;

        public DataViewModel()
        {
            table = new DataTable();
            valuesTable = new DataTable();
            columnList = new List<string>();
            errorStrings = new List<string>();
        }
    }
}
