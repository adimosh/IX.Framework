using System;

namespace IX.Framework.Metadata.Caching
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public sealed class CacheResetAttribute : Attribute, ITypeParameterOrParameterNameAttribute
    {
        // NOT UNIT TESTED

        public CacheResetAttribute(Type type)
        {
            ParameterType = type;
        }

        public CacheResetAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        internal string _parameterName;
        internal Type _type;

        public Type ParameterType { get; private set; }

        public string ParameterName { get; private set; }
    }
}
