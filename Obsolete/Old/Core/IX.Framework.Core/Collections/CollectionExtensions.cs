using System;
using System.Collections.Generic;
using System.Linq;

namespace IX.Framework.Collections
{
    /// <summary>
    /// Contains useful extensions for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        #region Synchronize

        /// <summary>
        /// Synchronizes a base collection with another derived collection, ensuring that it ends up with the same elements, while keeping the derived collection intact.
        /// </summary>
        /// <typeparam name="TBaseEntity">The type of the base collection entities.</typeparam>
        /// <typeparam name="TDerivedEntity">The type of the derived collection entities.</typeparam>
        /// <param name="source">The base collection.</param>
        /// <param name="collection">The derived collection.</param>
        /// <param name="comparer">The equality comparer.</param>
        /// <param name="assigner">The element update assigner.</param>
        /// <param name="remover">The element remover.</param>
        /// <param name="instanceGenerator">The new element instance generator.</param>
        public static void Synchronize<TBaseEntity, TDerivedEntity>(this ICollection<TBaseEntity> source,
            IEnumerable<TDerivedEntity> collection,
            Func<TBaseEntity, TDerivedEntity, bool> comparer,
            Func<TBaseEntity, TDerivedEntity, TBaseEntity> assigner,
            Action<ICollection<TBaseEntity>, TBaseEntity> remover,
            Func<TDerivedEntity, TBaseEntity> instanceGenerator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));
            if (assigner == null)
                throw new ArgumentNullException(nameof(assigner));
            if (remover == null)
                throw new ArgumentNullException(nameof(remover));
            if (instanceGenerator == null)
                throw new ArgumentNullException(nameof(instanceGenerator));

            if (collection.Count() == 0)
            {
                foreach (var p in source)
                    remover(source, p);
            }
            else
            {
                foreach (var p in source.Where(p => p == null || !collection.Any(q => comparer(p, q))).ToList())
                    remover(source, p);

                foreach (TBaseEntity entity in source.ToList())
                    assigner(entity, collection.Single(p => comparer(entity, p)));

                foreach (var p in collection.Where(p => p != null && !source.Any(q => comparer(q, p))))
                    source.Add(assigner(instanceGenerator(p), p));
            }
        }

        /// <summary>
        /// Synchronizes a base collection with another derived collection, ensuring that it ends up with the same elements, while keeping the derived collection intact.
        /// </summary>
        /// <typeparam name="TBaseEntity">The type of the base collection entities.</typeparam>
        /// <typeparam name="TDerivedEntity">The type of the derived collection entities.</typeparam>
        /// <param name="source">The base collection.</param>
        /// <param name="collection">The derived collection.</param>
        /// <param name="assigner">The element update assigner.</param>
        /// <param name="remover">The element remover.</param>
        /// <param name="instanceGenerator">The new element instance generator.</param>
        public static void Synchronize<TBaseEntity, TDerivedEntity>(this ICollection<TBaseEntity> source,
        IEnumerable<TDerivedEntity> collection,
        Func<TBaseEntity, TDerivedEntity, TBaseEntity> assigner,
        Action<ICollection<TBaseEntity>, TBaseEntity> remover,
        Func<TDerivedEntity, TBaseEntity> instanceGenerator)
            where TBaseEntity : IEquatable<TDerivedEntity>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (assigner == null)
                throw new ArgumentNullException(nameof(assigner));
            if (remover == null)
                throw new ArgumentNullException(nameof(remover));
            if (instanceGenerator == null)
                throw new ArgumentNullException(nameof(instanceGenerator));

            Synchronize(source, collection, (p, q) => p.Equals(q), assigner, remover, instanceGenerator);
        }

        #endregion Synchronize

        #region ContentsEquals

        /// <summary>
        /// An <see cref="IEnumerable{TBaseEntity}"/> extension method that verifies that two collections of
        /// different elements contains elements that are equal to one another, according to the comparer, even
        /// though they may be in a different sequence.
        /// </summary>
        /// <typeparam name="TBaseEntity">Type of the base entity.</typeparam>
        /// <typeparam name="TDerivedEntity">Type of the derived entity.</typeparam>
        /// <param name="source">The base collection.</param>
        /// <param name="collection">The derived collection.</param>
        /// <param name="comparer">The equality comparer.</param>
        /// <returns><c>true</c> if the two collections' elements match, otherwise <c>false</c>.</returns>
        public static bool ContentsEqual<TBaseEntity, TDerivedEntity>(this IEnumerable<TBaseEntity> source, IEnumerable<TDerivedEntity> collection, Func<TBaseEntity, TDerivedEntity, bool> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return source.Count() == collection.Count() && !source.Any(p => !collection.Any(q => comparer(p, q)));
        }

        #endregion ContentsEquals

        #region ConvertAll

        /// <summary>
        /// Converts all elements of an array to another type using a converter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to convert from.</typeparam>
        /// <typeparam name="TResultingEntity">The type of the entity to convert to.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="converter">The converter method.</param>
        /// <returns>An array of resulting entities.</returns>
        public static TResultingEntity[] ConvertAll<TEntity, TResultingEntity>(this TEntity[] source, Func<TEntity, TResultingEntity> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            TResultingEntity[] result = new TResultingEntity[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = converter(source[i]);
            }

            return result;
        }

        /// <summary>
        /// Converts all elements of an enumerable collection to another type using a converter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to convert from.</typeparam>
        /// <typeparam name="TResultingEntity">The type of the entity to convert to.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="converter">The converter method.</param>
        /// <returns>An enumerator of resulting entities that can be used in a <c>foreach</c> cycle.</returns>
        public static IEnumerable<TResultingEntity> ConvertAll<TEntity, TResultingEntity>(this IEnumerable<TEntity> source, Func<TEntity, TResultingEntity> converter)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            return new ConvertedIEnumerable<TEntity, TResultingEntity>(source, converter);
        }

        internal class ConvertedIEnumerable<TEntity, TResultingEntity> : IEnumerable<TResultingEntity>
        {
            private IEnumerable<TEntity> _source;
            private Func<TEntity, TResultingEntity> _converter;

            internal ConvertedIEnumerable(IEnumerable<TEntity> source, Func<TEntity, TResultingEntity> converter)
            {
                _source = source;
                _converter = converter;
            }

            public IEnumerator<TResultingEntity> GetEnumerator()
            {
                foreach (TEntity entity in _source)
                {
                    yield return _converter(entity);
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion ConvertAll

        #region ForEach

        /// <summary>
        /// An array extension method that applies an operation to all items in this array.
        /// </summary>
        /// <typeparam name="T">Generic array item type parameter.</typeparam>
        /// <param name="source">The array.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this T[] source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            for (int i = 0; i < source.Length; i++)
                action(source[i]);
        }

        /// <summary>
        /// An array extension method that applies an operation to all items in this array.
        /// </summary>
        /// <typeparam name="T">Generic array item type parameter.</typeparam>
        /// <param name="source">The array.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
                action(item);
        }

        #endregion ForEach
    }
}