using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests.Unit
{
    public class RuleExpressionGroupTests
    {
        [Fact]
        public void ComplexNestedRuleExpressionGroupTest()
        {
            string pineappleString = "pineapple";
            // starting with this complex condition
            // ((((eq1 OR eq2) OR (eq3 AND eq4 AND eq5)) AND eq6 AND eq7 AND (eq8 OR eq9)) OR eq10)

            // (eq1 OR eq2)
            // ==> g1
            RuleExpressionGroup g1 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            RuleExpression eq1 = new RuleExpression("Length", "=", 5);
            RuleExpression eq2 = new RuleExpression("", "Contains", "apple");
            g1.RuleExpressions.Add(eq1);
            g1.RuleExpressions.Add(eq2);
            bool g1Result = g1.EvaluateAgainstObject(pineappleString);
            Assert.True(g1Result); // 'pineapple' isn't 5 characters long, but it does contain 'apple'. Since this is an OR grouping, only one needs to be true

            // (eq3 AND eq4 AND eq5)
            // ==> g2
            RuleExpressionGroup g2 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            RuleExpression eq3 = new RuleExpression("", "StartsWith", "pine");
            RuleExpression eq4 = new RuleExpression("", "BeginsWith", "p");
            RuleExpression eq5 = new RuleExpression("", "EndsWith", "ple");
            g2.RuleExpressions.Add(eq3);
            g2.RuleExpressions.Add(eq4);
            g2.RuleExpressions.Add(eq5);
            bool g2Result = g2.EvaluateAgainstObject(pineappleString);
            Assert.True(g2Result);  // 'pineapple' starts w/ 'pine', 'p' and ends w/ 'ple'

            // ((eq1 OR eq2) OR (eq3 AND eq4 AND eq5))
            // (g1 OR g2)
            // ==> g3
            RuleExpressionGroup g3 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            g3.RuleExpressionGroups.Add(g1);
            g3.RuleExpressionGroups.Add(g2);
            g3.EvaluateAgainstObject(pineappleString);
            bool g3Result = g3.EvaluateAgainstObject(pineappleString);
            Assert.True(g3Result);

            //(eq8 OR eq9)
            // ==> g4
            RuleExpressionGroup g4 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            RuleExpression eq8 = new RuleExpression("Length", "=", 10);
            RuleExpression eq9 = new RuleExpression("Length", "is", 8);
            g4.RuleExpressions.Add(eq8);
            g4.RuleExpressions.Add(eq9);
            bool g4Result = g4.EvaluateAgainstObject(pineappleString);
            Assert.False(g4Result); //string.Length of 'pineapple' isn't 10 or 8


            // (((eq1 OR eq2) OR (eq3 AND eq4 AND eq5)) AND eq6 AND eq7 AND (eq8 OR eq9))
            //                g3 AND eq6 AND eq7 and g4
            // ==> g5
            RuleExpressionGroup g5 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            RuleExpression eq6 = new RuleExpression("Length", "=", 9);
            RuleExpression eq7 = new RuleExpression("", "is", "pineapple");
            g5.RuleExpressionGroups.Add(g3); //true
            g5.RuleExpressions.Add(eq6); //true
            g5.RuleExpressions.Add(eq7); //true
            g5.RuleExpressionGroups.Add(g4); //false
            bool g5Result = g5.EvaluateAgainstObject(pineappleString);
            Assert.False(g5Result);

            // ((((eq1 OR eq2) OR (eq3 AND eq4 AND eq5)) AND eq6 AND eq7 AND (eq8 OR eq9)) OR eq10)
            // g5 OR eq10
            // ==> g6
            RuleExpressionGroup g6 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            bool caseSensitive = false;
            RuleExpression eq10 = new RuleExpression("", "is", "PiNeAppLe", caseSensitive);
            g6.RuleExpressionGroups.Add(g5); // false
            g6.RuleExpressions.Add(eq10); // true
            bool g6Result = g6.EvaluateAgainstObject(pineappleString);
            Assert.True(g6Result);
        }

        [Fact]
        public void RunRuleAgainstObject_Column_EndsWithEmailVariants()
        {
            RuleExpressionGroup g = TestData_RuleExpressionGroups.LogicalNameEndsWithEmailVariants();

            DataColumn c = new DataColumn();
            c.ExtendedProperties["LogicalName"] = "person email";

            Assert.True(g.EvaluateAgainstObject(c));
        }



        [Fact]
        public void RunRuleAgainstObject_Column_DataTypeLong()
        {
            RuleExpressionGroup gOuter = TestData_RuleExpressionGroups.RuleExpresssion_LogicalName_EndsWith_TypeOrCode_And_Max_lengh_is_gt_5();

            DataColumn c = new DataColumn();
            c.ExtendedProperties["LogicalName"] = "Hearing Code";
            c.MaxLength = 6;

            Assert.True(gOuter.EvaluateAgainstObject(c));

        }
        [Fact]
        public void GetSentence_Column_DataTypeLong()
        {
            RuleExpressionGroup gOuter = TestData_RuleExpressionGroups.RuleExpresssion_LogicalName_EndsWith_TypeOrCode_And_Max_lengh_is_gt_5();

            string sentence = gOuter.GetSentence();
            Assert.Equal("(([LogicalName][Ends with][code] OR [LogicalName][Ends with][type]) AND ([MaxLength][Is greater than][5]))", sentence);
        }
    }
}
