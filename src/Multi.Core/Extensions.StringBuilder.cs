using System;
using System.Text;
using Multi;

namespace System
{
    public static partial class Extensions
    {
        public static StringBuilder AppendUserFriendlyTypeName(this StringBuilder sb, Type type, TypeNameStringShorteningFlags typeNameShorteningFlags = TypeNameStringShorteningFlags.All)
        {
            if (sb == null)
                throw new ArgumentNullException(nameof(sb));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            string typeName = typeNameShorteningFlags.HasFlag(TypeNameStringShorteningFlags.ExcludeNamespace) ? type.Name : type.FullName;

            if (type.IsConstructedGenericType)
            {
                Type[] genArgTypes = type.GenericTypeArguments;
                if ((genArgTypes != null) && (genArgTypes.Length > 0))
                {
                    if (typeNameShorteningFlags.HasFlag(TypeNameStringShorteningFlags.ShortNullableTypeNames) && type.FullName.StartsWith("System.Nullable`1"))
                        //sb.Append(genArgTypes[0].Name).Append("?");
                        AppendUserFriendlyTypeName(sb, genArgTypes[0], typeNameShorteningFlags).Append("?");
                    else
                    {
                        bool selfContaining = false;
                        int firstIndex = typeName.IndexOf('`');

                        sb.Append((firstIndex >= 0) ? typeName.Substring(0, firstIndex) : typeName);
                        sb.Append("<");
                        bool appendSeparator = false;
                        foreach (Type genArgType in genArgTypes)
                        {
                            if (appendSeparator)
                                sb.Append(", ");
                            if (genArgType.Equals(type))
                            {
                                selfContaining = true;
                                sb.Append("{0}");
                            }
                            else
                            {
                                AppendUserFriendlyTypeName(sb, genArgType, typeNameShorteningFlags);
                            }
                            appendSeparator = true;
                        }
                        sb.Append(">");
                        if (selfContaining)
                            throw new NotSupportedException("Generic classes containing themself as a generic argument are not supported!");
                    }
                }
            }
            else
            {
                if (typeNameShorteningFlags.HasFlag(TypeNameStringShorteningFlags.UseTypeNameAliases))
                {
                    if (type == typeof(bool))
                        sb.Append("bool");
                    else if (type == typeof(char))
                        sb.Append("char");
                    else if (type == typeof(string))
                        sb.Append("string");
                    else if (type == typeof(byte))
                        sb.Append("byte");
                    else if (type == typeof(sbyte))
                        sb.Append("sbyte");
                    else if (type == typeof(short))
                        sb.Append("short");
                    else if (type == typeof(ushort))
                        sb.Append("ushort");
                    else if (type == typeof(int))
                        sb.Append("int");
                    else if (type == typeof(uint))
                        sb.Append("uint");
                    else if (type == typeof(long))
                        sb.Append("long");
                    else if (type == typeof(ulong))
                        sb.Append("ulong");
                    else if (type == typeof(float))
                        sb.Append("float");
                    else if (type == typeof(double))
                        sb.Append("double");
                    else if (type == typeof(decimal))
                        sb.Append("decimal");
                    else if (type == typeof(object))
                        sb.Append("object");
                    else
                        sb.Append(typeName);
                }
                else
                    sb.Append(typeName);
            }
            return sb;
        }
    }
}
