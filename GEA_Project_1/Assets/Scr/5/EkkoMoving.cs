using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkkoMoving : MonoBehaviour
{

    public struct PathPoint
    {
        public Vector3 position;
        public bool isUndoPoint;
    }

    public float playbackSpeed = 5f;
    public float speed = 5f;
    public float minRecordingDistance = 0.5f;


    private Queue<PathPoint> moveQueue;
    private Stack<Vector3> moveHistory;
    private bool isMoving = false;

    private Renderer objectRenderer;
    private Color originalColor;

    private Vector3 targetPos;
    private Vector3 lastRecordedPosition;
    private Vector3 currentWaypoint;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        moveQueue = new Queue<PathPoint>();
        moveHistory = new Stack<Vector3>();

        targetPos = transform.position;
        lastRecordedPosition = transform.position;
        moveHistory.Push(targetPos);
    }

    void Update()
    {
        if (!isMoving)
        {
            HandlePathCreationInput();
        }
        else
        {
            ExecuteMovement();
        }
    }

    private void HandlePathCreationInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(x, y, 0);

        if (!isMoving)
        {
            if (x != 0 || y != 0)
            {
                Vector3 move = new Vector3(x, y, 0).normalized * speed * Time.deltaTime;
                targetPos += move;
                moveQueue.Enqueue(new PathPoint { position = targetPos, isUndoPoint = false });
                moveHistory.Push(targetPos);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (moveHistory.Count > 1)
                {
                    for (int i = 0; i < 200 && moveHistory.Count > 1; i++)
                    {
                        Vector3 poppedPos = moveHistory.Pop();
                        moveQueue.Enqueue(new PathPoint { position = poppedPos, isUndoPoint = true });
                    }
                    if (moveHistory.Count > 0)
                    {
                        Vector3 newLastPos = moveHistory.Peek();
                        moveQueue.Enqueue(new PathPoint { position = newLastPos, isUndoPoint = true });
                        moveHistory.Push(newLastPos);
                        targetPos = newLastPos;
                        lastRecordedPosition = newLastPos;
                    }
                }
            }
        }
        if (moveInput != Vector3.zero)
        {
            targetPos += moveInput.normalized * speed * Time.deltaTime;
        }
        if (Vector3.Distance(targetPos, lastRecordedPosition) > minRecordingDistance)
        {
            moveQueue.Enqueue(new PathPoint { position = targetPos, isUndoPoint = false });
            moveHistory.Push(targetPos);
            lastRecordedPosition = targetPos;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (moveHistory.Count > 1)
            {
                for (int i = 0; i < 200 && moveHistory.Count > 1; i++)
                {
                    Vector3 poppedPos = moveHistory.Pop();
                    moveQueue.Enqueue(new PathPoint { position = poppedPos, isUndoPoint = true });
                }
                if (moveHistory.Count > 0)
                {
                    Vector3 newLastPos = moveHistory.Peek();
                    moveQueue.Enqueue(new PathPoint { position = newLastPos, isUndoPoint = true });
                    moveHistory.Push(newLastPos);
                    targetPos = newLastPos;
                    lastRecordedPosition = newLastPos;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (moveQueue.Count > 0)
            {
                isMoving = true;
                SetNextWaypoint();
            }
        }
    }

    private void ExecuteMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, playbackSpeed * Time.deltaTime);

        if (transform.position == currentWaypoint)
        {
            SetNextWaypoint();
        }
    }

    private void SetNextWaypoint()
    {
        if (moveQueue.Count > 0)
        {
            PathPoint nextPoint = moveQueue.Dequeue();
            currentWaypoint = nextPoint.position;

            if (objectRenderer != null)
            {
                if (nextPoint.isUndoPoint)
                {
                    objectRenderer.material.color = Color.blue;
                }
                else
                {
                    objectRenderer.material.color = originalColor;
                }
            }
        }
        else
        {
            isMoving = false;
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor;
            }
        }
    }

}