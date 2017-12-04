using System;

namespace IX.Framework.Metadata.Caching
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public sealed class CacheableResultsAttribute : Attribute
    {
    }
}