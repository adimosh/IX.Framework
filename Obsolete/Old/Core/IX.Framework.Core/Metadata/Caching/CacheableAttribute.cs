using System;

namespace IX.Framework.Metadata.Caching
{
    /// <summary>
    /// Represents a resource that can be stored into a cache.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public sealed class CacheableAttribute : Attribute
    {
        // NOT UNIT TESTED
    }
}