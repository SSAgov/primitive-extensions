using System.Data;
using Xunit;

namespace SSAx.PrimitiveExtensions.Tests
{
    public class DataColumnExtensionsTests
    {
        [Fact]
        public void IsAPrimaryKeyColumn_GivenTableWithSingleColumnIntKey_ExpectTrue()
        {
            DataTable dtA = new DataTable();
            DataColumn c = new DataColumn("Id", typeof(int));
            dtA.Columns.Add(c);
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");
            dtA.PrimaryKey = new DataColumn[] { c };

            bool result = c.IsAPrimaryKeyColumn(dtA);

            Assert.True(result);
        }

        [Fact]
        public void IsAPrimaryKeyColumn_GiveNonPrimaryKey_ExpectFalse()
        {
            DataTable dtA = new DataTable();
            DataColumn a = new DataColumn("First Name");
            DataColumn b = new DataColumn("Last Name");
            DataColumn c = new DataColumn("Id", typeof(int));
            dtA.Columns.Add(c);
            dtA.Columns.Add(b);
            dtA.Columns.Add(a);
            dtA.PrimaryKey = new DataColumn[] { c };

            bool result = a.IsAPrimaryKeyColumn(dtA);

            Assert.False(result);
        }

        [Fact]
        public void IsAPrimaryKeyColumnKey_GivenColwithNonKey_ExpectFalse()
        {
            DataTable dtA = new DataTable();
            DataColumn c = new DataColumn("Id", typeof(int));
            dtA.Columns.Add("Id", typeof(int));
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");

            Assert.False(c.IsAPrimaryKeyColumn(dtA));
        }

        [Fact]
        public void IsInUniqueConstraint_GivenConstrainColumn_ExpectTrue()
        {
            DataTable dtA = new DataTable();
            DataColumn colId = new DataColumn("Id", typeof(int));
            DataColumn colLastName = new DataColumn("LastName",typeof(string));
            dtA.Columns.Add(colId);
            dtA.Columns.Add("First Name");
            dtA.Columns.Add(colLastName);
            dtA.PrimaryKey = new DataColumn[] { colId };

            dtA.Constraints.Add("alternateKey_names", new DataColumn[] { colLastName }, false);

            

            Assert.True(colLastName.IsInAUniqueConstraint(dtA));
        }

        [Fact]
        public void IsInUniqueConstraint_GivenNonConstraintColumn_ExpectFalse()
        {
            DataTable dtA = new DataTable();
            DataColumn colId = new DataColumn("Id", typeof(int));
            DataColumn colLastName = new DataColumn("LastName", typeof(string));
            DataColumn colFirstName = new DataColumn("FirstName", typeof(string));
            dtA.Columns.Add(colId);
            dtA.Columns.Add(colFirstName);
            dtA.Columns.Add(colLastName);
            dtA.PrimaryKey = new DataColumn[] { colId };

            dtA.Constraints.Add("alternateKey_names", new DataColumn[] { colLastName }, false);

            Assert.False(colFirstName.IsInAUniqueConstraint(dtA));
        }
    }
}
