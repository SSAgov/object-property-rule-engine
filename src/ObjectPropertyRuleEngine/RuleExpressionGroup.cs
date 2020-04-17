using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectPropertyRuleEngine
{
    public class RuleExpressionGroup
    {
        public RuleExpressionGroup  ()
        {
            RuleExpressions = new HashSet<RuleExpression>();
            RuleExpressionGroups = new HashSet<RuleExpressionGroup>();
        }
        public RuleExpressionGroup(LogicOperatorEnum logicOperator ):this()
        {
            LogicOperator = logicOperator;
        }
        public LogicOperatorEnum     LogicOperator { get; set; }
        public HashSet<RuleExpression> RuleExpressions { get; set; }
        public HashSet<RuleExpressionGroup> RuleExpressionGroups { get; set; }
               
        public enum LogicOperatorEnum
        {
            NotSet,
            And,
            Or
        }

        public bool EvaluateAgainstObject(object o)
        {
            switch (LogicOperator)
            {
                case LogicOperatorEnum.And:
                    foreach (var item in RuleExpressions)
                    {
                        if (!item.Evaluate(o))
                            return false;
                    }
                    foreach (var item in RuleExpressionGroups)
                    {
                        if (!item.EvaluateAgainstObject(o))
                            return false;
                    }
                    return true;

                case LogicOperatorEnum.Or:
                    foreach (var item in RuleExpressions)
                    {
                        if (item.Evaluate(o))
                            return true;
                    }
                    foreach (var item in RuleExpressionGroups)
                    {
                        if (item.EvaluateAgainstObject(o))
                            return true;
                    }
                    return false;

                case LogicOperatorEnum.NotSet:
                    throw new Exception("The Logic operator in the rule expression has not been set. It should be either set to 'OR' or 'AND'");

                default:
                    throw new Exception("Code should never make it here!");
            }
        }

        public string GetSentence()
        {
            StringBuilder sb = new StringBuilder("(");
            int i = 0;
            foreach (var item in RuleExpressions)
            {
                i++;
                sb.Append(item.GetSentence());

                if (i != RuleExpressions.Count)
                {
                    switch (LogicOperator)
                    {
                        case LogicOperatorEnum.And:
                            sb.Append(" AND ");
                            break;
                        case LogicOperatorEnum.Or:
                            sb.Append(" OR ");
                            break;
                        case LogicOperatorEnum.NotSet:
                            break;
                        default:
                            break;
                    }
                }
            }
            i = 0;
            foreach (var item in RuleExpressionGroups)
            {
                i++;
                sb.Append(item.GetSentence());
                if (i != RuleExpressionGroups.Count)
                {
                    switch (LogicOperator)
                    {
                        case LogicOperatorEnum.And:
                            sb.Append(" AND ");
                            break;
                        case LogicOperatorEnum.Or:
                            sb.Append(" OR ");
                            break;
                        case LogicOperatorEnum.NotSet:
                            break;
                        default:
                            break;
                    }
                }
            }
            sb.Append(")");
            return sb.ToString();

            //switch (LogicOperator)
            //{
            //    case LogicOperatorEnum.And:
                    

            //    case LogicOperatorEnum.Or:
                    

            //    case LogicOperatorEnum.NotSet:
            //        throw new Exception("THe Logic operator in the rule expression has not been set. It should be either set to 'OR' or 'AND'");

            //    default:
            //        throw new Exception("Code should never make it here!");
            //}
        }

    }
}
