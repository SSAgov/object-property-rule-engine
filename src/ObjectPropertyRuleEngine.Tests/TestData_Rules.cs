using System;
using Xunit;
using ObjectPropertyRuleEngine;
using System.Data;

namespace ObjectPropertyRuleEngine.Tests
{
    public static partial class TestData_Rules
    {
        internal static Rule Rule001()
        {
            Rule r = new Rule("Datatype of TIMESTAMP or related is expected", 1, "System.Data.DataColumn");
            RuleExpressionGroup gAnt = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            
            gAnt.RuleExpressions.Add(new RuleExpression(DataStructureProperty.LogicalName,ComparisonConditionEnum.EndsWith,"time stamp"));
            gAnt.RuleExpressions.Add(new RuleExpression(DataStructureProperty.LogicalName,ComparisonConditionEnum.EndsWith,"timestamp"));
            r.Antecedent.RuleExpressionGroups.Add(gAnt);


            RuleExpressionGroup gCon = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            gCon.RuleExpressions.Add(new RuleExpression(DataStructureProperty.Datatype, ComparisonConditionEnum.Contains, "timestamp"));
            gCon.RuleExpressions.Add(new RuleExpression(DataStructureProperty.Datatype, ComparisonConditionEnum.EqualTo, "datetime2"));
            gCon.RuleExpressions.Add(new RuleExpression(DataStructureProperty.Datatype, ComparisonConditionEnum.EqualTo, "datetime"));
            gCon.RuleExpressions.Add(new RuleExpression(DataStructureProperty.Datatype, ComparisonConditionEnum.EqualTo, "date"));
            r.Consequent.RuleExpressionGroups.Add(gCon);
            r.OutcomeLevel = OutcomeLevelEnum.Expected;
            return r;
        }

        public static Rule Rule019()
        {
            Rule r = new Rule("An Employer Identifcation Number / EIN is 9 characters long.",19, "System.Data.DataColumn");
            
            RuleExpressionGroup g1 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            g1.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "Employer Identification Number"));
            g1.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "employer id number"));
            g1.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "employer identity number"));
            r.Antecedent.RuleExpressionGroups.Add(g1);

            RuleExpressionGroup g2 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            g2.RuleExpressions.Add(new RuleExpression("DatatypeName", ComparisonConditionEnum.Contains, "char"));
            g2.RuleExpressions.Add(new RuleExpression("DatatypeLength", ComparisonConditionEnum.EqualTo, 9));
            r.Consequent.RuleExpressionGroups.Add(g2);
            return r;
        }

        public static Rule Rule025_descriptions_should_be_textual()
        {
            Rule r = new Rule("Description fields should be of textual dataypes.", 25, "System.Data.DataColumn");
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWithAny, "description;text"));

            r.Consequent.RuleExpressions.Add(new RuleExpression("Datatype", ComparisonConditionEnum.ContainsAny, "char;text"));

            return r;
        }

        public static Rule Rule024_text_types()
        {
            Rule r = new Rule("All attributes with a textual datatype must define the datatype length", 50, "System.Data.DataColumn");
            
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWithAny, "name;description;code,line;address;text"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Datatype", ComparisonConditionEnum.Contains, "char"));
            return r;
        }

        internal static Rule Rule038_AddresslineLength()
        {
            Rule r = new Rule("Address line",38, "System.Data.DataColumn");
            
            RuleExpressionGroup g1 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln"));
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln_1"));
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln_2"));
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln_3"));
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln_4"));
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln_5"));
            g1.RuleExpressions.Add(new RuleExpression("PhysicalName", ComparisonConditionEnum.EndsWith, "addrln_6"));
            r.Antecedent.RuleExpressionGroups.Add(g1);

            RuleExpressionGroup g2 = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            g2.RuleExpressions.Add(new RuleExpression("DatatypeName", ComparisonConditionEnum.Contains, "char"));
            g2.RuleExpressions.Add(new RuleExpression("DatatypeLength", "=", 22));
            r.Consequent.RuleExpressionGroups.Add(g2);
            return r;
        }

        public static Rule Rule047_parens_need_value()
        {
            Rule r = new Rule("Datatypes should never have empty parenthesis; there should be a number in between.", 47, "System.Data.DataColumn");
            r.Antecedent.RuleExpressions.Add(new RuleExpression("Datatype", ComparisonConditionEnum.Contains, "()"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Datatype", ComparisonConditionEnum.Contains, "(some value)"));
            return r;
        }
        

        public static Rule Rule050_datatype_length_required()
        {
            Rule r = new Rule("All attributes with a textual datatype must define the datatype length", 50, "System.Data.DataColumn");
            
            r.Antecedent.RuleExpressions.Add(new RuleExpression("Datatype", ComparisonConditionEnum.Contains, "char"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("DatatypeLength", ComparisonConditionEnum.GreaterThan, 0));
            return r;
        }


        internal static Rule Rule051_Attributes_Must_be_defined()
        {
            Rule r = new Rule("All attributes must be defined.", 51, "System.Data.DataColumn");
            
            r.Antecedent.RuleExpressions.Add(new RuleExpression("Definition",ComparisonConditionEnum.EqualTo,""));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition","Is Not", ""));
            return r;
        }

        internal static Rule Rule057_email()
        {
            Rule r = new Rule("Email addresses (xyz@ssa.gov) should end with email address.", 3, "System.Data.DataColumn");
            r.Antecedent.RuleExpressionGroups.Add(TestData_RuleExpressionGroups.LogicalNameEndsWithEmailVariants());

            RuleExpressionGroup gCon = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            gCon.RuleExpressions.Add(new RuleExpression(DataStructureProperty.DatatypeLength, ComparisonConditionEnum.GreaterThan, 50));
            gCon.RuleExpressions.Add(new RuleExpression(DataStructureProperty.LogicalName, ComparisonConditionEnum.EndsWith, "Address"));
            r.Consequent.RuleExpressionGroups.Add(gCon);

            return r;
        }

        internal static Rule Rule059_countryName()
        {
            Rule r = new Rule("Country Name should be used when referring to the name of a country.", 59, "System.Data.DataColumn");
            
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName",ComparisonConditionEnum.EndsWith,"Country"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "Country Name"));
            return r;

        }

        internal static Rule Rule060_utc_gmt()
        {
            Rule r = new Rule("Universal Time Coordinated should be used instead of Greenwich mean Time", 60, "System.Data.DataColumn");

            r.Antecedent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "Greenwich mean time"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "Grenwich mean time"));

            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Consequent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "Universal Time Coordinated"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "Coordinated Universal Time"));

            return r;
        }

        internal static Rule Rule070_from_irs()
        {
            Rule r = new Rule("Data that comes from the Internal Revenue Service (IRS) should begin with IRS. Rule request by Brian P 9/20/2011", 70, "System.Data.DataColumn");
            
            RuleExpressionGroup gAnt = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            gAnt.RuleExpressions.Add(new RuleExpression("Definition", ComparisonConditionEnum.Contains, "from irs"));
            gAnt.RuleExpressions.Add(new RuleExpression("Definition", ComparisonConditionEnum.Contains, "from internal revenue service"));
            gAnt.RuleExpressions.Add(new RuleExpression("Definition", ComparisonConditionEnum.Contains, "from the irs"));
            gAnt.RuleExpressions.Add(new RuleExpression("Definition", ComparisonConditionEnum.Contains, "from the internal revenue service"));
            r.Antecedent.RuleExpressionGroups.Add(gAnt);

            RuleExpressionGroup gCon = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            gCon.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.StartsWith, "IRS"));
            gCon.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.StartsWith, "Internal Revenue Service"));
            r.Consequent.RuleExpressionGroups.Add(gCon);
            return r;
        }

        internal static Rule Rule072_appointedrep_not_auth_rep()
        {
            Rule r = new Rule("Authorized Representative should now be appointed representatives.", 72, "System.Data.DataColumn");
            
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "authorized rep"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "Appointed Rep"));
            return r;
        }

        internal static Rule Rule079_A_field_that_contains_TSO()
        {
            Rule rule = new Rule("A field that contains a TSO ID should be a character datatype with a length of 5.", 79, "System.Data.DataColumn");
            
            rule.Antecedent.RuleExpressions.Add(TestData_RuleExpressions.PhysicalName_EndsWith_Term("tsoid"));
            RuleExpressionGroup g = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            g.RuleExpressions.Add(new RuleExpression("Datatype", ComparisonConditionEnum.Contains, "char"));
            g.RuleExpressions.Add(new RuleExpression("DatatypeLength", ComparisonConditionEnum.EqualTo, 5));
            rule.Consequent.RuleExpressionGroups.Add(g);
            return rule;
        }

        public static Rule Rule081_forms()
        {
            Rule r = new Rule("Definitions should not use SSA form numbers. If a definition does contain a SSA form number, then that form's purpose needs to be explained.",81, "System.Data.DataColumn");
            r.Antecedent.RuleExpressions.Add(new RuleExpression("Definition", ComparisonConditionEnum.Contains, "SSA-"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", ComparisonConditionEnum.Contains, "If a definition contains an SSA-#### form, then that form needs to be described."));
            r.OutcomeLevel = OutcomeLevelEnum.Info;

            return r;
        }

        public static Rule Rule085_switches_shouldntbe_null()
        {
            Rule r = new Rule("Switches should not allow for null values. Allowed values are Y or N.", 85);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "Contains", "switch"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.And;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition","DoesNotContain","null,"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Nullable","IsNot",true));
            return r;
        }


        public static Rule Rule089_Character_dates()
        {
            Rule r = new Rule("All character dates should be of a textual datatype with a data length of 8.", 89, "System.Data.DataColumn");
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName",ComparisonConditionEnum.EndsWith,"Character Date"));

            RuleExpressionGroup g = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            g.RuleExpressions.Add(new RuleExpression("DatatypeLength",ComparisonConditionEnum.EqualTo,8));
            g.RuleExpressions.Add(new RuleExpression("Datatype",ComparisonConditionEnum.Contains,"char"));
            r.Consequent.RuleExpressionGroups.Add(g);

            return r;
        }

        internal static Rule Rule091_long_codes()
        {
            Rule r = new Rule("Codes with unusually long datatype lengths.", 91, "System.Data.DataColumn");
            
            RuleExpressionGroup gInner = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.Or);
            gInner.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "code"));
            gInner.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.EndsWith, "type"));

            RuleExpressionGroup gOuter = new RuleExpressionGroup(RuleExpressionGroup.LogicOperatorEnum.And);
            gOuter.RuleExpressionGroups.Add(gInner);
            gOuter.RuleExpressions.Add(new RuleExpression("DatatypeLength", ComparisonConditionEnum.GreaterThan, 5));
            r.Antecedent.RuleExpressionGroups.Add(gOuter);

            r.Consequent.RuleExpressions.Add(new RuleExpression("DatatypeLength", ComparisonConditionEnum.EqualTo, "a questionable length"));
            return r;
        }

        public static Rule Rule092_ColumnsMustEndWithAClassWord()
        {
            Rule r = new Rule("All logical names are expected to end with a class term, or a combination thereof, that semantically represents the data type of a data element.", 92, "System.Data.DataColumn");
            
            r.Consequent.RuleExpressions.Add(TestData_RuleExpressions.LogicalName_EndsWithAny_CLASSWORDS());
            return r;
        }
        public static Rule Rule103_class()
        {
            Rule r = new Rule("Classword: Address", 103);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Address"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The location"));
            return r;
        }
        public static Rule Rule104_class()
        {
            Rule r = new Rule("Classword: Amount", 104);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Amount"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The monetary value specified in"));
            return r;
        }

        public static Rule Rule105_class()
        {
            Rule r = new Rule("Classword: Code", 105);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Code"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "Identifies"));
            return r;
        }
        public static Rule Rule106_class()
        {
            Rule r = new Rule("Classword: Count", 106);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Count"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The cumulative number of"));
            return r;
        }

        public static Rule Rule107_class()
        {
            Rule r = new Rule("Classword: Date", 107);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Date"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The month, day and year"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The month, day, and year"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The month day and year"));
            return r;
        }


        public static Rule Rule108_class()
        {
            Rule r = new Rule("Classword: Description", 108);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Description"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "Textual description of"));
            return r;
        }
        public static Rule Rule109_class()
        {
            Rule r = new Rule("Classword: File", 109);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "File"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.And;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "Contains", "of"));
            return r;
        }
        public static Rule Rule110_class()
        {
            Rule r = new Rule("Classword: Identifier", 110);
            r.Antecedent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.And;
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Identifier"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", " ID"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The value assigned to"));
            return r;
        }
        public static Rule Rule111_class()
        {
            Rule r = new Rule("Classword: Indicator", 111);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Indicator"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "A positive, negative, or unknown value that represents"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "A positive, negative or unknown value that represents"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "A positive negative or unknown value that represents"));
            return r;
        }

        public static Rule Rule112_class()
        {
            Rule r = new Rule("Classword: Line", 112);
            r.Antecedent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;

            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line 1"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line 2"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line 3"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line 4"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line 5"));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Line 6"));

            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.And;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "Starts With", "The"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "Contains", "line of"));
            return r;
        }

        public static Rule Rule113_class()
        {
            Rule r = new Rule("Classword: Name", 113);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Name"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The name"));
            return r;
        }

        public static Rule Rule114_class()
        {
            Rule r = new Rule("Classword: Number", 114);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Number"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "Identifies"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The numeric value of"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The value assigned to"));
            return r;
        }
        public static Rule Rule115_class()
        {
            Rule r = new Rule("Classword: Percent", 115);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Percent"));

            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.And;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The number of"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "Contains", "based on the parts per hundred of"));
            return r;
        }

        public static Rule Rule116_class()
        {
            Rule r = new Rule("Classword: Rate", 116);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Rate"));
            r.Consequent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.And;
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The number of"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "Contains", "per"));
            return r;
        }


        public static Rule Rule117_class()
        {
            Rule r = new Rule("Classword: Switch", 117);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Switch"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "A positive or negative value that"));
            return r;
        }

        public static Rule Rule118_class()
        {
            Rule r = new Rule("Classword: Text", 118);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Text"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "Free form text representing"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "Freeform text representing"));
            return r;
        }

        public static Rule Rule119_class()
        {
            Rule r = new Rule("Classword: Time", 119);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Time"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The time"));
            return r;
        }

        public static Rule Rule120_class()
        {
            Rule r = new Rule("Classword: Timestamp", 120);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Timestamp"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The date and time"));
            return r;
        }

        public static Rule Rule121_class()
        {
            Rule r = new Rule("Classword: Year", 121);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Year"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "StartsWith", "The year"));
            return r;
        }



        internal static Rule Rule123_ColumnsMustBeMoreThanAClassWord()
        {
            Rule r = new Rule("Attribute names need to be more than class words.", 123, "System.Data.DataColumn");
            
            r.Antecedent.RuleExpressions.Add(TestData_RuleExpressions.LogicalName_EndsWithAny_CLASSWORDS());
            r.Consequent.RuleExpressions.Add(new RuleExpression("LogicalName", ComparisonConditionEnum.Contains, "more than a classword"));
            return r;
        }



        public static Rule Columns_LogicalName_EndInTimeStamp_ExpectAppropriatePhysicalDatatype()
        {
            Rule r = new Rule();
            r.ID = 2;
            r.Description = "Expect columns that end in Timestamp or Time Stamp to be a datetime or related datatype";
            r.AppliesToTypeName = typeof(System.Data.DataColumn).FullName;

            r.Antecedent.LogicOperator = RuleExpressionGroup.LogicOperatorEnum.Or;
            r.Antecedent.RuleExpressions.Add(TestData_RuleExpressions.LogicalName_EndsWith_Timestamp());
            r.Antecedent.RuleExpressions.Add(TestData_RuleExpressions.LogicalName_EndsWith_Time_Stamp());
            r.Antecedent.RuleExpressions.Add(TestData_RuleExpressions.Caption_EndsWith_Time_Stamp());

            r.Consequent.RuleExpressions.Add(TestData_RuleExpressions.Physical_Datatype_Is_Timestamp());
            return r;
        }

        public static Rule Rule129_see()
        {
            Rule r = new Rule("SEE Term: Address Location Code", 129);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Address Location Code"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "Identifies the address as foreign or domestic."));
            return r;
        }

        public static Rule Rule130_see()
        {
            Rule r = new Rule("SEE Term: Address Purpose Code", 130);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Address Purpose Code"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "Identifies what purpose the address is being collected for."));
            return r;
        }
        public static Rule Rule131_see()
        {
            Rule r = new Rule("SEE Term: Address Use Code", 131);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Address Use Code"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "Identifies the type of address as to its use by the Social Security Administration."));
            return r;
        }
        public static Rule Rule132_see()
        {
            Rule r = new Rule("SEE Term: Average Current Earnings Method Code", 132);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Average Current Earnings Method Code"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "Identifies the method chosen by which the average current earnings (ACE) are computed for worker's compensation or public disability benefits offset."));
            return r;
        }

        public static Rule Rule133_see()
        {
            Rule r = new Rule("SEE Term: Birth Date", 133);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Birth Date"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "The month, day and year of a person's birth."));
            return r;
        }

        public static Rule Rule134_see()
        {
            Rule r = new Rule("SEE Term: Case Number", 134);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "EndsWith", "Case Number"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "BeginsWith", "The value assigned to the"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "Contains", "case"));
            return r;
        }

        public static Rule Rule135_see()
        {
            Rule r = new Rule("SEE Term: Case Number", 135);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Change Of Intent Full Time Attendance Code"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "Identifies the type of date used to determine the intent to return to school full time."));
            return r;
        }

        public static Rule Rule136_see()
        {
            Rule r = new Rule("SEE Term: Case Number", 136);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", "Character Date"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("Definition", "=", "A non-validated date stored as a character string."));
            return r;
        }

        public static Rule Rule200_defaultDatatype()
        {
            Rule r = new Rule("Default Dtatype", 200);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("DatatypeLength", "=", 18));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("DatatypeName", "contains", "char"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("DatatypeLength", "<>", 18));
            r.OutcomeLevel = OutcomeLevelEnum.Info;
            return r;
        }

        public static Rule Rule201_DoubleSpacesInLogicalName()
        {
            Rule r = new Rule("Default Dtatype", 201);
            r.Antecedent.RuleExpressions.Add(new RuleExpression("LogicalName", "=", 18));
            r.Antecedent.RuleExpressions.Add(new RuleExpression("DatatypeName", "contains", "char"));
            r.Consequent.RuleExpressions.Add(new RuleExpression("DatatypeLength", "<>", 18));
            r.OutcomeLevel = OutcomeLevelEnum.Info;
            return r;
        }

    }
}
