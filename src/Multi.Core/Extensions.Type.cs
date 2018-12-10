using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;

namespace System
{
    public static partial class Extensions
    {
        public static StringBuilder AppendUserFriendlyTypeName(this StringBuilder sb, Type type, bool shortNullableTypeNames, bool includeNamespace = true)
        {
            if (sb == null)
                throw new ArgumentNullException(nameof(sb));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            string typeName = (includeNamespace) ? type.FullName : type.Name;

            if (type.IsConstructedGenericType)
            {
                Type[] genArgTypes = type.GenericTypeArguments;
                if ((genArgTypes != null) && (genArgTypes.Length > 0))
                {
                    if (shortNullableTypeNames && type.FullName.StartsWith("System.Nullable`1"))
                        //sb.Append(genArgTypes[0].Name).Append("?");
                        AppendUserFriendlyTypeName(sb, genArgTypes[0], shortNullableTypeNames, includeNamespace).Append("?");
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
                                AppendUserFriendlyTypeName(sb, genArgType, shortNullableTypeNames, includeNamespace);
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
                sb.Append(typeName);
            }
            return sb;
        }

        public static string GetUserFriendlyTypeName(this Type type, bool shortNullableTypeNames, bool includeNamespace = true)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsConstructedGenericType)
            {
                return AppendUserFriendlyTypeName(new StringBuilder(), type, shortNullableTypeNames, includeNamespace)
                    .ToString();
            }
            return (includeNamespace) ? type.FullName : type.Name;
        }

        public static bool IsAnonymous(this Type type)
        {
            var ti = type.GetTypeInfo();
            if ((type.Namespace == null)
                && ti.IsGenericType
                && type.Name.Contains("AnonymousType"))
            {
                var d = type.GetGenericTypeDefinition();
                var dti = d.GetTypeInfo();
				if (dti.IsClass && dti.IsSealed && !dti.IsPublic
                    && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
					&& ti.IsDefined(typeof(CompilerGeneratedAttribute), false))
                {
                    return true;
                }
            }
            return false;
        }

        public static Type GetRootDeclaringType(this Type type)
        {
            Type ret = type;
            while (ret.DeclaringType != null)
                ret = ret.DeclaringType;
            return ret;
        }

        public static bool ImplementsInterface<T>(this Type type)
            where T : class
        {
            return type.ImplementsInterface(typeof(T));
        }
        public static bool ImplementsInterface(this Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (interfaceType == null)
                throw new ArgumentNullException(nameof(interfaceType));

            var iti = interfaceType.GetTypeInfo();
            if (!iti.IsInterface)
                throw new ArgumentException("The provided interfaceType is not an interface!", nameof(interfaceType));

            return iti.ImplementedInterfaces.Contains(interfaceType);
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static TypeInfo GetTypeInfo(this Type type)
        {
            IReflectableType reflectableType = (IReflectableType)type;
            return reflectableType.GetTypeInfo();
        }
    }
}
