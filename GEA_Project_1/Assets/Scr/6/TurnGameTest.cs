using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

public class TurnGameTest : MonoBehaviour
{

    private SimplePriorityQueue<(string name, int speed, float nextTurnTime)> turnQueue
        = new SimplePriorityQueue<(string, int, float)>();

    private int turnCount = 1;

    void Start()
    {

        turnQueue.Enqueue(("전사", 5, 0f), 0f);
        turnQueue.Enqueue(("궁수", 10, 0f), 0f);
        turnQueue.Enqueue(("도적", 12, 0f), 0f);
        turnQueue.Enqueue(("마법사", 7, 0f), 0f);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && turnQueue.Count > 0)
        {
            var currentUnit = turnQueue.Dequeue();
            Debug.Log($"{turnCount}턴 / {currentUnit.name} 의 턴입니다.");
            turnCount++;
            float cooldown = 100.0f / currentUnit.speed;
            float newNextTurnTime = currentUnit.nextTurnTime + cooldown;

            var nextTurnUnit = (currentUnit.name, currentUnit.speed, newNextTurnTime);

            turnQueue.Enqueue(nextTurnUnit, newNextTurnTime);
        }
    }

}
