using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;



namespace SSAx.PrimitiveExtensions
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Returns page of a specified page number and size
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataTable GetPage(this DataTable dt, int pageNum, int pageSize)
        {
            DataTable dtPage = new DataTable();

            if (pageNum < 1)
            {
                throw new IndexOutOfRangeException();
            }

            if (dt.TableName.IsNullOrEmptyString())
            {
                dt.TableName = "table1";
            }

            MemoryStream xmlStream = new MemoryStream();
            dt.WriteXmlSchema(xmlStream);

            xmlStream.Position = 0;

            StreamReader reader = new StreamReader(xmlStream);
            dtPage.ReadXmlSchema(reader);

            var pageRows = dt.Rows.Cast<System.Data.DataRow>()
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize);
            //.CopyToDataTable(dtPage, LoadOption.PreserveChanges);

            //TODO ?temp? fix until .CopyToDataTable is added to dotnetcore / dotnetstandard
            //https://github.com/dotnet/corefx/issues/19771
            //
            foreach (var row in pageRows)
            {
                dtPage.Rows.Add(row.ItemArray);
            }

            return dtPage;
        }



        /// <summary>
        /// Sets primary key column to read only mode
        /// </summary>
        /// <param name="dt"></param>
        public static void SetPrimaryKeyColumnsToReadOnly(this DataTable dt)
        {
            foreach (DataColumn pkColumn in dt.GetPrimaryKeyColumns())
            {
                pkColumn.ReadOnly = true;
            }
        }

        /// <summary>
        /// Returns a list of primary key column names of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetPrimaryKeyColumnNames(this DataTable dt)
        {
            return dt
                .GetPrimaryKeyColumns()
                .Select(a => a.ColumnName)
                .ToList();
        }

        /// <summary>
        /// Returns lsit of primary key columns of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<DataColumn> GetPrimaryKeyColumns(this DataTable dt)
        {
            return dt.PrimaryKey.ToList();
        }

        /// <summary>
        /// Returns list of non primary key column names of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetNonPrimaryKeyColumnNames(this DataTable dt)
        {

            List<string> s = new List<string>();
            foreach (DataColumn c in dt.Columns)
            {
                if (!c.IsAPrimaryKeyColumn(dt))
                {
                    s.Add(c.ColumnName);
                }
            }
            return s;
        }

        /// <summary>
        /// Returns list of non primary key columns of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<DataColumn> GetNonKeyColumns(this DataTable dt)
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            foreach (DataColumn column in dt.Columns)
            {
                if (!column.IsAPrimaryKeyColumn(dt))
                {
                    dataColumns.Add(column);
                }
            }
            return dataColumns;
        }

        /// <summary>
        /// Returns list of column names of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames(this DataTable dt)
        {
            List<string> s = new List<string>();
            foreach (DataColumn c in dt.Columns)
            {
                s.Add(c.ColumnName);
            }
            return s;
        }

        /// <summary>
        /// Returns list of column names that are non identity columns
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetNonIdentityColumnNames(this DataTable dt)
        {
            List<string> s = new List<string>();
            foreach (DataColumn c in dt.Columns)
            {
                if (c.AutoIncrement == false)
                {
                    s.Add(c.ColumnName);
                }
            }
            return s;
        }


        /// <summary>
        /// Gets a collection of SQL Statements based on the data in, and the structure of the DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetInsertSql(this DataTable dt, SqlGeneratorConfig config)
        {
            List<string> insertSqlStatements = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string sql = row.GetInsertSql(config);
                insertSqlStatements.Add(sql);
            }
            return insertSqlStatements;
        }
        

        /// <summary>
        /// Returns sql statement by row state to instert data into a table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="schemaName"></param>
        /// <param name="overrideTableName"></param>
        /// <param name="tabulate"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetInsertSqlByRowState(this DataTable dt, SqlGeneratorConfig config)
        {

            List<string> insertSqlStatements = new List<string>();
            List<string> valueList = new List<string>();
            List<string> columnNameList = dt.GetColumnNames().ToList();
            string targetTableName = dt.TableName;

            DataTable dtAdded = dt.GetChanges(DataRowState.Added);


            foreach (DataRow r in dtAdded.Rows)
            {
                insertSqlStatements.Add( r.GetInsertSql(config));
                //if (r.RowState == DataRowState.Added)
                //{
                //    foreach (string columnName in columnNameList)
                //    {
                //        valueList.Add(r[columnName].EncodeAsSqlVariable());
                //    }

                //    sql = string.Format("INSERT INTO {0}.{1} ({2}) ", schemaName, targetTableName, string.Join(", ", columnNameList));
                //    if (tabulate) sql += Environment.NewLine;
                //    sql += string.Format("VALUES ({0});", string.Join(", ", valueList));
                //    insertSqlStatements.Add(sql);
                //    valueList.Clear();
                //}
            }
            return insertSqlStatements;
        }

        /// <summary>
        /// Returns sql statement by row state to delete row from a table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="schemaName"></param>
        /// <param name="overrideTableName"></param>
        /// <param name="tabulate"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetDeleteSqlByRowState(this DataTable dt, SqlGeneratorConfig config)
        {
            if (dt.PrimaryKey.Count() == 0) throw new MissingPrimaryKeyException(dt.TableName + " does not have a Primary Key defined");

            DataTable dtDeleted = dt.GetChanges(DataRowState.Deleted);
            if (dtDeleted == null) return new List<string>();




            List<string> deleteSqlStatements = new List<string>();
            foreach (DataRow r in dtDeleted.Rows)
            {
                string sql = r.GetDeleteSql(config);
                deleteSqlStatements.Add(sql);
            }
            return deleteSqlStatements;
        }

        

        /// <summary>
        /// Returns sql statement by row state to udpate row in a table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="schemaName"></param>
        /// <param name="overrideTableName"></param>
        /// <param name="tabulate"></param>
        /// <returns></returns>

        public static IEnumerable<string> GetUpdateSqlByRowState(this DataTable dt, SqlGeneratorConfig config)
        {
            if (dt.PrimaryKey.Count() == 0) throw new MissingPrimaryKeyException(dt.TableName + " does not have a Primary Key defined");
            
            DataTable dtModified = dt.GetChanges(DataRowState.Modified);
            if (dtModified == null) return new List<string>();

            List<string> updateSqlStatements = new List<string>();
            foreach (DataRow r in dtModified.Rows)
            {
                updateSqlStatements.Add(r.GetUpdateSql(config));
            }
            return updateSqlStatements;
        }


        /// <summary>
        /// Returns a string of HTML code to display a Table on a web page
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToHtmlTable(this DataTable dt)
        {
            string cellValue = "";
            string tab = "\t";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tab + tab + "<table>");

            //headers
            sb.Append(tab + tab + tab + "<tr>");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", dc.ColumnName);
            }
            sb.Append(tab + tab + tab + "</tr>");

            //data rows
            foreach (DataRow row in dt.Rows)
            {
                sb.Append(tab + tab + tab + "<tr>");

                foreach (DataColumn col in dt.Columns)
                {
                    cellValue = row[col] != null ? row[col].ToString() : "";
                    if (col.DataType == typeof(System.DateTime) & cellValue != "")
                    {
                        try
                        {
                            DateTime dateTime = DateTime.Parse(cellValue);
                            cellValue = dateTime.ToString(dateTime.Date == dateTime ? "d" : "g");
                        }
                        catch (Exception)
                        {
                        }
                    }
                    sb.AppendFormat("<td>{0}</td>", cellValue);
                }

                sb.Append(tab + tab + tab + "</tr>");
            }




            sb.AppendLine(tab + tab + "</table>");
            return sb.ToString();
        }

        /// <summary>
        /// Copies data from a table to another table
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="allowDeletesOnDestination"></param>
        public static void SimpleDataTableCopier(this DataTable source, DataTable destination, bool allowDeletesOnDestination = true)
        {
            DataTable sourceCopy = source.Copy();



            sourceCopy.Load(source.CreateDataReader(), LoadOption.Upsert);
            destination.AcceptChanges();

            //destination.OfflineCopyOfDataTable.Load(sourceCopy.CreateDataReader(), LoadOption.Upsert);
            destination.Merge(sourceCopy, false, MissingSchemaAction.AddWithKey);
            if (allowDeletesOnDestination)
            {
                foreach (DataRow r in destination.Rows)
                {
                    //Console.WriteLine(r.RowState);
                    if (r.RowState == DataRowState.Unchanged)
                    {
                        r.Delete();
                    }
                }
            }
        }

        /// <summary>
        /// /// Sets a best guess of a primary key 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Returns true if a key unique key was able to be created as the primary key. Returns false is no key was able to be found.</returns>
        public static bool SetBestGuessPrimaryKey(this DataTable dt)
        {
            if (dt.PrimaryKey.Count() > 0)
                return true;

            if (TryToSetPrimaryKeyBasedOnCommonNames(dt))
                return true;

            DataColumn[] pkeys = null;

            var subsets = dt.GetColumnNames().ToArray().CreateSubsets();
            foreach (var subset in subsets.OrderBy(a => a.Count()))
            {
                pkeys = new DataColumn[subset.Count()];
                int i = 0;
                foreach (var columnName in subset)
                {
                    pkeys[i] = dt.Columns[columnName];
                    i++;
                }
                try
                {
                    dt.PrimaryKey = pkeys;
                    return true;
                }
                catch (Exception)
                { }
            }
            return false;
        }


        /// <summary>
        /// Attempts to set primary key based on column names
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static bool TryToSetPrimaryKeyBasedOnCommonNames(DataTable dt)
        {
            DataColumn[] pkeys = null;
            string[] commonPKs = { "ID", "GUID", "UID", "UUID" };
            foreach (var commonPkName in commonPKs)
            {
                if (dt.Columns.Contains(commonPkName))
                {
                    try
                    {
                        pkeys = new DataColumn[1];
                        pkeys[0] = dt.Columns[commonPkName];
                        dt.PrimaryKey = pkeys;
                        return true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Copies a list of data table into another list of data table
        /// </summary>
        /// <param name="dtsSource"></param>
        /// <param name="dtsDestination"></param>

        public static void CopyTo(this List<DataTable> dtsSource, List<DataTable> dtsDestination)
        {
            foreach (DataTable dt in dtsSource)
            {
                dtsDestination.Add(dt.Copy());
            }
        }





        /// <summary>
        /// Identifies and returns all the non key column names that are shared by all the datatables in the collection. Other properties of the columns are not compared and are ignored. 
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames_NonPrimaryKey_Shared(this IEnumerable<DataTable> dts)
        {
            return GetColumns(dts, false, true);
        }


        /// <summary>
        /// Identifies and returns all the key column names that are shared by all the datatables in the collection. Other properties of the columns are not compared and are ignored. 
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames_PrimaryKey_Shared(this IEnumerable<DataTable> dts)
        {
            return GetColumns(dts, true, true);
        }


        /// <summary>
        /// Identifies and returns all the non key column names that are shared by all the datatables in the collection. Other properties of the columns are not compared and are ignored. 
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames_PrimaryKey_NotShared(this IEnumerable<DataTable> dts)
        {
            return GetColumns(dts, true, false);
        }


        /// <summary>
        /// Identifies and returns all the key column names that are shared by all the datatables in the collection. Other properties of the columns are not compared and are ignored. 
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumnNames_NonPrimaryKey_NotShared(this IEnumerable<DataTable> dts)
        {
            return GetColumns(dts, false, false);
        }


        private static IEnumerable<string> GetColumns(IEnumerable<DataTable> dts, bool getKey = true, bool getShared = false)
        {
            Dictionary<string, int> keyColumns = new Dictionary<string, int>();
            Dictionary<string, int> nonKeyColumns = new Dictionary<string, int>();
            List<string> notSharedColumns = new List<string>();
            List<string> sharedColumns = new List<string>();


            foreach (DataTable dt in dts)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    string columnName = c.ColumnName;
                    if (c.IsAPrimaryKeyColumn(dt))
                    {
                        if (keyColumns.ContainsKey(columnName))
                        {
                            keyColumns[columnName] = keyColumns[columnName] + 1;
                        }
                        else
                        {
                            keyColumns.Add(columnName, 1);
                        }
                    }
                    else
                    {
                        if (nonKeyColumns.ContainsKey(columnName))
                        {
                            nonKeyColumns[columnName] = nonKeyColumns[columnName] + 1;
                        }
                        else
                        {
                            nonKeyColumns.Add(columnName, 1);
                        }
                    }
                }
            }

            Dictionary<string, int> colKvp = new Dictionary<string, int>();
            if (getKey)
            {
                colKvp = keyColumns;
            }
            else
            {
                colKvp = nonKeyColumns;
            }


            foreach (KeyValuePair<string, int> column in colKvp)
            {
                if (column.Value < dts.Count())
                {
                    notSharedColumns.Add(column.Key);
                }
                else
                {
                    sharedColumns.Add(column.Key);
                }
            }

            if (getShared)
            {
                return sharedColumns;
            }
            else
            {
                return notSharedColumns;
            }
        }


        /// <summary>
        /// Renames a column name if exists in the table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <param name="newName"></param>
        /// <param name="deleteNewIfExists"></param>
        public static void RenameColumnIfExists(this DataTable dt, string columnName, string newName, bool deleteNewIfExists = true)
        {
            if (dt.Columns.Contains(newName) && deleteNewIfExists)
            {
                dt.Columns.Remove(newName);
            }
            if (dt.Columns.Contains(columnName))
            {
                dt.Columns[columnName].ColumnName = newName;
            }
        }

        /// <summary>
        /// Deletes a column name if exists in the table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        public static void DeleteColumnIfExists(this DataTable dt, string columnName)
        {
            if (dt.Columns.Contains(columnName))
            {
                if (dt.Columns[columnName].IsAPrimaryKeyColumn(dt))
                {

                }
                else
                {
                    dt.Columns.Remove(columnName);
                }
            }
            //return dt;
        }


        /// <summary>
        /// Deletes all columns from table except the ones passed in an array
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnsToKeep"></param>
        public static void DeleteAllColumnsButThese(this DataTable dt, string[] columnsToKeep)
        {
            List<string> colsToDelete = new List<string>();

            foreach (DataColumn c in dt.Columns)
            {
                if (!columnsToKeep.Contains(c.ColumnName))
                {
                    colsToDelete.Add(c.ColumnName);
                }
            }
            foreach (string colToDelete in colsToDelete)
            {
                dt.DeleteColumnIfExists(colToDelete);
            }
            /*int i = 0;
            foreach (string s in columnsToKeep)
            {
                dt.Columns[s].SetOrdinal(i);
                i++;
            }*/

            int i = 0;
            foreach (string s in columnsToKeep)
            {
                if (s != null)
                {
                    dt.Columns[s].SetOrdinal(i);
                    i++;
                }
            }
        }


        /// <summary>
        /// Deletes row from a table that are under unchanged row state
        /// </summary>
        /// <param name="dt"></param>
        public static void DeleteUnchangedRows(this DataTable dt)
        {
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Unchanged)
                {
                    rowsToDelete.Add(row);
                }
            }

            foreach (DataRow row in rowsToDelete)
            {
                row.Delete();
            }
        }


        /// <summary>
        /// Adds a column if does not exist in the table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <param name="t"></param>
        /// <param name="expression"></param>
        /// <param name="deleteIfExists"></param>
        public static DataColumn AddColumnIfNotExists(this DataTable dt, string columnName, Type t, string expression = null, bool deleteIfExists = true)
        {
            if (!dt.Columns.Contains(columnName))
            {
                return dt.Columns.Add(columnName, t, expression);
            }
            else
            {
                if (deleteIfExists)
                {
                    dt.Columns.Remove(columnName);
                    return dt.Columns.Add(columnName, t, expression);
                }
                else
                {
                    return dt.Columns[columnName];
                }
            }
        }

        /// <summary>
        /// Sets column ordinal if the column exist
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <param name="ordinal"></param>
        public static void SetColumnOrdinalIfExists(this DataTable dt, string columnName, int ordinal)
        {
            if (dt.Columns.Contains(columnName))
            {
                if (dt.Columns.Count >= ordinal)
                {
                    dt.Columns[columnName].SetOrdinal(ordinal);
                }
            }
            //return dt;
        }

        /// <summary>
        /// Returns true if the table name exist in the list of data table
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool ContainsTableName(this IEnumerable<DataTable> dts, string tableName)
        {
            foreach (DataTable dt in dts)
            {
                if (dt.TableName == tableName)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Returns the primary key value of a data row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="delimeter"></param>
        /// <param name="useOriginalRowStateIfAvailable"></param>
        /// <returns></returns>
        public static string GetPrimaryKeyValueListText(this DataRow row, char delimeter = ';', bool useOriginalRowStateIfAvailable = true)
        {

            DataTable dt = row.Table;
            if (dt == null)
            {
                throw new Exception("This row doesn't below to a table");
            }
            if (dt.PrimaryKey == null)
            {
                throw new Exception("This table doesn't have a primary key");
            }
            DataColumn[] columns = dt.PrimaryKey;
            string pkValue = GetValueListText(row, columns, useOriginalRowStateIfAvailable, delimeter);
            return pkValue;

        }


        /// <summary>
        /// Returns the column values of a data row seprated by delimeter
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columns"></param>
        /// <param name="useOriginalRowStateIfAvailable"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static string GetValueListText(this DataRow row, DataColumn[] columns, bool useOriginalRowStateIfAvailable = true, char delimeter = ';')
        {
            string pkValue = "";

            object fieldValue;
            string fieldValueAsString;
            foreach (DataColumn c in columns)
            {
                if (row.HasVersion(DataRowVersion.Original) & useOriginalRowStateIfAvailable)
                {
                    fieldValue = row[c.ColumnName, DataRowVersion.Original];
                }
                else
                {
                    fieldValue = row[c.ColumnName];
                }

                //if (formatDatesForSQL && c.DataType == typeof(DateTime) && fieldValue != null)
                //{
                //    if (DateTime.TryParse(fieldValue.ToString(), out dateValue))
                //    {
                //        if (dateValue.TimeOfDay.Ticks == 0)
                //        {
                //            fieldValueAsString = dateValue.ToString("yyyy-MM-dd");
                //        }
                //        else
                //        {
                //            fieldValueAsString = dateValue.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //        }
                //    }
                //    else
                //    {
                //        fieldValueAsString = fieldValue.ToString();
                //    }
                //}
                //else
                //{
                fieldValueAsString = fieldValue.ToString();
                //}
                pkValue += delimeter + fieldValueAsString;
            }
            pkValue = pkValue.RemoveBefore(1);
            return pkValue;
        }


        /// <summary>
        /// Returns an object array with data and structure of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static object[,] ToArray(this DataTable dt)
        {
            object[,] myArray = new object[dt.Rows.Count, dt.Columns.Count];
            int irow = 0;
            int icol = 0;
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    myArray[irow, icol] = row[col];
                    //if (myArray[irow,icol].GetType() = )
                    //                    {
                    //                      
                    //                }
                    icol = icol + 1;
                }
                irow = irow + 1;
                icol = 0;
            }
            return myArray;
        }
        //public static int GetBytes(this DataTable dt, int pageNum, int pageSize)


        /// <summary>
        /// Returns storage bytes value of a data table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetBytes(this DataTable dt)
        {
            int numberOfBytes = 0;
            using (DataTableReader reader = new DataTableReader(dt))
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + " ");

                        byte[] stringArray = Encoding.UTF8.GetBytes(reader[i].ToString());

                        numberOfBytes += stringArray.Length;

                        /*var test = reader.GetStream(i);
                        Stream stream = reader.GetStream(i);
                        int byteCtr = stream.ReadByte();
                        if (byteCtr != -1)
                        {
                            numberOfBytes++;
                        }*/

                    }
                    //Console.WriteLine();
                }


            }
            //string breakpoint = "";

            return numberOfBytes;
        }

    }


}
