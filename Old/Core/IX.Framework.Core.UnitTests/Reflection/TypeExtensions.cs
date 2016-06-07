using IX.Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace IX.Framework.Core.UnitTests.Reflection
{
    public class TypeExtensions
    {
        #region GetFullyQualifiedTypeName

        [Fact]
        public void GetFullyQualifiedTypeName_NullType_Exception()
        {
            Type type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetFullyQualifiedTypeName());
        }

        [Fact]
        public void GetFullyQualifiedTypeName_NullTypeInfo_Exception()
        {
            TypeInfo type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetFullyQualifiedTypeName());
        }

        [Theory]
        [MemberData("GetFullyQualifiedTypeName_TypeInfo_TestData", null)]
        public void GetFullyQualifiedTypeName_Type(Type type, string expectedResult)
        {
            string result = type.GetFullyQualifiedTypeName();

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData("GetFullyQualifiedTypeName_TypeInfo_TestData", null)]
        public void GetFullyQualifiedTypeName_TypeInfo(Type type, string expectedResult)
        {
            string result = type.GetTypeInfo().GetFullyQualifiedTypeName();

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> GetFullyQualifiedTypeName_TypeInfo_TestData = new List<object[]>
        {
            new object[]
            {
                typeof(TestType<,>),
                "IX.Framework.Core.UnitTests.Reflection.TypeExtensions+TestType<T1, T2>"
            },
            new object[]
            {
                typeof(TypeExtensions),
                "IX.Framework.Core.UnitTests.Reflection.TypeExtensions"
            },
            new object[]
            {
                typeof(NonNestedGenericTest<,,,,>),
                "IX.Framework.Core.UnitTests.Reflection.NonNestedGenericTest<T1, T2, T3, T4, T5>"
            },
            new object[]
            {
                typeof(TestType<int, string>),
                "IX.Framework.Core.UnitTests.Reflection.TypeExtensions+TestType<System.Int32, System.String>"
            }
        };

        #endregion GetFullyQualifiedTypeName

        #region GetTypeNameWithoutNamespace

        [Fact]
        public void GetTypeNameWithoutNamespace_NullType_Exception()
        {
            Type type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetTypeNameWithoutNamespace());
        }

        [Fact]
        public void GetTypeNameWithoutNamespace_NullTypeInfo_Exception()
        {
            TypeInfo type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetTypeNameWithoutNamespace());
        }

        [Theory]
        [MemberData("GetTypeNameWithoutNamespace_TypeInfo_TestData", null)]
        public void GetTypeNameWithoutNamespace_Type(Type type, string expectedResult)
        {
            string result = type.GetTypeNameWithoutNamespace();

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData("GetTypeNameWithoutNamespace_TypeInfo_TestData", null)]
        public void GetTypeNameWithoutNamespace_TypeInfo(Type type, string expectedResult)
        {
            string result = type.GetTypeInfo().GetTypeNameWithoutNamespace();

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> GetTypeNameWithoutNamespace_TypeInfo_TestData = new List<object[]>
        {
            new object[]
            {
                typeof(TestType<,>),
                "TypeExtensions+TestType<T1, T2>"
            },
            new object[]
            {
                typeof(TypeExtensions),
                "TypeExtensions"
            },
            new object[]
            {
                typeof(NonNestedGenericTest<,,,,>),
                "NonNestedGenericTest<T1, T2, T3, T4, T5>"
            },
            new object[]
            {
                typeof(TestType<int, string>),
                "TypeExtensions+TestType<System.Int32, System.String>"
            },
            new object[]
            {
                typeof(TestType<int[], string>),
                "TypeExtensions+TestType<System.Int32[], System.String>"
            },
            new object[]
            {
                typeof(TestType<int, string>[]),
                "TypeExtensions+TestType<System.Int32, System.String>[]"
            }
        };

        #endregion GetTypeNameWithoutNamespace

        internal class TestType<T1, T2>
        { }

        #region GetGenericParameterByName

        [Fact]
        public void GetGenericParameterByName_NullType_Exception()
        {
            Type type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("T1"));
        }

        [Fact]
        public void GetGenericParameterByName_NullTypeInfo_Exception()
        {
            TypeInfo type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("T1"));
        }

        [Fact]
        public void GetGenericParameterByName_NullName_Exception()
        {
            TypeInfo type = (typeof(int)).GetTypeInfo();

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(null));
        }

        [Fact]
        public void GetGenericParameterByName_EmptyName_Exception()
        {
            TypeInfo type = (typeof(int)).GetTypeInfo();

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(string.Empty));
        }

        [Fact]
        public void GetGenericParameterByName_SpaceName_Exception()
        {
            TypeInfo type = (typeof(int)).GetTypeInfo();

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("      "));
        }

        [Fact]
        public void GetGenericParameterByName_TypeNullName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(null));
        }

        [Fact]
        public void GetGenericParameterByName_TypeEmptyName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(string.Empty));
        }

        [Fact]
        public void GetGenericParameterByName_TypeSpaceName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("      "));
        }

        [Fact]
        public void GetGenericParameterByName_WhitespaceName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("   \t "));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedNullType_Exception()
        {
            Type type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("T1", true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedNullTypeInfo_Exception()
        {
            TypeInfo type = null;

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("T1", true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedNullName_Exception()
        {
            TypeInfo type = (typeof(int)).GetTypeInfo();

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(null, true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedEmptyName_Exception()
        {
            TypeInfo type = (typeof(int)).GetTypeInfo();

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(string.Empty, true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedSpaceName_Exception()
        {
            TypeInfo type = (typeof(int)).GetTypeInfo();

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("      ", true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedTypeNullName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(null, true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedTypeEmptyName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName(string.Empty, true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedTypeSpaceName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("      ", true));
        }

        [Fact]
        public void GetGenericParameterByName_InheritedWhitespaceName_Exception()
        {
            Type type = (typeof(int));

            Assert.Throws<ArgumentNullException>(() => type.GetGenericParameterByName("   \t ", true));
        }

        [Fact]
        public void GetGenericParameterByName_GenericMethod()
        {
            Type typ = GetType()
                .GetRuntimeMethod("TestStuff", new Type[0])
                .MakeGenericMethod(typeof(int))
                .GetGenericParameterByName("T1");

            Assert.NotNull(typ);
            Assert.Equal(typeof(int), typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericMethodDefinition()
        {
            Type typ = GetType()
                .GetRuntimeMethod("TestStuff", new Type[0])
                .GetGenericParameterByName("T1");

            Assert.NotNull(typ);
            Assert.Equal("T1", typ.Name);
        }

        [Fact]
        public void GetGenericParameterByName_NonGenericMethod()
        {
            Type typ = GetType()
                .GetRuntimeMethod("GetGenericParameterByName_GenericMethod", new Type[0])
                .GetGenericParameterByName("T1");

            Assert.Null(typ);
        }

        public T1 TestStuff<T1>()
        {
            return default(T1);
        }

        [Fact]
        public void GetGenericParameterByName_GenericType()
        {
            Type typ = typeof(TestType<,>)
                .GetTypeInfo()
                .MakeGenericType(typeof(int), typeof(string))
                .GetTypeInfo()
                .GetGenericParameterByName("T1");

            Assert.NotNull(typ);
            Assert.Equal(typeof(int), typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericTypeDefinition()
        {
            Type typ = typeof(TestType<,>)
                .GetTypeInfo()
                .GetGenericParameterByName("T1");

            Assert.NotNull(typ);
            Assert.Equal("T1", typ.Name);
        }

        [Fact]
        public void GetGenericParameterByName_NonGenericType()
        {
            Type typ = GetType()
                .GetTypeInfo()
                .GetGenericParameterByName("T1");

            Assert.Null(typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericDefinition_Recursive()
        {
            Type typ = typeof(TextFixtureForRecursive<,,>)
                .GetTypeInfo()
                .GetGenericParameterByName("T1", true);

            Assert.NotNull(typ);
            Assert.Equal("AAA", typ.Name);
        }

        [Fact]
        public void GetGenericParameterByName_GenericImplementation_Recursive()
        {
            Type typ = typeof(TextFixtureForRecursive<int, string, long>)
                .GetTypeInfo()
                .GetGenericParameterByName("T1", true);

            Assert.NotNull(typ);
            Assert.Equal(typeof(int), typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericMethodDefinition_Recursive()
        {
            Type typ = typeof(TextFixtureForRecursive<,,>)
                .GetTypeInfo()
                .GetDeclaredMethod("Something")
                .GetGenericParameterByName("T1", true);

            Assert.NotNull(typ);
            Assert.Equal("AAA", typ.Name);
        }

        [Fact]
        public void GetGenericParameterByName_GenericMethodImplementation_Recursive()
        {
            Type typ = typeof(TextFixtureForRecursive<int, string, long>)
                .GetTypeInfo()
                .GetDeclaredMethod("Something")
                .GetGenericParameterByName("T1", true);

            Assert.NotNull(typ);
            Assert.Equal(typeof(int), typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericType_Type()
        {
            Type typ = typeof(TestType<,>)
                .GetTypeInfo()
                .MakeGenericType(typeof(int), typeof(string))
                .GetGenericParameterByName("T1");

            Assert.NotNull(typ);
            Assert.Equal(typeof(int), typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericTypeDefinition_Type()
        {
            Type typ = typeof(TestType<,>)
                .GetGenericParameterByName("T1");

            Assert.NotNull(typ);
            Assert.Equal("T1", typ.Name);
        }

        [Fact]
        public void GetGenericParameterByName_NonGenericType_Type()
        {
            Type typ = GetType()
                .GetGenericParameterByName("T1");

            Assert.Null(typ);
        }

        [Fact]
        public void GetGenericParameterByName_GenericDefinition_RecursiveType()
        {
            Type typ = typeof(TextFixtureForRecursive<,,>)
                .GetGenericParameterByName("T1", true);

            Assert.NotNull(typ);
            Assert.Equal("AAA", typ.Name);
        }

        [Fact]
        public void GetGenericParameterByName_GenericImplementation_RecursiveType()
        {
            Type typ = typeof(TextFixtureForRecursive<int, string, long>)
                .GetGenericParameterByName("T1", true);

            Assert.NotNull(typ);
            Assert.Equal(typeof(int), typ);
        }

        internal class TextFixtureForRecursive<AAA, BBB, CCC> : TestType<AAA, CCC>
        {
            public void Something() { }
        }

        #endregion GetGenericParameterByName
    }

    internal class NonNestedGenericTest<T1, T2, T3, T4, T5>
    { }
}