using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraMazeManager : MonoBehaviour
{
    public MazeMapGenerator generator;
    public MazeMapVisualizer visualizer;
    public MazePathfinder pathfinder;

    public GameObject pathMarker;
    public GameObject playerPrefab;
    private int[,] map;
    private Vector2Int start;
    private Vector2Int goal;

    void Start()
    {
        // 맵 생성 및 시각화
        map = generator.GenerateValidMap();
        visualizer.Render(map);

        start = new Vector2Int(1, 1);
        goal = new Vector2Int(map.GetLength(1) - 2, map.GetLength(0) - 2);

        // 플레이어 생성
        if (playerPrefab != null)
            Instantiate(playerPrefab, new Vector3(start.x, 0.5f, start.y), Quaternion.identity);
    }

    // 버튼 클릭 시 호출
    public void ShowShortestPath()
    {
        List<Vector2Int> path = pathfinder.Dijkstra(map, start, goal);

        if (path == null)
        {
            Debug.Log("경로 없음!");
            return;
        }

        foreach (var p in path)
        {
            Instantiate(pathMarker, new Vector3(p.x, 0.5f, p.y), Quaternion.identity);
        }
    }
}
