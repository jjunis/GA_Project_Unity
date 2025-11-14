using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMazeMangnager : MonoBehaviour
{
    public RandomMazeGenerator generator;
    public GameObject mazeCubePrefab;

    private int[,] map;

    private bool[,] visited;

    private Vector2Int goal;

    private Vector2Int[] dirs = { new(1, 0), new(-1, 0), new(0, 1), new(0, -1) };

    private List<Vector2Int> mazeList = new List<Vector2Int>(); // DFS 경로 저장용

    void Start()
    {
        GenerateNewMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clearmaze();
            GenerateNewMap();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Clearmaze();
            ShowPath();
        }
    }

    void GenerateNewMap()
    {
        do
        {
            map = generator.GenerateRandomMap();
            visited = new bool[map.GetLength(0), map.GetLength(1)];
            mazeList.Clear();
            goal = new Vector2Int(map.GetLength(0) - 2, map.GetLength(1) - 2);
        }
        while (!SearchMaze(1, 1));

        generator.DrawMap(map);
        //Debug.Log("탈출 가능한 맵 생성 완료!");
    }


    bool SearchMaze(int x, int y)
    {
        if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1)) return false;
        if (map[x, y] == 1 || visited[x, y]) return false;

        visited[x, y] = true;
        mazeList.Add(new Vector2Int(x, y));
        //Debug.Log($"이동: ({x}, {y})");

        if (x == goal.x && y == goal.y) return true;

        foreach (var d in dirs)
            if (SearchMaze(x + d.x, y + d.y)) return true;

        mazeList.RemoveAt(mazeList.Count - 1);
        //Debug.Log($"되돌아감: ({x}, {y})");
        return false;
    }

    void ShowPath()
    {
        foreach (var pos in mazeList)
        {
            Vector3 cubePos = new Vector3(pos.x, 0.5f, pos.y); // 바닥 위에 살짝 띄우기
            GameObject mazeCube = Instantiate(mazeCubePrefab, cubePos, Quaternion.identity);
            mazeCube.transform.localScale = Vector3.one * 0.5f; // 작게 (0.5배 크기)
        }
    }

    //경로 큐브 삭제용
    void Clearmaze()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("mazeCube"))
        {
            Destroy(obj);
        }
    }
}
