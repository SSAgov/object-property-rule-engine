using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectPropertyRuleEngine
{
    public class RuleSet
    {
        public RuleSet()
        {
            Rules = new HashSet<Rule>();
            RuleSetGuid = Guid.NewGuid().ToString() ;
        }

        public string RuleSetGuid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public HashSet<Rule> Rules { get; set; }

        internal RuleSetCheckResult RunRuleSetAgainstObject(object dataStructureObject)
        {
            RuleSetCheckResult setResult = new RuleSetCheckResult(this);
            setResult.Object = dataStructureObject;
            foreach (var item in Rules)
            {
                RuleCheckResult ruleResult = new RuleCheckResult(item);
                if (item.AppliesToTypeName == dataStructureObject.GetType().FullName)
                {
                    ruleResult = item.RunRuleAgainstObject(dataStructureObject);
                    if (ruleResult.HasError)
                    {
                        setResult.RuleCheckResults_Errored.Add(ruleResult);
                    }
                    else
                    {
                        if (ruleResult.AntecedentEvaluatesToTrue.Value)
                        {
                            if (ruleResult.ConsequentEvaluatesToTrue.Value)
                            {
                                setResult.RuleCheckResults_Passed.Add(ruleResult);
                            }
                            else
                            {
                                setResult.RuleCheckResults_Failed.Add(ruleResult);
                                //setResult.Pass = false;
                            }
                        }
                        else
                        {
                            setResult.RuleCheckResults_NotApplicable.Add(ruleResult);
                        }
                    }
                }
                else
                {
                    setResult.RuleCheckResults_NotApplicable.Add(ruleResult);
                }
                ruleResult.Stopwatch.Stop();
            }
            setResult.Stopwatch.Stop();
            setResult.AllApplicableTestsPassed = setResult.RuleCheckResults_Passed.Count == Rules.Count - setResult.RuleCheckResults_NotApplicable.Count;
            

            setResult.ResultText = $"{dataStructureObject} Applicable:  {Rules.Count - setResult.RuleCheckResults_NotApplicable.Count}/{Rules.Count} | Passed: {setResult.RuleCheckResults_Passed.Count}/{Rules.Count - setResult.RuleCheckResults_NotApplicable.Count}";
            return setResult;
        }

        internal IEnumerable<RuleSetCheckResult> RunRuleSetAgainstObjects(IEnumerable<object> dataStructureObjects, bool skipNonApplicableRules = true)
        {
            HashSet<RuleSetCheckResult> results = new HashSet<RuleSetCheckResult>();
            foreach (var item in dataStructureObjects)
            {
                results.Add(RunRuleSetAgainstObject(item));
            }
            return results;
        }

        public IEnumerable<int> GetRepeatedRuleIds()
        {
            var q = Rules.GroupBy(x => x.ID)
                .Where(a => a.Count() > 1)
                .SelectMany(a => a.Select(b => b.ID));

            return q;
        }

        public string GetYamlString()
        {
            YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
            return serializer.Serialize(this);
        }
    }
}
