using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathfinder : MonoBehaviour
{
    public List<Vector2Int> Dijkstra(int[,] map, Vector2Int start, Vector2Int goal)
    {
        int height = map.GetLength(0);
        int width = map.GetLength(1);

        int[,] dist = new int[height, width];
        Vector2Int?[,] parent = new Vector2Int?[height, width];
        bool[,] visited = new bool[height, width];

        // 거리 초기화
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                dist[y, x] = int.MaxValue;

        dist[start.y, start.x] = 0;

        PriorityQueue<Vector2Int> pq = new PriorityQueue<Vector2Int>();
        pq.Enqueue(start, 0);

        Vector2Int[] dirs =
        {
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
        };

        while (pq.TryDequeue(out Vector2Int cur, out int curPriority))
        {
            int cx = cur.x;
            int cy = cur.y;

            if (visited[cy, cx]) continue;
            visited[cy, cx] = true;

            // 목표 도착
            if (cur == goal)
                return BuildPath(parent, start, goal);

            foreach (var d in dirs)
            {
                int nx = cx + d.x;
                int ny = cy + d.y;

                if (nx < 0 || ny < 0 || nx >= width || ny >= height)
                    continue;

                if (map[ny, nx] == 0) // 벽은 통과 불가
                    continue;

                int cost = TileCost(map[ny, nx]);
                int newDist = dist[cy, cx] + cost;

                if (newDist < dist[ny, nx])
                {
                    dist[ny, nx] = newDist;
                    parent[ny, nx] = cur;
                    pq.Enqueue(new Vector2Int(nx, ny), newDist);
                }
            }
        }

        return null; // 경로 없음
    }

    int TileCost(int tile)
    {
        switch (tile)
        {
            case 1: return 1;
            case 2: return 3;
            case 3: return 5;
        }
        return 999999;
    }

    List<Vector2Int> BuildPath(Vector2Int?[,] parent, Vector2Int start, Vector2Int goal)
    {
        List<Vector2Int> path = new();

        Vector2Int? cur = goal;

        while (cur.HasValue)
        {
            path.Add(cur.Value);
            if (cur.Value == start) break;

            cur = parent[cur.Value.y, cur.Value.x];
        }

        path.Reverse();
        return path;
    }
}
