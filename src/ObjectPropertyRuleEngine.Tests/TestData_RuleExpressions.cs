using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests
{
    public static class TestData_RuleExpressions
    {
        public static RuleExpression PhysicalName_Equals_PRSN()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "PhysicalName";
            expression.ComparisonCondition = "=";
            expression.ExpressionValue = "PRSN";
            return expression;
        }

        internal static RuleExpression Table_Columns_Count_Equals_1()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "Columns.Count";
            expression.ComparisonCondition = "Equals";
            expression.ExpressionValue = "1";
            return expression;
        }

        public static RuleExpression LogicalName_EndsWith_Timestamp()
        {
            RuleExpression expression = new RuleExpression(
                "LogicalName",
                ComparisonConditionEnum.EndsWith,
                "Timestamp");
            return expression;
        }

        public static RuleExpression PhysicalName_Length_LessThan_19()
        {
            RuleExpression expression = new RuleExpression(
                "PhysicalName.Length",
                ComparisonConditionEnum.LessThan,
                19                );
            return expression;
        }

        public static RuleExpression LogicalName_EndsWith_Time_Stamp()
        {
            RuleExpression expression = new RuleExpression(
                "LogicalName", ComparisonConditionEnum.EndsWith, "Time Stamp"
                );

            return expression;
        }
        public static RuleExpression LogicalName_EndsWith_Term(string term)
        {
            RuleExpression expression = new RuleExpression(
                "LogicalName", ComparisonConditionEnum.EndsWith, term
                );

            return expression;
        }

        public static RuleExpression PhysicalName_EndsWith_Term(string term)
        {
            RuleExpression expression = new RuleExpression(
                "PhysicalName", ComparisonConditionEnum.EndsWith, term
                );

            return expression;
        }

        public static RuleExpression Caption_EndsWith_Time_Stamp()
        {
            RuleExpression expression = new RuleExpression(
                "Caption", ComparisonConditionEnum.EndsWith, "Time Stamp"
                );

            return expression;
        }

        public static RuleExpression Physical_Datatype_Is_Timestamp()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "Datatype";
            expression.ComparisonCondition = "Is";
            expression.ExpressionValue = "Timestamp";
            return expression;
        }

        public static RuleExpression ColumnName_EqualTo_HOFC()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "ColumnName";
            expression.ComparisonCondition = "is";
            expression.ExpressionValue = "HOFC";
            return expression;
        }
        public static RuleExpression PhysicalName_EqualTo_HOFC()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "PhysicalName";
            expression.ComparisonCondition = "equals";
            expression.ExpressionValue = "HOFC";
            return expression;
        }
        public static RuleExpression PhysicalName_NotEqual_HOFC()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "PhysicalName";
            expression.ComparisonCondition = "Notequal";
            expression.ExpressionValue = "HOFC";
            return expression;
        }

        public static RuleExpression LogicalName_EndsWithAny_CLASSWORDS()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "LogicalName";
            expression.ComparisonCondition = "EndsWithAny";
            expression.ExpressionValue = "File;Line 4;Line 3;Line 2;Line 1;Line;Code;Address;Year;Timestamp;Time;Text;Switch;Rate;Percent;Number;Name;Indicator;Identifier;Description;Date;Count;Amount";
            return expression;
        }

        public static RuleExpression LogicalName_EqualsAny_CLASSWORDS()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "LogicalName";
            expression.ComparisonCondition = "EqualsAny";
            expression.ExpressionValue = "File;Line 4;Line 3;Line 2;Line 1;Line;Code;Address;Year;Timestamp;Time;Text;Switch;Rate;Percent;Number;Name;Indicator;Identifier;Description;Date;Count;Amount";
            return expression;
        }
        public static RuleExpression LogicalName_IsAny_CLASSWORDS()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "LogicalName";
            expression.ComparisonCondition = "EqualToAny";
            expression.ExpressionValue = "File;Line 4;Line 3;Line 2;Line 1;Line;Code;Address;Year;Timestamp;Time;Text;Switch;Rate;Percent;Number;Name;Indicator;Identifier;Description;Date;Count;Amount";
            return expression;
        }

        public static RuleExpression IsNullable()
        {
            RuleExpression expression = new RuleExpression();
            expression.PropertyName = "Nullable";
            expression.ComparisonCondition = "EqualTo";
            expression.ExpressionValue = true;
            return expression;
        }


    }

}
