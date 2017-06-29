using UnityEngine;
using System.Collections;

public static class Drow
{
    private static GameObject[] prefab = new GameObject[14]
    {
        (GameObject)Resources.Load("prefab/0"),
        (GameObject)Resources.Load("prefab/2"),
        (GameObject)Resources.Load("prefab/4"),
        (GameObject)Resources.Load("prefab/8"),
        (GameObject)Resources.Load("prefab/16"),
        (GameObject)Resources.Load("prefab/32"),
        (GameObject)Resources.Load("prefab/64"),
        (GameObject)Resources.Load("prefab/128"),
        (GameObject)Resources.Load("prefab/256"),
        (GameObject)Resources.Load("prefab/512"),
        (GameObject)Resources.Load("prefab/1024"),
        (GameObject)Resources.Load("prefab/2048"),
        (GameObject)Resources.Load("prefab/4096"),
        (GameObject)Resources.Load("prefab/8192")
    };

    private static int[] position_x = new int[4] { -3, -1, 1, 3 };
    private static int[] position_y = new int[4] { 3, 1, -1, -3 };

    private static int[] tile = new int[14] { 0, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };


    public static void DrowTable()
    {
        for(int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                Object.Instantiate(prefab[TileNumber(Puzzle.GetTable(i,j))],Point(i,j),Quaternion.identity);
            }
        }
    }

    private static Vector3 Point(int i, int j) { return new Vector3(position_x[j],position_y[i],0); }

    private static int TileNumber(int n)
    {
        for(int i = 0; i < 14; ++i)
        {
            if (tile[i] == n) return i;
        }
        return -1;
    }

    public static void Clear()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("tile");
        foreach(GameObject obj in objs) GameObject.Destroy(obj);
    }
}
