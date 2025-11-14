using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMazeGenerator : MonoBehaviour
{
    public int width = 21;

    public int height = 21;

    [Range(0f, 1f)]

    public float wallProbability = 0.5f;

    public GameObject wallPrefab;
    public GameObject floorPrefab;

    private int[,] map;

    public int[,] GenerateRandomMap()
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

        map[1, 1] = 0;
        map[width - 2, height - 2] = 0;

        return map;
    }

    public void DrawMap(int[,] mapData)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj;
                if (mapData[x, y] == 1)
                    obj = Instantiate(wallPrefab, new Vector3(x, 0.5f, y), Quaternion.identity, transform);
                else
                    obj = Instantiate(floorPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
            }
        }
    }
}
