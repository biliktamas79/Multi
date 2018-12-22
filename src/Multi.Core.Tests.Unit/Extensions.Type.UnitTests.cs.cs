using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Core.Tests.Unit
{
    [TestClass]
    public class SystemExtensionsTypeUnitTests
    {
        #region GetFriendlyTypeName
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Type_GetFriendlyTypeName_throws_ArgumentNullException_if_Type_isNull()
        {
            Type type = null;
            type.GetFriendlyTypeName();
        }

        private class StringBuilderGetFriendlyTypeNameTestArgs
        {
            public readonly Type type;
            public readonly TypeNameStringShorteningFlags typeNameShorteningFlags;
            public readonly string ExpectedResult;

            public StringBuilderGetFriendlyTypeNameTestArgs(Type type, TypeNameStringShorteningFlags typeNameShorteningFlags, string expectedResult)
            {
                this.type = type;
                this.typeNameShorteningFlags = typeNameShorteningFlags;
                this.ExpectedResult = expectedResult;
            }
        }

        private class CSharpTypesWithAlias
        {
            public readonly Type type;
            public readonly string alias;

            public CSharpTypesWithAlias(Type type, string alias)
            {
                this.type = type;
                this.alias = alias;
            }
        }

        [TestMethod]
        public void Type_GetFriendlyTypeName_returns_the_right_string()
        {
            var typeAliases = new CSharpTypesWithAlias[]
                {
                    new CSharpTypesWithAlias(typeof(bool), "bool"),
                    new CSharpTypesWithAlias(typeof(char), "char"),
                    new CSharpTypesWithAlias(typeof(string), "string"),
                    new CSharpTypesWithAlias(typeof(byte), "byte"),
                    new CSharpTypesWithAlias(typeof(sbyte), "sbyte"),
                    new CSharpTypesWithAlias(typeof(short), "short"),
                    new CSharpTypesWithAlias(typeof(ushort), "ushort"),
                    new CSharpTypesWithAlias(typeof(int), "int"),
                    new CSharpTypesWithAlias(typeof(uint), "uint"),
                    new CSharpTypesWithAlias(typeof(long), "long"),
                    new CSharpTypesWithAlias(typeof(ulong), "ulong"),
                    new CSharpTypesWithAlias(typeof(float), "float"),
                    new CSharpTypesWithAlias(typeof(double), "double"),
                    new CSharpTypesWithAlias(typeof(decimal), "decimal"),
                    new CSharpTypesWithAlias(typeof(object), "object"),
                };

            var casesToTest = new List<StringBuilderGetFriendlyTypeNameTestArgs>()
                    {
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<bool?>), TypeNameStringShorteningFlags.All, "List<bool?>"),

                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.All, "int?"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.ExcludeNamespace, "Nullable<Int32>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.ShortNullableTypeNames, "System.Int32?"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.UseTypeNameAliases, "System.Nullable<int>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.None, "System.Nullable<System.Int32>"),

                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.All, "List<int>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.ExcludeNamespace, "List<Int32>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.ShortNullableTypeNames, "System.Collections.Generic.List<System.Int32>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.UseTypeNameAliases, "System.Collections.Generic.List<int>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.None, "System.Collections.Generic.List<System.Int32>"),

                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.All,
                            "List<KeyValuePair<int, Queue<int?>>>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.ExcludeNamespace,
                            "List<KeyValuePair<Int32, Queue<Nullable<Int32>>>>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.ShortNullableTypeNames,
                            "System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32, System.Collections.Generic.Queue<System.Int32?>>>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.UseTypeNameAliases,
                            "System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.Queue<System.Nullable<int>>>>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.None,
                            "System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32, System.Collections.Generic.Queue<System.Nullable<System.Int32>>>>"),
                    };

            foreach (var typeWithAlias in typeAliases)
            {
                casesToTest.AddRange(new StringBuilderGetFriendlyTypeNameTestArgs[]
                    {
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.All, typeWithAlias.alias),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.ExcludeNamespace, typeWithAlias.type.Name),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.ShortNullableTypeNames, typeWithAlias.type.FullName),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.UseTypeNameAliases, typeWithAlias.alias),
                        new StringBuilderGetFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.None, typeWithAlias.type.FullName),
                    });

                if (typeWithAlias.type.IsValueType)
                {
                    var type = typeof(Nullable<>).MakeGenericType(typeWithAlias.type); // ulong?
                    var listType = typeof(List<>).MakeGenericType(type); // List<ulong?>

                    casesToTest.AddRange(new StringBuilderGetFriendlyTypeNameTestArgs[]
                    {
                        new StringBuilderGetFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.All,
                            $"List<{typeWithAlias.alias}?>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.ExcludeNamespace,
                            $"List<Nullable<{typeWithAlias.type.Name}>>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.ShortNullableTypeNames,
                            $"System.Collections.Generic.List<{typeWithAlias.type.FullName}?>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.UseTypeNameAliases,
                            $"System.Collections.Generic.List<System.Nullable<{typeWithAlias.alias}>>"),
                        new StringBuilderGetFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.None,
                            $"System.Collections.Generic.List<System.Nullable<{typeWithAlias.type.FullName}>>"),
                    });
                }
            }

            foreach (var testCase in casesToTest)
            {
                var result = testCase.type
                    .GetFriendlyTypeName(testCase.typeNameShorteningFlags);

                Assert.AreEqual(testCase.ExpectedResult, result, $"GetFriendlyTypeName test failed for Type='{testCase.type.FullName}', Flags='{testCase.typeNameShorteningFlags}'.");
                //System.Diagnostics.Debug.WriteLine($"GetFriendlyTypeName test passed for Type='{result}', Flags='{testCase.typeNameShorteningFlags}'.");
            }
        }
        #endregion GetFriendlyTypeName

        #region IsAnonymous
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Type_IsAnonymous_throws_ArgumentNullException_if_Type_isNull()
        {
            Type type = null;
            type.IsAnonymous();
        }

        [TestMethod]
        public void Type_IsAnonymous_returns_true_if_Type_isAnonymous()
        {
            var a = new { Prop1 = 1 };
            Assert.IsTrue(a.GetType().IsAnonymous());
        }

        [TestMethod]
        public void Type_IsAnonymous_returns_false_if_Type_isNotAnonymous()
        {
            Assert.IsFalse(typeof(string).IsAnonymous());
            Assert.IsFalse(typeof(int).IsAnonymous());
            Assert.IsFalse(typeof(Random).IsAnonymous());
            Assert.IsFalse(typeof(HttpStyleUriParser).IsAnonymous());
        }
        #endregion IsAnonymous

        #region ImplementsInterface
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Type_ImplementsInterface_throws_ArgumentNullException_if_type_isNull()
        {
            Type type = null;
            type.ImplementsInterface(typeof(IAsyncResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Type_ImplementsInterface_throws_ArgumentNullException_if_interfaceType_isNull()
        {
            typeof(HttpStyleUriParser).ImplementsInterface(null);
        }

        [TestMethod]
        public void Type_ImplementsInterface_returns_true_if_type_implements_interfaceType()
        {
            Assert.IsTrue(typeof(List<int>).ImplementsInterface(typeof(IEnumerable<int>)));
            Assert.IsTrue(typeof(List<int>).ImplementsInterface<IEnumerable<int>>());

            Assert.IsTrue(typeof(Dictionary<int, string>).ImplementsInterface(typeof(IDictionary<int, string>)));
            Assert.IsTrue(typeof(Dictionary<int, string>).ImplementsInterface<IDictionary<int, string>>());
        }

        [TestMethod]
        public void Type_ImplementsInterface_returns_false_if_type_doesNotImplement_interfaceType()
        {
            Assert.IsFalse(typeof(List<int>).ImplementsInterface(typeof(IDisposable)));
            Assert.IsFalse(typeof(List<int>).ImplementsInterface<IEnumerable<string>>());
        }
        #endregion ImplementsInterface
    }
}
