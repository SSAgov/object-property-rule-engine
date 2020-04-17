using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace ObjectPropertyRuleEngine.Tests.Unit
{
    public class RuleSetTests
    {
        [Fact]
        public void RunRuleAgainstObject_Column_ClassWord()
        {
            RuleEngine e = new RuleEngine();
            e.RuleSet =  TestData.RuleSetWith3Rules();
            DataTable dt = new DataTable();
            string logicalName = "Hearing Office Donkey Monkey";
            string physicalName = "HOFC";
            var col =  dt.AddNewDataColumnWithExtendedProperties(logicalName,physicalName, "char(10)");

            var ruleResult = e.RunRuleSetAgainstObject(col);

            Assert.Single(ruleResult.RuleCheckResults_Failed);
        }

        [Fact]
        public void RunRuleAgainstObject_Column_HyphenNeeded()
        {
            RuleEngine e = new RuleEngine();
            e.RuleSet =  TestData.RuleSetWith3Rules();
    
            DataTable dt = new DataTable();
            string logicalName = "Hearing Office";
            string physicalName = "HRG_OFC";
            var col = dt.AddNewDataColumnWithExtendedProperties(logicalName, physicalName, "char(10)");

            var ruleResult = e.RunRuleSetAgainstObject(col);

            Assert.Single(ruleResult.RuleCheckResults_Failed);
        }

        [Fact]
        public void SerializeDeserializeTest()
        {
            RuleSet rs = TestData.MainRuleSet();
            string yaml = rs.GetYamlString();
            RuleEngine e = new RuleEngine();
            e.LoadRuleSetFromYamlString(yaml);

            Assert.True(yaml.Length > 0);
        }

        [Fact]
        public void GetYamlString()
        {
            RuleSet rs = TestData.RuleSetWith3Rules();
            string yaml = rs.GetYamlString();
            Assert.Equal(YamlOf3Rules, yaml);
        }

        private const string YamlOf3Rules = @"RuleSetGuid: b89dfba6-8028-4c98-b827-7320a47afd14
Name: Main naming standards ruleset
Description: This is the default ruleset that is used for naming standards checks
Rules:
- ID: 92
  Description: All logical names are expected to end with a class term, or a combination thereof, that semantically represents the data type of a data element.
  AppliesToTypeName: System.Data.DataColumn
  Antecedent:
    LogicOperator: And
    RuleExpressions: []
    RuleExpressionGroups: []
  Consequent:
    LogicOperator: And
    RuleExpressions:
    - PropertyName: LogicalName
      ComparisonCondition: EndsWithAny
      ExpressionValue: File;Line 4;Line 3;Line 2;Line 1;Line;Code;Address;Year;Timestamp;Time;Text;Switch;Rate;Percent;Number;Name;Indicator;Identifier;Description;Date;Count;Amount
      CaseSensitive: false
    RuleExpressionGroups: []
  OutcomeLevel: Expected
- ID: 2
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
- ID: 3
  Description: Email addresses (xyz@ssa.gov) should end with email address.
  AppliesToTypeName: System.Data.DataColumn
  Antecedent:
    LogicOperator: And
    RuleExpressions: []
    RuleExpressionGroups:
    - LogicOperator: Or
      RuleExpressions:
      - PropertyName: LogicalName
        ComparisonCondition: Ends with
        ExpressionValue: email address
        CaseSensitive: false
      - PropertyName: LogicalName
        ComparisonCondition: Ends with
        ExpressionValue: e-mail address
        CaseSensitive: false
      - PropertyName: LogicalName
        ComparisonCondition: Ends with
        ExpressionValue: e mail address
        CaseSensitive: false
      - PropertyName: LogicalName
        ComparisonCondition: Ends with
        ExpressionValue: email
        CaseSensitive: false
      - PropertyName: LogicalName
        ComparisonCondition: Ends with
        ExpressionValue: e-mail
        CaseSensitive: false
      RuleExpressionGroups: []
  Consequent:
    LogicOperator: And
    RuleExpressions: []
    RuleExpressionGroups:
    - LogicOperator: And
      RuleExpressions:
      - PropertyName: DatatypeLength
        ComparisonCondition: Is greater than
        ExpressionValue: 50
        CaseSensitive: false
      - PropertyName: LogicalName
        ComparisonCondition: Ends with
        ExpressionValue: Address
        CaseSensitive: false
      RuleExpressionGroups: []
  OutcomeLevel: Expected
";
    }
}
