using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Queue<string> queue = new Queue<string>();

        // Enqueue: 데이터 넣기
        queue.Enqueue("첫 번째");
        queue.Enqueue("두 번째");
        queue.Enqueue("세 번째");

        Debug.Log("========== Queue 1 ==========");
        foreach (string item in queue)
            Debug.Log(item);
        Debug.Log("============================");

        // Peek: 맨 앞 확인
        Debug.Log("Peek: " + queue.Peek()); // "첫 번째"

        // Dequeue: 맨 앞 꺼내기
        Debug.Log("Dequeue: " + queue.Dequeue());   // "첫 번째"
        Debug.Log("Dequeue: " + queue.Dequeue());   // "두 번째"

        Debug.Log("남은 데이터 수: " + queue.Count);  // 1
        
        Debug.Log("========== Queue 2 ==========");
        foreach (string item in queue)
            Debug.Log(item);
        Debug.Log("============================");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
