using System;
using System.Data;

namespace PrimitiveExtensions
{
    /// <summary>
    /// Provides static methods that aid in performing database related operations.
    /// </summary>
    public static class IDbConnectionExtensions
    {
        /// <summary>
        ///  Return a resultset based on provided SQL
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="sql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="dbTransaction"></param>
        /// <returns> Data reader object</returns>
        public static IDataReader GetDataReader(this IDbConnection cn, string sql, int commandTimeout = 30, IDbTransaction dbTransaction = null)
        {
            var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction = dbTransaction ?? cmd.Transaction;
            cmd.CommandTimeout = commandTimeout;
            return cmd.ExecuteReader();
        }

        /// <summary>
        /// Returns true if the connection to the database is open
        /// </summary>
        /// <param name="cn"></param>
        /// <returns>True if connect is open</returns>
        public static bool IsOpen(this IDbConnection cn)
        {
            return cn.State == ConnectionState.Open;
        }


        /// <summary>
        /// Returns true if the connection test to the database is successful 
        /// </summary>
        /// <param name="cn"></param>
        /// <returns>True if the connection is successfully opened</returns>
        public static bool ConnectionTestsSuccessfully(this IDbConnection cn)
        {
            if (cn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                try
                {
                    cn.Open();
                    cn.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  Retruns number of rows affected from non SQl query (insert, delete, update)
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="sql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="dbTransaction"></param>
        /// <returns>Number of records affected by the query</returns>
        public static int ExecuteSqlNonQuery(this IDbConnection cn, string sql, int commandTimeout = 30, IDbTransaction dbTransaction = null)
        {
            var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction = dbTransaction ?? cmd.Transaction;
            cmd.CommandTimeout = commandTimeout;
            return cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// Returns the value from the first column of the first row of your query.
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="sql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="dbTransaction"></param>
        /// <returns>Scalar aggregate value (returns one row consisting of one column)</returns>
        public static object ExecuteSqlScalar(this IDbConnection cn, string sql, int commandTimeout = 30, IDbTransaction dbTransaction = null)
        {
            var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction =  dbTransaction ?? cmd.Transaction;
            cmd.CommandTimeout = commandTimeout;
            return cmd.ExecuteScalar();
        }


        /// <summary>
        ///  Retruns a datatable.
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="sql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="dbTransaction"></param>
        /// <returns> Data table</returns>
        public static DataTable GetDataTable(this IDbConnection cn, string sql, int commandTimeout = 30, IDbTransaction dbTransaction = null)
        {

            DataTable dt = new DataTable();
            dt.Load(GetDataReader(cn, sql, commandTimeout, dbTransaction));
            return dt;
        }
        /// <summary>
        ///  Excutes sql command
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="sql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="dbTransaction"></param>
        /// <returns>Command Object</returns>
        public static IDbCommand GetNewCommand(this IDbConnection cn, string sql, int commandTimeout = 30, IDbTransaction dbTransaction = null)
        {
            var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction = dbTransaction ?? cmd.Transaction;
            cmd.CommandTimeout = commandTimeout;
            return cmd;
        }
    }
}
