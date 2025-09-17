using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class StopTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        int[] data1 = GenerateRandomArray(10000);
        int[] data2 = (int[])data1.Clone();
        int[] data3 = (int[])data1.Clone();

        Stopwatch sw = new Stopwatch();

        // 선택 정렬
        sw.Start();
        StartSelectionSort(data1);
        sw.Stop();
        long selectionTime = sw.ElapsedMilliseconds;

        // 버블 정렬
        sw.Reset();
        sw.Start();
        StartBubbleSort(data2);
        sw.Stop();
        long BubbleTime = sw.ElapsedMilliseconds;

        // 퀵 정렬
        sw.Restart();
        sw.Start();
        StartQuickSoft(data3, 0, data3.Length - 1);
        sw.Stop();
        long quickTime = sw.ElapsedMilliseconds;

        // 결과 출력
        UnityEngine.Debug.Log(
            $"Selcion Sort: {selectionTime} ms\n" +
            $"Bubble Sort {BubbleTime} ms\n +" +
            $"Quick Sort {quickTime} ms");
    }
    int[] GenerateRandomArray(int size)
    {
        int[] arr = new int[size];
        System.Random rand = new System.Random();
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(0, 10000);
        }
        return arr;
    }

    public static void StartQuickSoft(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            StartQuickSoft(arr, low, pivotIndex - 1);   //피벗 왼쪽으로 정렬
            StartQuickSoft(arr, pivotIndex + 1, high); //피벗 오른쪽으로 정렬
        }
    }

    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                //swap
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        // pivot 자리 교환
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
    public static void StartBubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    //swap
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            // 이미 정렬된 경우 조기 종료
            if (!swapped) break;
        }
    }
    public static void StartSelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            // swap
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }
}
