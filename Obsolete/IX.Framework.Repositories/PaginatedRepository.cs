using System;
using System.Collections.Generic;
using System.Linq;

namespace IX.Framework.Repositories
{
    public abstract class PaginatedRepository<TDataContract> : Repository<TDataContract>
        where TDataContract : class
    {
        public override IEnumerable<TDataContract> Select(Func<IQueryable<TDataContract>, IQueryable<TDataContract>> whereFunc)
        {
            return new PaginationEnumerable<TDataContract>(base.Select(whereFunc) as IQueryable<TDataContract>, Globals.DefaultPageSize);
        }
    }
}