using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimitiveExtensions;

namespace ObjectPropertyRuleEngine
{
    public static class SystemDataExtensions
    {
        public static DataColumn AddNewDataColumnWithExtendedProperties(this DataTable dt, string logicalName, string physicalName, string datatype, bool allowDbNull = true, string definition = "")
        {
            string datatypeName = datatype.RemoveFirstAndAfter("(");
            int datatypeLength = datatype.ExtractNumberFromString();
            

            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = physicalName;
            dataColumn.ExtendedProperties.Add("PhysicalName", physicalName);

            dataColumn.Caption = logicalName;
            dataColumn.ExtendedProperties.Add("LogicalName", logicalName);
            
            dataColumn.AllowDBNull = allowDbNull;
            dataColumn.ExtendedProperties.Add("Nullable", allowDbNull);

            dataColumn.ExtendedProperties.Add("Definition", definition);

            dataColumn.ExtendedProperties.Add("DatatypeLength", datatypeLength);
            dataColumn.ExtendedProperties.Add("Datatype", datatype);
            dataColumn.ExtendedProperties.Add("DatatypeName", datatypeName);
            switch (datatypeName.ToLower())
            {
                case "decimal":
                    dataColumn.DataType = typeof(decimal);
                    break;

                case "int":
                case "integer":
                    dataColumn.DataType = typeof(int);
                    break;

                case "date":
                case "datetime":
                case "datetime2":
                case "time":
                case "timestamp":
                    dataColumn.DataType = typeof(DateTime);
                    break;
                default:
                    dataColumn.DataType = typeof(string);
                    dataColumn.MaxLength = datatypeLength;
                    break;
            }
            dt.Columns.Add(dataColumn);
            return dataColumn;
        }

        public static DataTable AddNewDataTableWithExtendedProperties(this DataSet ds, string logicalName, string physicalName, string definition)
        {

            DataTable dataTable = new DataTable();
            dataTable.TableName = physicalName;
            dataTable.ExtendedProperties.Add("LogicalName", logicalName);
            dataTable.ExtendedProperties.Add("PhysicalName", physicalName);
            ds.Tables.Add(dataTable);
            return dataTable;
        }

        public static HashSet<DataColumn> GetAllDataColumns(this DataSet ds)
        {
            HashSet<DataColumn> columns = new HashSet<DataColumn>();
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column);
                }
            }
            return columns;
        }

        //public static RuleCheckResult EvaluateAgainstRule(this DataColumn o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}

        //public static RuleCheckResult EvaluateAgainstRule(this DataTable o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}

        //public static RuleCheckResult EvaluateAgainstRule(this Constraint o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}

        //public static RuleCheckResult EvaluateAgainstRule(this DataRelation o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}

        //public static RuleCheckResult EvaluateAgainstRule(this DataView o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}
        //public static RuleCheckResult EvaluateAgainstRule(this ForeignKeyConstraint o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}

        //public static RuleCheckResult EvaluateAgainstRule(this DataSet o, Rule rule)
        //{
        //    return rule.RunRuleAgainstObject(o);
        //}
    }
}
