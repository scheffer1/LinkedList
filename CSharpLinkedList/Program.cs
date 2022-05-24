
using System.Collections;

namespace LinkedList
{
    class LinkedList<T> : ICollection<T> where T : IComparable<T>
    {
        internal class LinkedListNode<T>
        {
            public T info;
            public LinkedListNode<T> next;

            public LinkedListNode(T data, LinkedListNode<T> next = null)
            {
                info = data;
                this.next = next;
            }
        }

        public LinkedListNode<T> _first;
        public LinkedListNode<T> _last;

        private class Enumerator : IEnumerator<T>
        {
            private LinkedListNode<T> _first;
            private LinkedListNode<T> _current;
            private bool atBegin;

            public Enumerator(LinkedListNode<T> first)
            {
                _first = first;
                _current = null;
                atBegin = true;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (atBegin)
                {
                    atBegin = false;
                    _current = _first;
                    return true;
                }
                else
                {
                    _current = _current.next;
                    return _current != null;
                }
            }

            public void Reset()
            {
                atBegin = true;
                _current = null;
            }

            public T Current
            {
                get { return _current.info; }
            }

            object IEnumerator.Current => Current;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        void ICollection<T>.Add(T item)
        {
            LinkedListNode<T> tmp = new LinkedListNode<T>(item, null);
            if (Count == 0)
                _first = tmp;
            else
                _last.next = tmp;
            _last = tmp;
            ++Count;
        }

        public void AddLast(T item)
        {
            ((ICollection<T>) this).Add(item);
        }

        public void Clear()
        {
            _first = null;
            _last = null;
            Count = 0;
        }

        public void RemoveFirst()
        {
            Remove(this.First());
        }
        
        public bool Contains(T item)
        {
            foreach (var x in this)
                if (x.Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (var item in this)
            {
                array[i] = item;
                ++i;
            }
        }

        public void RemoveLast()
        {
            Remove(this.Last());
        }
        public bool Remove(T item)
        {
            if (_first != null && _first.info.Equals(item))
            {
                _first = _first.next;
                --Count;
                return true;
            }
            else
            {
                LinkedListNode<T> before = FindPrev(item);
                if (before == null)
                    return false;
                before.next = before.next?.next;
                --Count;
                return true;
            }
        }
        
        public bool Find(T item)
        {
            if (_first != null && _first.info.Equals(item))
            {
                Console.Clear();
                Console.WriteLine(item);
                Console.WriteLine("existe");
                Console.ReadLine();
                return true;

            }
            else
            {
                LinkedListNode<T> before = FindPrev(item);
                if (before == null)
                {
                    Console.Clear();
                    Console.WriteLine("Não tem");
                    Console.ReadLine();
                    return false;
                }
                Console.Clear();
                Console.WriteLine(item);
                Console.WriteLine("existe");
                Console.ReadLine();
                return true;
            }
        }

        public void FindCount(T item)
        {
            int counter = 0;
            if (_first != null && _first.info.Equals(item))
            {
                counter++;
            }
            else
            {
                LinkedListNode<T> before = FindPrev(item);
                if (before == null)
                {
                    Console.Clear();
                    Console.ReadLine();
                    Console.WriteLine("Não tem");

                }
                counter++;

            }
            Console.Clear();
            Console.WriteLine(counter);
            Console.ReadLine();
        }

        private LinkedListNode<T> FindPrev(T item)
        {
            var prev = _first;
            var cur = _first?.next;
            while (cur != null)
            {
                if (cur.info.Equals(item))
                    return prev;
                prev = cur;
                cur = cur.next;
            }

            return null;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;
        
    }
    class Program
    {
        public static void Main(string[] args)
        {
            LinkedList<string> x = new LinkedList<string>();
            do
            {
                Console.WriteLine("Digite a opção");
                Console.WriteLine("1. para add");
                Console.WriteLine("2. para add fim");
                Console.WriteLine("3. para exibir a lista");
                Console.WriteLine("4. para exibir o primeiro elemento da lista");
                Console.WriteLine("5. para exibir o ultimo elemento da lista");
                Console.WriteLine("6. para remover no fim");
                Console.WriteLine("7. para remover no inicio");
                Console.WriteLine("8. para remover um valor");
                Console.WriteLine("9. para pesquisar um valor");
                Console.WriteLine("10. para mostrar quantas vezes um valor está presente na lista");
                Console.WriteLine("11. Exibir a lista em ordem inversa");
                int opt = Int32.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                         
                        ((ICollection<string>)x).Add(Console.ReadLine());
                        break;
                    case 2:
                        x.AddLast(Console.ReadLine());
                        break;
                    case 3:
                        Console.Clear();
                        foreach (var item in x)
                            Console.Write(item + " ");
                        Console.ReadLine();
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine(x.First());
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine(x.Last());
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.Clear();
                        x.RemoveLast();
                        break;
                    case 7:
                        Console.Clear();
                        x.RemoveFirst();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Valor para remover");
                        x.Remove(Console.ReadLine());
                        break;
                    case 9:
                        Console.WriteLine("Valor para pesquisar");
                        x.Find(Console.ReadLine());
                        break;
                    case 10:
                        Console.Clear();
                        x.FindCount(Console.ReadLine());
                        break;
                    case 11:
                        Console.Clear();
                        foreach (var item in x.Reverse())
                            Console.Write(item + " ");
                        Console.ReadLine();
                        Console.WriteLine();
                        break;
                }
            }while(true);
        }
    }
}
    