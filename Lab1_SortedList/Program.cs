using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SortedList
{
    class Program
    {
        static void Main(string[] args)
        {
            MySortedList<string, string> sortedlist = new MySortedList<string, string>()
            {
                new Node<string, string>("1", "Paul"),
                new Node<string, string>("4", "Maria"),
                new Node<string, string>("3", "Polina"),
                new Node<string, string>("2", "Max")
            };

            CatchEvent<string, string> catchEvent = new CatchEvent<string, string>();
            sortedlist.Updated += catchEvent.EventHandler;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Remove");
                Console.WriteLine("3. Contains key");
                Console.WriteLine("4. Check keys");
                Console.WriteLine("5. Check values");
                Console.WriteLine("6. Is empty?");
                Console.WriteLine("7. Clear");
                Console.WriteLine("8. Count");
                Console.WriteLine("9. View list");
                Console.WriteLine("10. Exit");

                Console.WriteLine("\nEnter the value: ");
                string v = Console.ReadLine();

                switch(v)
                {
                    case "1":
                        {
                            bool flag = true;
                            while(flag)
                            {
                                Console.Clear();
                                Console.WriteLine("Enter the key: ");
                                string key = Console.ReadLine();
                                Console.WriteLine("Enter the value: ");
                                string value = Console.ReadLine();
                                sortedlist.Add(key, value);

                                Console.WriteLine("Continue? [1 - yes; else - no]");
                                string cont = Console.ReadLine();
                                if(cont == "1")
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                }
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            string key;
                            Console.WriteLine("Enter the key: ");
                            key = Console.ReadLine();

                            sortedlist.Remove(key);
                            Console.ReadLine();
                            break;
                        }
                    case "3":
                        {
                            Console.Clear();
                            string key;
                            Console.WriteLine("Enter the key: ");
                            key = Console.ReadLine();

                            Console.WriteLine(sortedlist.ContainsKey(key));
                            Console.ReadLine();
                            break;
                        }
                    case "4":
                        {
                            Console.Clear();
                            Console.WriteLine("Keys: ");
                            for (int i = 0; i < sortedlist.Count; i++) 
                            {
                                Console.WriteLine(sortedlist.Keys[i]);
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "5":
                        {
                            Console.Clear();
                            Console.WriteLine("Keys: ");
                            for (int i = 0; i < sortedlist.Count; i++)
                            {
                                Console.WriteLine(sortedlist.Values[i]);
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "6":
                        {
                            Console.Clear();
                            Console.WriteLine(sortedlist.IsEmpty());
                            Console.ReadLine();
                            break;
                        }
                    case "7":
                        {
                            Console.Clear();
                            sortedlist.Clear();
                            Console.ReadLine();
                            break;
                        }
                    case "8":
                        {
                            Console.Clear();
                            Console.WriteLine("Count: " + sortedlist.Count);
                            Console.ReadLine();
                            break;
                        }
                    case "9":
                        {
                            Console.Clear();
                            foreach (Node<string, string> node in sortedlist)
                            {
                                Console.WriteLine(node.ToString());
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "10":
                        {
                            Console.Clear();
                            Environment.Exit(0);
                            Console.ReadLine();
                            break;
                        }
                }
            }    

        }
    }
}
