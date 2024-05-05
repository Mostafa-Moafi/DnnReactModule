// MIT License

using System;
using System.Linq;

namespace DnnReactModule.Utilities.Helper
{
    /// <summary>
    /// Helper class for string manipulation and conversion.
    /// </summary>
    /// <returns>
    /// Various extension methods to check if a string has value, convert a string to int, decimal, or double.
    /// </returns>
    public static class StringHelper
    {
        /// <summary>
        /// Checks if the input string has a value, optionally ignoring white spaces.
        /// </summary>
        /// <param name="value">The input string to check for value.</param>
        /// <param name="ignoreWhiteSpace">Flag to indicate whether to ignore white spaces when checking for value.</param>
        /// <returns>
        /// True if the input string has a value (not null or empty based on the ignoreWhiteSpace flag), false otherwise.
        /// </returns>
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <returns>
        /// An integer equivalent to the number contained in the specified string.
        /// </returns>
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts the specified object to a decimal value.
        /// </summary>
        /// <param name="value">The object to convert to decimal.</param>
        /// <returns>
        /// The decimal representation of the object. Returns 0 if the object is null or cannot be parsed to decimal.
        /// </returns>

        public static decimal ToDecimal(this object value)
        {
            if (value is null)
                return 0;
            return decimal.TryParse(value.ToString(), out decimal val) ? val : 0;
        }

        /// <summary>
        /// Converts the specified object to a double value.
        /// If the object is null or cannot be parsed to a double, returns 0.
        /// </summary>
        /// <param name="value">The object to convert to double.</param>
        /// <returns>The double value of the object, or 0 if conversion fails.</returns>
        public static double ToDouble(this object value)
        {
            if (value is null)
                return 0;
            return double.TryParse(value.ToString(), out double val) ? val : 0;
        }

        /// <summary>
        /// Converts the integer value to a string representation with thousand separators.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>A string representation of the integer value with thousand separators.</returns>
        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        /// <summary>
        /// Define an extension method for the decimal data type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }
        /// <summary>
        /// Extension method to get the display value of an Enum based on the specified DisplayProperty.
        /// </summary>
        /// <param name="value">The Enum value to get the display value for.</param>
        /// <param name="property">The DisplayProperty to use for retrieving the display value (default is DisplayProperty.Name).</param>
        /// <returns>
        /// The display value of the Enum based on the specified DisplayProperty.
        /// </returns>

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public enum DisplayProperty
        {
            Description,
            GroupName,
            Name,
            Prompt,
            ShortName,
            Order
        }
    }
}
