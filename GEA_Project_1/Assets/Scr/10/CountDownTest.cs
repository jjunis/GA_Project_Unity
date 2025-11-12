using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTest : MonoBehaviour
{
    void CountDown(int n)
    {
        if (n == 0) return;
        Debug.Log(n);
        CountDown(n - 1);
    }
}
