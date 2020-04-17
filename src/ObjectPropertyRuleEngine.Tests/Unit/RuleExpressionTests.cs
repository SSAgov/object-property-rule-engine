using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests.Unit
{
    public class RuleExpressionTests
    {
        [Fact]
        public void Evaluate_Column_Table_DataColumn_count_Equals_1()
        {
            RuleExpression expression = TestData_RuleExpressions.Table_Columns_Count_Equals_1();
            DataTable dt = new DataTable();
            DataColumn c = new DataColumn();
            c.ColumnName = "HOFC";
            dt.Columns.Add(c);

            Assert.True(expression.Evaluate(dt));
        }
        
        [Fact]
        public void Evaluate_Column_ColumnName_Equals_HOFC()
        {
            RuleExpression expression = TestData_RuleExpressions.ColumnName_EqualTo_HOFC();

            DataColumn c = new DataColumn();
            c.ColumnName = "HOFC";

            Assert.True(expression.Evaluate(c));
        }

        [Fact]
        public void Evaluate_Column_PhysicalNameLength_LessThan_19()
        {
            RuleExpression expression = TestData_RuleExpressions.PhysicalName_Length_LessThan_19();

            DataColumn c = new DataColumn();
            c.ExtendedProperties.Add("PhysicalName", "HOFC_HOFC_HOFC_HOFC_LONGGGGGGGGGGGGGNAME");
            Assert.False(expression.Evaluate(c));
        }

        [Fact]
        public void Evaluate_Column_UsingExtendedProperties_Physical_Name_Equals_HOFC()
        {
            RuleExpression expression = TestData_RuleExpressions.PhysicalName_EqualTo_HOFC();

            DataColumn c = new DataColumn();
            c.ExtendedProperties.Add("PhysicalName", "HOFC");

            Assert.True(expression.Evaluate(c));
        }

        [Fact]
        public void Evaluate_Column_UsingExtendedProperties_PhysicalName_DoesNotEqual_HOFC()
        {
            RuleExpression expression = TestData_RuleExpressions.PhysicalName_NotEqual_HOFC();

            DataColumn c = new DataColumn();
            
            c.ExtendedProperties.Add("PhysicalName", "blah blah");

            Assert.True(expression.Evaluate(c));
        }

        [Fact]
        public void Evaluate_Column_UsingExtendedProperties_PhysicalName_EndsWith_Timestamp()
        {
            DataColumn c = new DataColumn();
            c.ExtendedProperties.Add("LogicalName", "Last Update Timestamp");
            RuleExpression expression = TestData_RuleExpressions.LogicalName_EndsWith_Timestamp();
            Assert.True(expression.Evaluate(c));
        }

        [Fact]
        public void Evaluate_Column_UsingExtendedProperties_LogicallName_EndsWith_ClassWord()
        {
            RuleExpression expression = TestData_RuleExpressions.LogicalName_EndsWithAny_CLASSWORDS();

            DataColumn c = new DataColumn();
            c.ExtendedProperties.Add("LogicalName", "Last Update Timestamp");

            Assert.True(expression.Evaluate(c));
        }

        [Fact]
        public void Evaluate_Column_UsingExtendedProperties_NotNull()
        {
            RuleExpression expression = TestData_RuleExpressions.IsNullable();

            DataTable dt = new DataTable();
           var c = dt.AddNewDataColumnWithExtendedProperties("Person", "PRSN", "char(1)");

            Assert.True(expression.Evaluate(c));
        }


        [Fact]
        public void Evaluate_Table_UsingExtendedProperties_PhysicalName_Is_PRSN()
        {
            RuleExpression expression = TestData_RuleExpressions.PhysicalName_Equals_PRSN();
            
            DataTable t = new DataTable();
            t.ExtendedProperties.Add("PhysicalName", "PRSN");

            Assert.True(expression.Evaluate(t));
        }

        [Fact]
        public void GetSentence_Column_ColumnName_Equals_HOFC()
        {
            RuleExpression expression = TestData_RuleExpressions.ColumnName_EqualTo_HOFC();
            Assert.Equal("[ColumnName][is][HOFC]", expression.GetSentence());
        }
    }
}
