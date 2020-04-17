using System.Data;

namespace ObjectPropertyRuleEngine.Tests
{
    public static partial class TestData
    {
        public static DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            ds.ExtendedProperties.Add("DatabaseManagementSystemCode", "DB2");

            DataTable dtPerson = new DataTable();
            dtPerson.AddNewDataColumnWithExtendedProperties("Person ID", "PRSN_ID", "int",false, "The Id of the person");
            dtPerson.AddNewDataColumnWithExtendedProperties("First Name", "FNM", "char(50)", true,"The first name of the person");
            dtPerson.AddNewDataColumnWithExtendedProperties("Last Name", "LNM", "char(50)", true, "The last name of the person");
            dtPerson.AddNewDataColumnWithExtendedProperties("Birthdate", "DOB", "DATE", true, "When the person was born");
            dtPerson.AddNewDataColumnWithExtendedProperties("Last Eat", "LEAT", "DATE", true, "The last time the person ate");
            dtPerson.AddNewDataColumnWithExtendedProperties("Email", "EMAIL", "varchar", true, "The last time the person ate");
            ds.Tables.Add(dtPerson);

            DataTable dtCar = new DataTable();
            dtCar.AddNewDataColumnWithExtendedProperties("Car ID", "CAR_ID", "int", true, "The Id of the car");
            dtCar.AddNewDataColumnWithExtendedProperties("Color Code", "COLOR_CD", "char(50)", true, "The code taht represents a color");
            ds.Tables.Add(dtCar);

            DataTable dtIncompleteColumntable = new DataTable();
            DataColumn c = new DataColumn("TheColumn");
            c.ExtendedProperties["LogicalName"] = "Insert Time Stamp";
            c.ExtendedProperties["PhysicalDatatype"] = "Timestamp";
            dtIncompleteColumntable.Columns.Add(c);
            dtIncompleteColumntable.Columns.Add(new DataColumn("ColumnWithNothingElseSetButItsName"));

            return ds;
        }
    }
}
