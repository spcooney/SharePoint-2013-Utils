namespace Spc.SharePoint.Utils.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains methods for checking if a value is null or within a specified range.
    /// </summary>
    public class NullUtil
    {
        #region "Properties"

        /// <summary>"Empty string: {0}, are not allowed."</summary>
        private const string EMPTY_STRING_NOT_ALLOWED = "Empty string: {0}, are not allowed.";

        /// <summary>"Value: {0}, must be greathar than zero."</summary>
        private const string VALUE_GREATER_THAN_ZERO = "Value: {0}, must be greathar than zero.";

        /// <summary>"Value: {0}, cannot be zero."</summary>
        private const string VALUE_EQUAL_TO_ZERO = "Value: {0}, cannot be zero.";

        /// <summary>"Value: {0}, cannot be null or zero."</summary>
        private const string VALUE_NULL_OR_EQUAL_TO_ZERO = "Value: {0}, cannot be null or zero.";

        /// <summary>"Value: {0}, cannot be null and zero."</summary>
        private const string VALUE_NULL_AND_EQUAL_TO_ZERO = "Value: {0}, cannot be null and zero.";

        /// <summary>"Value: {0}, must not be null."</summary>
        private const string VALUE_IS_NULL = "Value: {0}, must not be null.";

        /// <summary>"Value: {0}, must not be less than {1} or greater than {2}."</summary>
        private const string VALUE_LESS_THAN_OR_GREATER = "Value: {0}, must not be less than {1} or greater than {2}.";

        /// <summary>"Value: {0}, must not be less than {1}."</summary>
        private const string VALUE_LESS_THAN = "Value: {0}, must not be less than {1}.";

        #endregion

        #region "Methods"

        /// <summary>
        /// Determines if the specified DateTime falls within the minimum or maximum values.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified DateTime is outside the minumum and maxiumum range.
        /// </summary>
        /// <param name="value">The DateTime to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="minimum">The minimum value.  Will check less than.</param>
        /// <param name="maximum">The maximum value.  Will check greater than.</param>
        public static void CheckForDateTimeRange(DateTime value, string varName, DateTime minimum, DateTime maximum)
        {
            if ((value < minimum) || (value > maximum))
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_LESS_THAN_OR_GREATER, varName, minimum, maximum);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified long value is less than the minimum value.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified long is outside the minumum range.
        /// </summary>
        /// <param name="value">The long to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="minimum">The minimum value.  Will check less than.</param>
        public static void CheckForOutOfRange(long value, string varName, long minimum)
        {
            if (value < minimum)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_LESS_THAN, varName, minimum);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified long value is less than or equal to the minimum value.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified long is outside the minumum range.
        /// </summary>
        /// <param name="value">The long to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="minimum">The minimum value.  Will check less than or equal.</param>
        public static void CheckForEqualOutOfRange(long value, string varName, long minimum)
        {
            if (value <= minimum)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_LESS_THAN, varName, minimum);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified long falls within the minimum or maximum values.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified long is outside the minumum and maxiumum range.
        /// </summary>
        /// <param name="value">The long to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="minimum">The minimum value.  Will check less than.</param>
        /// <param name="maximum">The maximum value.  Will check greater than.</param>
        public static void CheckForOutOfRange(long value, string varName, long minimum, long maximum)
        {
            if ((value < minimum) || (value > maximum))
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_LESS_THAN_OR_GREATER, varName, minimum, maximum);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified int value is less than the minimum value.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified int is outside the minumum range.
        /// </summary>
        /// <param name="value">The int to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="minimum">The minimum value.  Will check less than.</param>
        public static void CheckForOutOfRange(int value, string varName, int minimum)
        {
            if (value < minimum)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_LESS_THAN, varName, minimum);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified integer falls within the minimum or maximum values.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified integer is outside the minumum and maxiumum range.
        /// </summary>
        /// <param name="value">The integer to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="minimum">The minimum value.  Will check less than.</param>
        /// <param name="maximum">The maximum value.  Will check greater than.</param>
        public static void CheckForOutOfRange(int value, string varName, int minimum, int maximum)
        {
            if ((value < minimum) || (value > maximum))
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_LESS_THAN_OR_GREATER, varName, minimum, maximum);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(varName);
            }
        }

        /// <summary>
        /// Compares value1 against value2 based on the specified ComparisonOperator.
        /// </summary>
        /// <param name="comparison">The type of ComparisonOperator.</param>
        /// <param name="value1">The integer value on the left-hand side of the operator.</param>
        /// <param name="value2">The integer value on the right-hand side of the operator.</param>
        /// <param name="varName1">The value1 variable name.</param>
        public static void CheckForOutOfRange(ComparisonOperator comparison, int value1, int value2, string varName1)
        {
            switch (comparison)
            {
                case ComparisonOperator.GreaterThan:
                    {
                        if (value1 > value2)
                        {
                            throw new ArgumentOutOfRangeException(varName1);
                        }
                        break;
                    }
                case ComparisonOperator.GreaterThanEqualTo:
                    {
                        if (value1 >= value2)
                        {
                            throw new ArgumentOutOfRangeException(varName1);
                        }
                        break;
                    }
                case ComparisonOperator.LessThan:
                    {
                        if (value1 < value2)
                        {
                            throw new ArgumentOutOfRangeException(varName1);
                        }
                        break;
                    }
                case ComparisonOperator.LessThanEqualTo:
                    {
                        if (value1 <= value2)
                        {
                            throw new ArgumentOutOfRangeException(varName1);
                        }
                        break;
                    }
                case ComparisonOperator.EqualTo:
                    {
                        if (value1 == value2)
                        {
                            throw new ArgumentOutOfRangeException(varName1);
                        }
                        break;
                    }
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Determines if the specified float is less than zero.  This method will throw an 
        /// ArgumentOutOfRangeException if the specified float is outside the minumum range.
        /// </summary>
        /// <param name="value">The float to compare.</param>
        /// <param name="varName">The variable name.</param>
        public static void CheckGreaterThanOrEqualToZero(float value, string varName)
        {
            if (value < 0f)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_GREATER_THAN_ZERO, varName);
                Log4NetHelper.LogError(msg);
                throw new ArgumentOutOfRangeException(msg, varName);
            }
        }

        /// <summary>
        /// Determines if the specified string is null or empty.  This method will throw an 
        /// ArgumentOutOfRangeException if the string is null or empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="varName">The variable name.</param>
        public static void CheckStringForNullOrEmpty(string value, string varName)
        {
            CheckStringForNullOrEmpty(value, varName, false);
        }

        /// <summary>
        /// Determines if the specified string is null or empty.  This method will throw an 
        /// ArgumentOutOfRangeException if the string is null or empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="shouldTrim">True, if the string should be trimmed before the comparison.</param>
        public static void CheckStringForNullOrEmpty(string value, string varName, bool shouldTrim)
        {
            CheckForNull(value, varName);
            if (shouldTrim)
            {
                value = value.Trim();
            }
            if (value.Length == 0)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.EMPTY_STRING_NOT_ALLOWED, varName);
                Log4NetHelper.LogError(msg);
                throw new ArgumentNullException(msg, varName);
            }
        }

        /// <summary>
        /// Determines if the specified object is null.  This method will throw an 
        /// ArgumentNullException if the string is null or empty.
        /// </summary>
        /// <param name="value">The object to compare.</param>
        /// <param name="varName">The variable name.</param>
        public static void CheckForNull(object value, string varName)
        {
            if (value == null)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_IS_NULL, varName);
                Log4NetHelper.LogError(msg);
                throw new ArgumentNullException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified integer is equal to zero.  This method will throw an 
        /// ArgumentNullException if the integer is equal to zero.
        /// </summary>
        /// <param name="value">The integer to compare.</param>
        /// <param name="varName">The variable name.</param>
        public static void CheckForZero(int value, string varName)
        {
            if (value == 0)
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_EQUAL_TO_ZERO, varName);
                Log4NetHelper.LogError(msg);
                throw new ArgumentNullException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified object is null or equal to zero.  This method will throw an 
        /// ArgumentNullException if the object is null or equal to zero.
        /// </summary>
        /// <param name="value">The object to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="length">The length of the object.</param>
        public static void CheckForNullOrLength(object value, string varName, int length)
        {
            if ((value == null) || (length == 0))
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_NULL_OR_EQUAL_TO_ZERO, varName);
                Log4NetHelper.LogError(msg);
                throw new ArgumentNullException(varName);
            }
        }

        /// <summary>
        /// Determines if the specified object is null and equal to zero.  This method will throw an 
        /// ArgumentNullException if the object is null and equal to zero.
        /// </summary>
        /// <param name="value">The object to compare.</param>
        /// <param name="varName">The variable name.</param>
        /// <param name="length">The length of the object.</param>
        public static void CheckForNullAndLength(object value, string varName, int length)
        {
            if ((value == null) && (length == 0))
            {
                string msg = String.Format(CultureInfo.InvariantCulture, NullUtil.VALUE_NULL_AND_EQUAL_TO_ZERO, varName);
                Log4NetHelper.LogError(msg);
                throw new ArgumentNullException(varName);
            }
        }
    }

        #endregion

    #region "Enums"

    public enum ComparisonOperator
    {
        GreaterThan,
        GreaterThanEqualTo,
        LessThan,
        LessThanEqualTo,
        EqualTo
    }

    #endregion
}
