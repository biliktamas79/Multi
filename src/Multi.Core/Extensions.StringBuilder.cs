using System;
using System.Collections.Generic;
using System.Text;
using Multi;

namespace System
{
    public static partial class Extensions
    {
        internal static Dictionary<ValueTuple<RuntimeTypeHandle, TypeNameStringShorteningFlags>, string> _friendlyGenericTypeNameCache = new Dictionary<ValueTuple<RuntimeTypeHandle, TypeNameStringShorteningFlags>, string>();

        public static StringBuilder AppendFriendlyTypeName(this StringBuilder sb, Type type, TypeNameStringShorteningFlags typeNameShorteningFlags = TypeNameStringShorteningFlags.All)
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
                    var cacheKey = ValueTuple.Create(type.TypeHandle, typeNameShorteningFlags);
                    if (_friendlyGenericTypeNameCache.TryGetValue(cacheKey, out var cachedString))
                        return sb.Append(cachedString);

                    StringBuilder cacheStringBuilder = new StringBuilder();
                    if (typeNameShorteningFlags.HasFlag(TypeNameStringShorteningFlags.ShortNullableTypeNames) && type.FullName.StartsWith("System.Nullable`1"))
                        //sb.Append(genArgTypes[0].Name).Append("?");
                        AppendFriendlyTypeName(cacheStringBuilder, genArgTypes[0], typeNameShorteningFlags).Append("?");
                    else
                    {
                        bool selfContaining = false;
                        int firstIndex = typeName.IndexOf('`');

                        cacheStringBuilder.Append((firstIndex >= 0) ? typeName.Substring(0, firstIndex) : typeName);
                        cacheStringBuilder.Append("<");
                        bool appendSeparator = false;
                        foreach (Type genArgType in genArgTypes)
                        {
                            if (appendSeparator)
                                cacheStringBuilder.Append(", ");
                            if (genArgType.Equals(type))
                            {
                                selfContaining = true;
                                cacheStringBuilder.Append("{0}");
                            }
                            else
                            {
                                AppendFriendlyTypeName(cacheStringBuilder, genArgType, typeNameShorteningFlags);
                            }
                            appendSeparator = true;
                        }
                        cacheStringBuilder.Append(">");
                        if (selfContaining)
                            throw new NotSupportedException("Generic classes containing themself as a generic argument are not supported!");
                    }

                    var cachedTypeName = cacheStringBuilder.ToString();
                    _friendlyGenericTypeNameCache[cacheKey] = cachedTypeName;
                    sb.Append(cachedTypeName);
                }
            }
            else
            {
                if (typeNameShorteningFlags.HasFlag(TypeNameStringShorteningFlags.UseTypeNameAliases))
                {
                    if (TryGetNameAlias(type, out var alias))
                        sb.Append(alias);
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
