using PrimitiveExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectPropertyRuleEngine
{
    public enum ComparisonConditionEnum
    {
        notset,
        EqualTo,
        EqualToAny,
        NotEqual,
        NotEqualToAny,
        Contains,
        ContainsAny,
        DoesNotContain,
        DoesNotContainAny,
        StartsWith,
        StartWithAny,
        EndsWith,
        EndsWithAny,
        DoesNotStartWith,
        DoesNotStartWithAny,
        DoesNotEndWith,
        DoesNotEndWithAny,
        GreaterThan,
        GreaterThanAny,
        GreaterThanOrEqualTo,
        GreaterThanOrEqualToAny,
        LessThan,
        LessThanAny,
        LessThanOrEqualTo,
        LessThanOrEqualToAny
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
