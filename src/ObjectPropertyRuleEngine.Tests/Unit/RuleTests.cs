using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests.Unit
{
    public class RuleTests
    {
        [Fact]
        public void Rule001_applies_pass()
        {
            Rule rule = TestData_Rules.Rule001();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Last Update Timestamp", "LU_TS", "timestamp");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule001_applies_fail()
        {
            Rule rule = TestData_Rules.Rule001();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Last Update Timestamp", "LU_TS", "char(8)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule001_na()
        {
            Rule rule = TestData_Rules.Rule001();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person", "PRSN", "char(9)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule019_applies_pass()
        {
            Rule rule = TestData_Rules.Rule019();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Employer Id number", "EMP_ID_NUM", "char(9)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule019_applies_fail()
        {
            Rule rule = TestData_Rules.Rule019();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Employer Id number", "EMP_ID_NUM", "char(99)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule019_na()
        {
            Rule rule = TestData_Rules.Rule019();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person", "PRSN", "char(9)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule024_applies_pass()
        {
            Rule rule = TestData_Rules.Rule024_text_types();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Employer Name", "EMP_ID_NUM", "char(9)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule024_applies_fail()
        {
            Rule rule = TestData_Rules.Rule024_text_types();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Employer Name", "EMP_ID_NUM", "date");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule024_na()
        {
            Rule rule = TestData_Rules.Rule024_text_types();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person Number", "PRSN", "char(9)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }



        [Fact]
        public void Rule038_applies_pass()
        {
            Rule rule = TestData_Rules.Rule038_AddresslineLength();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home Address Line", "ADDRLN", "char(22)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule038_applies_fail()
        {
            Rule rule = TestData_Rules.Rule038_AddresslineLength();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home Address Line", "ADDRLN", "char(32)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule038_na()
        {
            Rule rule = TestData_Rules.Rule038_AddresslineLength();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Top secret id", "TSOI", "char(5)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule047_applies_fail()
        {
            Rule rule = TestData_Rules.Rule047_parens_need_value();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home Address Line", "ADDRLN", "char()");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule047_na()
        {
            Rule rule = TestData_Rules.Rule047_parens_need_value();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Top secret id", "TSOI", "char(5)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule050_applies_pass()
        {
            Rule rule = TestData_Rules.Rule050_datatype_length_required();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home Address Line", "ADDRLN", "char(32)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule050_applies_fail()
        {
            Rule rule = TestData_Rules.Rule050_datatype_length_required();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home Address Line", "ADDRLN", "char");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule050_na()
        {
            Rule rule = TestData_Rules.Rule050_datatype_length_required();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Top secret id", "TSOI", "date");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule057_applies_pass()
        {
            Rule rule = TestData_Rules.Rule057_email();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person Email Address", "PRSN_EML_ADDR", "char(51)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule057_applies_fail()
        {
            Rule rule = TestData_Rules.Rule057_email();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person Email Address", "PRSN_EML_ADDR", "char(20)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule057_na()
        {
            Rule rule = TestData_Rules.Rule057_email();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Hamburger", "TSOI", "date");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        
        [Fact]
        public void Rule060_applies_fail()
        {
            Rule rule = TestData_Rules.Rule060_utc_gmt();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person Birth Greenwich Mean Time", "PRSN_BRTH_UTC", "date");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule060_na()
        {
            Rule rule = TestData_Rules.Rule060_utc_gmt();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Hamburger", "TSOI", "date");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule59_applies_fails()
        {
            Rule rule = TestData_Rules.Rule059_countryName();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home Country", "TSOI", "date");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule59_na()
        {
            Rule rule = TestData_Rules.Rule059_countryName();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Home State", "TSOI", "date");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule079_applies_pass()
        {
            Rule rule = TestData_Rules.Rule079_A_field_that_contains_TSO();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Top secret id", "TSOID", "char(5)");
            var result = rule.RunRuleAgainstObject(c);
            
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule079_applies_fail()
        {
            Rule rule = TestData_Rules.Rule079_A_field_that_contains_TSO();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Top secret id", "TSOID", "char(6)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule079_na()
        {
            Rule rule = TestData_Rules.Rule079_A_field_that_contains_TSO();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Top secret id", "TSOI", "char(5)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule085_applies_pass()
        {
            Rule rule = TestData_Rules.Rule085_switches_shouldntbe_null();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Alive Switch", "ALV_SW", "char(1)",false);
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule085_applies_fail()
        {
            Rule rule = TestData_Rules.Rule085_switches_shouldntbe_null();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Alive Switch", "ALV_SW", "char(1)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule085_na()
        {
            Rule rule = TestData_Rules.Rule085_switches_shouldntbe_null();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person Name", "TSOI", "char(5)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule089_applies_pass()
        {
            Rule rule = TestData_Rules.Rule089_Character_dates();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Application Character Date", "APP_CHARDT", "char(8)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule089_applies_fail()
        {
            Rule rule = TestData_Rules.Rule089_Character_dates();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Application Character Date", "APP_CHARDT", "char(6)");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule089_na()
        {
            Rule rule = TestData_Rules.Rule089_Character_dates();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Person Name", "TSOI", "char(5)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule091_applies_fail()
        {
            Rule rule = TestData_Rules.Rule091_long_codes();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Application Character Code", "APP_CHAR_CD", "char(6)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        

        [Fact]
        public void Rule123_applies_fails()
        {
            Rule rule = TestData_Rules.Rule123_ColumnsMustBeMoreThanAClassWord();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Date", "DT", "datetime");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule051_applies_fails()
        {
            Rule rule = TestData_Rules.Rule051_Attributes_Must_be_defined();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Date", "DT", "datetime");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }


   
        [Fact]
        public void Rule070_applies_pass()
        {
            Rule rule = TestData_Rules.Rule070_from_irs();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Internal Revenue Service Code", "IRS_CD", "char(5)", true,"This data comes from the irs");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.True(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule070_applies_fail()
        {
            Rule rule = TestData_Rules.Rule070_from_irs();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("The Code Code", "IRS_CD", "char(5)", true, "This data comes from the irs");
            var result = rule.RunRuleAgainstObject(c);
            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }
        [Fact]
        public void Rule070_na()
        {
            Rule rule = TestData_Rules.Rule070_from_irs();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Hamburger Code", "TSOI", "char(5)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.False(result.AntecedentEvaluatesToTrue);
            Assert.Null(result.ConsequentEvaluatesToTrue);
        }

        [Fact]
        public void Rule072_applies_fails()
        {
            Rule rule = TestData_Rules.Rule072_appointedrep_not_auth_rep();
            DataTable dt = new DataTable();
            var c = dt.AddNewDataColumnWithExtendedProperties("Authorized Representiative Name", "AUTH_RP_NM", "char(10)");
            var result = rule.RunRuleAgainstObject(c);

            Assert.True(result.AntecedentEvaluatesToTrue);
            Assert.False(result.ConsequentEvaluatesToTrue);
        }

       


        [Fact]
        public void RunRuleAgainstObject_Column_EndsWithTimeStamp()
        {
            Rule r = TestData_Rules.Columns_LogicalName_EndInTimeStamp_ExpectAppropriatePhysicalDatatype();

            DataColumn c = new DataColumn("TheColumn");
            c.ExtendedProperties["LogicalName"] = "Insert Time Stamp";
            c.ExtendedProperties["PhysicalDatatype"] = "Timestamp";

            RuleCheckResult ruleCheckResult = r.RunRuleAgainstObject(c);
            Assert.True(ruleCheckResult.HasError);
        }

        [Fact]
        public void GetYamlString()
        {
            Rule r = TestData_Rules.Columns_LogicalName_EndInTimeStamp_ExpectAppropriatePhysicalDatatype();
            string yamlString = r.GetYamlString();

            Assert.Equal(YamlRuleString , yamlString);
        }
        private const string YamlRuleString = @"ID: 2
Description: Expect columns that end in Timestamp or Time Stamp to be a datetime or related datatype
AppliesToTypeName: System.Data.DataColumn
Antecedent:
  LogicOperator: Or
  RuleExpressions:
  - PropertyName: LogicalName
    ComparisonCondition: Ends with
    ExpressionValue: Timestamp
    CaseSensitive: false
  - PropertyName: LogicalName
    ComparisonCondition: Ends with
    ExpressionValue: Time Stamp
    CaseSensitive: false
  - PropertyName: Caption
    ComparisonCondition: Ends with
    ExpressionValue: Time Stamp
    CaseSensitive: false
  RuleExpressionGroups: []
Consequent:
  LogicOperator: And
  RuleExpressions:
  - PropertyName: Datatype
    ComparisonCondition: Is
    ExpressionValue: Timestamp
    CaseSensitive: false
  RuleExpressionGroups: []
OutcomeLevel: Expected
";
    }
    
}
