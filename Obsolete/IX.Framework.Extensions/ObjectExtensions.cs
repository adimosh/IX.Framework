using System;
using System.Threading.Tasks;

namespace IX.Framework.Extensions
{
    /// <summary>
    /// A class containing object extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Chains the specified action to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object to chain to.</typeparam>
        /// <param name="object">The object to chain to.</param>
        /// <param name="action">The action to chain.</param>
        /// <returns>The chain object reference.</returns>
        public static T Chain<T>(this T @object, Action<T> action)
        {
            if (@object == null)
                throw new ArgumentNullException(nameof(@object));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            action(@object);

            return @object;
        }

        /// <summary>
        /// Chains the specified action to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object to chain to.</typeparam>
        /// <param name="object">The object to chain to.</param>
        /// <param name="action">The action to chain.</param>
        /// <returns>The chain object reference.</returns>
        public static async Task<T> ChainAsync<T>(this T @object, Func<T, Task> action)
        {
            if (@object == null)
                throw new ArgumentNullException(nameof(@object));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            await action(@object);

            return @object;
        }
    }
}