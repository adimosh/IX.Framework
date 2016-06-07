using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IX.Framework.Repositories
{
    public class PaginationEnumerable<TDataContract> : IEnumerable<TDataContract>
        where TDataContract : class
    {
        internal PaginationEnumerable(IQueryable<TDataContract> query, int pageSize)
        {
            this.query = query;
            this.pageSize = pageSize;
        }
        
        private IQueryable<TDataContract> query;
        
        private int pageSize;

        public IEnumerator<TDataContract> GetEnumerator()
        {
            int count = 0;
            
            IEnumerable<TDataContract> q1 = query.Take(pageSize);
            foreach (TDataContract c in q1)
            {
                yield return c;
                count++;
            }
            
            if (count == pageSize)
            {
                int pageIndex = 1;
                
                while (true)
                {
                    int currentPage = 0;
                    foreach (TDataContract c in query.Skip(pageSize * pageIndex).Take(pageSize))
                    {
                        yield return c;
                        count++;
                        currentPage++;
                    }
                    
                    if (currentPage < pageSize)
                        break;
                }
            }
            
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}