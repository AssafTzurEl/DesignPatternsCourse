using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    class StackOverflowException : Exception
    { }

    class StackUnderflowException : Exception
    { }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> is the Aggregate.
    /// <see cref="Stack{T}"/> is the ConcreteAggregate. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Stack<T> : IEnumerable<T>
    {
        public Stack(int size)
        {
            _items = new T[size];
            _nextIndex = 0;
        }

        public void Push(T item)
        {
            if (_nextIndex < _items.Length)
            {
                _items[_nextIndex] = item; // avoid _items[_nextIndex++]
                ++_nextIndex;
            }
            else
            {
                throw new StackOverflowException();
            }
        }

        public T Pop()
        {
            if (_nextIndex > 0)
            {
                _nextIndex--; // avoid _items[_nextIndex--]
                return _items[_nextIndex + 1];
            }
            else
            {
                throw new StackUnderflowException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new StackIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _nextIndex;

        private T[] _items;
        private int _nextIndex;

        class StackIterator : IEnumerator<T>
        {
            public StackIterator(Stack<T> stack)
            {
                _stack = stack;
                Reset();
            }

            public T Current
            {
                get
                {
                    if (_stack == null)
                    {
                        throw new ObjectDisposedException(nameof(_stack));
                    }

                    if ((_currentIndex < 0) || (_currentIndex >= _stack._nextIndex))
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return _stack._items[_currentIndex];
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _stack = null;
            }

            public bool MoveNext()
            {
                ++_currentIndex;
                return (_currentIndex < _stack._nextIndex);
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            private Stack<T> _stack;
            private int _currentIndex;
        }
    }

    class Program
    {
        static void PrintCollection(IEnumerable<int> aggregate)
        {
            foreach (var item in aggregate)
            {
                Console.WriteLine(item);
            }
        }

        static void Main(string[] args)
        {
            var stack = new Stack<int>(10);

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            PrintCollection(stack);
        }
    }
}
