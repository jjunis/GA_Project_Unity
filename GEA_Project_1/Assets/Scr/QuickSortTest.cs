using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortTest : MonoBehaviour
{
    public void Start()
    {
        int[] data = GenerateRandomArray(100);
        StartQuickSoft(data, 0, data.Length - 1);
        foreach (var item in data)
        {
            Debug.Log(item);
        }

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
