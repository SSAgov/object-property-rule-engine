using PrimitiveExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectPropertyRuleEngine
{
    public class Rule
    {
        public Rule() {
            Antecedent = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            Consequent = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            OutcomeLevel = OutcomeLevelEnum.Expected;
        }
        public Rule(string description, int id, string appliesToTypeName = "System.Data.DataColumn")
            :this()
        {
            Description = description;
            ID = id;
            AppliesToTypeName = appliesToTypeName;
        }


        public int ID { get; set; }
        public string Description { get; set; }

        public string AppliesToTypeName { get; set; }

        public RuleExpressionGroup Antecedent { get; set; }
        public RuleExpressionGroup Consequent { get; set; }
        public OutcomeLevelEnum OutcomeLevel { get; set; }

        //public FailureLevel ConsequentFailureLevel { get; set; }

        public RuleCheckResult RunRuleAgainstObject(object dataStructureObject)
        {
            RuleCheckResult result = new RuleCheckResult(this);
            
            result.Object = dataStructureObject;
            

            try
            {
                result.AntecedentEvaluatesToTrue = Antecedent.EvaluateAgainstObject(dataStructureObject);
            }
            catch (Exception ex)
            {
                result.ResultText = "Error evaluating antecedent: " +ex.Message;
                result.HasError = true;
                return result;
            }

            try
            {
                if (result.AntecedentEvaluatesToTrue.Value)
                {
                    result.ConsequentEvaluatesToTrue = Consequent.EvaluateAgainstObject(dataStructureObject);
                }
            }
            catch (Exception ex)
            {
                result.ResultText = "Error evaluating consequent: " + ex.Message;
                result.HasError = true;
                return result;
            }
            

            if (result.AntecedentEvaluatesToTrue.Value)
            {
                if (result.ConsequentEvaluatesToTrue.Value)
                {
                    result.ResultText = $"PASS: {Consequent.GetSentence()}";
                }
                else
                {
                    result.ResultText = $"{Enum.GetName(typeof(OutcomeLevelEnum), OutcomeLevel)}: {Consequent.GetSentence()}";
                }
            }
            else
            {
                result.ResultText = $"N/A: {Antecedent.GetSentence()}";
            }
            result.Stopwatch.Stop();
            return result;
        }
        public override string ToString()
        {
            return $"{Description}";
        }
        public string GetYamlString()
        {
            YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
            return serializer.Serialize(this);
        }
    }
}
