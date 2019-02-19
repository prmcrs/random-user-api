namespace Random.User.Rest.Infrastructure
{
    public class SortHelper
    {
        #region QuickSort

        public static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
                }
            }

        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }

        #endregion

        #region Bubble

        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int passedNum = 1; passedNum < n; passedNum++) //Count how many times
            {
                // This next loop becomes shorter and shorter
                for (int i = 0; i < n - passedNum; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        // exchange elements
                        int temp = array[i]; array[i] = array[i + 1]; array[i + 1] = temp;
                    }
                }
            }
        }

        #endregion

        #region InsertionSort

        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                int B = array[i];
                while ((j > 0) && (array[j - 1] > B))
                {
                    array[j] = array[j - 1];
                    j--;
                }
                array[j] = B;
            }
        }

        #endregion


        #region SelectionSort

        public static void SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minPosition = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[minPosition] > array[j])
                        minPosition = j; //position of the new minimum number
                }
                if (minPosition != i)
                {
                    //Exchange the current element with the smallest remaining
                    int temp = array[i];
                    array[i] = array[minPosition];
                    array[minPosition] = temp;
                }
            }
        }

        #endregion
    }

}