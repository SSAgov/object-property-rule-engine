using PrimitiveExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectPropertyRuleEngine
{
    public class ComparisonConditionCodes
    {
        public static string GetTextForEnum(ComparisonConditionEnum comparisonConditionCode)
        {
            switch (comparisonConditionCode)
            {
                case ComparisonConditionEnum.EqualTo:
                    return "Is equal to";

                case ComparisonConditionEnum.EqualToAny:
                    return "Is equal to any of the following";

                case ComparisonConditionEnum.NotEqual:
                    return "Is not equal to";

                case ComparisonConditionEnum.NotEqualToAny:
                    return "Is not equal to any of the folllowing";

                case ComparisonConditionEnum.Contains:
                    return "Contains";

                case ComparisonConditionEnum.ContainsAny:
                    return "Contains any of the following";

                case ComparisonConditionEnum.DoesNotContain:
                    return "Does not contain";

                case ComparisonConditionEnum.DoesNotContainAny:
                    return "Does not contain any of the following";

                case ComparisonConditionEnum.StartsWith:
                    return "Starts with";

                case ComparisonConditionEnum.StartWithAny:
                    return "Starts with any of the following";

                case ComparisonConditionEnum.EndsWith:
                    return "Ends with";

                case ComparisonConditionEnum.EndsWithAny:
                    return "Ends with any of the following";
                case ComparisonConditionEnum.DoesNotStartWith:
                    return "Does not start with";
                case ComparisonConditionEnum.DoesNotStartWithAny:
                    return "Does not start with any of the following";



                case ComparisonConditionEnum.DoesNotEndWith:
                    return "Does not end with";

                case ComparisonConditionEnum.DoesNotEndWithAny:
                    return "Does not end with any of the following";

                case ComparisonConditionEnum.GreaterThan:
                    return "Is greater than";

                case ComparisonConditionEnum.GreaterThanAny:
                    return "Is greater than any of the following";

                case ComparisonConditionEnum.GreaterThanOrEqualToAny:
                    return "Is greater than or equal to any of the following";

                case ComparisonConditionEnum.LessThan:
                    return "Is less than";

                case ComparisonConditionEnum.LessThanAny:
                    return "Is less than any of the following";

                case ComparisonConditionEnum.LessThanOrEqualToAny:
                    return "Is less than or equal to any of the following";

                default:
                    throw new Exception("Invalid comparison condition code");
            }
        }

        internal static ComparisonConditionEnum GetEnumFromText(string comparisonCondition)
        {
            switch (comparisonCondition.ToLower().Replace(" ","").Replace("'",""))
            {
                case "=":
                case "==":
                case "===":
                case "is":
                case "equal":
                case "equals":
                case "equalto":
                case "isequal":
                case "isequalto":
                    return ComparisonConditionEnum.EqualTo;


                case "isequaltoanyofthefollowing":
                case "equaltoanyofthefollowing":
                case "isequaltoany":
                case "equaltoany":
                case "equalsany":
                case "isany":
                    return ComparisonConditionEnum.EqualToAny;

                case "!=":
                case "<>":
                case "isnot":
                case "isnt":
                case "isnotequal":
                case "doesnotequal":
                case "doesntequal":
                case "isnotequalto":
                case "notequalto":
                case "notequal":
                    return ComparisonConditionEnum.NotEqual;

                case "isnotequaltoanyofthefolllowing":
                case "notequaltoanyofthefolllowing":
                case "isnotequaltoany":
                case "notequaltoany":
                case "doesntequalany":
                    return ComparisonConditionEnum.NotEqualToAny;

                case "contains":
                    return ComparisonConditionEnum.Contains;

                case "containsanyofthefollowing":
                case "containsany":
                    return ComparisonConditionEnum.ContainsAny;

                case "doesnotcontain":
                case "doesntcontain":
                    return ComparisonConditionEnum.DoesNotContain;

                case "doesnotcontainanyofthefollowing":
                case "doesntcontainanyofthefollowing":
                case "doesnotcontainany":
                case "doesntcontainany":
                    return ComparisonConditionEnum.DoesNotContainAny;

                case "startswith":
                case "beginswith":
                    return ComparisonConditionEnum.StartsWith;

                case "startswithanyofthefollowing":
                case "beginsswithanyofthefollowing":
                case "startswithany":
                case "beginswithany":
                    return ComparisonConditionEnum.StartWithAny;

                case "endswith":
                    return ComparisonConditionEnum.EndsWith;

                case "endswithanyofthefollowing":
                case "endswithany":
                    return ComparisonConditionEnum.EndsWithAny;

                case "doesntstartwith":
                case "doesntbeginwith":
                case "doesnotstartwith":
                case "doesnotbeginwith":
                    return ComparisonConditionEnum.DoesNotStartWith;

                case "doesnotstartwithanyofthefollowing":
                case "doesnotbeginwithanyofthefollowing":
                case "doesntstartwithanyofthefollowing":
                case "doesntbeginwithanyofthefollowing":
                    return ComparisonConditionEnum.DoesNotStartWithAny;

                case "doesnotendwith":
                case "doesntendwith":
                    return ComparisonConditionEnum.DoesNotEndWith;

                case "doesnotendwithanyofthefollowing":
                case "doesntendwithanyofthefollowing":
                case "doesnotendwithany":
                case "doesntendwithany":
                    return ComparisonConditionEnum.DoesNotEndWithAny;

                case ">":
                case "isgreaterthan":
                case "isgreaterthen":
                case "greaterthan":
                case "greaterthen":
                    return ComparisonConditionEnum.GreaterThan;

                case "isgreaterthananyofthefollowing":
                case "greaterthananyofthefollowing":
                case "isgreaterthanany":
                case "greaterthanany":
                    return ComparisonConditionEnum.GreaterThanAny;

                case ">=":
                case "isgreaterthanorequalto":
                case "greaterthanorequalto":
                    return ComparisonConditionEnum.GreaterThanOrEqualTo;

                case "isgreaterthanorequaltoanyofthefollowing":
                case "greaterthanorequaltoanyofthefollowing":
                case "isgreaterthanorequaltoany":
                case "greaterthanorequaltoany":
                    return ComparisonConditionEnum.GreaterThanOrEqualToAny;

                case "<":
                case "islessthan":
                case "lessthan":
                    return ComparisonConditionEnum.LessThan;

                case "islessthananyofthefollowing":
                case "lessthananyofthefollowing":
                case "islessthanany":
                case "lessthanany":
                    return ComparisonConditionEnum.LessThanAny;

                case "<=":
                case "islessthanorequal":
                case "lessthanorequal":
                    return ComparisonConditionEnum.LessThanOrEqualTo;

                case "islessthanorequaltoanyofthefollowing":
                case "lessthanorequaltoanyofthefollowing":
                case "islessthanorequaltoany":
                case "lessthanorequaltoany":
                    return ComparisonConditionEnum.LessThanOrEqualToAny;

                default:
                    throw new Exception("Invalid comparison condition code");
            }
            throw new NotImplementedException();
        }
    }
    //public static string GetTextForCode(string comparisonConditionCode)
    //{
    //    switch (comparisonConditionCode)
    //    {
    //        case ComparisonConditionCodes.EqualTo:
    //            return "Is equal to";

    //        case ComparisonConditionCodes.EqualToAny:
    //            return "Is equal to any of the following";

    //        case ComparisonConditionCodes.NotEqual:
    //            return "Is not equal to";

    //        case ComparisonConditionCodes.NotEqualToAny:
    //            return "Is not equal to any of the folllowing";

    //        case ComparisonConditionCodes.Contains:
    //            return "Contains";

    //        case ComparisonConditionCodes.ContainsAny:
    //            return "Contains any of the following";

    //        case ComparisonConditionCodes.DoesNotContain:
    //            return "Does not contain";

    //        case ComparisonConditionCodes.DoesNotContainAny:
    //            return "Does not contain any of the following";

    //        case ComparisonConditionCodes.StartsWith:
    //            return "Starts with";

    //        case ComparisonConditionCodes.StartWithAny:
    //            return "Starts with any of the following";

    //        case ComparisonConditionCodes.DoesNotStartWith:
    //            return "Does not start with";

    //        case ComparisonConditionCodes.DoesNotStartWithAny:
    //            return "Does not start with any of the following";

    //        case ComparisonConditionCodes.EndsWith:
    //            return "Ends with";

    //        case ComparisonConditionCodes.EndsWithAny:
    //            return "Ends with any of the following";

    //        case ComparisonConditionCodes.DoesNotEndWith:
    //            return "Does not end with";

    //        case ComparisonConditionCodes.DoesNotEndWithAny:
    //            return "Does not end with any of the following";

    //        case ComparisonConditionCodes.GreaterThan:
    //            return "Is greater than";

    //        case ComparisonConditionCodes.GreaterThanAny:
    //            return "Is greater than any of the following";

    //        case ComparisonConditionCodes.GreaterThanOrEqualToAny:
    //            return "Is greater than or equal to any of the following";

    //        case ComparisonConditionCodes.LessThan:
    //            return "Is less than";

    //        case ComparisonConditionCodes.LessThanAny:
    //            return "Is less than any of the following";

    //        case ComparisonConditionCodes.LessThanOrEqualToAny:
    //        return    "Is less than or equal to any of the following";

    //        default:
    //            throw new Exception("Invalid comparison condition code");
    //}
    //}
    //}





}
