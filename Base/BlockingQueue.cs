using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tools.Base
{
    internal sealed class BlockingQueue<T> : IDisposable
    {
        private Queue<T> _queue = new Queue<T>();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(0, int.MaxValue);

        public void Enqueue(T data)
        {
            if (data == null) throw new ArgumentNullException();
            lock (_queue) _queue.Enqueue(data);
            _semaphore.Release();
        }

        public T Dequeue()
        {
            _semaphore.Wait();
            lock (_queue) return _queue.Dequeue();
        }

        void IDisposable.Dispose()
        {
            if (_semaphore != null)
            {
                _semaphore.Dispose();
                _semaphore = null;
            }
        }
    }
}
