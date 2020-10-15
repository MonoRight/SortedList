using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab1_SortedList.Tests
{
    [TestClass]
    public class MySortedListTests
    {
        [TestClass]
        public class AddedByNodeTests
        {
            [TestMethod]
            public void AddByNode_NotNullNodeKey_AddedNodeToSortedList()
            {
                //arrange
                int expectedcount = 3;

                //act (adding)
                MySortedList<int, int> sortlist = new MySortedList<int, int>()
                {
                     new Node<int, int>(1, 2),
                     new Node<int, int>(2, 3),
                     new Node<int, int> (4, 123)
                };

                //assert
                Assert.AreEqual(expectedcount, sortlist.Count);
            }

            [TestMethod]
            public void AddByNode_NullNodeKey_ExpectedThrowArgumentNullException()
            {
                //arrange
                Node<string, int> node = new Node<string, int>(null, 1);
                MySortedList<string, int> sortlist = new MySortedList<string, int>();

                //act and assert
                Assert.ThrowsException<ArgumentNullException>(() => sortlist.Add(node), "Key can not be null");
            }

            [TestMethod]
            public void AddByNode_ExistingNodeKey_ExpectedThrowArgumentException()
            {
                //arrange
                Node<int, int> node = new Node<int, int>(1, 1);
                MySortedList<int, int> sortlist = new MySortedList<int, int>();

                //act
                sortlist.Add(node);

                //act and assert
                Assert.ThrowsException<ArgumentException>(() => sortlist.Add(new Node<int,int>(1,2)), "Key already exists");
            }

            [TestMethod]
            public void AddByNode_UniqueNodeKey_AddedNodeToSortedList()
            {
                //arrange
                Node<int, int> node1 = new Node<int, int>(1, 1);
                Node<int, int> node2 = new Node<int, int>(2, 1234);
                MySortedList<int, int> sortlist = new MySortedList<int, int>();

                //act
                sortlist.Add(node1);
                sortlist.Add(node2);

                //assert
                Assert.AreNotEqual(sortlist[0].Key, sortlist[1].Key);
            }

            [TestMethod]
            public void AddByNode_WithNodeArray_AddedNodesToSortedList()
            {
                //arrange
                int expectedcount = 3;
                Node<int, int>[] nodes = new Node<int, int>[3];
                nodes[0] = new Node<int, int>(1, 2);
                nodes[1] = new Node<int, int>(2, 3);
                nodes[2] = new Node<int, int>(4, 123);

                //act (adding)
                MySortedList<int, int> sortlist = new MySortedList<int, int>(nodes);

                //assert
                Assert.AreEqual(expectedcount, sortlist.Count);
            }
        }

        [TestClass]
        public class AddedByKeyAndValueTests
        {
            [TestMethod]
            public void AddByKeyAndValue_NotNullNodeKey_AddedNodeToSortedList()
            {
                //arrange
                int expectedcount = 2;
                MySortedList<int, int> sortlist = new MySortedList<int, int>();
                int key1 = 1;
                int value1 = 123;
                int key2 = 2;
                int value2 = -321;

                //act (adding)
                sortlist.Add(key1, value1);
                sortlist.Add(key2, value2);

                //assert
                Assert.AreEqual(expectedcount, sortlist.Count);
            }

            [TestMethod]
            public void AddByKeyAndValue_NullNodeKey_ExpectedThrowArgumentNullException()
            {
                //arrange
                string key = null;
                int value = 1;
                MySortedList<string, int> sortlist = new MySortedList<string, int>();

                //act and assert
                Assert.ThrowsException<ArgumentNullException>(() => sortlist.Add(key, value), "Key can not be null");
            }

            [TestMethod]
            public void AddByKeyAndValue_ExistingNodeKey_ExpectedThrowArgumentException()
            {
                //arrange
                int key = 1;
                int value = 123;
                MySortedList<int, int> sortlist = new MySortedList<int, int>();

                //act
                sortlist.Add(key, value);

                //act and assert
                Assert.ThrowsException<ArgumentException>(() => sortlist.Add(1, 412), "Key already exists");
            }

            [TestMethod]
            public void AddByKeyAndValue_UniqueNodeKey_AddedNodeToSortedList()
            {
                //arrange
                int key1 = 1;
                int value1 = 1;
                int key2 = 2;
                int value2 = 1234;
                MySortedList<int, int> sortlist = new MySortedList<int, int>();

                //act
                sortlist.Add(key1, value1);
                sortlist.Add(key2, value2);

                //assert
                Assert.AreNotEqual(sortlist[0].Key, sortlist[1].Key);
            }
        }

        [TestClass]
        public class ContainsKeyTests
        {
            [TestMethod]
            public void SortedListDoesNotContains_Key_ReturnedFalse()
            {
                //arrange
                bool expected = false, actual;
                MySortedList<int, int> sortlist = new MySortedList<int, int>();

                //act
                sortlist.Add(1, 1);
                actual = sortlist.ContainsKey(2);

                //assert
                Assert.AreEqual(expected, actual);
            }

            [TestMethod]
            public void SortedListDoesContains_Key_ReturnedTrue()
            {
                //arrange
                bool expected = true, actual;
                MySortedList<int, int> sortlist = new MySortedList<int, int>();

                //act
                sortlist.Add(1, 1);
                actual = sortlist.ContainsKey(1);

                //assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestClass]
        public class EmptySortedListTests
        {
            [TestMethod]
            public void SortedListIsEmpty_ReturnedTrue()
            {
                //arrange
                bool expected = true, actual;
                MySortedList<int, int> sortedlist = new MySortedList<int, int>();

                //act
                actual = sortedlist.IsEmpty();

                //assert
                Assert.AreEqual(expected, actual);
            }

            [TestMethod]
            public void SortedListIsNotEmpty_ReturnedFalse()
            {
                //arrange
                bool expected = false, actual;
                MySortedList<int, int> sortedlist = new MySortedList<int, int>();
                sortedlist.Add(1, 1);

                //act
                actual = sortedlist.IsEmpty();

                //assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestClass]
        public class ClearSortedListTests
        {
            [TestMethod]
            public void SortedListClearing_HeadIsEqualNullAndCountIs0()
            {
                //arrange
                int expectedcount = 0;
                MySortedList<int, int> sortedlist = new MySortedList<int, int>()
                {
                    new Node<int, int>(1, 2),
                    new Node<int, int>(3, 123),
                    new Node<int, int>(2, 11)
                };

                //act
                sortedlist.Clear();

                //assert
                Assert.AreEqual(expectedcount, sortedlist.Count);
            }
        }

        [TestClass]
        public class SortedListIndexTests
        {
            [TestMethod]
            public void TakeNodeByIndex_CountIsEqual0_ExpectedThrowNullReferenceException()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>();

                //act and assert
                Assert.ThrowsException<NullReferenceException>(() => sortedlist[0], "SortList is empty");
            }

            [TestMethod]
            public void TakeNodeByIndex_IndexLessThan0_ExpectedThrowIndexOutOfRangeException()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>();
                sortedlist.Add("1", "2");

                //act and assert
                Assert.ThrowsException<IndexOutOfRangeException>(() => sortedlist[-1], "Index can not be < 0");
            }

            [TestMethod]
            public void TakeNodeByIndex_IndexMoreThanCount_ExpectedThrowIndexOutOfRangeException()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>();
                sortedlist.Add("1", "2");

                //act and assert
                Assert.ThrowsException<IndexOutOfRangeException>(() => sortedlist[1], "Out of count");
            }

            [TestMethod]
            public void TakeNodeByIndex_ExistingIndex_ReturnedNode()
            {
                //arrange
                Node<string, string> expectednode = new Node<string, string>("1", "2");
                MySortedList<string, string> sortedlist = new MySortedList<string, string>();
                sortedlist.Add("1", "2");

                //act
                Node<string, string> actualdnode = sortedlist[0];

                //assert
                Assert.AreEqual(expectednode.Key, actualdnode.Key);
                Assert.AreEqual(expectednode.Value, actualdnode.Value);
            }
        }

        [TestClass]
        public class RemoveNodesTests
        {
            [TestMethod]
            public void RemoveNode_EmptySortedList_ExpectedThrowNullReferenceException()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>();

                //act and assert
                Assert.ThrowsException<NullReferenceException>(() => sortedlist.Remove("1"), "SortedList is empty");
            }

            [TestMethod]
            public void RemoveNode_KeyDoesNotExists_ExpectedThrowNullReferenceException()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>()
                {
                    new Node<string, string>("123", "123")
                };

                //act and assert
                Assert.ThrowsException<NullReferenceException>(() => sortedlist.Remove("1"), "SortedList is empty");
            }

            [TestMethod]
            public void RemoveFirstNode_KeyExists_NodeWithExistingKeyRemoved()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>()
                {
                    new Node<string, string>("123", "123"),
                    new Node<string, string>("1", "1"),
                    new Node<string, string>("2", "12"),
                    new Node<string, string>("3", "12333")
                };
                int expectedcount = 3;

                //act
                sortedlist.Remove("1");

                //assert
                Assert.AreEqual(expectedcount, sortedlist.Count);
            }

            [TestMethod]
            public void RemoveMiddleNode_KeyExists_NodeWithExistingKeyRemoved()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>()
                {
                    new Node<string, string>("123", "123"),
                    new Node<string, string>("1", "1"),
                    new Node<string, string>("2", "12"),
                    new Node<string, string>("3", "12333")
                };
                int expectedcount = 3;

                //act
                sortedlist.Remove("2");

                //assert
                Assert.AreEqual(expectedcount, sortedlist.Count);
            }

            [TestMethod]
            public void RemoveLastNode_KeyExists_NodeWithExistingKeyRemoved()
            {
                //arrange
                MySortedList<string, string> sortedlist = new MySortedList<string, string>()
                {
                    new Node<string, string>("1", "1"),
                    new Node<string, string>("2", "12"),
                    new Node<string, string>("3", "12333")
                };
                int expectedcount = 2;

                //act
                sortedlist.Remove("3");

                //assert
                Assert.AreEqual(expectedcount, sortedlist.Count);
            }
        }

        [TestClass]
        public class GetEnumeratorTests
        {
            [TestMethod]
            public void GetEnumeratorTests_ReturnedEnumerator()
            {
                //arrange
                int i = 0;
                MySortedList<int, int> sortedlist = new MySortedList<int, int>()
                {
                    new Node<int, int>(1,3),
                    new Node<int, int>(123, 33),
                    new Node<int, int>(14, 54)
                };

                //act and assert
                foreach(Node<int,int> node in sortedlist)
                {
                    Assert.AreEqual(sortedlist[i], node);
                    i++;
                }
            }
        }

        [TestClass]
        public class SortByKeysTests
        {
            [TestMethod]
            public void SortByKeys_ReturnedSortedList()
            {
                //arrange
                int count = 4;
                Node<int, int>[] nodes = new Node<int, int>[count];
                nodes[0] = new Node<int, int>(1, 1);
                nodes[1] = new Node<int, int>(2, 2);
                nodes[2] = new Node<int, int>(3, 3);
                nodes[3] = new Node<int, int>(4, 4);

                MySortedList<int, int> sortedlist = new MySortedList<int, int>()
                {
                    new Node<int,int>(2,2),
                    new Node<int,int>(4,4),
                    new Node<int,int>(1,1),
                    new Node<int,int>(3,3)
                };

                //act and assert
                for (int i = 0; i < count; i++)
                {
                    Assert.AreEqual(nodes[i].Key, sortedlist[i].Key);
                    Assert.AreEqual(nodes[i].Value, sortedlist[i].Value);
                }
            }
        }
    }
}
