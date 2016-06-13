using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IX.Framework.Extensions
{
    /// <summary>
    /// A dictionary-usable equality comparer for arrays.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity contained by the array.</typeparam>
    public class ArrayEqualityComparer<TEntity> : IEqualityComparer<TEntity[]>
    {
        private IEqualityComparer<TEntity> _comparer;

        /// <summary>
        /// Initializes the <see cref="ArrayEqualityComparer{TEntity}"/> class.
        /// </summary>
        public ArrayEqualityComparer()
            : this(null)
        { }

        /// <summary>
        /// Initializes the <see cref="ArrayEqualityComparer{TEntity}"/> class with an equality comparer that can compare each of the arrays' individual element pairs.
        /// </summary>
        /// <param name="comparer">The custom comparer for each of the arrays' elements.</param>
        public ArrayEqualityComparer(IEqualityComparer<TEntity> comparer)
        {
            _comparer = comparer;
        }

        /// <summary>
        /// Verifies equality between two arrays of the same type.
        /// </summary>
        /// <param name="x">The left-hand operator.</param>
        /// <param name="y">The right-hand operator.</param>
        /// <returns><c>true</c> if the length, content and order of the two arrays is the same, <c>false</c> otherwise.</returns>
        /// <remarks>
        /// This method is pure and fully reliable inside a Constrained Execution Region.
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool Equals(TEntity[] x, TEntity[] y)
        {
            if (x == null && y == null)
                return true;
            else if ((x == null && y != null) || y == null)
                return false;
            else if (x.Length != y.Length)
                return false;
            else
                return _comparer == null ? x.SequenceEqual(y) : x.SequenceEqual(y, _comparer);
        }

        /// <summary>
        /// Returns a hash code for an instance of an array.
        /// </summary>
        /// <param name="obj">The array.</param>
        /// <returns>
        /// A hash code for the array, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        /// <remarks>
        /// This method is pure and fully reliable inside a Constrained Execution Region.
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int GetHashCode(TEntity[] obj)
        {
            if (obj == null || obj.Length == 0)
                return 0;

            return obj.Sum(p => p.GetHashCode());
        }
    }
}
