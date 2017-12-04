using IX.Framework.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IX.Framework.Reflection
{
    /// <summary>
    /// Contains extension methods for types and assemblies.
    /// </summary>
    public static class TypeExtensions
    {
        #region GetFullyQualifiedTypeName

        /// <summary>
        /// Gets the fully qualified type name of a type info.
        /// </summary>
        /// <param name="type">The type info.</param>
        /// <returns>The FQTN of the type.</returns>
        public static string GetFullyQualifiedTypeName(this TypeInfo type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsGenericParameter)
                return GetTypeNameWithoutNamespace(type);
            else
                return type.Namespace + "." + GetTypeNameWithoutNamespace(type);
        }

        /// <summary>
        /// Gets the fully qualified type name of a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The FQTN of the type.</returns>
        [Pure]
        public static string GetFullyQualifiedTypeName(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return GetFullyQualifiedTypeName(type.GetTypeInfo());
        }

        #endregion GetFullyQualifiedTypeName

        #region GetTypeNameWithoutNamespace

        private const string c_arrayTypeMarker = "[]";

        /// <summary>A <see cref="System.Reflection.TypeInfo"/> extension method that gets type name without the namespace.</summary>
        /// <param name="type">The type info.</param>
        /// <returns>The type name without the namespace.</returns>
        [Pure]
        public static string GetTypeNameWithoutNamespace(this TypeInfo type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            StringBuilder bldr = new StringBuilder();

            if (type.IsNested && !type.IsGenericParameter)
            {
                bldr.Append(type.DeclaringType.GetTypeInfo().GetTypeNameWithoutNamespace());
                bldr.Append("+");
            }

            if (type.IsArray)
            {
                bldr.Append(type.GetElementType().GetTypeNameWithoutNamespace());
                bldr.Append(c_arrayTypeMarker);
            }
            else if (type.IsGenericTypeDefinition)
                bldr.Append(GetGenericTypeNameWithoutNamespace(type, type.GenericTypeParameters));
            else if (type.IsGenericType)
                bldr.Append(GetGenericTypeNameWithoutNamespace(type, type.GenericTypeArguments));
            else
                bldr.Append(type.Name);

            return bldr.ToString();
        }

        /// <summary>
        /// A <see cref="System.Type"/> extension method that gets type name without the
        /// namespace.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name without the namespace.</returns>
        [Pure]
        public static string GetTypeNameWithoutNamespace(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return GetTypeNameWithoutNamespace(type.GetTypeInfo());
        }

        private const string s_genericTypeParameterBeginMarker = "<";
        private const string s_genericTypeParameterEndMarker = ">";
        private const string s_genericTypeParameterCommaSpace = ", ";

        private static string GetGenericTypeNameWithoutNamespace(TypeInfo type, Type[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Split('`')[0]);

            List<string> typeArguments = new List<string>();

            foreach (TypeInfo p in parameters.ConvertAll(p => p.GetTypeInfo())) { typeArguments.Add(GetFullyQualifiedTypeName(p)); };

            if (typeArguments.Count > 0)
            {
                sb.Append(s_genericTypeParameterBeginMarker);
                sb.Append(string.Join(s_genericTypeParameterCommaSpace, typeArguments));
                sb.Append(s_genericTypeParameterEndMarker);
            }

            return sb.ToString();
        }

        #endregion GetTypeNameWithoutNamespace

        #region GetGenericParameterByName

        /// <summary>A MethodInfo extension method that gets a generic parameter by name.</summary>
        /// <param name="info">The method info to act on.</param>
        /// <param name="name">The name of the generic parameter to seek.</param>
        /// <returns>The generic parameter by name.</returns>
        /// <remarks>
        /// This method only works on generic methods and generic method definitions. If the <paramref name="info"/> parameter
        /// does not represent a generic method, this method will always return null.
        /// </remarks>
        public static Type GetGenericParameterByName(this MethodInfo info, string name)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (info.IsGenericMethodDefinition)
            {
                return info.GetGenericArguments().SingleOrDefault(p => p.Name == name);
            }
            else if (info.IsGenericMethod)
            {
                var genericParameter = info.GetGenericMethodDefinition().GetGenericArguments().SingleOrDefault(p => p.Name == name);

                if (genericParameter == null)
                    return null;

                return info.GetGenericArguments()[genericParameter.GenericParameterPosition];
            }
            else
                return null;
        }

        /// <summary>A TypeInfo extension method that gets a generic parameter by name.</summary>
        /// <param name="info">The type to act on.</param>
        /// <param name="name">The name of the generic parameter to seek.</param>
        /// <returns>The generic parameter by name.</returns>
        /// <remarks>
        /// This method only works on generic types and generic type definitions. If the <paramref name="info"/> parameter
        /// does not represent a generic type, this method will always return null.
        /// </remarks>
        public static Type GetGenericParameterByName(this Type info, string name)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return GetGenericParameterByName(info.GetTypeInfo(), name);

        }

        /// <summary>A TypeInfo extension method that gets a generic parameter by name.</summary>
        /// <param name="info">The type info to act on.</param>
        /// <param name="name">The name of the generic parameter to seek.</param>
        /// <returns>The generic parameter by name.</returns>
        /// <remarks>
        /// This method only works on generic types and generic type definitions. If the <paramref name="info"/> parameter
        /// does not represent a generic type, this method will always return null.
        /// </remarks>
        public static Type GetGenericParameterByName(this TypeInfo info, string name)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (info.IsGenericTypeDefinition)
            {
                return info.GenericTypeParameters.SingleOrDefault(p => p.Name == name);
            }
            else if (info.IsGenericType)
            {
                var genericParameter = info.GetGenericTypeDefinition().GetTypeInfo().GenericTypeParameters.SingleOrDefault(p => p.Name == name);

                if (genericParameter == null)
                    return null;

                return info.GenericTypeArguments[genericParameter.GenericParameterPosition];
            }
            else
                return null;
        }

        /// <summary>A TypeInfo extension method that gets a generic parameter by name.</summary>
        /// <param name="info">The type to act on.</param>
        /// <param name="name">The name of the generic parameter to seek.</param>
        /// <param name="inherit"><c>true</c> to look in base classes and implemented interfaces.</param>
        /// <returns>The generic parameter by name.</returns>
        public static Type GetGenericParameterByName(this Type info, string name, bool inherit)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return GetGenericParameterByName(info.GetTypeInfo(), name, inherit);
        }

        /// <summary>A TypeInfo extension method that gets a generic parameter by name.</summary>
        /// <param name="info">The type info to act on.</param>
        /// <param name="name">The name of the generic parameter to seek.</param>
        /// <param name="inherit"><c>true</c> to look in base classes and implemented interfaces.</param>
        /// <returns>The generic parameter by name.</returns>
        public static Type GetGenericParameterByName(this TypeInfo info, string name, bool inherit)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Type typ = GetGenericParameterByName(info, name);

            if (!inherit)
                return typ;

            if (typ != null)
                return typ;

            if (info.BaseType != null)
            {
                typ = info.BaseType.GetTypeInfo().GetGenericParameterByName(name, true);
            }

            if (typ != null)
                return typ;

            foreach (var interfaceType in info.ImplementedInterfaces)
            {
                typ = interfaceType.GetTypeInfo().GetGenericParameterByName(name, true);
                if (typ != null)
                    break;
            }

            return typ;
        }

        /// <summary>A TypeInfo extension method that gets a generic parameter by name.</summary>
        /// <param name="info">The type info to act on.</param>
        /// <param name="name">The name of the generic parameter to seek.</param>
        /// <param name="inherit"><c>true</c> to look in base classes and implemented interfaces.</param>
        /// <returns>The generic parameter by name.</returns>
        public static Type GetGenericParameterByName(this MethodInfo info, string name, bool inherit)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Type typ = GetGenericParameterByName(info, name);

            if (!inherit)
                return typ;

            if (typ != null)
                return typ;

            Type baseType = info.DeclaringType.GetTypeInfo().BaseType;

            if (baseType != null)
            {
                typ = baseType.GetTypeInfo().GetGenericParameterByName(name, true);

                if (typ != null)
                    return typ;

                foreach (var interfaceType in baseType.GetTypeInfo().ImplementedInterfaces)
                {
                    typ = interfaceType.GetTypeInfo().GetGenericParameterByName(name, true);
                    if (typ != null)
                        break;
                }
            }

            return typ;
        }

        #endregion GetGenericParameterByName
    }
}