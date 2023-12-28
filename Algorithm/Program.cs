using System;
using System.Text;

namespace Algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Mảng đã cho là :");

            var array = new int[] { 6, 9, 2, 4, 7, 1, 5, 10, 8, 3 };
            PrintArray(array);

            Console.WriteLine();
            Console.WriteLine("-----------------------------------");

            Console.Write("Nhập vào phần tử lớn thứ k cần tìm :  = ");
            var kPosition = int.Parse(Console.ReadLine());

            Console.WriteLine();
            var number = GetNumberInKPosition(array, 0, array.Length - 1, kPosition);

            Console.WriteLine($"Số lớn ở vị trí thứ {kPosition} là {number}");
        }

        static int GetNumberInKPosition(int[] arr, int left, int right, int kPosition)
        {
            if (kPosition > 0 && kPosition <= right - left + 1)
            {
                var pivotIndex = GetPivotNumber(arr, left, right);

                if (pivotIndex - left == kPosition - 1) return arr[pivotIndex];

                if (pivotIndex - left > kPosition - 1) return GetNumberInKPosition(arr, left, pivotIndex - 1, kPosition);

                if (pivotIndex - left < kPosition - 1) return GetNumberInKPosition(arr, pivotIndex + 1, right, kPosition - 1 + left - pivotIndex);
            }

            return int.MinValue;

        }

        static void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
        }

        static int GetPivotNumber(int[] arr, int left, int right)
        {
            var pivot = arr[right];

            var indexPivot = left - 1;

            for (var index = left; index < right; index++)
            {
                if (arr[index] >= pivot)
                {
                    indexPivot++;
                    // đổi chỗ cho phần tử indexPivot và phần tử Index 
                    Swap(arr, indexPivot, index);
                }
            }

            indexPivot += 1;

            Swap(arr, indexPivot, right);

            // đổi chỗ pivot lên đầu để tất cả phần tử bé hơn xuống
            return indexPivot;
        }

        static void Swap(int[]arr, int indexPivot, int index)
        {
            var temp = arr[indexPivot];
            arr[indexPivot] = arr[index];
            arr[index] = temp;
        }
    }
}
