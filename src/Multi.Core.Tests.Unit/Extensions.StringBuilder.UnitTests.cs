using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Core.Tests.Unit
{
    public class LinkedListBase<T>
    {
        public T Next { get; set; }
        public T Previous { get; set; }
    }

    public class PersonLinkedList : LinkedListBase<PersonLinkedList>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class SystemExtensionsStringBuilderUnitTests
    {
        //private static readonly StringBuilder Sb = new StringBuilder();

        #region AppendUserFriendlyTypeName
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringBuilder_AppendUserFriendlyTypeName_Type_bool_bool_throws_ArgumentNullException_if_StringBuilder_isNull()
        {
            StringBuilder sb = null;
            sb.AppendUserFriendlyTypeName(typeof(int));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringBuilder_AppendUserFriendlyTypeName_Type_bool_bool_throws_ArgumentNullException_if_type_isNull()
        {
            new StringBuilder().AppendUserFriendlyTypeName(null);
        }

        //[TestMethod]
        //[ExpectedException(typeof(NotSupportedException))]
        //public void StringBuilder_AppendUserFriendlyTypeName_Type_bool_bool_throws_NotSupportedException_if_type_isSelfContainingGeneric()
        //{
        //    var instance = new PersonLinkedList();
        //    Sb.AppendUserFriendlyTypeName(instance.GetType());
        //}

        private class StringBuilderAppendUserFriendlyTypeNameTestArgs
        {
            public readonly Type type;
            public readonly TypeNameStringShorteningFlags typeNameShorteningFlags;
            public readonly string ExpectedResult;

            public StringBuilderAppendUserFriendlyTypeNameTestArgs(Type type, TypeNameStringShorteningFlags typeNameShorteningFlags, string expectedResult)
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
        public void StringBuilder_AppendUserFriendlyTypeName_returns_the_right_string()
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

            var casesToTest = new List<StringBuilderAppendUserFriendlyTypeNameTestArgs>()
                    {
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<bool?>), TypeNameStringShorteningFlags.All, "List<bool?>"),

                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.All, "int?"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.ExcludeNamespace, "Nullable<Int32>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.ShortNullableTypeNames, "System.Int32?"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.UseTypeNameAliases, "System.Nullable<int>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(int?), TypeNameStringShorteningFlags.None, "System.Nullable<System.Int32>"),

                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.All, "List<int>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.ExcludeNamespace, "List<Int32>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.ShortNullableTypeNames, "System.Collections.Generic.List<System.Int32>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.UseTypeNameAliases, "System.Collections.Generic.List<int>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<int>), TypeNameStringShorteningFlags.None, "System.Collections.Generic.List<System.Int32>"),

                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.All,
                            "List<KeyValuePair<int, Queue<int?>>>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.ExcludeNamespace,
                            "List<KeyValuePair<Int32, Queue<Nullable<Int32>>>>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.ShortNullableTypeNames,
                            "System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32, System.Collections.Generic.Queue<System.Int32?>>>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.UseTypeNameAliases,
                            "System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.Queue<System.Nullable<int>>>>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeof(List<KeyValuePair<int, Queue<Nullable<int>>>>), TypeNameStringShorteningFlags.None,
                            "System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Int32, System.Collections.Generic.Queue<System.Nullable<System.Int32>>>>"),
                    };

            foreach (var typeWithAlias in typeAliases)
            {
                casesToTest.AddRange(new StringBuilderAppendUserFriendlyTypeNameTestArgs[]
                    {
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.All, typeWithAlias.alias),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.ExcludeNamespace, typeWithAlias.type.Name),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.ShortNullableTypeNames, typeWithAlias.type.FullName),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.UseTypeNameAliases, typeWithAlias.alias),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(typeWithAlias.type, TypeNameStringShorteningFlags.None, typeWithAlias.type.FullName),
                    });

                if (typeWithAlias.type.IsValueType)
                {
                    var type = typeof(Nullable<>).MakeGenericType(typeWithAlias.type); // ulong?
                    var listType = typeof(List<>).MakeGenericType(type); // List<ulong?>

                    casesToTest.AddRange(new StringBuilderAppendUserFriendlyTypeNameTestArgs[]
                    {
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.All, 
                            $"List<{typeWithAlias.alias}?>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.ExcludeNamespace, 
                            $"List<Nullable<{typeWithAlias.type.Name}>>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.ShortNullableTypeNames,
                            $"System.Collections.Generic.List<{typeWithAlias.type.FullName}?>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.UseTypeNameAliases,
                            $"System.Collections.Generic.List<System.Nullable<{typeWithAlias.alias}>>"),
                        new StringBuilderAppendUserFriendlyTypeNameTestArgs(listType, TypeNameStringShorteningFlags.None,
                            $"System.Collections.Generic.List<System.Nullable<{typeWithAlias.type.FullName}>>"),
                    });
                }
            }

            var sb = new StringBuilder();
            foreach (var testCase in casesToTest)
            {
                var result = sb.Clear()
                    .AppendUserFriendlyTypeName(testCase.type, testCase.typeNameShorteningFlags)
                    .ToString();

                Assert.AreEqual(testCase.ExpectedResult, result, $"AppendUserFriendlyTypeName test failed for Type='{testCase.type.FullName}', Flags='{testCase.typeNameShorteningFlags}'.");
                //System.Diagnostics.Debug.WriteLine($"AppendUserFriendlyTypeName test passed for Type='{result}', Flags='{testCase.typeNameShorteningFlags}'.");
            }
        }
        #endregion AppendUserFriendlyTypeName
    }
}
