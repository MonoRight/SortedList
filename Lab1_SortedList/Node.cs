using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SortedList
{
    public class Node<U, V>
    {
        public V Value { get; set; }
        public U Key { get; set; }
        public Node<U,V> Next { get; set; }
        public Node(U key, V value)
        {
            Value = value;
            Key = key;
        }

        public override string ToString()
        {
            return "Key: " + Key + " Value: " + Value;
        }
    }
}
