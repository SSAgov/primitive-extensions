using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;


namespace PrimitiveExtensions
{
    public class SqlGeneratorConfig
    {
        public SqlGeneratorConfig(string tableName, string schemaName = "")
        {
            if (tableName.IsNullOrEmptyString())
            {
                throw new ArgumentNullException("Table name can't be null or empty!");
            }

            NameWrapperCharacter = '`';
            NameWrappingPattern = IdentifierWrappingPattern.WrapOnlyObjectNamesThatContainSpaces;
            FormatSqlText = false;
            PrefixTableWithSchema = schemaName.IsNotNullOrEmptyString();
            SchemaName = schemaName;
            TableName = tableName;
            
            PrefixColumnNameWithTableName = false;
        }

        public string SchemaName { get; set; }

        /// <summary>
        /// The name of the target table to be used in SQL Generation
        /// </summary>
        public string TableName { get; set; }

        public string GetFormattedColumnIdentifier(string columnName)
        {
            switch (NameWrappingPattern)
            {

                case IdentifierWrappingPattern.WrapAllObjectNames:
                    if (PrefixColumnNameWithTableName)
                        return $"{TableName.Wrap(NameWrapperCharacter)}.{columnName.Wrap(NameWrapperCharacter)}";

                    else
                        return $"{columnName.Wrap(NameWrapperCharacter)}";

                default:
                case IdentifierWrappingPattern.WrapOnlyObjectNamesThatContainSpaces:
                    if (PrefixColumnNameWithTableName)
                        return $"{(TableName.Contains(" ") ? TableName.Wrap(NameWrapperCharacter) : TableName)}.{(columnName.Contains(" ") ? columnName.Wrap(NameWrapperCharacter) : columnName)}";

                    else
                        return $"{(columnName.Contains(" ") ? columnName.Wrap(NameWrapperCharacter) : columnName)}";
            }
        }
        public string FormattedTableIdentifier
        {
            get
            {
                bool wrap = false;
                switch (NameWrappingPattern)
                {
                    case IdentifierWrappingPattern.WrapAllObjectNames:
                        wrap = true;
                        break;

                    case IdentifierWrappingPattern.WrapOnlyObjectNamesThatContainSpaces:
                    default:
                        wrap = TableName.Contains(" ");
                        break;
                }

                if (PrefixTableWithSchema && SchemaName.IsNotNullOrEmptyString())
                {
                    if (wrap)
                        return $"{NameWrapperCharacter}{SchemaName}{NameWrapperCharacter}.{NameWrapperCharacter}{TableName}{NameWrapperCharacter}";
                    else
                        return $"{SchemaName}.{TableName}";
                }
                else
                {
                    if (wrap)
                        return NameWrapperCharacter+TableName+NameWrapperCharacter;
                    else
                        return TableName;
                }
            }
        }

        public bool PrefixTableWithSchema { get; set; }
        public bool PrefixColumnNameWithTableName { get; set; }
        public char NameWrapperCharacter { get; set; }
        public bool FormatSqlText { get; set; }
        
                
        /// <summary>
        /// Defaults to WrapOnlyTableNamesThatContainSpaces
        /// </summary>
        public IdentifierWrappingPattern  NameWrappingPattern{ get; set; }
        
    }

    public enum IdentifierWrappingPattern
    {
        WrapOnlyObjectNamesThatContainSpaces,
        WrapAllObjectNames
    }

}
