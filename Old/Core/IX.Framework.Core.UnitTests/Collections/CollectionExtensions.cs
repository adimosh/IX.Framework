using IX.Framework.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IX.Framework.Core.UnitTests.Collections
{
    public class CollectionExtensions
    {
        #region Synchronize

        [Theory]
        [MemberData("Synchronize_WithComparer_TestData", null)]
        public void Synchronize_WithComparer(List<int> initial, List<int> final, Func<int, int, bool> comparer, Func<int, int, int> assigner, Action<ICollection<int>, int> remover, Func<int, int> instanceGenerator, List<int> expectedResult)
        {
            initial.Synchronize(final, comparer, assigner, remover, instanceGenerator);

            Assert.True(initial.ContentsEqual(expectedResult, (p, q) => p == q));
        }

        public static IEnumerable<object[]> Synchronize_WithComparer_TestData = new List<object[]>
        {
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 2, 6, 8, 5, 11, 16, 28 },
                (Func<int, int, bool>)((p, q) => p == q),
                (Func<int, int, int>)((p, q) => { p = q; return q; }),
                (Action<ICollection<int>, int>)((p, q) => p.Remove(q)),
                (Func<int, int>)(p => 0),
                new List<int> { 2, 6, 8, 5, 11, 16, 28 }
            },
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 2, 6, 8, 5, 11, 16, 28 },
                (Func<int, int, bool>)((p, q) => p == q + 1),
                (Func<int, int, int>)((p, q) => { p = q; return q; }),
                (Action<ICollection<int>, int>)((p, q) => p.Remove(q)),
                (Func<int, int>)(p => 0),
                new List<int> { 3, 6, 7, 9, 11, 16, 28 }
            }
        };

        [Theory]
        [MemberData("Synchronize_WithoutComparer_TestData", null)]
        public void Synchronize_WithoutComparer(List<int> initial, List<int> final, Func<int, int, int> assigner, Action<ICollection<int>, int> remover, Func<int, int> instanceGenerator, List<int> expectedResult)
        {
            initial.Synchronize(final, assigner, remover, instanceGenerator);

            Assert.True(initial.ContentsEqual(expectedResult, (p, q) => p == q));
        }

        public static IEnumerable<object[]> Synchronize_WithoutComparer_TestData = new List<object[]>
        {
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 2, 6, 8, 5, 11, 16, 28 },
                (Func<int, int, int>)((p, q) => { p = q; return q; }),
                (Action<ICollection<int>, int>)((p, q) => p.Remove(q)),
                (Func<int, int>)(p => 0),
                new List<int> { 2, 6, 8, 5, 11, 16, 28 }
            },
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 2, 6, 8, 5, 11, 16, 28 },
                (Func<int, int, int>)((p, q) => { p = q; return q; }),
                (Action<ICollection<int>, int>)((p, q) => p.Remove(q)),
                (Func<int, int>)(p => 0),
                new List<int> { 2, 6, 8, 5, 11, 16, 28 }
            }
        };

        #endregion Synchronize

        #region ContentsEqual

        [Theory]
        [MemberData("ContentsEqual_TestData", null)]
        public void ContentsEqual(List<int> initial, List<int> final, bool expectedResult)
        {
            bool result = initial.ContentsEqual(final, (p, q) => p == q);

            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> ContentsEqual_TestData = new List<object[]>
        {
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 2, 6, 8, 5, 11, 16, 28 },
                false
            },
            new object[]
            {
                new List<int> { 2, 6, 8, 5, 11, 16, 28 },
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                false
            },
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                true
            },
            new object[]
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new List<int> { 7, 8, 3, 4, 1, 2, 5, 6, 9, 10 },
                true
            },
        };

        #endregion ContentsEqual

        #region ConvertAll

        [Theory]
        [MemberData("ConvertAll_Array_TestData", null)]
        public void ConvertAll_Array(int[] array, string[] expectedResult)
        {
            string[] result = array.ConvertAll(p => p.ToString());

            Assert.NotNull(result);
            Assert.True(expectedResult.ContentsEqual(result, (p, q) => p.CompareTo(q) == 0));
        }

        [Theory]
        [MemberData("ConvertAll_Array_TestData", null)]
        public void ConvertAll_Enumerable(IEnumerable<int> array, IEnumerable<string> expectedResult)
        {
            foreach (var current in array.ConvertAll(p => p.ToString()))
            {
                Assert.True(expectedResult.Contains(current));
            }
        }

        public static IEnumerable<object[]> ConvertAll_Array_TestData = new List<object[]>
        {
            new object[]
            {
                new[] { 1, 3, 5, 8, 10 },
                new[] { "1", "3", "5", "8", "10" },
            },
            new object[]
            {
                new[] { 1, 3, 7, 8, 10 },
                new[] { "1", "3", "7", "8", "10" },
            },
        };

        #endregion ConvertAll

        #region ForEach

        private static int _counter;

        [Theory]
        [MemberData("ForEach_Array_int_TestData", null)]
        public void ForEach_Array_int(int[] array, Action<int> action, int expectedState)
        {
            _counter = 0;

            array.ForEach(action);

            Assert.NotNull(array);
            Assert.True(_counter == expectedState);
        }

        [Theory]
        [MemberData("ForEach_Array_int_TestData", null)]
        public void ForEach_Enumerable_int(IEnumerable<int> array, Action<int> action, int expectedState)
        {
            _counter = 0;

            array.ForEach(action);

            Assert.NotNull(array);
            Assert.True(_counter == expectedState);
        }

        public static IEnumerable<object[]> ForEach_Array_int_TestData = new List<object[]>
        {
            new object[]
            {
                new[] { 1, 3, 5, 8, 10 },
                (Action<int>)(p => { _counter++; }),
                5
            },
            new object[]
            {
                new[] { 1, 3, 5, 8, 10 },
                (Action<int>)(p => { _counter += p; }),
                27
            },
            new object[]
            {
                new[] { 1, 3, 5, 8, 10, 18, 512, 6592 },
                (Action<int>)(p => { _counter++; }),
                8
            },
            new object[]
            {
                new[] { 1, -7, 3, 5, -11, 8, 10, -9 },
                (Action<int>)(p => { _counter += p; }),
                0
            }
        };

        #endregion ForEach
    }
}