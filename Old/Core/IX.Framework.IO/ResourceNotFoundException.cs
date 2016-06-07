using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IX.Framework.IO
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
            : base()
        { }

        public ResourceNotFoundException(string message)
            : base(message)
        { }

        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
