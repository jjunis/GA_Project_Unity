using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMaze : MonoBehaviour
{
    // 1 = 벽, 0 = 길 (아주 작은 예시 맵)
    int[,] map = {
        {1,1,1,1,1},
        {1,0,0,0,1},
        {1,0,1,0,1},
        {1,0,0,0,1},
        {1,1,1,1,1}
    };

    bool[,] visited;    //방문 기록
    Vector2Int goal = new Vector2Int(1, 3);     //도착지 (x = 3, y  = 3)
    Vector2Int[] dirs = { new(1, 0), new(-1, 0), new(0, 1), new(0, -1) };   //탐색 순서

    void Start()
    {
        visited = new bool[map.GetLength(0), map.GetLength(1)];
        bool ok = SearchMaze(1, 1); //시작점 (1,1)
        Debug.Log(ok ? "출구 찾음!" : "출구 없음.");
    }

    bool SearchMaze(int x, int y)
    {
        //범위/벽/재방문 체크
        if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1)) return false;
        if (map[x, y] == 1 || visited[x, y]) return false;

        //방문 표시
        visited[x, y] = true;
        Debug.Log($"이동: ({x},{y})");

        //목표 도달?
        if (x == goal.x && y == goal.y) return true;

        //4방향 재귀 탐색
        foreach (var d in dirs)
            if (SearchMaze(x + d.x, y + d.y)) return true;

        //막혔으면 되돌아감
        Debug.Log($"되돌아감: ({x},{y})");
        return false;
    }

    
    void Update()
    {
        
    }
}
