using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Utilities
{
    public class AlgorithmsUtilities
    {
        /// <summary>
        /// Delegate representing a sorting method with an array parameter.
        /// </summary>
        /// <param name="array">The array to be sorted.</param>
        public delegate void SortingDelegate(int[] array);

        /// <summary>
        /// Delegate representing a search method with an array and target parameter.
        /// </summary>
        /// <param name="array">The array to be sorted.</param>
        /// <param name="target">The target to be searched.</param>
        public delegate int SearchMethod(int[] array, int target);

        /// <summary>
        /// Swaps two elements in an integer array at the specified positions.
        /// <param name="array">Gets array input.</param>
        /// <param name="index1">Index to swap.</param>
        /// <param name="index2">Index to swap.</param>
        /// </summary>
        public static void Swap(int[] array, int index1, int index2)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");

            if (index1 < 0 || index1 >= array.Length || index2 < 0 || index2 >= array.Length)
                throw new ArgumentOutOfRangeException("Index is out of bounds.");

            // Swap logic
            int temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        /// <summary>
        /// Populates an array with random numbers between 0 and 10 times the array size.
        /// </summary>
        public static void Randomize(int[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");

            Random random = new Random();
            int maxValue = array.Length * 10;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, maxValue + 1); // Random value between 0 and maxValue
            }
        }

        /// <summary>
        /// Creates an array of integers with the specified size, and populates it with random values by using randomize method.
        /// </summary>
        /// <param name="size">The size of the array to create.</param>
        /// <returns>A randomized integer array.</returns>
        public static int[] Prepare(int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than 0.");

            int[] array = new int[size];
            Randomize(array);
            return array;
        }

        /// <summary>
        /// Sorts the given array in ascending order using the Insertion Sort algorithm.
        /// </summary>
        /// <param name="array">The array of integers to be sorted.</param>
        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = key;
            }
            //return array;
        }

        /// <summary>
        /// Sorts the given array in ascending order using the Selection Sort algorithm.
        /// </summary>
        /// <param name="array">The array of integers to be sorted.</param>
        public static void SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                        minIndex = j;
                }
                Swap(array, i, minIndex);
            }
            //return array;
        }

        /// <summary>
        /// Sorts the given array in ascending order using the Bubble Sort algorithm.
        /// </summary>
        /// <param name="array">The array of integers to be sorted.</param>
        public static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(array, j, j + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Sorts the given array in ascending order using the Merge Sort algorithm.
        /// </summary>
        /// <param name="array">The array of integers to be sorted.</param>
        public static void MergeSort(int[] array)
        {
            // if the array has one or no elements, it's already sorted
            if (array.Length <= 1)
                return;

            // Split the array into left and right halves
            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            Array.Copy(array, 0, left, 0, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            // Recursively sort the left and right halves
            MergeSort(left);
            MergeSort(right);

            // Merge the sorted halves into the original array
            Merge(array, left, right);
        }

        /// <summary>
        /// Merges two sorted arrays into a single sorted array.
        /// </summary>
        /// <param name="left">The left sorted sub-array.</param>
        /// <param name="right">The right sorted sub-array.</param>
        /// <returns>A merged sorted array.</returns>
        private static void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;

            // Merge the two arrays while both have elements remaining
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            // Copy any remaining elements from the left array
            while (i < left.Length)
            {
                array[k++] = left[i++];
            }

            // Copy any remaining elements from the right array
            while (j < right.Length)
            {
                array[k++] = right[j++];
            }
        }

        /// <summary>
        /// Sorts the given array in ascending order using the Quick Sort algorithm.
        /// </summary>
        /// <param name="array">The array of integers to be sorted.</param>
        public static void QuickSort(int[] array)
        {
            QuickSortHelper(array, 0, array.Length - 1); ;
        }

        /// <summary>
        /// Helper method for Quick Sort. Partitions the array and recursively sorts each partition.
        /// </summary>
        /// <param name="array">The array of integers to be sorted.</param>
        /// <param name="low">The starting index of the partition.</param>
        /// <param name="high">The ending index of the partition.</param>
        private static int[] QuickSortHelper(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                QuickSortHelper(array, low, pivotIndex - 1);
                QuickSortHelper(array, pivotIndex + 1, high);
            }
            return array;
        }

        /// <summary>
        /// Partitions the array into elements less than and greater than the pivot.
        /// </summary>
        /// <param name="array">The array to partition.</param>
        /// <param name="low">The starting index of the partition.</param>
        /// <param name="high">The ending index of the partition.</param>
        /// <returns>The index of the pivot after partitioning.</returns>
        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, high);

            return i + 1;
        }

        /// <summary>
        /// Sorts a given array without mutating the original array using a Lambda expression.
        /// </summary>
        /// <param name="array">The array to be sorted.</param>
        /// <returns>A new array sorted in ascending order.</returns>
        public static void SortByLambda(int[] array)
        {
            // Create a copy of the original array using Clone() to avoid mutation
            int[] sortedArray = (int[])array.Clone();

            // Use a Lambda expression to sort the copied array
            Array.Sort(sortedArray, (x, y) => x.CompareTo(y));
        }

        /// <summary>
        /// Measures and displays the running time of a sorting algorithm using a delegate.
        /// </summary>
        /// <param name="array">The array to be sorted.</param>
        /// <param name="sortingMethod">The delegate object pointing to the sorting algorithm.</param>
        public static async Task DisplayRunningTime(int[] array, SortingDelegate sortingMethod)
        {
            await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                sortingMethod(array);

                stopwatch.Stop();
                double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine($"{sortingMethod.Method.Name} took {elapsedTime} ms.");
            });
        }

        /// <summary>
        /// checks each element in the list until it finds a match or exhausts the list
        /// </summary>
        public static int LinearSearch(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                    return i; // Return the index if the target is found
            }
            return -1; // Return -1 if the target is not found
        }

        /// <summary>
        /// divides the sorted list, comparing the middle element with the target value
        /// </summary>
        public static int BinarySearch(int[] array, int target)
        {
            int low = 0;
            int high = array.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (array[mid] == target)
                    return mid; // Return the index if the target is found
                else if (array[mid] < target)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return -1; // Return -1 if the target is not found
        }
        public static int SearchByLambda(int[] array, int target)
        {
            return Array.IndexOf(array, array.FirstOrDefault(x => x == target));
        }
        public static async Task DisplayRunningTime(int[] array, SearchMethod searchDelegate, int target)
        {
            await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                int result = searchDelegate(array, target);

                stopwatch.Stop();
                Console.WriteLine(searchDelegate.Method.Name);
                Console.WriteLine($"Search completed in {stopwatch.Elapsed.TotalMilliseconds} ms. Result index: {result}");
            });
        }
    }
}

