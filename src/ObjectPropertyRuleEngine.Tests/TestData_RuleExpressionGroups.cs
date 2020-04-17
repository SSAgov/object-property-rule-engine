using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests
{
    public static class TestData_RuleExpressionGroups
    {
        internal static RuleExpressionGroup LogicalNameEndsWithEmailVariants()
        {
            RuleExpressionGroup g = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            g.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "email address"));
            g.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "e-mail address"));
            g.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "e mail address"));
            g.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "email"));
            g.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "e-mail"));
            return g;
        }

        internal static RuleExpressionGroup RuleExpresssion_LogicalName_EndsWith_TypeOrCode_And_Max_lengh_is_gt_5()
        {
            RuleExpressionGroup gInner1 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            gInner1.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "code"));
            gInner1.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "type"));

            RuleExpressionGroup gInner2 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            gInner2.RuleExpressions.Add(new RuleExpression("MaxLength", ComparisonConditionEnum.GreaterThan, 5));

            RuleExpressionGroup gOuter = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            gOuter.RuleExpressionGroups.Add(gInner1);
            gOuter.RuleExpressionGroups.Add(gInner2);
            return gOuter;
        }
    }

}
