using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using Multi;

namespace System
{
    public static partial class Extensions
    {
        public static string GetUserFriendlyTypeName(this Type type, TypeNameStringShorteningFlags typeNameShorteningFlags = TypeNameStringShorteningFlags.All)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsConstructedGenericType)
            {
                return AppendUserFriendlyTypeName(new StringBuilder(), type, typeNameShorteningFlags)
                    .ToString();
            }
            return typeNameShorteningFlags.HasFlag(TypeNameStringShorteningFlags.ExcludeNamespace) ? type.Name : type.FullName;
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
