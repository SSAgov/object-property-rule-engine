using PrimitiveExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Reflection;

namespace ObjectPropertyRuleEngine
{
    public class RuleExpression
    {
        public RuleExpression() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName">The name of the property to be compared. For example: 'Logical Name'</param>
        /// <param name="comparisonCondition">The comparison condition code. You can use the ComparisonConditionCodes object to get the appropriate code for your usecase. For example. ComparisonCodes.EndsWith or 'END' .</param>
        /// <param name="value">The value to be compared against. For example: 'Timestamp' </param>
        public RuleExpression(string propertyName, ComparisonConditionEnum comparisonCondition, object value, bool caseSensitive = false)
        {
            PropertyName = propertyName;
            ComparisonCondition = ComparisonConditionCodes.GetTextForEnum(comparisonCondition);
            ExpressionValue = value;
            CaseSensitive = caseSensitive;
        }

        public RuleExpression(string propertyName, string comparisonCondition, object value, bool caseSensitive = false)
        {
            PropertyName = propertyName;
            ComparisonCondition = comparisonCondition;
            ExpressionValue = value;
            CaseSensitive = caseSensitive;
        }

        public RuleExpression(DataStructureProperty property, ComparisonConditionEnum comparisonCondition, object value, bool caseSensitive = false)
        {
            switch (property)
            {
                case DataStructureProperty.notset:
                case DataStructureProperty.LogicalName:
                case DataStructureProperty.PhysicalName:
                case DataStructureProperty.Definition:
                case DataStructureProperty.Datatype:
                case DataStructureProperty.DatatypeName:
                case DataStructureProperty.DatatypeLength:
                case DataStructureProperty.TableName:
                case DataStructureProperty.DatabaseManagementSystem:
                default:
                    PropertyName = Enum.GetName(typeof(DataStructureProperty), property);
                    break;
            }
            ComparisonCondition = ComparisonConditionCodes.GetTextForEnum(comparisonCondition);
            ExpressionValue = value;
            CaseSensitive = caseSensitive;
        }

        public string PropertyName { get; set; }
        public string ComparisonCondition { get; set; }
        public object ExpressionValue
        {
            get
            {
                return _Value;
            }

            set
            {
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.String:
                    case TypeCode.Int32:
                    case TypeCode.Boolean:
                    case TypeCode.Byte:
                    case TypeCode.Char:
                    case TypeCode.DateTime:
                    case TypeCode.DBNull:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Empty:
                    case TypeCode.Int16:
                    case TypeCode.SByte:
                    case TypeCode.Single:
                    case TypeCode.Int64:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        _Value = value;
                        break;

                    case TypeCode.Object:
                    default:
                        throw new Exception($"Value type of '{value.GetType().FullName}' not supported (yet?).");
                }
            }
        }

        public bool CaseSensitive { get; set; }

        private object _Value;

        private object GetPropertyValue(object o)
        {
            if (PropertyName == "")
            {
                return o;
            }
            object propertyValue = null;
            string[] nameParts = PropertyName.Split('.');
            if (nameParts.Length == 1)
            {
                // Get the object's properties
                PropertyInfo[] properties = o.GetType().GetProperties();

                //if the object has the property, get it's value. Otherwise, then see if there is an ExtendedProperties property and check there

                PropertyInfo theNamedProperty = properties.Where(a => a.Name == PropertyName).FirstOrDefault();
                if (theNamedProperty != null)
                    propertyValue = theNamedProperty.GetValue(o);
                else
                {
                    PropertyInfo extendedProperties = properties
                        .Where(a => a.Name == "ExtendedProperties")
                        .FirstOrDefault();
                    if (extendedProperties != null)
                    {
                        dynamic x = o;
                        propertyValue = x.ExtendedProperties[PropertyName];
                    }
                }
            }
            else
            {
                propertyValue = o;

                foreach (string namePart in nameParts)
                {
                    PropertyInfo[] properties = o.GetType().GetProperties();
                    Type t = propertyValue.GetType();
                    PropertyInfo info = t.GetProperty(namePart);
                    if (info == null)
                    {
                        PropertyInfo extendedProperties = properties
                                                .Where(a => a.Name == "ExtendedProperties")
                                                .FirstOrDefault();
                        if (extendedProperties != null)
                        {
                            dynamic x = o;
                            propertyValue = x.ExtendedProperties[namePart];
                        }
                        //return null;
                    }
                    else
                    {
                        propertyValue = info.GetValue(propertyValue);
                        if (propertyValue == null)
                            return null;
                    }
                }
            }
            return propertyValue;
        }

      

        public string GetSentence()
        {
            return $"[{PropertyName}][{ComparisonCondition}][{ExpressionValue}]";
        }

        public bool Evaluate(Object o)
        {
            object propertyValue = GetPropertyValue(o);

            if (propertyValue == null)
                throw new Exception($"Property '{PropertyName}' not found on object '{o.ToString()}'");

            TypeCode expressionValueTypeCode = Type.GetTypeCode(ExpressionValue.GetType());
            TypeCode propertyValueTypeCode = Type.GetTypeCode(propertyValue.GetType());
            bool typeCodesMatch = expressionValueTypeCode == propertyValueTypeCode;

            string propertyValueAsString = propertyValue.ToString();
            string expressionValueAsString = ExpressionValue.ToString();
            if (CaseSensitive)
            {
                propertyValueAsString = propertyValueAsString.ToLower();
                expressionValueAsString = expressionValueAsString.ToLower();
            }

            switch (ComparisonConditionCodes.GetEnumFromText(ComparisonCondition))
            {
                case ComparisonConditionEnum.EqualTo:
                    return propertyValueAsString.IsEqualTo(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.EqualToAny:
                    return propertyValueAsString.IsEqualToAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.NotEqual:
                    return !propertyValueAsString.IsEqualTo(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.NotEqualToAny:
                    return !propertyValueAsString.IsEqualToAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.Contains:
                    return propertyValueAsString.Contains(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.ContainsAny:
                    return propertyValueAsString.ContainsAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.DoesNotContain:
                    return !propertyValueAsString.Contains(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.DoesNotContainAny:
                    return propertyValueAsString.ContainsAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.StartsWith:
                    return propertyValueAsString.StartsWith(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.StartWithAny:
                    return propertyValueAsString.StartsWithAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.DoesNotStartWith:
                    return !propertyValueAsString.StartsWith(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.DoesNotStartWithAny:
                    return !propertyValueAsString.StartsWithAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.EndsWith:
                    return propertyValueAsString.EndsWith(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.EndsWithAny:
                    return propertyValueAsString.EndsWithAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.DoesNotEndWith:
                    return !propertyValueAsString.EndsWith(expressionValueAsString, CaseSensitive);

                case ComparisonConditionEnum.DoesNotEndWithAny:
                    return !propertyValueAsString.EndsWithAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.GreaterThan:
                    if (typeCodesMatch)
                    {
                        switch (propertyValueTypeCode)
                        {
                            case TypeCode.Boolean:
                                break;
                            case TypeCode.Byte:
                                break;
                            case TypeCode.Char:
                                break;
                            case TypeCode.DateTime:
                                break;
                            case TypeCode.DBNull:
                                break;
                            case TypeCode.Decimal:
                                break;
                            case TypeCode.Double:
                                break;
                            case TypeCode.Empty:
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.UInt16:
                            case TypeCode.UInt32:
                            case TypeCode.UInt64:
                                return System.Convert.ToInt64(propertyValue) > System.Convert.ToInt64(ExpressionValue);

                            case TypeCode.Object:
                                break;
                            case TypeCode.SByte:
                                break;
                            case TypeCode.Single:
                                break;
                            case TypeCode.String:
                                break;
                            default:
                                break;
                        }
                    }

                    return propertyValueAsString.IsGreaterThan(expressionValueAsString);

                case ComparisonConditionEnum.GreaterThanOrEqualTo:
                    return propertyValueAsString.IsGreaterThanOrEqualTo(expressionValueAsString);

                case ComparisonConditionEnum.GreaterThanAny:
                    return propertyValueAsString.IsGreaterThanAnyOfTheFollowing(expressionValueAsString.Split(';'));

                case ComparisonConditionEnum.GreaterThanOrEqualToAny:
                    return propertyValueAsString.IsGreaterThanOrEqualToAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                case ComparisonConditionEnum.LessThan:
                    if (typeCodesMatch)
                    {
                        switch (propertyValueTypeCode)
                        {
                            case TypeCode.Boolean:
                                break;
                            case TypeCode.Byte:
                                break;
                            case TypeCode.Char:
                                break;
                            case TypeCode.DateTime:
                                break;
                            case TypeCode.DBNull:
                                break;
                            case TypeCode.Decimal:
                                break;
                            case TypeCode.Double:
                                break;
                            case TypeCode.Empty:
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.UInt16:
                            case TypeCode.UInt32:
                            case TypeCode.UInt64:
                                return System.Convert.ToInt64(propertyValue) < System.Convert.ToInt64(ExpressionValue);

                            case TypeCode.Object:
                                break;
                            case TypeCode.SByte:
                                break;
                            case TypeCode.Single:
                                break;
                            case TypeCode.String:
                                break;
                            default:
                                break;

                        }
                    }
                    return propertyValueAsString.IsLessThan(expressionValueAsString);

                //less than any
                case ComparisonConditionEnum.LessThanAny:
                    return propertyValueAsString.IsLessThanAnyOfTheFollowing(expressionValueAsString.Split(';'));

                //less than or equal to any
                case ComparisonConditionEnum.LessThanOrEqualToAny:
                    return propertyValueAsString.IsLessThanOrEqualToAnyOfTheFollowing(expressionValueAsString.Split(';'), CaseSensitive);

                default:
                    throw new Exception("Invalid comparison condition code");
            }
        }
        public override string ToString()
        {
            return $"[{PropertyName}] [{ComparisonCondition}] [{ExpressionValue}]";
        }
    }
}
