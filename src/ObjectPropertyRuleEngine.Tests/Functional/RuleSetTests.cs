using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace ObjectPropertyRuleEngine.Tests.Functional
{
    public class RuleSetTests
    {
        [Fact]
        public void FullTest()
        {
            RuleEngine e = new RuleEngine(TestData.RuleSetWith3Rules());
    

            string logicalName = "Hearing Office Code";
            string physicalName = "HOCD";
            string datatype = "varchar(120)";
            
            //nullability
            //switches and indicators should have constraints

            DataTable dt = new DataTable();
            DataColumn c = dt.AddNewDataColumnWithExtendedProperties(logicalName, physicalName, datatype);
            
            RuleSetCheckResult ruleResult=  e.RunRuleSetAgainstObject(c);

            Assert.True(ruleResult.AllApplicableTestsPassed);
            Assert.True(ruleResult.EvaluationTimeSpan.TotalMilliseconds > 0);
        }

        
    }
}
