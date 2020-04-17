using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests
{
    public static partial class TestData
    {
        public static RuleSet MainRuleSet()
        {
            RuleSet rs = new RuleSet();
            rs.RuleSetGuid = Guid.NewGuid().ToString();
            rs.Name = "Main naming standards Ruleset";
            rs.Rules.Add(TestData_Rules.Rule001());
            rs.Rules.Add(TestData_Rules.Rule019());
            rs.Rules.Add(TestData_Rules.Rule024_text_types());
            rs.Rules.Add(TestData_Rules.Rule025_descriptions_should_be_textual());
            rs.Rules.Add(TestData_Rules.Rule038_AddresslineLength());
            rs.Rules.Add(TestData_Rules.Rule047_parens_need_value());
            rs.Rules.Add(TestData_Rules.Rule050_datatype_length_required());
            rs.Rules.Add(TestData_Rules.Rule051_Attributes_Must_be_defined());
            rs.Rules.Add(TestData_Rules.Rule057_email());
            rs.Rules.Add(TestData_Rules.Rule059_countryName());
            rs.Rules.Add(TestData_Rules.Rule060_utc_gmt());
            rs.Rules.Add(TestData_Rules.Rule070_from_irs());
            rs.Rules.Add(TestData_Rules.Rule072_appointedrep_not_auth_rep());
            rs.Rules.Add(TestData_Rules.Rule079_A_field_that_contains_TSO());
            rs.Rules.Add(TestData_Rules.Rule081_forms());
            rs.Rules.Add(TestData_Rules.Rule085_switches_shouldntbe_null());
            rs.Rules.Add(TestData_Rules.Rule089_Character_dates());
            rs.Rules.Add(TestData_Rules.Rule091_long_codes());
            rs.Rules.Add(TestData_Rules.Rule092_ColumnsMustEndWithAClassWord());
            rs.Rules.Add(TestData_Rules.Rule103_class());
            rs.Rules.Add(TestData_Rules.Rule104_class());
            rs.Rules.Add(TestData_Rules.Rule105_class());
            rs.Rules.Add(TestData_Rules.Rule106_class());
            rs.Rules.Add(TestData_Rules.Rule107_class());
            rs.Rules.Add(TestData_Rules.Rule108_class());
            rs.Rules.Add(TestData_Rules.Rule109_class());
            rs.Rules.Add(TestData_Rules.Rule110_class());
            rs.Rules.Add(TestData_Rules.Rule111_class());
            rs.Rules.Add(TestData_Rules.Rule112_class());
            rs.Rules.Add(TestData_Rules.Rule113_class());
            rs.Rules.Add(TestData_Rules.Rule114_class());
            rs.Rules.Add(TestData_Rules.Rule115_class());
            rs.Rules.Add(TestData_Rules.Rule116_class());
            rs.Rules.Add(TestData_Rules.Rule117_class());
            rs.Rules.Add(TestData_Rules.Rule118_class());
            rs.Rules.Add(TestData_Rules.Rule119_class());
            rs.Rules.Add(TestData_Rules.Rule120_class());
            rs.Rules.Add(TestData_Rules.Rule121_class());


            rs.Rules.Add(TestData_Rules.Rule123_ColumnsMustBeMoreThanAClassWord());
            rs.Rules.Add(TestData_Rules.Rule129_see());
            rs.Rules.Add(TestData_Rules.Rule130_see());
            rs.Rules.Add(TestData_Rules.Rule131_see());
            rs.Rules.Add(TestData_Rules.Rule132_see());
            rs.Rules.Add(TestData_Rules.Rule133_see());
            rs.Rules.Add(TestData_Rules.Rule134_see());
            rs.Rules.Add(TestData_Rules.Rule135_see());
            rs.Rules.Add(TestData_Rules.Rule136_see());
            rs.Rules.Add(TestData_Rules.Rule200_defaultDatatype());
            rs.Rules.Add(TestData_Rules.Rule201_DoubleSpacesInLogicalName());

            

            return rs;
        }

        public static RuleSet RuleSetWith3Rules()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.RuleSetGuid = "b89dfba6-8028-4c98-b827-7320a47afd14";
            ruleSet.Name = "Main naming standards ruleset";
            ruleSet.Description = "This is the default ruleset that is used for naming standards checks";
            ruleSet.Rules.Add(TestData_Rules.Rule092_ColumnsMustEndWithAClassWord());
            ruleSet.Rules.Add(TestData_Rules.Columns_LogicalName_EndInTimeStamp_ExpectAppropriatePhysicalDatatype());
            ruleSet.Rules.Add(TestData_Rules.Rule057_email());
            return ruleSet;
        }
    }
}
