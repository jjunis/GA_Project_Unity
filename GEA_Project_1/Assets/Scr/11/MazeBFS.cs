using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBFS : MonoBehaviour
{
        int[,] map =
        {
            {1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 1, 0, 0, 1},
            {1, 0, 0, 0, 1, 0, 1},
            {1, 1, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 1, 1},
            {1, 1, 1, 1, 1, 1, 1},
        };
        Vector2Int start = new Vector2Int(1, 1);
        Vector2Int goal = new Vector2Int(5, 3);
        bool[,] visited;
        Vector2Int?[,] parent;
        Vector2Int[] dirs =
        {
            new Vector2Int(1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
        };
    
    void Start()
    {
        List<Vector2Int> path = FindPathBFS();
    }

    List<Vector2Int> FindPathBFS()
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);
        visited = new bool[w, h];
        parent = new Vector2Int?[w, h];
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(start);
        visited[start.x, start.y] = true;
        while (q.Count > 0)
        {
            Vector2Int cur = q.Dequeue();

            //목표 도착
            if (cur == goal)
            {
                Debug.Log("BFS: Goal 도착!");
                return ReconstructPath();
            }
            
            //네 방향 이웃 탐색
            foreach (var d in dirs)
            {
                int nx = cur.x + d.x;
                int ny = cur.y + d.y;

                if (!InBounds(nx, ny)) continue;    //전체 바운더리
                if (map[nx, ny] == 1) continue;     //넥
                if (visited[nx, ny]) continue;      //이미 방문

                visited[nx, ny] = true;
                parent[nx, ny] = cur;               //경로 복원용 부모
                q.Enqueue(new Vector2Int(nx, ny));
            }
        }
        Debug.Log("BFS: 경로 없음");
        return null;
    }

    bool InBounds(int x, int y)
    {
        return x >= 0 && y >= 0 &&
               x < map.GetLength(0) &&
               y < map.GetLength(1);
    }

    List<Vector2Int> ReconstructPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int? cur = goal;

        // goal -> start 방향으로 parent 따라가기
        while (cur.HasValue)
        {
            path.Add(cur.Value);
            cur = parent[cur.Value.x, cur.Value.y];
        }

        path.Reverse(); //start -> goal 순서로 반전
        Debug.Log($"경로 길이: {path.Count}");
        foreach(var p in path)
        {
            Debug.Log(p);
        }
        return path;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
