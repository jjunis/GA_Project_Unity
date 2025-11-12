using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaze : MonoBehaviour
{

    public static RandomMaze Instance;

    [Header("미로 설정")]
    public int width = 5;
    public int height = 5;
    public GameObject cellPrefab;
    public float cellSize = 1f;

    private MazeCell[,] maze;
    private Stack<MazeCell> cellstack;                  //DFS를 위한 스택

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMaze();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMaze();

        }
    }

    public void GenerateMaze()
    {
        maze = new MazeCell[width, height];
        cellstack = new Stack<MazeCell>();

        CreateCells();              //모든 셀 생성

    }

    void GenerateWithDFS()          //DFS 알고리즘으로 생성
    {
        MazeCell current = maze[0, 0];
        current.visited = true;
        cellstack.Push(current);        //첫번째 현재칸을 스택에 넣는다

        while (cellstack.Count > 0)
        {
            current = cellstack.Peek();

            List<MazeCell> unvisitedNeighbors = GetUnvisitedNeighbors(current); //방문하지 않은 이웃 찾기

            if (unvisitedNeighbors.Count > 0)
            {
                MazeCell next = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];  //랜덤하게 이웃 선택
                RemoveWallBetween(current, next);       //벽 제거
                next.visited = true;
                cellstack.Push(next);
            }
            else
            {
                cellstack.Pop();        //벽 트래킹
            }
        }
    }

    void CreateCells()
    {
        if (cellstack == null)
        {
            Debug.LogError("셀 프리팹이 없음");
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * cellSize, 0, z * cellSize);
                GameObject CellObj = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                CellObj.name = $"Cell {x} {z}";

                MazeCell cell = CellObj.GetComponent<MazeCell>();
                if (cell == null)
                {
                    Debug.LogError("MazeCell 스크립트 없음");
                    return;
                }
                cell.Initialize(x, z);
                maze[x, z] = cell;
            }
        }
    }

    List<MazeCell> GetUnvisitedNeighbors(MazeCell cell)
    {
        List<MazeCell> neighbors = new List<MazeCell>();

        //상하좌우 체크
        if (cell.x > 0 && !maze[cell.x - 1, cell.z].visited)
            neighbors.Add(maze[cell.x - 1, cell.z]);
        if (cell.x < width - 1 && !maze[cell.x + 1, cell.z].visited)
            neighbors.Add(maze[cell.x + 1, cell.z]);
        if (cell.z > 0 && !maze[cell.x, cell.z - 1].visited)
            neighbors.Add(maze[cell.x, cell.z - 1]);
        if (cell.z < width - 1 && !maze[cell.x, cell.z + 1].visited)
            neighbors.Add(maze[cell.x, cell.z + 1]);

        return neighbors;
    }

    void RemoveWallBetween(MazeCell current, MazeCell next)
    {
        if (current.x < next.x)      //오른쪽
        {
            current.RemoveWall("right");
            next.RemoveWall("left");
        }
        else if (current.x > next.x)     //왼쪽
        {
            current.RemoveWall("left");
            next.RemoveWall("right");
        }
        else if (current.z < next.z)    //위쪽
        {
            current.RemoveWall("top");
            next.RemoveWall("bottom");
        }
        else if (current.z > next.z)    //아래쪽
        {
            current.RemoveWall("bottom");
            next.RemoveWall("top");
        }
    }

    public MazeCell GetCell(int x, int z)
    {
        if (x >= 0 && x < width && z >= 0 && z < height)
            return maze[x, z];

        return null;
    }

    void ResetAllColors()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                maze[x, z].SetColor(Color.white);
            }
        }
    }

    void Clear()
    {

    }
}