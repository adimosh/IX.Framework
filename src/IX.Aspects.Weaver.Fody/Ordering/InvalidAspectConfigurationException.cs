using System;

namespace IX.Aspects.Weaver.Fody.Ordering
{
    public class InvalidAspectConfigurationException : Exception
    {
        public InvalidAspectConfigurationException(string msg)
            : base(msg)
        {
        }
    }
}