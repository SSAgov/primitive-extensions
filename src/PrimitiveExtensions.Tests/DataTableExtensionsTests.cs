using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace PrimitiveExtensions.Tests
{
    public class DataTableExtensionsTests
    {

        #region GetPage

        [Fact]
        public void GetPage_Given100Records_10RecordsPerPage_GetPage2_ExpectItems11thru20()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 1; i <= 100; i++)
            {
                dt.Rows.Add(i);
                            }

            DataTable page1 = dt.GetPage(1, 10);
            DataTable page2 = dt.GetPage(2, 10);

            Assert.Equal(10, page1.Rows.Count);
            Assert.Equal("1", page1.Rows[0][0].ToString());


            Assert.Equal(10, page2.Rows.Count);
            Assert.Equal("11", page2.Rows[0][0].ToString());
        }


        [Fact]
        public void GetPage_GivenInvalidPageNum_ExpectException()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 1; i <= 100; i++)
            {
                dt.Rows.Add(i);
            }

            Assert.Throws<IndexOutOfRangeException>(() => dt.GetPage(-11, 10));
        }

        [Fact]
        public void GetPage_Given100Records_10RecordsPerPage_GetPage11_ExpectEmptyPage()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 1; i <= 100; i++)
            {
                dt.Rows.Add(i);
            }
            DataTable page11 = dt.GetPage(11, 10);
            Assert.Equal(0, page11.Rows.Count);
        }


        [Fact]
        public void GetPage_Given100Records_10RecordsPerPage_GetPage0_ExpectOutofRangeException()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 1; i <= 100; i++)
            {
                dt.Rows.Add(i);
            }
            
            Assert.Throws<IndexOutOfRangeException>(() => dt.GetPage(0, 10));
        }
        #endregion

        #region Key/Non Key Identification
        [Fact]
        public void SetBestGuessPrimaryKey_Given3Columns1ID_ExpectID()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Id", typeof(int));
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");

            dtA.Rows.Add(1, "Joe", "Smith");
            dtA.Rows.Add(2, "Beatrice", "Smith");

            bool result = dtA.SetBestGuessPrimaryKey();
            Assert.True(result);
            Assert.Equal("Id", dtA.PrimaryKey[0].ColumnName);
        }

        [Fact]
        public void SetBestGuessPrimaryKey_Given3Columns_AllKey_Expect3()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Id", typeof(int));
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");

            dtA.Rows.Add(1, "Beatrice", "Smith");
            dtA.Rows.Add(1, "Beatrice", "Smith II");
            dtA.Rows.Add(1, "Mike", "Smith II");
            dtA.Rows.Add(2, "Mike", "Smith II");

            bool result = dtA.SetBestGuessPrimaryKey();
            Assert.True(result);
            Assert.Equal(3, dtA.PrimaryKey.Count());
        }


        [Fact]
        public void SetBestGuessPrimaryKey_Given4Columns_Expect3()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Location");
            dtA.Columns.Add("Model");
            dtA.Columns.Add("Color Name");
            dtA.Columns.Add("Device");

            dtA.Rows.Add("Kitchen", "Sorny", "Black", "TV");
            dtA.Rows.Add("Kitchen", "Sorny", "Red", "TV");
            dtA.Rows.Add("Kitchen", "Sorny", "Red", "Radio");
            dtA.Rows.Add("Kitchen", "Panaphonics", "Black", "TV");
            dtA.Rows.Add("Bathroom", "Panaphonics", "Blue", "TV");

            bool result = dtA.SetBestGuessPrimaryKey();
            Assert.True(result);
            Assert.Equal(3, dtA.PrimaryKey.Count());
            
        }

        [Fact]
        public void SetBestGuessPrimaryKey_Given4Columns_Expect3ColumnKey()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Location");
            dtA.Columns.Add("Model");
            dtA.Columns.Add("Color Name");
            dtA.Columns.Add("Device");

            dtA.Rows.Add("Kitchen", "Sorny", "Black", "TV");
            dtA.Rows.Add("Kitchen", "Sorny", "Red", "TV");
            dtA.Rows.Add("Kitchen", "Sorny", "Red", "Radio");
            dtA.Rows.Add("Kitchen", "Panaphonics", "Black", "TV");
            dtA.Rows.Add("Bathroom", "Panaphonics", "Blue", "TV");

            bool result  = dtA.SetBestGuessPrimaryKey();
            Assert.True(result);
            Assert.NotEqual("Location", dtA.PrimaryKey[0].ColumnName);
            Assert.Equal(3,dtA.PrimaryKey.Count());
        }

        [Fact]
        public void SetBestGuessPrimaryKey_Given4Columns_ExpectNoKeyFound()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Location");
            dtA.Columns.Add("Model");
            dtA.Columns.Add("Color Name");
            dtA.Columns.Add("Device");

            dtA.Rows.Add("Kitchen", "Sorny", "Black", "TV");
            dtA.Rows.Add("Kitchen", "Sorny", "Red", "TV");
            dtA.Rows.Add("Kitchen", "Sorny", "Red", "Radio");
            dtA.Rows.Add("Kitchen", "Panaphonics", "Black", "TV");
            dtA.Rows.Add("Bathroom", "Panaphonics", "Blue", "TV");
            dtA.Rows.Add("Bathroom", "Panaphonics", "Blue", "TV");

            bool result = dtA.SetBestGuessPrimaryKey();
            Assert.False(result);
            Assert.Empty(dtA.PrimaryKey);
        }



        [Fact]
        public void SetBestGuessPrimaryKey_Given5Columns_AllKey_ExpectFirst2AndLast2()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("State Code");
            dtA.Columns.Add("Letter Code");
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Color Code");
            dtA.Columns.Add("The Number");

            dtA.Rows.Add("A", "B", "C", "D", "E");
            dtA.Rows.Add("A1", "B", "C", "D", "E");
            dtA.Rows.Add("A1", "B1", "C", "D", "E");
            dtA.Rows.Add("A", "B1", "C", "D", "E");
            dtA.Rows.Add("A", "B", "C", "D1", "E1");
            dtA.Rows.Add("A", "B1", "C", "D1", "E1");


            dtA.SetBestGuessPrimaryKey();
            Assert.Equal(3, dtA.PrimaryKey.Count());
           
        }


        [Fact]
        public void GetPrimaryKeyColumnNames_GivenNoPK_ExpectEmptyList()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Id", typeof(int));
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");

            var x = dtA.GetPrimaryKeyColumnNames();
            Assert.True(x.Count() == 0);
        }

        [Fact]
        public void GetPrimaryKeyColumnNames_GivenPK_ExpectThatColumn()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");
            dtA.PrimaryKey = new DataColumn[] { pkCol };


            var x = dtA.GetPrimaryKeyColumnNames();
            Assert.True(x.Count() == 1);
            Assert.Equal("Id", x.FirstOrDefault());
        }

        [Fact]
        public void GetPrimaryKeyColumnNames_GivenPKandRegcol_NotExpectRegColumn()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");
            dtA.PrimaryKey = new DataColumn[] { pkCol };


            var x = dtA.GetPrimaryKeyColumnNames();
            Assert.False(x.Contains("First Name"));
            Assert.False(x.Contains("Last Name"));

        }


        [Fact]
        public void GetPrimaryKeyColumnNames_Given1Pk_Expect1()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            dt.Columns.Add(c);
            dt.PrimaryKey = new DataColumn[] { c };

            IEnumerable<string> pks = dt.GetPrimaryKeyColumnNames();
            Assert.Single(pks);
        }


        [Fact]
        public void GetPrimaryKeyColumnNames_Given3Pk_Expect3()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.Columns.Add("F");
            dt.Columns.Add("G");
            dt.PrimaryKey = new DataColumn[] { c, d, e };

            IEnumerable<string> pks = dt.GetPrimaryKeyColumnNames();
            Assert.Equal(3, pks.Count());
            Assert.Contains("Idc", pks);
            Assert.Contains("Idd", pks);
            Assert.Contains("Ide", pks);
        }

        [Fact]
        public void GetNonPrimaryKeyColumnNames_Given3Pk2Nk_Expect2()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.Columns.Add("F");
            dt.Columns.Add("G");
            dt.PrimaryKey = new DataColumn[] { c, d, e };

            IEnumerable<string> nonPks = dt.GetNonPrimaryKeyColumnNames();
            Assert.Equal(2, nonPks.Count());
            Assert.Contains("F", nonPks);
            Assert.Contains("G", nonPks);

        }


        [Fact]
        public void GetPrimaryKeyColumn_Given3Pk_Expect3()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.Columns.Add("F");
            dt.Columns.Add("G");
            dt.PrimaryKey = new DataColumn[] { c, d, e };

            var x = dt.GetPrimaryKeyColumns();
            Assert.Equal(3, x.Count());

        }

        [Fact]
        public void GetPrimaryKeyColumn_Given3Pk_ExpectAll3()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.Columns.Add("F");
            dt.Columns.Add("G");
            dt.PrimaryKey = new DataColumn[] { c, d, e };

            var x = dt.GetPrimaryKeyColumns();

            Assert.Contains(c, x.ToList());
            Assert.Contains(d, x.ToList());
            Assert.Contains(e, x.ToList());
        }

        [Fact]
        public void GetPrimaryKeyColumn_Given2NonPk_NotExpect2()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            DataColumn f = new DataColumn("F");
            DataColumn g = new DataColumn("G");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);

            dt.PrimaryKey = new DataColumn[] { c, d, e };

            var x = dt.GetPrimaryKeyColumns();

            Assert.False(x.ToList().Contains(f));
            Assert.False(x.ToList().Contains(g));
        }

        [Fact]
        public void GetNonKeyColumnNames_GivenPKand3Columns_Expect3Columns()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            DataColumn firstNameColumn = new DataColumn("First Name");
            DataColumn lastNameColumn = new DataColumn("Last Name");
            DataColumn birthdateColumn = new DataColumn("Birthdate", typeof(DateTime));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add(firstNameColumn);
            dtA.Columns.Add(lastNameColumn);
            dtA.Columns.Add(birthdateColumn);
            UniqueConstraint uc = new UniqueConstraint("name", new DataColumn[] { firstNameColumn, lastNameColumn }, false);

            dtA.PrimaryKey = new DataColumn[] { pkCol };
            dtA.Constraints.Add(uc);

            var x = dtA.GetNonPrimaryKeyColumnNames();
            Assert.True(x.Count() == 3);
        }


        [Fact]
        public void GetNonKeyColumnNames_GivenPKand3Columns_ExpectAll3Columns()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            DataColumn firstNameColumn = new DataColumn("First Name");
            DataColumn lastNameColumn = new DataColumn("Last Name");
            DataColumn birthdateColumn = new DataColumn("Birthdate", typeof(DateTime));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add(firstNameColumn);
            dtA.Columns.Add(lastNameColumn);
            dtA.Columns.Add(birthdateColumn);
            UniqueConstraint uc = new UniqueConstraint("name", new DataColumn[] { firstNameColumn, lastNameColumn }, false);

            dtA.PrimaryKey = new DataColumn[] { pkCol };
            dtA.Constraints.Add(uc);

            var x = dtA.GetNonPrimaryKeyColumnNames();
         
            Assert.Contains("First Name", x);
            Assert.Contains("Last Name", x);
            Assert.Contains("Birthdate", x);
        }


        [Fact]
        public void GetNonKeyColumnNames_GivenPKand3Columns_NotExpectPk()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            DataColumn firstNameColumn = new DataColumn("First Name");
            DataColumn lastNameColumn = new DataColumn("Last Name");
            DataColumn birthdateColumn = new DataColumn("Birthdate", typeof(DateTime));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add(firstNameColumn);
            dtA.Columns.Add(lastNameColumn);
            dtA.Columns.Add(birthdateColumn);
            UniqueConstraint uc = new UniqueConstraint("name", new DataColumn[] { firstNameColumn, lastNameColumn }, false);

            dtA.PrimaryKey = new DataColumn[] { pkCol };
            dtA.Constraints.Add(uc);

            var x = dtA.GetNonPrimaryKeyColumnNames();

            Assert.False(x.Contains("Id"));
        }

        
        [Fact]
        public void GetNonPrimaryKeyColumnNames_GivenNoNonPK_ExpectEmptyList()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.PrimaryKey = new DataColumn[] { c, d, e };

            IEnumerable<string> nonPks = dt.GetNonPrimaryKeyColumnNames();
            Assert.Equal(0, nonPks.Count());
        }

        [Fact]
        public void GetNonPrimaryKeyColumnNames_Given3Pk2Nk_NotExpect3()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Idc");
            DataColumn d = new DataColumn("Idd");
            DataColumn e = new DataColumn("Ide");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.Columns.Add("F");
            dt.Columns.Add("G");
            dt.PrimaryKey = new DataColumn[] { c, d, e };

            IEnumerable<string> nonPks = dt.GetNonPrimaryKeyColumnNames();
            Assert.False(nonPks.Contains("Idc"));
            Assert.False(nonPks.Contains("Idd"));
            Assert.False(nonPks.Contains("Ide"));

        }

        [Fact]
        public void GetColumnNames_GivenN3Col_ExpectTotal3()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
           
            IEnumerable<string> collist = dt.GetColumnNames();
            Assert.Equal(3, collist.Count());
        }
        [Fact]
        public void GetColumnNames_GivenN3Col_ExpectAll3()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);

            IEnumerable<string> collist = dt.GetColumnNames();
            Assert.Contains("Id", collist);
            Assert.Contains("First Name", collist);
            Assert.Contains("Last Name", collist);
        }

        [Fact]
        public void GetNonIdentityColumnNames_Given3NonIdentityColumns_ExpectTotal3()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            DataColumn firstNameColumn = new DataColumn("First Name");
            DataColumn lastNameColumn = new DataColumn("Last Name");
            DataColumn birthdateColumn = new DataColumn("Birthdate", typeof(DateTime));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add(firstNameColumn);
            dtA.Columns.Add(lastNameColumn);
            dtA.Columns.Add(birthdateColumn);
            // UniqueConstraint uc = new UniqueConstraint("name", new DataColumn[] { firstNameColumn, lastNameColumn }, false);

            // dtA.PrimaryKey = new DataColumn[] { pkCol };
            // dtA.Constraints.Add(uc);

            IEnumerable<string> collist = dtA.GetNonIdentityColumnNames();
            Assert.True(collist.Count() == 3);
        }


        [Fact]
        public void GetNonIdentityColumnNames_Given3NonIdentityColumns_ExpectAll3()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            DataColumn firstNameColumn = new DataColumn("First Name");
            DataColumn lastNameColumn = new DataColumn("Last Name");
            DataColumn birthdateColumn = new DataColumn("Birthdate", typeof(DateTime));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add(firstNameColumn);
            dtA.Columns.Add(lastNameColumn);
            dtA.Columns.Add(birthdateColumn);
            // UniqueConstraint uc = new UniqueConstraint("name", new DataColumn[] { firstNameColumn, lastNameColumn }, false);

            // dtA.PrimaryKey = new DataColumn[] { pkCol };
            // dtA.Constraints.Add(uc);

            IEnumerable<string> collist = dtA.GetNonIdentityColumnNames();
            Assert.Contains("First Name", collist);
            Assert.Contains("Last Name", collist);
            Assert.Contains("Birthdate", collist);
        }

        [Fact]
        public void GetNonIdentityColumnNames_Given1IdentityColumns_NotExpect1()
        {
            DataTable dtA = new DataTable();
            DataColumn pkCol = new DataColumn("Id", typeof(int));
            DataColumn firstNameColumn = new DataColumn("First Name");
            DataColumn lastNameColumn = new DataColumn("Last Name");
            DataColumn birthdateColumn = new DataColumn("Birthdate", typeof(DateTime));
            pkCol.AutoIncrement = true;

            dtA.Columns.Add(pkCol);
            dtA.Columns.Add(firstNameColumn);
            dtA.Columns.Add(lastNameColumn);
            dtA.Columns.Add(birthdateColumn);
            // UniqueConstraint uc = new UniqueConstraint("name", new DataColumn[] { firstNameColumn, lastNameColumn }, false);

            // dtA.PrimaryKey = new DataColumn[] { pkCol };
            // dtA.Constraints.Add(uc);

            IEnumerable<string> collist = dtA.GetNonIdentityColumnNames();
            Assert.False(collist.Contains("Id"));
        }

        #endregion

        #region Set Key

        [Fact]
        public void SetPrimaryKeyColumnsToReadOnly_Given1Pk_ExpectPKReadOnly()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("ID");
            DataColumn d = new DataColumn("Fist Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.PrimaryKey = new DataColumn[] { c };

            dt.SetPrimaryKeyColumnsToReadOnly();

            Assert.True(c.ReadOnly);

        }
        [Fact]
        public void SetPrimaryKeyColumnsToReadOnly_Given2NonPk_NotExpect2Readonly()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("ID");
            DataColumn d = new DataColumn("Fist Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.PrimaryKey = new DataColumn[] { c };

            dt.SetPrimaryKeyColumnsToReadOnly();
            
            Assert.False(d.ReadOnly);
            Assert.False(e.ReadOnly);

        }


        #endregion

        #region get sql update 

        [Fact]
        public void GetInsertSql_OnlySpacesWrapped_Formatted_Given1Row_ExpectOnly1Insert()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();
            dt.Rows.Add(1, "John", "Doe");

            SqlGeneratorConfig c = new SqlGeneratorConfig("new table");
            c.FormatSqlText = true;

            string sql = dt.GetInsertSql(c).First();
            string expected = "INSERT INTO `new table` (Id, `First Name`, `Last Name`) \r\nVALUES ('1', 'John', 'Doe');";

            Assert.Equal(expected, sql);
        }


        [Fact]
        public void GetInsertSql_OnlySpacesWrapped_NotFormatted_Given1Row_ExpectOnly1Insert()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();
            dt.Rows.Add(1, "John", "Doe");

            SqlGeneratorConfig c = new SqlGeneratorConfig("new table");
            c.FormatSqlText = false;

            string sql = dt.GetInsertSql(c).First();
            string expected = "INSERT INTO `new table` (Id, `First Name`, `Last Name`) VALUES ('1', 'John', 'Doe');";

            Assert.Equal(expected, sql);
        }

        [Fact]
        public void GetInsertSql_AllWrapped_Formatted_Given1Row_ExpectOnly1Insert()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();
            dt.Rows.Add(1, "John", "Doe");

            SqlGeneratorConfig c = new SqlGeneratorConfig("new table");
            c.FormatSqlText = true;
            c.NameWrappingPattern = IdentifierWrappingPattern.WrapAllObjectNames;

            string sql = dt.GetInsertSql(c).First();
            string expected = "INSERT INTO `new table` (`Id`, `First Name`, `Last Name`) \r\nVALUES ('1', 'John', 'Doe');";

            Assert.Equal(expected, sql);
        }


        [Fact]
        public void GetInsertSql_AllQuoted_NotFormatted_Given1Row_ExpectOnly1Insert()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();
            dt.Rows.Add(1, "John", "Doe");

            SqlGeneratorConfig c = new SqlGeneratorConfig("new table");
            c.FormatSqlText = false;
            c.NameWrappingPattern = IdentifierWrappingPattern.WrapAllObjectNames;

            string sql = dt.GetInsertSql(c).First();
            string expected = "INSERT INTO `new table` (`Id`, `First Name`, `Last Name`) VALUES ('1', 'John', 'Doe');";

            Assert.Equal(expected, sql);
        }

        [Fact]
        public void GetInsertSql_Given3Row_Expect3insertsql()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "John", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");
            SqlGeneratorConfig config = new SqlGeneratorConfig("newtable","schema");
            
            var x = dt.GetInsertSql(config);

            Assert.Equal("INSERT INTO schema.newtable (Id, `First Name`, `Last Name`) VALUES ('1', 'John', 'Doe');", x.ElementAt(0));
            Assert.Equal("INSERT INTO schema.newtable (Id, `First Name`, `Last Name`) VALUES ('2', 'Mark', 'Smith');", x.ElementAt(1));
            Assert.Equal("INSERT INTO schema.newtable (Id, `First Name`, `Last Name`) VALUES ('3', 'Mary', 'Doe');", x.ElementAt(2));
          
        }

        [Fact]
        public void GetInsertSql_Given0Row_ExpectNoInsert()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();
            SqlGeneratorConfig config = new SqlGeneratorConfig("no table");
            var x = dt.GetInsertSql(config);

            Assert.Empty(x);
           
        }

        [Fact]
        public void GetInsertSqlByRowState_Given3Col_Expectsqlwith3col()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "Jhon", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");

            SqlGeneratorConfig config = new SqlGeneratorConfig("newtable", "schema");
            var x = dt.GetInsertSqlByRowState(config);

            Assert.Equal("INSERT INTO schema.newtable (Id, `First Name`, `Last Name`) VALUES ('1', 'Jhon', 'Doe');", x.ElementAt(0));
            Assert.Equal("INSERT INTO schema.newtable (Id, `First Name`, `Last Name`) VALUES ('2', 'Mark', 'Smith');", x.ElementAt(1));
            Assert.Equal("INSERT INTO schema.newtable (Id, `First Name`, `Last Name`) VALUES ('3', 'Mary', 'Doe');", x.ElementAt(2));


        }

        [Fact]
        public void GetDeleteSqlByRowState_Given3Row_Expect3deletesql()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "Jhon", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");            
            dt.Rows.Add(3, "Mary", "Doe");
            dt.AcceptChanges();

            dt.Rows[0].Delete();
            dt.Rows[1].Delete();
            dt.Rows[2].Delete();

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            var x = dt.GetDeleteSqlByRowState(config);

            Assert.Equal(3, x.Count());
        }


        [Fact]
        public void GetDeleteSqlByRowState_Given0Row_Expect0deletesql()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            var x = dt.GetDeleteSqlByRowState(config);


            Assert.Empty(x);
        }


        [Fact]
        public void GetDeleteSqlByRowState_Given3Row_Expect3Exactdeletesql()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "Jhon", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");
            dt.AcceptChanges();

            dt.Rows[0].Delete();
            dt.Rows[1].Delete();
            dt.Rows[2].Delete();

            SqlGeneratorConfig config = new SqlGeneratorConfig("dt","schema");
            config.PrefixColumnNameWithTableName = true;
            var x = dt.GetDeleteSqlByRowState(config);

            Assert.Equal("DELETE FROM schema.dt WHERE dt.Id = '1';", x.ElementAt(0));
            Assert.Equal("DELETE FROM schema.dt WHERE dt.Id = '2';", x.ElementAt(1));
            Assert.Equal("DELETE FROM schema.dt WHERE dt.Id = '3';", x.ElementAt(2));
        }

        [Fact]
        public void GetUpdateSqlByRowState_Given3row_Expect3sqlwithupdate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "John", "Doe");
            dt.Rows.Add(2, "Mark", "Mark");
            dt.Rows.Add(3, "Mary", "Doe");

            dt.AcceptChanges();

            dt.Rows[0]["Last Name"] = "Smith";
            dt.Rows[1]["Last Name"] = "Smith";
            dt.Rows[2]["Last Name"] = "Smith";

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            var x = dt.GetUpdateSqlByRowState(config);

            
            Assert.Equal(3, x.Count());
        }

        [Fact]
        public void GetUpdateSqlByRowState_Given0row_Expect0sql()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            var x = dt.GetUpdateSqlByRowState(config);
            Assert.Empty(x);
        }

        [Fact]
        public void GetUpdateSqlByRowState_Given3row_Expect3exactsql()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "John", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");


            dt.AcceptChanges();

            dt.Rows[0]["Last Name"] = "Smith";
            dt.Rows[1]["Last Name"] = "Smith";
            dt.Rows[2]["Id"] = 4;

            SqlGeneratorConfig config = new SqlGeneratorConfig("dt", "schema");
            config.PrefixColumnNameWithTableName = true;
            config.PrefixTableWithSchema = true;

            var x = dt.GetUpdateSqlByRowState(config);

            Assert.Equal("UPDATE schema.dt SET dt.Id = 1, dt.`First Name` = 'John', dt.`Last Name` = 'Smith' WHERE dt.Id = 1;", x.ElementAt(0));
            Assert.Equal("UPDATE schema.dt SET dt.Id = 2, dt.`First Name` = 'Mark', dt.`Last Name` = 'Smith' WHERE dt.Id = 2;", x.ElementAt(1));
            Assert.Equal("UPDATE schema.dt SET dt.Id = 4, dt.`First Name` = 'Mary', dt.`Last Name` = 'Doe' WHERE dt.Id = 3;", x.ElementAt(2));
        }
       
        #endregion

        #region others
        [Fact]
        public void ToHtmlTable_Given1Col_ExpectHTMLwith1col()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            dt.Columns.Add(c);
            
            dt.PrimaryKey = new DataColumn[] { c };

            string y = dt.ToHtmlTable();

            string x = "\t\t<table>\r\n\t\t\t<tr><td>Id</td>\t\t\t</tr>\t\t</table>\r\n";

            Assert.Equal(x, y);
            
        }

        [Fact]
        public void ToHtmlTable_Given3Col_ExpectHTMLwith3col()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);

            dt.PrimaryKey = new DataColumn[] { c };

            string y = dt.ToHtmlTable();

            string x = "\t\t<table>\r\n\t\t\t<tr><td>Id</td><td>First Name</td><td>Last Name</td>\t\t\t</tr>\t\t</table>\r\n";

            Assert.Equal(x, y);
            
        }


        [Fact]
        public void SimpleDataTableCopier_Given1Table3Row_ExpectAll3Rows()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.SetBestGuessPrimaryKey();

            dt.Rows.Add(1, "Jhon","Doe");
            dt.Rows.Add(2 ,"Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");

            DataTable dtcopy = new DataTable();
            DataColumn ccopy = new DataColumn("Id");
            DataColumn dcopy = new DataColumn("First Name");
            DataColumn ecopy = new DataColumn("Last Name");
            dtcopy.Columns.Add(ccopy);
            dtcopy.Columns.Add(dcopy);
            dtcopy.Columns.Add(ecopy);
            dtcopy.PrimaryKey = new DataColumn[] { ccopy };
            
            dt.SimpleDataTableCopier(dtcopy);
            
           Assert.Equal(dt.Rows.Count, dtcopy.Rows.Count);
        
        }

        [Fact]
        public void SimpleDataTableCopier_Given1Table3Row_ExpectExact3Rows()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.PrimaryKey = new DataColumn[] { c };

            dt.Rows.Add(1, "Jhon", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");

            DataTable dtcopy = new DataTable();
            
            dt.SimpleDataTableCopier(dtcopy);
           
            Assert.True(dt.Rows.Contains(1));
            Assert.True(dt.Rows.Contains(2));
            Assert.True(dt.Rows.Contains(3));

        }


        [Fact]
        public void SimpleDataTableCopier_Given1Table3Row_NotExpectExact4Row()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.PrimaryKey = new DataColumn[] { c };

            dt.Rows.Add(1, "Jhon", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");

            DataTable dtcopy = new DataTable();

            dt.SimpleDataTableCopier(dtcopy);
            
            Assert.False(dt.Rows.Contains(4));
         }

        [Fact]
        public void SimpleDataTableCopier_Given1Table3Col_Expect3col()
        {
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt.Columns.Add(c);
            dt.Columns.Add(d);
            dt.Columns.Add(e);
            dt.PrimaryKey = new DataColumn[] { c };

            dt.Rows.Add(1, "Jhon", "Doe");
            dt.Rows.Add(2, "Mark", "Smith");
            dt.Rows.Add(3, "Mary", "Doe");

            DataTable dtcopy = new DataTable();

            dt.SimpleDataTableCopier(dtcopy);
            
           DataRow source = dtcopy.Rows.Find(2);
           DataRow result = dtcopy.Rows.Find(2);

           Assert.Equal(source[0].ToString(), result[0].ToString());
           Assert.Equal(source[1].ToString(), result[1].ToString());
           Assert.Equal(source[2].ToString(), result[2].ToString());
        }


        [Fact]
        public void CopyTo_Given3Tablelist_Expect3Tablelist()
        {
            DataTable dt1 = new DataTable();
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.Rows.Add(1, "Jhon", "Doe");
            dt1.Rows.Add(2, "Mark", "Smith");
            dt1.Rows.Add(3, "Mary", "Doe");

            DataTable dt2 = new DataTable();

            DataTable dt3 = new DataTable();

            List<DataTable> source = new List<DataTable>();
            List<DataTable> copy = new List<DataTable>();

            source.Add(dt1);
            source.Add(dt2);
            source.Add(dt3);

            source.CopyTo(copy);

            Assert.Equal(source.Count, copy.Count);
        }

        [Fact]
        public void CopyTo_Given3Tablelist_Expect3SameTablelist()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.Rows.Add(1, "Jhon", "Doe");
            dt1.Rows.Add(2, "Mark", "Smith");
            dt1.Rows.Add(3, "Mary", "Doe");
            
            DataTable dt2 = new DataTable("dt2");

            DataTable dt3 = new DataTable("dt3");

            List<DataTable> source = new List<DataTable>();
            List<DataTable> copy = new List<DataTable>();

            source.Add(dt1);
            source.Add(dt2);
            source.Add(dt3);

            source.CopyTo(copy);

            Assert.Equal("dt1", copy.ElementAt(0).TableName);
            Assert.Equal("dt2", copy.ElementAt(1).TableName);
            Assert.Equal("dt3", copy.ElementAt(2).TableName);
        }

        [Fact]
        public void TryToSetPrimaryKeyBasedOnCommonNames_GivenTablewithID_ExpectIDPrimaryKey()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);

            //private method

            Assert.Equal(3, dt1.Columns.Count );
         
        }



        [Fact]
        public void GetSharedNonKeyColumnNames_GivenTwoTable_ExpectAllCommonNonKeyColumns()
        {
            DataTable dt1 = new DataTable("dt1");
            dt1.Columns.Add(new DataColumn("Id"));
            dt1.Columns.Add(new DataColumn("First Name"));
            dt1.Columns.Add(new DataColumn("Last Name"));
            dt1.Columns.Add(new DataColumn("Birth Date"));
            dt1.SetBestGuessPrimaryKey();

            DataTable dt2 = new DataTable("dt2");
            dt2.Columns.Add(new DataColumn("Id"));
            dt2.Columns.Add(new DataColumn("First Name"));
            dt2.Columns.Add(new DataColumn("Last Name"));
            dt2.Columns.Add(new DataColumn("Birth Date"));
            dt2.SetBestGuessPrimaryKey();

            List<DataTable> l = new List<DataTable>();
            l.Add(dt1);
            l.Add(dt2);

            var shared_nonKey = l.GetColumnNames_Shared_NonPrimaryKey();
            var shared_key = l.GetColumnNames_Shared_PrimaryKey();
            var notshared_nonKey = l.GetColumnNames_NotShared_NonPrimaryKey();
            var notshared_key = l.GetColumnNames_NotShared_NonPrimaryKey();

            Assert.Empty(notshared_key);
            Assert.Empty(notshared_nonKey);

            Assert.Single(shared_key);
            

            Assert.Equal(3, shared_nonKey.Count());
            Assert.Contains("First Name", shared_nonKey);
            Assert.Contains("Last Name", shared_nonKey);
            Assert.Contains("Birth Date", shared_nonKey);
            Assert.DoesNotContain("Id", shared_nonKey);
        }
        [Fact]
        public void GetSharedColumnNames_GivenTwoTable_ExpectAllCommonNonKeyColumns()
        {
            DataTable dt1 = new DataTable("dt1");
            dt1.Columns.Add(new DataColumn("Id"));
            dt1.Columns.Add(new DataColumn("First Name"));
            dt1.Columns.Add(new DataColumn("Last Name"));
            dt1.Columns.Add(new DataColumn("Birth Date"));
            dt1.SetBestGuessPrimaryKey();

            DataTable dt2 = new DataTable("dt2");
            dt2.Columns.Add(new DataColumn("Id"));
            dt2.Columns.Add(new DataColumn("First Name"));
            dt2.Columns.Add(new DataColumn("Last Name"));
            dt2.Columns.Add(new DataColumn("Birth Date"));
            dt2.SetBestGuessPrimaryKey();

            List<DataTable> l = new List<DataTable>();
            l.Add(dt1);
            l.Add(dt2);

            var shared_nonKey = l.GetColumnNames_Shared_NonPrimaryKey();
            var shared_key = l.GetColumnNames_Shared_PrimaryKey();
            var notshared_nonKey = l.GetColumnNames_NotShared_NonPrimaryKey();
            var notshared_key = l.GetColumnNames_NotShared_PrimaryKey();
            var shared = l.GetColumnNames_Shared_All();
            var notShared = l.GetColumnNames_NotShared_All();

            Assert.Equal(4, shared.Count());
            Assert.Empty(notShared);
            Assert.Empty(notshared_key);
            Assert.Empty(notshared_nonKey);

            Assert.Single(shared_key);


            Assert.Equal(3, shared_nonKey.Count());
            Assert.Contains("First Name", shared_nonKey);
            Assert.Contains("Last Name", shared_nonKey);
            Assert.Contains("Birth Date", shared_nonKey);
            Assert.DoesNotContain("Id", shared_nonKey);
        }

        [Fact]
        public void GetSharedNonKeyColumnNames_GivenTwoTable_NotExpectUnCommonColumns()
        {
            DataTable dt1 = new DataTable("dt1");
            dt1.Columns.Add(new DataColumn("Id"));
            dt1.Columns.Add(new DataColumn("First Name"));
            dt1.Columns.Add(new DataColumn("Last Name"));
            dt1.Columns.Add(new DataColumn("Birth Date"));
            dt1.SetBestGuessPrimaryKey();

            DataTable dt2 = new DataTable("dt2");
            dt2.Columns.Add(new DataColumn("Id"));
            dt2.Columns.Add(new DataColumn("First Name"));
            dt2.Columns.Add(new DataColumn("Last Name"));
            dt2.Columns.Add(new DataColumn("Birth Date"));
            dt2.SetBestGuessPrimaryKey();

            List<DataTable> l = new List<DataTable>();
            
            l.Add(dt1);
            l.Add(dt2);
            
            IEnumerable<string> result = l.GetColumnNames_Shared_NonPrimaryKey();

            Assert.False(result.Contains("Id"));            
            Assert.False(result.Contains("Birth Date1"));
            Assert.False(result.Contains("Birth Date2"));


        }


        [Fact]
        public void RenameColumnIfExists_GivenTable_ExpectColumnNameRenamed()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date1");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.RenameColumnIfExists("First Name", "Fname");

            Assert.True(dt1.Columns.Contains("Fname"));
            
        }

        [Fact]
        public void RenameColumnIfExists_GivenTable_NotExpectOldColumnName()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date1");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.RenameColumnIfExists("First Name", "Fname");

            Assert.False(dt1.Columns.Contains("First Name"));

        }


        [Fact]
        public void AddColumnIfNotExists_GivenTable_ExpectOneMoreCountColumn()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
           // DataColumn f = new DataColumn("Birth Date1");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
           // dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.AddColumnIfNotExists("Birth Date", typeof(DateTime));
            
            Assert.Equal(4, dt1.Columns.Count);

        }

        [Fact]
        public void AddColumnIfNotExists_GivenTable_ExpectExactColumn()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            DataColumn cc = dt1.AddColumnIfNotExists("Birth Date", typeof(DateTime));

            Assert.True(dt1.Columns.Contains("Birth Date"));

        }

        [Fact]
        public void AddColumnIfNotExists_GivenTablewithExistingCol_ExpectExactColumn()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.AddColumnIfNotExists("Birth Date", typeof(DateTime));

            Assert.True(dt1.Columns.Contains("Birth Date"));
            Assert.Equal(4, dt1.Columns.Count);

        }

        [Fact]
        public void DeleteColumnIfExists_GivenTable_ExpectOneLessCountColumn()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date1");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            string columnToDelete = "First Name";
            dt1.DeleteColumnIfExists(columnToDelete);

            Assert.Equal(3, dt1.Columns.Count);
            Assert.False(dt1.Columns.Contains(columnToDelete));

        }


        [Fact]
        public void DeleteAllColumnsButThese_GivenTable_ExpectLessColumnCount()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date1");

            DataColumn g = new DataColumn("Street Name");
            DataColumn h = new DataColumn("City");
            DataColumn i = new DataColumn("State");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.Columns.Add(g);
            dt1.Columns.Add(h);
            dt1.Columns.Add(i);
            

            String[] collist = new string[10];
            collist[0] = "Street Name";
            collist[1] = "City";
            collist[2] = "State";

            dt1.DeleteAllColumnsButThese(collist);

            Assert.Equal(3, dt1.Columns.Count);

        }

        [Fact]
        public void DeleteAllColumnsButThese_GivenTable_ExpectOnlyColumnsNotDeleted()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date1");

            DataColumn g = new DataColumn("Street Name");
            DataColumn h = new DataColumn("City");
            DataColumn i = new DataColumn("State");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.Columns.Add(g);
            dt1.Columns.Add(h);
            dt1.Columns.Add(i);
            dt1.PrimaryKey = new DataColumn[] { c };

            String[] collist = new string[10];
            collist[0] = "Street Name";
            collist[1] = "City";
            collist[2] = "State";

            dt1.DeleteAllColumnsButThese(collist);

            Assert.True(dt1.Columns.Contains("Street Name"));
            Assert.True(dt1.Columns.Contains("City"));
            Assert.True(dt1.Columns.Contains("State"));
            Assert.True(dt1.Columns.Contains("ID"));

        }

        [Fact]
        public void DeleteAllColumnsButThese_GivenTable_NotExpectColumnsDeleted()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date1");

            DataColumn g = new DataColumn("Street Name");
            DataColumn h = new DataColumn("City");
            DataColumn i = new DataColumn("State");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.Columns.Add(g);
            dt1.Columns.Add(h);
            dt1.Columns.Add(i);
            dt1.PrimaryKey = new DataColumn[] { c };

            String[] collist = new string[10];
            collist[0] = "Street Name";
            collist[1] = "City";
            collist[2] = "State";

            dt1.DeleteAllColumnsButThese(collist);

            Assert.False(dt1.Columns.Contains("First Name"));
            Assert.False(dt1.Columns.Contains("Last Name"));
            Assert.False(dt1.Columns.Contains("Birth Date1"));

        }


        [Fact]
        public void DeleteUnchangedRows_Given2RowChanges_Expect2Deleted()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Id", typeof(int));
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");

            dtA.Rows.Add(1, "Beatrice", "Smith");
            dtA.Rows.Add(2, "Beatrice", "Smith II");
            dtA.Rows.Add(3, "Mike", "Smith");
            dtA.Rows.Add(4, "Mike", "Smith II");

            dtA.AcceptChanges();

            dtA.Rows[0]["Last Name"] = "Smith I";
            dtA.Rows[2]["Last Name"] = "Smith I";

            dtA.DeleteUnchangedRows();

            Assert.Equal("Deleted", dtA.Rows[1].RowState.ToString());
           Assert.Equal("Deleted", dtA.Rows[3].RowState.ToString());
        }

        [Fact]
        public void DeleteUnchangedRows_Given2RowChanges_NotExpect2Deleted()
        {
            DataTable dtA = new DataTable();
            dtA.Columns.Add("Id", typeof(int));
            dtA.Columns.Add("First Name");
            dtA.Columns.Add("Last Name");

            dtA.Rows.Add(1, "Joe", "Smith");
            dtA.Rows.Add(2, "Beatrice", "Smath");
            dtA.Rows.Add(3, "Mike", "Smith");
            dtA.Rows.Add(4, "Don", "Smith II");

            dtA.AcceptChanges();

            dtA.Rows[0]["Last Name"] = "Smith I";
            dtA.Rows[2]["Last Name"] = "Smith I";

            Assert.Equal(4, dtA.Rows.Count);
            dtA.DeleteUnchangedRows();
            dtA.AcceptChanges();
            Assert.Equal(2,dtA.Rows.Count);
        }

        [Fact]
        public void SetColumnOrdinalIfExists_GivenTable_ExpectOrdinal()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.SetColumnOrdinalIfExists("Birth Date",2);

            Assert.Equal(2, f.Ordinal);

        }

        [Fact]
        public void SetColumnOrdinalIfExists_GivenTable_NotExpectOrdinal()
        {
            DataTable dt1 = new DataTable("dt1");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            DataColumn f = new DataColumn("Birth Date");

            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.Columns.Add(f);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.SetColumnOrdinalIfExists("Birth Date", 1);

            Assert.NotEqual(3, f.Ordinal);
            

        }


        [Fact]
        public void ContainsTableName_Given3Tablelist_ExpectTableName()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

         

            DataTable dt2 = new DataTable("Orders");

            DataTable dt3 = new DataTable("Inventory");

            List<DataTable> source = new List<DataTable>();
           
            source.Add(dt1);
            source.Add(dt2);
            source.Add(dt3);

            bool b = source.ContainsTableName("Orders");

            Assert.True(b);
        }

        [Fact]
        public void ContainsTableName_Given3Tablelist_NotExpectTableName()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };



            DataTable dt2 = new DataTable("Orders");

            DataTable dt3 = new DataTable("Inventory");

            List<DataTable> source = new List<DataTable>();

            source.Add(dt1);
            source.Add(dt2);
            source.Add(dt3);

            bool b = source.ContainsTableName("Products");

            Assert.False(b);
        }

        [Fact]
        public void GetPrimaryKeyValueListText_GivenRow_ExpectPkValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");
            
            String result = dt1.Rows[0].GetPrimaryKeyValueListText();
         
            Assert.Equal("1", result);
        }

        [Fact]
        public void GetPrimaryKeyValueListText_GivenRow_NotExpectOtherPkValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");

            String result = dt1.Rows[2].GetPrimaryKeyValueListText();

            Assert.NotEqual("1", result);
        }


        [Fact]
        public void GetValueListText_GivenRow_ExpectPkValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };
            
            DataColumn[] dc = { c, d, e };
           

            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");


            String result = dt1.Rows[2].GetValueListText(dc);

            Assert.Equal("3;Mike;Smith", result);
        }

        [Fact]
        public void GetValueListText_GivenRow_NotExpectOtherPkValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            DataColumn[] dc = { c, d, e };


            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");


            String result = dt1.Rows[0].GetValueListText(dc);

            Assert.NotEqual("2;Beatrice;Smith II", result);
        }

        [Fact]
        public void DataTableToArray_GivenDataTable_ExpectSameValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id", typeof(int));
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            DataColumn[] dc = { c, d, e };


            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");


            object[,] result = dt1.ToArray();

            Assert.Equal(1, result[0,0]);
            Assert.Equal("Beatrice", result[0,1]);
            Assert.Equal("Smith", result[0,2]);
        }

        [Fact]
        public void DataTableToArray_GivenDataTable_NotExpectOtherValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id", typeof(int));
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            DataColumn[] dc = { c, d, e };


            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");


            object[,] result = dt1.ToArray();

            Assert.NotEqual(3, result[0, 0]);
            Assert.NotEqual("Mike", result[0, 1]);
            Assert.NotEqual("", result[0, 2]);
        }


        [Fact]
        public void GetBytes_GivenTable_ExpectBytesValue()
        {
            DataTable dt1 = new DataTable("Customer");
            DataColumn c = new DataColumn("Id");
            DataColumn d = new DataColumn("First Name");
            DataColumn e = new DataColumn("Last Name");
            dt1.Columns.Add(c);
            dt1.Columns.Add(d);
            dt1.Columns.Add(e);
            dt1.PrimaryKey = new DataColumn[] { c };

            DataColumn[] dc = { c, d, e };


            dt1.Rows.Add(1, "Beatrice", "Smith");
            dt1.Rows.Add(2, "Beatrice", "Smith II");
            dt1.Rows.Add(3, "Mike", "Smith");
            dt1.Rows.Add(4, "Mike", "Smith II");


            int result = dt1.GetBytes();

            Assert.Equal(54, result);
        }
        #endregion
    }
}
