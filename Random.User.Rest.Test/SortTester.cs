using System;
using Random.User.Rest.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Random.User.Rest.Test
{
    [TestClass]
    public class SortTester
    {
        [TestMethod]
        public void TestQuickSort()
        {

            int[] arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };
            int[] sortedArr = new int[] { -4, 0, 2, 5, 6, 11, 18, 22, 51, 67 };

            SortHelper.QuickSort(arr, 0, arr.Length - 1);
            CollectionAssert.AreEqual(sortedArr,arr);

        }

        [TestMethod]
        public void TestBubble()
        {

            int[] arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };
            int[] sortedArr = new int[] { -4, 0, 2, 5, 6, 11, 18, 22, 51, 67 };

            SortHelper.BubbleSort(arr);
            CollectionAssert.AreEqual(sortedArr, arr);

        }

        [TestMethod]
        public void TestInsertionSort()
        {
            int[] arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };
            int[] sortedArr = new int[] { -4, 0, 2, 5, 6, 11, 18, 22, 51, 67 };

            SortHelper.InsertionSort(arr);
            CollectionAssert.AreEqual(sortedArr, arr);

        }

        [TestMethod]
        public void TestSelectionSort()
        {

            int[] arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };
            int[] sortedArr = new int[] { -4, 0, 2, 5, 6, 11, 18, 22, 51, 67 };

            SortHelper.SelectionSort(arr);
            CollectionAssert.AreEqual(sortedArr, arr);

        }
    }
}
