using System;

namespace IX.Framework.Metadata
{
    public interface ITypeParameterOrParameterNameAttribute
    {
        Type ParameterType { get; }

        string ParameterName { get; }
    }
}
