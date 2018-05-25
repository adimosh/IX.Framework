using global::System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IX.System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;

namespace IX.Guaranteed.Collections
{
    public class PersistedQueue<T> : IQueue<T>
    {
        private readonly string persistenceFolderPath;

        public PersistedQueue(string persistenceFolderPath)
        {
            if (string.IsNullOrWhiteSpace(persistenceFolderPath))
            {
                throw new ArgumentNullException(nameof(persistenceFolderPath));
            }

            this.persistenceFolderPath = persistenceFolderPath;
        }

        public int Count => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public void Clear() => throw new NotImplementedException();
        public bool Contains(T item) => throw new NotImplementedException();
        public void CopyTo(Array array, int index) => throw new NotImplementedException();
        public T Dequeue() => throw new NotImplementedException();
        public void Enqueue(T item) => throw new NotImplementedException();
        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();
        public T Peek() => throw new NotImplementedException();
        public T[] ToArray() => throw new NotImplementedException();
        public void TrimExcess() => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
