using Xunit;
using System.Data;
using System.Collections.Generic;

namespace PrimitiveExtensions.Tests
{
    public class DataRowExtensionTests

    {
        [Fact]
        public void GetWhereClause_wrap_all()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            
            d.Add("first name", "'john'");
            d.Add("last", "'smith'");

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            config.NameWrappingPattern = IdentifierWrappingPattern.WrapAllObjectNames;

            string where =  DataRowExtensions.GetWhereClause(config, d);
            Assert.Equal("WHERE `first name` = 'john' AND `last` = 'smith'", where);
        }

        [Fact]
        public void GetWhereClause_wrap_all_prefix_columns()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("first name", "'john'");
            d.Add("last", "'smith'");

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            config.NameWrappingPattern = IdentifierWrappingPattern.WrapAllObjectNames;
            config.PrefixColumnNameWithTableName = true;

            string where = DataRowExtensions.GetWhereClause(config, d);
            Assert.Equal("WHERE `table`.`first name` = 'john' AND `table`.`last` = 'smith'", where);
        }


        [Fact]
        public void GetWhereClause_wrap_only_spaces()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("first name", "'john'");
            d.Add("last", "'smith'");

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            config.NameWrappingPattern = IdentifierWrappingPattern.WrapOnlyObjectNamesThatContainSpaces;

            string where = DataRowExtensions.GetWhereClause(config, d);
            Assert.Equal("WHERE `first name` = 'john' AND last = 'smith'", where);
        }

        [Fact]
        public void GetSetClause_wrap_all()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("first name", "'john'");
            d.Add("last", "'smith'");

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            config.NameWrappingPattern = IdentifierWrappingPattern.WrapAllObjectNames;

            string set = DataRowExtensions.GetSetClause(config, d);
            Assert.Equal("SET `first name` = 'john', `last` = 'smith'", set);
        }

        [Fact]
        public void GetStClause_wrap_all_prefix_columns()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("first name", "'john'");
            d.Add("last", "'smith'");

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            config.NameWrappingPattern = IdentifierWrappingPattern.WrapAllObjectNames;
            config.PrefixColumnNameWithTableName = true;

            string set = DataRowExtensions.GetSetClause(config, d);
            Assert.Equal("SET `table`.`first name` = 'john', `table`.`last` = 'smith'", set);
        }


        [Fact]
        public void GetSetClause_wrap_only_spaces()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("first name", "'john'");
            d.Add("last", "'smith'");

            SqlGeneratorConfig config = new SqlGeneratorConfig("table");
            config.NameWrappingPattern = IdentifierWrappingPattern.WrapOnlyObjectNamesThatContainSpaces;

            string set = DataRowExtensions.GetSetClause(config, d);
            Assert.Equal("SET `first name` = 'john', last = 'smith'", set);
        }
    }
}