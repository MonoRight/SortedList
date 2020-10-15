using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.CodeDom;
using System.Data;

namespace Lab1_SortedList
{
    public class MySortedList<U, V> : IEnumerable<Node<U, V>> where U : IComparable<U>
    {
        private Node<U, V> _head;
        private Node<U, V> _tail;

        public U[] Keys { get; private set; }
        public V[] Values { get; private set; }
        public int Count { get; private set; } = 0;

        public delegate void Delegate(U key, V value);
        public event Delegate Updated;

        public MySortedList(params Node<U,V>[] listnodes)
        {
            Count = 0;
            for (int i = 0; i < listnodes.Length; i++) 
            {
                Add(listnodes[i]);
            }
        }
        public void Add(Node<U,V> newnode)
        {
            if (newnode.Key != null) 
            {
                if (ContainsKey(newnode.Key) == false)
                {
                    if (_head == null)
                    {
                        _head = newnode;
                    }
                    else
                    {
                        _tail.Next = newnode;
                    }
                    _tail = newnode;
                    Count++;
                    Updated?.Invoke(newnode.Key, newnode.Value);
                    FillKeysAndValues();
                    SortByKeys();
                }
                else throw new ArgumentException("Key already exists");
            }
            else
            {
                throw new ArgumentNullException("Key can not be null");
            }
        }
        public void Add(U key, V value)
        {
            if(key != null)
            {
                if (ContainsKey(key) == false)
                {
                    Node<U, V> node = new Node<U, V>(key, value);

                    if (_head == null)
                    {
                        _head = node;
                    }
                    else
                    {
                        _tail.Next = node;
                    }
                    _tail = node;
                    Count++;
                    Updated?.Invoke(key, value);
                    FillKeysAndValues();
                    SortByKeys();
                }
                else throw new ArgumentException("Key already exists");
            }
            else throw new ArgumentNullException("Key can not be null");
        }

        private void FillKeysAndValues()
        {
            Node<U, V> current = _head;
            Keys = new U[Count];
            Values = new V[Count];
            int i = 0;

            while (current != null)
            {
                Keys[i] = current.Key;
                Values[i] = current.Value;
                i++;
                current = current.Next;
            }
        }
        public bool ContainsKey(U key)
        {
            bool flag = false;

            for (int i = 0; i < Count; i++) 
            {
                if(key.CompareTo(Keys[i]) == 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private void SortByKeys()
        {
            U swapKey;
            V swapValue;

            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = i + 1; j < Count; j++) 
                {
                    if (this[i].Key.CompareTo(this[j].Key) > 0)
                    {
                        swapKey = this[i].Key;
                        swapValue = this[i].Value;

                        this[i].Key = this[j].Key;
                        this[i].Value = this[j].Value;

                        this[j].Key = swapKey;
                        this[j].Value = swapValue;
                    }
                }
            }
        }

        public void Remove(U key)
        {
            bool flag = ContainsKey(key);
            Node<U, V> current = _head;
            Node<U,V> previous = null;

            if (current == null && Count == 0)
            { 
                throw new NullReferenceException("SortedList is empty");
            }

            if (flag == false) 
            {
                throw new NullReferenceException("Key does not exists");
            }

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current.Next == null)
                        {
                            _tail = current;
                        }
                    }
                    else
                    {
                        _head = _head.Next;

                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }
                    Count--;
                    break;
                }
                previous = current;
                current = current.Next;
            }

            Updated?.Invoke(key, default);

            FillKeysAndValues();
        }

        public bool IsEmpty()
        {
            return (Count == 0);
        }

        public void Clear()
        {
            Count = 0;
            _head = null;
            _tail = null;
        }

        public IEnumerator<Node<U,V>> GetEnumerator()
        {
            Node<U,V> current = _head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Node<U,V> this[int index]
        {
            get
            {
                if (Count == 0)
                {
                    throw new NullReferenceException("SortList is empty");
                }
                else if (index < 0)
                {
                    throw new IndexOutOfRangeException("Index can not be < 0");
                }
                else if (index > Count - 1)
                {
                    throw new IndexOutOfRangeException("Out of count");
                }
                else
                {
                    Node<U,V> node = _head;

                    for (int i = 0; i < index; i++)
                    {
                        node = node.Next;
                    }
                    return node;
                }
            }
        }
    }
}
