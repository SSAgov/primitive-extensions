using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SSAx.PrimitiveExtensions
{
    public static class DataRowExtensions
    {
        public static string GetInsertSql(this DataRow row, SqlGeneratorConfig config)
        {
            Dictionary<string, string> columnsAndvalues = new Dictionary<string, string>();
            
            foreach (DataColumn column in row.Table.Columns)
            {
                bool wrap = false;
                switch (config.NameWrappingPattern)
                {
                    case IdentifierWrappingPattern.WrapAllObjectNames:
                        wrap = true;
                        break;

                    case IdentifierWrappingPattern.WrapOnlyObjectNamesThatContainSpaces:
                    default:
                        wrap = column.ColumnName.Contains(" ");
                        break;
                }

                if (wrap)
                    columnsAndvalues.Add(column.ColumnName.Wrap(config.NameWrapperCharacter), row[column.ColumnName].EncodeAsSqlVariable());
                else
                    columnsAndvalues.Add(column.ColumnName, row[column.ColumnName].EncodeAsSqlVariable());
            }

            string sql = "";
            sql = $"INSERT INTO {config.FormattedTableIdentifier} ({string.Join(", ", columnsAndvalues.Select(a => a.Key))}) ";

            if (config.FormatSqlText) sql += Environment.NewLine;
            sql += $"VALUES ({string.Join(", ", columnsAndvalues.Select(a => a.Value))});";
            return sql;
        }

        public static string GetUpdateSql(this DataRow r, SqlGeneratorConfig config)
        {
            Dictionary<string, string> pkColumnNamesAndSqlEncodedValues = new Dictionary<string, string>();
            Dictionary<string, string> nonPkColumnNamesAndSqlEncodedValues = new Dictionary<string, string>();

            foreach (string columnName in r.Table.GetPrimaryKeyColumnNames())
            {
                pkColumnNamesAndSqlEncodedValues.Add(columnName, r[columnName, DataRowVersion.Original].EncodeAsSqlVariable());
            }

            foreach (string columnName in r.Table.GetNonIdentityColumnNames())
            {
                nonPkColumnNamesAndSqlEncodedValues.Add(columnName, r[columnName].EncodeAsSqlVariable());
            }


            string sql ="";
            sql = $"UPDATE {config.FormattedTableIdentifier}";

            if (config.FormatSqlText)
                sql += Environment.NewLine;
            else
                sql += " ";

            sql += GetSetClause(config, nonPkColumnNamesAndSqlEncodedValues);
            if (config.FormatSqlText)
                sql += Environment.NewLine;
            else
                sql += " ";
            sql += GetWhereClause(config, pkColumnNamesAndSqlEncodedValues);
            sql += ";";
            return sql;
        }

        public static string GetDeleteSql(this DataRow r, SqlGeneratorConfig config)
        {
            //IList<string> pkColumnNames = dt.GetPrimaryKeyColumnNames().ToList();

            //List<string> valueList = new List<string>();
            Dictionary<string, string> columnNamesAndSqlFormattedValues = new Dictionary<string, string>();

            foreach (string columnName in r.Table.GetPrimaryKeyColumnNames())
                columnNamesAndSqlFormattedValues.Add(columnName, r[columnName, DataRowVersion.Original].EncodeAsSqlVariable());

            string sql = "";
            sql = $"DELETE FROM {config.FormattedTableIdentifier}";
            if (config.FormatSqlText)
                sql += Environment.NewLine;
            else
                sql += " ";

            string whereClause = GetWhereClause(config,columnNamesAndSqlFormattedValues);
            sql += whereClause + ";";

            return sql;
        }


        /// <summary>
        /// Generates a SQL WHERE clause based on the dictionary passed
        /// </summary>
        /// <param name="config"></param>
        /// <param name="columnNamessAndSqlFormattedValues"></param>
        /// <returns></returns>
        public static string GetWhereClause(SqlGeneratorConfig config, Dictionary<string,string> columnNamessAndSqlFormattedValues)
        {
            return "WHERE " + string.Join(" AND ", columnNamessAndSqlFormattedValues.Select(a => $"{config.GetFormattedColumnIdentifier(a.Key)} = {a.Value}"));
        }

        /// <summary>
        /// Generates a SQL SET clause based on the dictionary passed
        /// </summary>
        /// <param name="config"></param>
        /// <param name="columnNamessAndSqlFormattedValues"></param>
        /// <returns></returns>
        public static string GetSetClause(SqlGeneratorConfig config, Dictionary<string,string> columnNamessAndSqlFormattedValues)
        {
            if (config.FormatSqlText)
                return "SET " + string.Join(", ", columnNamessAndSqlFormattedValues.Select(a => $"{config.GetFormattedColumnIdentifier(a.Key)} = {a.Value}")) + Environment.NewLine + "\t";

            else
                return "SET " + string.Join(", ", columnNamessAndSqlFormattedValues.Select(a => $"{config.GetFormattedColumnIdentifier(a.Key)} = {a.Value}"));
        }
    }
}
