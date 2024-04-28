// MIT License

using System;
using System.Collections;
using System.Linq;

namespace DnnReactDemo.Utilities
{
    /// <summary>
    /// Contains methods for asserting that objects are not null or empty.
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// Checks if the input object is not null and throws an ArgumentNullException if it is null.
        /// </summary>
        /// <typeparam name="T">The type of the object to check</typeparam>
        /// <param name="obj">The object to check for null</param>
        /// <param name="name">The name of the object being checked</param>
        /// <param name="message">Optional message to include in the exception</param>
        public static void NotNull<T>(T obj, string name, string message = null)
            where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        /// <summary>
        /// Checks if a nullable struct object is not null, otherwise throws an ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">The type of the struct object.</typeparam>
        /// <param name="obj">The nullable struct object to check.</param>
        /// <param name="name">The name of the object being checked.</param>
        /// <param name="message">The optional message to include in the exception.</param>
        public static void NotNull<T>(T? obj, string name, string message = null)
            where T : struct
        {
            if (!obj.HasValue)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);

        }

        /// <summary>
        /// Checks if the provided object is not empty based on the specified conditions and throws an ArgumentException if it is empty.
        /// </summary>
        /// <typeparam name="T">The type of the object to check</typeparam>
        /// <param name="obj">The object to check for emptiness</param>
        /// <param name="name">The name of the object being checked</param>
        /// <param name="message">Optional message to include in the exception</param>
        /// <param name="defaultValue">Optional default value for comparison</param>
        public static void NotEmpty<T>(T obj, string name, string message = null, T defaultValue = null)
            where T : class
        {
            if (obj == defaultValue
                || (obj is string str && string.IsNullOrWhiteSpace(str))
                || (obj is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
            }
        }
    }
}
