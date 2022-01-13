using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

public static class DAL
{
    public static string GetTypeName(Type type)
    {
        var nullableType = Nullable.GetUnderlyingType(type);

        bool isNullableType = nullableType != null;

        if (isNullableType)
            return nullableType.Name;
        else
            return type.Name;
    }

    private static void SetApiSettingValue(object source, string propertyName, object valueToSet)
    {
        // find out the type
        Type type = source.GetType();

        // get the property information based on the type
        System.Reflection.PropertyInfo property = type.GetProperty(propertyName);

        // Convert.ChangeType does not handle conversion to nullable types
        // if the property type is nullable, we need to get the underlying type of the property
        Type propertyType = property.PropertyType;
        var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;

        // special case for enums
        if (targetType.IsEnum)
        {
            // we could be going from an int -> enum so specifically let
            // the Enum object take care of this conversion
            if (valueToSet != null)
            {
                valueToSet = Enum.ToObject(targetType, valueToSet);
            }
        }
        else
        {
            // returns an System.Object with the specified System.Type and whose value is
            // equivalent to the specified object.
            valueToSet = Convert.ChangeType(valueToSet, targetType);
        }

        // set the value of the property
        property.SetValue(source, valueToSet, null);
    }
    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }

    public static void SetItemFromRow<T>(T item, DataRow row)
    where T : new()
    {
        // go through each column
        foreach (DataColumn c in row.Table.Columns)
        {
            // find the property for the column
            PropertyInfo p = item.GetType().GetProperty(c.ColumnName);
            
            if (p != null && row[c] != DBNull.Value)
            {
                try
                {
                    p.SetValue(item, row[c], null);
                    
                }
                catch(Exception)
                {
                    Type type = p.PropertyType;
                    if (GetTypeName(type) == "DateTime")
                    {
                        if (Nullable.GetUnderlyingType(type) != null)
                        {
                            //p.SetValue(item, (DateTime?)(DateTime)row[c], null);
                            //p.SetValue(item, row[c], null);
                            p.SetValue(item, Convert.ToDateTime(row[c].ToString()), null);
                        }
                        else
                        {
                            p.SetValue(item, Convert.ToDateTime(row[c].ToString()), null);
                        }
                    }
                }
               
            }
        }
    }
    // function that creates an object from the given data row
    public static T CreateItemFromRow<T>(DataRow row) where T : new()
    {
        // create a new object
        T item = new T();

        // set the item
        SetItemFromRow(item, row);

        // return 
        return item;
    }
    public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
    {
        // define return list
        List<T> lst = new List<T>();

        // go through each row
        foreach (DataRow r in tbl.Rows)
        {
            // add to the list
            lst.Add(CreateItemFromRow<T>(r));
        }

        // return the list
        return lst;
    }

    // get a single customer in a data row
    //DataRow customerRow = GetSingleCustomer(3); // select * from customer where id = 3

    // map to a customer
    //Customer customer = CreateItemFromRow(customerRow);
}
