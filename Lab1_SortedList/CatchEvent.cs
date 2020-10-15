using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SortedList
{
    public class CatchEvent<U,V>
    {
        public void EventHandler(U key, V value)
        {
            Console.WriteLine("Event catched! Key: " + key + " Value: " + value);
        }
    }
}
