namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Utility class for dealing with null data types within a database.
    /// </summary>
    public class DbNullUtil
    {
        #region "Properties"

        private static bool _nullBoolean = false;
        private static byte _nullByte = 255;
        private static byte[] _nullBytes = new byte[255];
        private static DateTime _nullDateTime = DateTime.MinValue;
        private static decimal _nullDecimal = -79228162514264337593543950335M;
        private static double _nullDouble = Double.MinValue;
        private static Guid _nullGuid = Guid.Empty;
        private static Guid _tempGuid = new Guid("11111111-1111-1111-1111-111111111111");
        private static Int16 _nullInt16 = -1;
        private static Int32 _nullInt32 = -1;
        private static Int32 _nullInt64 = -1;
        private static Single _nullSingle = Single.MinValue;
        private static string _nullString = String.Empty;

        #endregion

        #region "Methods"

        /// <summary>
        /// Checks if the field is null.
        /// </summary>
        /// <param name="objField">The field to check if null.</param>
        /// <param name="objDbNull">The database null object.</param>
        /// <returns></returns>
        public static object GetNull(object objField, object objDbNull)
        {
            if (objField == null)
            {
                return objDbNull;
            }
            object obj1 = objField;
            if (objField is byte)
            {
                if (Convert.ToByte(objField) == _nullByte)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is short)
            {
                if (Convert.ToInt16(objField) == _nullInt16)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is int)
            {
                if (Convert.ToInt32(objField) == _nullInt32)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is float)
            {
                if (Convert.ToSingle(objField) == _nullSingle)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is double)
            {
                if (Convert.ToDouble(objField) == _nullDouble)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is decimal)
            {
                if (Convert.ToDecimal(objField) == _nullDecimal)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is DateTime)
            {
                if (Convert.ToDateTime(objField) == _nullDateTime.Date)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is bool)
            {
                if (Convert.ToBoolean(objField) == _nullBoolean)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is Guid)
            {
                Guid guid = (Guid)objField;
                if (guid.Equals(_nullGuid))
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            if (objField is string)
            {
                if (objField == null)
                {
                    return objDbNull;
                }
                if (objField.ToString() == _nullString)
                {
                    obj1 = objDbNull;
                }
                return obj1;
            }
            return obj1;
        }

        /// <summary>
        /// Determines if the specified object is null.
        /// </summary>
        /// <param name="objField">The field to check for null.</param>
        /// <returns>If null, return true.  Otherwise, return false.</returns>
        public static bool IsNull(object objField)
        {
            if (objField != null)
            {
                if (objField is int)
                {
                    return objField.Equals(_nullInt32);
                }
                if (objField is short)
                {
                    return objField.Equals(_nullInt16);
                }
                if (objField is byte)
                {
                    return objField.Equals(_nullByte);
                }
                if (objField is float)
                {
                    return objField.Equals(_nullSingle);
                }
                if (objField is double)
                {
                    return objField.Equals(_nullDouble);
                }
                if (objField is decimal)
                {
                    return objField.Equals(_nullDecimal);
                }
                if (objField is string)
                {
                    return objField.Equals(_nullString);
                }
                if (objField is bool)
                {
                    return objField.Equals(_nullBoolean);
                }
                if (objField is DateTime)
                {
                    DateTime dt = (DateTime)objField;
                    return dt.Date.Equals(_nullDateTime.Date);
                }
                return ((objField is Guid) && (objField.Equals(_nullGuid)));
            }
            return true;
        }

        public static bool IsNotNull(object objField)
        {
            return !(IsNull(objField));
        }

        public static object SetNull(PropertyInfo objPropertyInfo)
        {
            switch (objPropertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                    return _nullInt16;
                case "System.Int32":
                    return _nullInt32;
                case "System.Int64":
                    return _nullInt32;
                case "System.Byte":
                    return _nullByte;
                case "System.Single":
                    return _nullSingle;
                case "System.Double":
                    return _nullDouble;
                case "System.Decimal":
                    return _nullDecimal;
                case "System.DateTime":
                    return _nullDateTime;
                case "System.String":
                    return _nullString;
                case "System.Char":
                    return _nullString;
                case "System.Boolean":
                    return _nullBoolean;
                case "System.Guid":
                    return _nullGuid;
            }
            Type type = objPropertyInfo.PropertyType;
            if (type.BaseType.Equals(typeof(Enum)))
            {
                Array values = Enum.GetValues(type);
                Array.Sort(values);
                return Enum.ToObject(type, values.GetValue(0));
            }
            return null;
        }

        public static object SetNull(object objValue, object objField)
        {
            if (objValue == DBNull.Value)
            {
                if (objField is short)
                {
                    return _nullInt16;
                }
                if (objField is byte)
                {
                    return _nullByte;
                }
                if (objField is int)
                {
                    return _nullInt32;
                }
                if (objField is float)
                {
                    return _nullSingle;
                }
                if (objField is double)
                {
                    return _nullDouble;
                }
                if (objField is decimal)
                {
                    return _nullDecimal;
                }
                if (objField is string)
                {
                    return _nullString;
                }
                if (objField is bool)
                {
                    return _nullBoolean;
                }
                if (objField is Guid)
                {
                    return _nullGuid;
                }
                if (objField is DateTime)
                {
                    return _nullDateTime;
                }
                return null;
            }
            return objValue;
        }

        public static bool SetNullBoolean(object objValue)
        {
            bool nullBool = _nullBoolean;
            if (objValue != DBNull.Value)
            {
                nullBool = Convert.ToBoolean(objValue);
            }
            return nullBool;
        }

        public static int SetNullInteger(object objValue)
        {
            int nullInt32 = _nullInt32;
            if (objValue != DBNull.Value)
            {
                nullInt32 = Convert.ToInt32(objValue);
            }
            return nullInt32;
        }

        public static float SetNullSingle(object objValue)
        {
            float nullFloat = _nullSingle;
            if (objValue != DBNull.Value)
            {
                nullFloat = Convert.ToSingle(objValue);
            }
            return nullFloat;
        }

        public static short SetNullShort(object objValue)
        {
            short nullShort = _nullInt16;
            if (objValue != DBNull.Value)
            {
                nullShort = Convert.ToInt16(objValue);
            }
            return nullShort;
        }

        public static decimal SetNullDecimal(object objValue)
        {
            decimal nullDecimal = _nullDecimal;
            if (objValue != DBNull.Value)
            {
                nullDecimal = Convert.ToDecimal(objValue);
            }
            return nullDecimal;
        }

        public static double SetNullDouble(object objValue)
        {
            double nullDouble = _nullDouble;
            if (objValue != DBNull.Value)
            {
                nullDouble = Convert.ToDouble(objValue);
            }
            return nullDouble;
        }

        public static byte SetNullByte(object objValue)
        {
            byte nullByte = _nullByte;
            if (objValue != DBNull.Value)
            {
                nullByte = Convert.ToByte(objValue);
            }
            return nullByte;
        }

        public static string SetNullString(object objValue)
        {
            string nullString = _nullString;
            if (objValue != DBNull.Value)
            {
                nullString = Convert.ToString(objValue);
            }
            return nullString;
        }

        public static Guid SetNullGuid(object objValue)
        {
            Guid nullGuid = _nullGuid;
            if (!((objValue == DBNull.Value) || StringUtil.IsNullOrWhitespace(objValue.ToString().Trim())))
            {
                nullGuid = new Guid(objValue.ToString());
            }
            return nullGuid;
        }

        public static DateTime SetNullDateTime(object objValue)
        {
            DateTime nullDT = _nullDateTime;
            if (objValue != DBNull.Value)
            {
                nullDT = Convert.ToDateTime(objValue);
            }
            return nullDT;
        }

        public static object ObjectOrDbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return value;
        }

        public static string IsNullDecimalThenEmpty(decimal value)
        {
            if (value == NullDecimal)
            {
                return String.Empty;
            }
            return value.ToString();
        }

        public static string IsNullDecimalThenEmpty(decimal? value)
        {
            if ((!value.HasValue) || (value == NullDecimal))
            {
                return String.Empty;
            }
            return value.ToString();
        }

        public static string IsNullDecimalThen(decimal value, string defVal)
        {
            if (value == NullDecimal)
            {
                return defVal;
            }
            return value.ToString();
        }

        public static decimal IsZeroDecimalThenNull(decimal value)
        {
            if ((value == null) || (value <= 0))
            {
                return DbNullUtil.NullDecimal;
            }
            return value;
        }

        #endregion

        #region "Accessors"

        /// <summary>
        /// false
        /// </summary>
        public static bool NullBoolean
        {
            get { return _nullBoolean; }
        }

        public static byte NullByte
        {
            get { return _nullByte; }
        }

        public static byte[] NullBytes
        {
            get { return _nullBytes; }
        }

        /// <summary>
        /// 1/1/0001 12:00:00 AM
        /// </summary>
        public static DateTime NullDateTime
        {
            get { return _nullDateTime; }
        }

        /// <summary>
        /// -79228162514264337593543950335
        /// </summary>
        public static decimal NullDecimal
        {
            get { return _nullDecimal; }
        }

        /// <summary>
        /// -3.40282347E+38F
        /// </summary>
        public static Single NullSingle
        {
            get { return _nullSingle; }
        }

        /// <summary>
        /// -1.7976931348623157E+308
        /// </summary>
        public static double NullDouble
        {
            get { return _nullDouble; }
        }

        /// <summary>
        /// 00000000-0000-0000-0000-000000000000
        /// </summary>
        public static Guid NullGuid
        {
            get { return DbNullUtil._nullGuid; }
        }

        /// <summary>
        /// 11111111-1111-1111-1111-111111111111
        /// </summary>
        public static Guid TempGuid
        {
            get { return DbNullUtil._tempGuid; }
        }

        /// <summary>
        /// -1
        /// </summary>
        public static Int16 NullInt16
        {
            get { return DbNullUtil._nullInt16; }
        }

        /// <summary>
        /// -1
        /// </summary>
        public static Int32 NullInt32
        {
            get { return DbNullUtil._nullInt32; }
        }

        /// <summary>
        /// -1
        /// </summary>
        public static Int64 NullInt64
        {
            get { return DbNullUtil._nullInt64; }
        }

        /// <summary>
        /// ""
        /// </summary>
        public static string NullString
        {
            get { return DbNullUtil._nullString; }
        }

        #endregion
    }
}