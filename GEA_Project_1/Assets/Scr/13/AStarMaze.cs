using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMaze : MonoBehaviour
{
    public int width = 30;
    public int height = 30;

    // 0=º®, 1=¶¥, 2=½£, 3=ÁøÈë
    public int[,] map;

    public int[,] GenerateValidMap()
    {
        while (true)
        {
            map = GenerateRandomMap();

            // DFS·Î Å»Ãâ °¡´ÉÇÑÁö °Ë»ç
            if (CanEscape(map))
                return map;
        }
    }

    int[,] GenerateRandomMap()
    {
        int[,] m = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    m[y, x] = 0;   // ¿Ü°û º®
                else
                {
                    int r = Random.Range(0, 100);
                    if (r < 35) m[y, x] = 0;       // 35%
                    else if (r < 65) m[y, x] = 1;  // 30%
                    else if (r < 85) m[y, x] = 2;  // 20%
                    else m[y, x] = 3;             // 15%
                }
            }
        }
        return m;
    }

    bool CanEscape(int[,] map)
    {
        int h = map.GetLength(0);
        int w = map.GetLength(1);

        bool[,] visited = new bool[h, w];
        Stack<Vector2Int> stack = new Stack<Vector2Int>();

        stack.Push(new Vector2Int(1, 1));

        while (stack.Count > 0)
        {
            var cur = stack.Pop();
            int x = cur.x;
            int y = cur.y;

            if (visited[y, x]) continue;
            visited[y, x] = true;

            if (x == w - 2 && y == h - 2)
                return true; // goal µµÂø!

            Vector2Int[] dirs =
            {
                new Vector2Int(1,0),
                new Vector2Int(-1,0),
                new Vector2Int(0,1),
                new Vector2Int(0,-1),
            };

            foreach (var d in dirs)
            {
                int nx = x + d.x;
                int ny = y + d.y;

                if (nx < 0 || ny < 0 || nx >= w || ny >= h)
                    continue;
                if (map[ny, nx] == 0) continue; // º®

                if (!visited[ny, nx])
                    stack.Push(new Vector2Int(nx, ny));
            }
        }
        return false;
    }
}
