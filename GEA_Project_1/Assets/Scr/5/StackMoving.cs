using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMoving : MonoBehaviour
{
    public float speed = 5f;

    private Stack<Vector3> moveHistory;

    // Start is called before the first frame update
    void Start()
    {
        moveHistory = new Stack<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // 이동 입력이 있을 때
        if (x != 0 || y != 0)
        {
            moveHistory.Push(transform.position);

            Vector3 move = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
            transform.position += move;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (moveHistory.Count > 0)
            {
                transform.position = moveHistory.Pop();
            }
        }
    }
}
