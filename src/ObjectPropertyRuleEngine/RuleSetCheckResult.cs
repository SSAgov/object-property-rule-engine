using PrimitiveExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ObjectPropertyRuleEngine
{
    public class RuleSetCheckResult
    {
        internal RuleSetCheckResult(RuleSet ruleSet)
        {
            RuleSet = ruleSet;
            RuleCheckResults_Failed = new HashSet<RuleCheckResult>();
            RuleCheckResults_NotApplicable = new HashSet<RuleCheckResult>();
            RuleCheckResults_Passed = new HashSet<RuleCheckResult>();
            RuleCheckResults_Errored = new HashSet<RuleCheckResult>();
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }
        internal Stopwatch Stopwatch { get; set; }
        public TimeSpan EvaluationTimeSpan { get { return Stopwatch.Elapsed; } }

        public RuleSet RuleSet { get; set; }

        public string GetYAML()
        {
            YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
            return serializer.Serialize(this);
        }
        //public bool Pass { get; internal set; }
        public string ResultText { get; internal set; }

        public Object Object { get; internal set; }
        
        public HashSet<RuleCheckResult> RuleCheckResults_NotApplicable { get; set; }
        public HashSet<RuleCheckResult> RuleCheckResults_Passed { get; set; }
        public HashSet<RuleCheckResult> RuleCheckResults_Failed { get; set; }
        public HashSet<RuleCheckResult> RuleCheckResults_Errored { get; set; }
        public bool AllApplicableTestsPassed { get; internal set; }

        public override string ToString()
        {
            return ResultText;
        }
    }
}