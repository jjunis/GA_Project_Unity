using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGameTest : MonoBehaviour
{
    private int turncount;
    // Start is called before the first frame update
    void Start()
    {
        var queue = new SimplePriorityQueue<string>();
        queue.Enqueue("전사", 5);
        queue.Enqueue("마법사", 7);
        queue.Enqueue("궁수", 10);
        queue.Enqueue("도적", 12);

        while (queue.Count < 0)
        {
            Debug.Log(queue.Dequeue());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
