using IX.Framework.Collections;
using System.Collections.Generic;
using Xunit;

namespace IX.Framework.Core.UnitTests.Collections
{
    public class ArrayEqualityComparer
    {
        #region Equals

        #region Int arrays

        [Theory]
        [InlineData(null, null, true)]
        [MemberData("Equals_IntArrays_TestData", null)]
        public void Equals_IntArrays(int[] array1, int[] array2, bool desiredResult)
        {
            ArrayEqualityComparer<int> comparer = new ArrayEqualityComparer<int>();

            bool result = comparer.Equals(array1, array2);

            Assert.Equal(desiredResult, result);
        }

        #region Test data for previous test

        public static IEnumerable<object[]> Equals_IntArrays_TestData = new List<object[]>
        {
            new object[]
            {
                null,
                new[] { 1 },
                false
            },
            new object[]
            {
                new[] { 1 },
                null,
                false
            },
            new object[]
            {
                new[] { 1 },
                new[] { 1, 2 },
                false
            },
            new object[]
            {
                new[] { 1, 2 },
                new[] { 1 },
                false
            },
            new object[]
            {
                new[] { 1, 2 },
                new[] { 1, 4 },
                false
            },
            new object[]
            {
                new[] { 1, 2, 7, 2, 9, 1, 7 },
                new[] { 1, 4, 6, 5, 3, 9, 0 },
                false
            },
            new object[]
            {
                new[] { 1, 2, 7, 2, 9, 1, 7 },
                new[] { 1, 2, 7, 2, 9, 1, 7 },
                true
            }
        };

        #endregion Test data for previous test

        #endregion Int arrays

        #region String arrays

        [Theory]
        [InlineData(null, null, true)]
        [MemberData("Equals_StringArrays_TestData", null)]
        public void Equals_StringArrays(string[] array1, string[] array2, bool desiredResult)
        {
            ArrayEqualityComparer<string> comparer = new ArrayEqualityComparer<string>();

            bool result = comparer.Equals(array1, array2);

            Assert.Equal(desiredResult, result);
        }

        #region Test data for previous test

        public static IEnumerable<object[]> Equals_StringArrays_TestData = new List<object[]>
        {
            new object[]
            {
                null,
                new[] { "aaa" },
                false
            },
            new object[]
            {
                new[] { "aaa" },
                null,
                false
            },
            new object[]
            {
                new[] { "aaa" },
                new[] { "aaa", "bbb" },
                false
            },
            new object[]
            {
                new[] { "aaa", "bbb" },
                new[] { "aaa" },
                false
            },
            new object[]
            {
                new[] { "aaa", "bbb" },
                new[] { "aaa", "ccc" },
                false
            },
            new object[]
            {
                new[] { "aaa", "asdad", "yuioyuoiuyo", "zxczxcvzxcv", "23453456456", "kpo][iop[[", "schgi8kret3r2ddFGC" },
                new[] { "bbb", "asdfasdf", "asdfgasdfgasd", "adsfgsdfgh", "tyutyuty", "xfgnbcvnbv", "yuioyuioyuio" },
                false
            },
            new object[]
            {
                new[] { "aaa", "asdad", "yuioyuoiuyo", "zxczxcvzxcv", "23453456456", "kpo][iop[[", "schgi8kret3r2ddFGC" },
                new[] { "aaa", "asdad", "yuioyuoiuyo", "zxczxcvzxcv", "23453456456", "kpo][iop[[", "schgi8kret3r2ddFGC" },
                true
            }
        };

        #endregion Test data for previous test

        #endregion String arrays

        #endregion Equals

        #region Equals with equality comparer

        #region Int arrays

        [Theory]
        [MemberData("Equals_IntArrays_WithComparer_TestData", null)]
        public void Equals_IntArrays_WithComparer(int[] array1, int[] array2, IEqualityComparer<int> comp, bool desiredResult)
        {
            ArrayEqualityComparer<int> comparer = new ArrayEqualityComparer<int>(comp);

            bool result = comparer.Equals(array1, array2);

            Assert.Equal(desiredResult, result);
        }

        #region Test data for previous test

        public static IEnumerable<object[]> Equals_IntArrays_WithComparer_TestData = new List<object[]>
        {
            new object[]
            {
                null,
                null,
                new Equals_IntArrays_WithComparer_TestClass(),
                true
            },
            new object[]
            {
                null,
                new[] { 1 },
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { 1 },
                null,
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { 1 },
                new[] { 1, 2 },
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { 1, 2 },
                new[] { 1 },
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { 1, 2 },
                new[] { 1, 4 },
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { 1, 2, 7, 2, 9, 1, 7 },
                new[] { 1, 4, 6, 5, 3, 9, 0 },
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { 1, 2, 7, 2, 9, 1, 7 },
                new[] { 1, 2, 7, 2, 9, 1, 7 },
                new Equals_IntArrays_WithComparer_TestClass(),
                false
            }
        };

        #endregion Test data for previous test

        #region Test helper class

        internal class Equals_IntArrays_WithComparer_TestClass : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x != y;
            }

            public int GetHashCode(int obj)
            {
                return obj.GetHashCode();
            }
        }

        #endregion Test helper class

        #endregion Int arrays

        #region String arrays

        [Theory]
        [MemberData("Equals_StringArrays_WithComparer_TestData", null)]
        public void Equals_StringArrays_WithComparer(string[] array1, string[] array2, IEqualityComparer<string> comp, bool desiredResult)
        {
            ArrayEqualityComparer<string> comparer = new ArrayEqualityComparer<string>(comp);

            bool result = comparer.Equals(array1, array2);

            Assert.Equal(desiredResult, result);
        }

        #region Test data for previous test

        public static IEnumerable<object[]> Equals_StringArrays_WithComparer_TestData = new List<object[]>
        {
            new object[]
            {
                null,
                null,
                new Equals_StringArrays_WithComparer_TestClass(),
                true
            },
            new object[]
            {
                null,
                new[] { "aaa" },
                new Equals_StringArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { "aaa" },
                null,
                new Equals_StringArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { "aaa" },
                new[] { "aaa", "bbb" },
                new Equals_StringArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { "aaa", "bbb" },
                new[] { "aaa" },
                new Equals_StringArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { "aaa", "bbb" },
                new[] { "aaa", "ccc" },
                new Equals_StringArrays_WithComparer_TestClass(),
                false
            },
            new object[]
            {
                new[] { "aaa", "asdad", "yuioyuoiuyo", "zxczxcvzxcv", "23453456456", "kpo][iop[[", "schgi8kret3r2ddFGC" },
                new[] { "bbb", "asdfasdf", "asdfgasdfgasd", "adsfgsdfgh", "tyutyuty", "xfgnbcvnbv", "yuioyuioyuio" },
                new Equals_StringArrays_WithComparer_TestClass(),
                true
            },
            new object[]
            {
                new[] { "aaa", "asdad", "yuioyuoiuyo", "zxczxcvzxcv", "23453456456", "kpo][iop[[", "schgi8kret3r2ddFGC" },
                new[] { "aaa", "asdad", "yuioyuoiuyo", "zxczxcvzxcv", "23453456456", "kpo][iop[[", "schgi8kret3r2ddFGC" },
                new Equals_StringArrays_WithComparer_TestClass(),
                false
            }
        };

        #endregion Test data for previous test

        #region Test helper class

        internal class Equals_StringArrays_WithComparer_TestClass : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return !x.Equals(y);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        #endregion Test helper class

        #endregion String arrays

        #endregion Equals with equality comparer

        #region GetHashCode

        [Theory]
        [InlineData(6, 36)]
        [InlineData(1, 6)]
        [InlineData(2, 12)]
        [InlineData(8, 48)]
        [InlineData(10, 60)]
        [InlineData(60, 360)]
        public void GetHashCode_CustomObjects(int numberOfObjects, int desiredResult)
        {
            ArrayEqualityComparer<GetHashCode_CustomObject> comparer = new ArrayEqualityComparer<GetHashCode_CustomObject>();

            List<GetHashCode_CustomObject> objects = new List<GetHashCode_CustomObject>();
            for (int i = 0; i < numberOfObjects; i++)
            {
                objects.Add(new GetHashCode_CustomObject());
            }

            int result = comparer.GetHashCode(objects.ToArray());

            Assert.NotNull(result);
            Assert.Equal(desiredResult, result);
        }

        [Fact]
        public void GetHashCode_NullArray()
        {
            ArrayEqualityComparer<int> comparer = new ArrayEqualityComparer<int>();

            int result = comparer.GetHashCode(null);

            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetHashCode_EmptyArray()
        {
            ArrayEqualityComparer<int> comparer = new ArrayEqualityComparer<int>();

            int result = comparer.GetHashCode(new int[0]);

            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        #region Helper class
        internal class GetHashCode_CustomObject
        {
            public override int GetHashCode()
            {
                return 6;
            }
        }
        #endregion

        #endregion
    }
}