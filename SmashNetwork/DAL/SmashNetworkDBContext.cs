using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using SmashNetwork.Areas.Users.Models;
using System.Data.SqlClient;

namespace SmashNetwork.DAL
{
    public enum SortDirection
    {
        Up,
        Down,
        None
    }

    public class SmashNetworkDBContext : DbContext
    {
        public SmashNetworkDBContext()
        {
            Database.SetInitializer<SmashNetworkDBContext>(null);
        }

        // User Authentication tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DataTable getTableFromParameters(List<string> columnList, string tableName, string limit = "", string sortBy = "", SortDirection sortDirection = SortDirection.None)
        {
            DataTable table = new DataTable();

            string sqlStatement = getStatementFromParameters(columnList, tableName, limit, sortBy, sortDirection);
            table = getTableFromStatement(sqlStatement);

            return table;
        }

        public DataTable getTableFromStatement(string sqlStatement)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SmashNetworkDBContext"].ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlStatement, connection))
                {
                    try
                    {
                        adapter.Fill(table);
                    }
                    catch (SqlException e)
                    {
                        //sqlTable.errors = e.Errors;
                        /*foreach (SqlError se in e.Errors)
                        {
                            errorStrings.Add("Message: " + se.Message
                                + " | Number: " + se.Number
                                + " | Line: " + se.LineNumber
                                + " | Source: " + se.Source
                                + " | Procedure: " + se.Procedure);
                        }*/
                    }
                }
            }

            return table;
        }

        public string getStatementFromParameters(List<string> columnList, string tableName, string limit = "", string sortBy = "", SortDirection sortDirection = SortDirection.None)
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
                switch (sortDirection)
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
                        /*foreach (SqlError se in e.Errors)
                        {
                            errorStrings.Add("Message: " + se.Message
                                + " | Number: " + se.Number
                                + " | Line: " + se.LineNumber
                                + " | Source: " + se.Source
                                + " | Procedure: " + se.Procedure);
                        }*/
                    }
                }
            }
        }
    }
}
