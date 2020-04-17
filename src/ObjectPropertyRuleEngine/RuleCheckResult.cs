using PrimitiveExtensions;
using System;
using System.Diagnostics;

namespace ObjectPropertyRuleEngine
{
    public class RuleCheckResult
    {
        internal RuleCheckResult(Rule rule)
        {
            Rule = rule;
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }
        internal Stopwatch Stopwatch;

        public TimeSpan Elapsed { get { return Stopwatch.Elapsed; } }
        public Rule Rule { get; }
        public bool? AntecedentEvaluatesToTrue { get; internal set; }
        public bool? ConsequentEvaluatesToTrue { get; internal set; }
        public string ResultText { get; internal set; }
        
        public Object Object { get; internal set; }
        public bool HasError { get; internal set; }

        public override string ToString()
        {
            return  $"{ResultText} : {Rule.Description}";
        }
    }
}