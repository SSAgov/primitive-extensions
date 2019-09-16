using System.Data;

namespace SSAx.PrimitiveExtensions
{
    public static class DbColumnExtensions
    {
        /// <summary>
        /// Returns true if the column is primary key column
        /// </summary>
        /// <param name="c"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsAPrimaryKeyColumn(this DataColumn c, DataTable dt)
        {
            if (dt.PrimaryKey != null)
            {
                foreach (DataColumn pk in dt.PrimaryKey)
                {
                    if (pk.ColumnName == c.ColumnName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns true if the column has unique constraint
        /// </summary>
        /// <param name="c"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsInAUniqueConstraint(this DataColumn c, DataTable dt)
        {
            foreach (UniqueConstraint con in dt.Constraints)
            {
                foreach (DataColumn akColumn in con.Columns)
                {
                    if (akColumn.ColumnName == c.ColumnName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
