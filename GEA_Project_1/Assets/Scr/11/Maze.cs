using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int width = 21;
    public int height = 21;
    [Range(0f, 1f)]
    public float wallProbability = 1f;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject BluePathPrefab;
    public GameObject RedPlayerPrefab;
    public GameObject GreenFarthestPrefab;

    public float moveSpeed = 0.5f;

    private int[,] map;
    private List<Vector2Int> validPath;
    private GameObject playerInstance;
    private GameObject farthestInstance;
    private List<GameObject> pathObjects = new List<GameObject>();

    private Vector2Int startPos = new Vector2Int(1, 1);
    private Vector2Int goalPos;
    private Vector2Int farthestPos;

    Vector2Int[] dirs =
    {
        new Vector2Int(1,0), new Vector2Int(-1,0),
        new Vector2Int(0,1), new Vector2Int(0,-1),
    };

    void Start()
    {
        goalPos = new Vector2Int(width - 2, height - 2);
        GenerateValidMap();
    }

    public void GenerateValidMap()
    {
        int attempt = 0;
        while (true)
        {
            attempt++;
            GenerateRandomMap();
            validPath = FindPathBFS(goalPos);

            if (validPath != null)
            {
                Debug.Log($"맵 생성 성공! ({attempt}회 시도)");
                break;
            }
        }
        FindFarthestPointBFS();

        DrawMap(map);
        SpawnPlayer();
        ShowPath();
        SpawnFarthestCube();
    }

    public void GenerateRandomMap()
    {
        map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    map[x, y] = 1;
                else
                    map[x, y] = (Random.value < wallProbability) ? 1 : 0;
            }
        }
        map[startPos.x, startPos.y] = 0;
        map[goalPos.x, goalPos.y] = 0;
    }

    public void DrawMap(int[,] mapData)
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
        pathObjects.Clear();
        playerInstance = null;
        farthestInstance = null;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapData[x, y] == 1)
                    Instantiate(wallPrefab, new Vector3(x, 0.5f, y), Quaternion.identity, transform);
                else
                    Instantiate(floorPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
            }
        }
    }

    List<Vector2Int> FindPathBFS(Vector2Int target)
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);
        bool[,] visited = new bool[w, h];
        Vector2Int?[,] parent = new Vector2Int?[w, h];
        Queue<Vector2Int> q = new Queue<Vector2Int>();

        q.Enqueue(startPos);
        visited[startPos.x, startPos.y] = true;

        while (q.Count > 0)
        {
            Vector2Int cur = q.Dequeue();
            if (cur == target) return ReconstructPath(parent, target);

            foreach (var d in dirs)
            {
                int nx = cur.x + d.x;
                int ny = cur.y + d.y;

                if (!InBounds(nx, ny) || map[nx, ny] == 1 || visited[nx, ny]) continue;

                visited[nx, ny] = true;
                parent[nx, ny] = cur;
                q.Enqueue(new Vector2Int(nx, ny));
            }
        }
        return null;
    }

    void FindFarthestPointBFS()
    {
        int w = map.GetLength(0);
        int h = map.GetLength(1);
        bool[,] visited = new bool[w, h];
        Queue<Vector2Int> q = new Queue<Vector2Int>();

        q.Enqueue(startPos);
        visited[startPos.x, startPos.y] = true;
        farthestPos = startPos;

        while (q.Count > 0)
        {
            Vector2Int cur = q.Dequeue();
            farthestPos = cur;

            foreach (var d in dirs)
            {
                int nx = cur.x + d.x;
                int ny = cur.y + d.y;

                if (!InBounds(nx, ny) || map[nx, ny] == 1 || visited[nx, ny]) continue;

                visited[nx, ny] = true;
                q.Enqueue(new Vector2Int(nx, ny));
            }
        }
    }

    bool InBounds(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;

    List<Vector2Int> ReconstructPath(Vector2Int?[,] parent, Vector2Int target)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int? cur = target;
        while (cur.HasValue)
        {
            path.Add(cur.Value);
            cur = parent[cur.Value.x, cur.Value.y];
        }
        path.Reverse();
        return path;
    }

    void SpawnPlayer()
    {
        playerInstance = Instantiate(RedPlayerPrefab, new Vector3(startPos.x, 1.2f, startPos.y), Quaternion.identity);
        playerInstance.transform.parent = this.transform;
    }

    public void ShowPath()
    {
        if (validPath == null) return;
        foreach (var node in validPath)
        {
            GameObject pathObj = Instantiate(BluePathPrefab, new Vector3(node.x, 0.1f, node.y), Quaternion.identity);
            pathObj.transform.localScale = Vector3.one * 0.4f;
            pathObj.transform.parent = this.transform;
            pathObjects.Add(pathObj);
        }
    }

    void SpawnFarthestCube()
    {
        if (GreenFarthestPrefab == null) return;

        farthestInstance = Instantiate(GreenFarthestPrefab, new Vector3(farthestPos.x, 0.5f, farthestPos.y), Quaternion.identity);
        farthestInstance.transform.localScale = Vector3.one * 1.2f;
        farthestInstance.transform.parent = this.transform;
    }

    public void MoveButton()
    {
        if (validPath != null && playerInstance != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveRoutine());
        }
    }

    IEnumerator MoveRoutine()
    {
        foreach (var targetPos in validPath)
        {
            playerInstance.transform.position = new Vector3(targetPos.x, 1.2f, targetPos.y);
            yield return new WaitForSeconds(moveSpeed);
        }
    }
}
