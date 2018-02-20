using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IX.Math.Registration
{
    public class StandardConstantRegistry : IConstantsRegistry
    {
        public bool Populated => throw new NotImplementedException();

        public ConstantContext AdvertiseConstant(string constant) => throw new NotImplementedException();
        public ConstantContext CloneFrom(ConstantContext previousContext) => throw new NotImplementedException();
        public ConstantContext[] Dump() => throw new NotImplementedException();
        public bool Exists(string constant) => throw new NotImplementedException();
        public ConstantExpression GetConstantExpression(string constant) => throw new NotImplementedException();
    }
}