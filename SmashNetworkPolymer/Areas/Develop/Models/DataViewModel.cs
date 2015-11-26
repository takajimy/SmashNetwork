using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace SmashNetworkPolymer.Areas.Develop.Models
{
    public enum SortDirection
    {
        Up,
        Down,
        None
    }

    public enum Table
    {
        Default,
        Values
    }

    public class DataViewModel
    {
        public string id;
        public string tableName { get; set; }
        public DataTable table;
        public DataTable valuesTable;
        public List<string> errorStrings;
        public SqlParameters sqlParameters;

        public DataViewModel()
        {
            table = new DataTable();
            valuesTable = new DataTable();
            errorStrings = new List<string>();
            sqlParameters = new SqlParameters();
        }

        public void constructTableFromParameters(Table table = Table.Default)
        {
            string sqlStatement = sqlParameters.constructSqlStatement();
            constructTableFromStatement(sqlStatement, table);
        }

        public void constructTableFromStatement(string sqlStatement, Table whichTable = Table.Default)
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SmashNetworkDBContext"].ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, connection))
                {
                    try
                    {
                        if (whichTable == Table.Values)
                        {
                            adapter.Fill(valuesTable);
                        }
                        else
                        {
                            adapter.Fill(table);
                        }
                    }
                    catch (SqlException e)
                    {
                        //sqlTable.errors = e.Errors;
                        foreach (SqlError se in e.Errors)
                        {
                            errorStrings.Add("Message: " + se.Message
                                + " | Number: " + se.Number
                                + " | Line: " + se.LineNumber
                                + " | Source: " + se.Source
                                + " | Procedure: " + se.Procedure);
                        }
                    }
                }
            }
        }

        public void executeStatement(string sqlStatement)
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SmashNetworkDBContext"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlStatement, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        //sqlTable.errors = e.Errors;
                        foreach (SqlError se in e.Errors)
                        {
                            errorStrings.Add("Message: " + se.Message
                                + " | Number: " + se.Number
                                + " | Line: " + se.LineNumber
                                + " | Source: " + se.Source
                                + " | Procedure: " + se.Procedure);
                        }
                    }
                }
            }
        }

        public class SqlParameters
        {
            public List<string> columnList;
            public string tableName;
            public string limit;
            public string sortBy;
            public SortDirection sortDirection;

            public SqlParameters()
            {
                columnList = new List<string>();
            }

            public string constructSqlStatement()
            {
                string sqlStatement = "SELECT ";
                if (!String.IsNullOrEmpty(limit))
                {
                    sqlStatement += "TOP " + limit + " ";
                }
                if (columnList == null || columnList.Count == 0)
                {
                    sqlStatement += "* ";
                }
                else
                {
                    int count = 1;
                    foreach (string column in columnList)
                    {
                        if (count == 1)
                        {
                            sqlStatement += column + " ";
                        }
                        else
                        {
                            sqlStatement += ", " + column + " ";
                        }
                        count++;
                    }
                }
                sqlStatement += "FROM " + tableName;
                if (!String.IsNullOrEmpty(sortBy))
                {
                    switch(sortDirection)
                    {
                        case SortDirection.Up:
                            sqlStatement += " ORDER BY " + sortBy + " ASC";
                            break;
                        case SortDirection.Down:
                            sqlStatement += " ORDER BY " + sortBy + " DESC";
                            break;
                        default:
                            break;
                    }
                }
                
                return sqlStatement;
            }
        }
    }
}
