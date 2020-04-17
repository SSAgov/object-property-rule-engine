using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace ObjectPropertyRuleEngine.Tests.Unit
{
    public class RuleEngineTests
    {
        [Fact]
        public void LoadRuleSetFromYamlString()
        {
            RuleEngine e = new RuleEngine();
            e.LoadRuleSetFromYamlString(YamlOf3Rules);
            Assert.Equal(3, e.RuleSet.Rules.Count);
        }

    

        [Fact]
        public void LoadRulesetFromYamlFileUrl()
        {
            RuleEngine e = new RuleEngine();
            string urlToYaml = "https://github.com/SSAgov/object-property-rule-engine/raw/master/src/ObjectPropertyRuleEngine.Tests/unit-test-01.YAML";
            e.LoadRulesetFromYamlFileUrl(urlToYaml);
            Assert.Equal("a385c378-3e7b-4890-a9f1-731a745201a0", e.RuleSet.RuleSetGuid);
            Assert.Equal(3, e.RuleSet.Rules.Count);
        }

        [Fact]
        public void LoadRulesetFromYamlFileUrl_CheckForDuplicateRuleID_1_duplicate()
        {
            RuleEngine e = new RuleEngine();
            string urlToYaml = "https://github.com/SSAgov/object-property-rule-engine/raw/master/src/ObjectPropertyRuleEngine.Tests/unit-test-02-duplicate-rule-id.YAML";
            e.LoadRulesetFromYamlFileUrl(urlToYaml);
            Assert.Equal("8e6273b7-c719-4b08-b53f-6a7ac741c030", e.RuleSet.RuleSetGuid);
            var repeatedIds = e.RuleSet.GetRepeatedRuleIds();
            //Rule id 2 should be duplicated
            Assert.Contains(2, repeatedIds);
        }

        [Fact]
        public void LoadYamlFromUrl_CheckForDuplicateRuleID_Multiple_Duplicates()
        {
            RuleEngine e = new RuleEngine();
            string urlToYaml = "https://github.com/SSAgov/object-property-rule-engine/raw/master/src/ObjectPropertyRuleEngine.Tests/unit-test-03-duplicate-rule-id.YAML?";
            e.LoadRulesetFromYamlFileUrl(urlToYaml);
            Assert.Equal("eff64cca-3c37-438e-9a12-edf7f975159c", e.RuleSet.RuleSetGuid);
            var dupes = e.RuleSet.GetRepeatedRuleIds();
            //expect that IDs 2 and 3 have been duplicated
            Assert.Contains(3, dupes);
            Assert.Contains(2, dupes);
        }


        public const string YamlOf3Rules = @"RuleSetGuid: b89dfba6-8028-4c98-b827-7320a47afd14
Name: Main naming standards ruleset
Description: This is the default ruleset that is used for naming standards checks
Rules:
- ID: 1
  Description: All columns must end with a class word
  AppliesToTypeName: System.Data.DataColumn
  Antecedent:
    RuleExpressions: []
    RuleExpressionGroups: []
  Consequent:
    RuleExpressions:
    - PropertyName: LogicalName
      ComparisonCondition: EndsWithAny
      ExpressionValue: File;Line 4;Line 3;Line 2;Line 1;Line;Code;Address;Year;Timestamp;Time;Text;Switch;Rate;Percent;Number;Name;Indicator;Identifier;Description;Date;Count;Amount
    RuleExpressionGroups: []
- ID: 2
  Description: Expect columns that end in Timestamp or Time Stamp to be a datetime or related datatype
  AppliesToTypeName: System.Data.DataColumn
  Antecedent:
    LogicOperator: Or
    RuleExpressions:
    - PropertyName: LogicalName
      ComparisonCondition: EndsWith
      ExpressionValue: Timestamp
    - PropertyName: LogicalName
      ComparisonCondition: EndsWith
      ExpressionValue: Time Stamp
    - PropertyName: Caption
      ComparisonCondition: EndsWith
      ExpressionValue: Time Stamp
    RuleExpressionGroups: []
  Consequent:
    RuleExpressions:
    - PropertyName: Datatype
      ComparisonCondition: EqualTo
      ExpressionValue: Timestamp
    RuleExpressionGroups: []
- ID: 3
  Description: Email addresses (xyz@ssa.gov) should end with email address.
  AppliesToTypeName: System.Data.DataColumn
  Antecedent:
    RuleExpressions: []
    RuleExpressionGroups:
    - LogicOperator: Or
      RuleExpressions:
      - PropertyName: LogicalName
        ComparisonCondition: EndsWith
        ExpressionValue: email address
      - PropertyName: LogicalName
        ComparisonCondition: EndsWith
        ExpressionValue: e-mail address
      - PropertyName: LogicalName
        ComparisonCondition: EndsWith
        ExpressionValue: e mail address
      - PropertyName: LogicalName
        ComparisonCondition: EndsWith
        ExpressionValue: email
      - PropertyName: LogicalName
        ComparisonCondition: EndsWith
        ExpressionValue: e-mail
      RuleExpressionGroups: []
  Consequent:
    RuleExpressions: []
    RuleExpressionGroups: []
";
    }
}
