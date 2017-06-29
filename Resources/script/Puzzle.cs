using UnityEngine;
using System.Collections.Generic;


public static class Puzzle
{
    private static int[,] table = new int[4,4];
    private static int hiscore = 0;
    private static int score = 0;

    public static void MoveLeft()
    {
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j <= 2; ++j)
            {
                for(int k = j+1; k <= 3; ++k)
                {
                    if((table[i,j] == 0) && (table[i,k] != 0))
                    {
                        table[i,j] = table[i,k];
                        table[i,k] = 0;
                        break;
                    }
                }
            }
        }
    }

    public static void MoveRight()
    {
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 3; j >= 1; --j)
            {
                for(int k = j-1; k >= 0; --k)
                {
                    if ((table[i,j] == 0) && (table[i,k] != 0))
                    {    
                        table[i,j] = table[i,k];
                        table[i,k] = 0;
                        break;
                    }
                }
            }
        }
    }

    public static void MoveUp()
    {
        for(int j = 0; j < 4; ++j)
        {
            for (int i = 0; i <= 2; ++i)
            {
                for (int k = i+1; k <= 3; ++k)
                {
                    if ((table[i,j] == 0) && (table[k,j] != 0))
                    {
                        table[i,j] = table[k,j];
                        table[k,j] = 0;
                        break;
                    }
                }
            }
        }
    }

    public static void MoveDown()
    {
        for(int j = 0; j < 4; ++j)
        {
            for(int i = 3; i >= 1; --i)
            {
                for(int k = i-1; k >= 0; --k)
                {
                    if ((table[i,j] == 0) && (table[k,j] != 0))
                    {
                        table[i,j] = table[k,j];
                        table[k,j] = 0;
                        break;
                    }
                }
            }
        }
    }

    public static void AddLeft()
    {
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j <= 2; ++j)
            {
                if ((table[i,j] != 0) && (table[i,j] == table[i,j+1]))
                {
                    table[i,j] *= 2;
                    table[i,j+1] = 0;
                    Score(table[i,j]);
                }
            }
        }
    }

    public static void AddRight()
    {
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 3; j >= 1; --j)
            {
                if ((table[i,j] != 0) && (table[i,j] == table[i,j-1]))
                {
                    table[i,j] *= 2;
                    table[i,j-1] = 0;
                    Score(table[i,j]);
                }
            }
        }
    }

    public static void AddUp()
    {
        for(int j = 0; j < 4; ++j)
        {
            for(int i = 0; i <= 2; ++i)
            {
                if ((table[i,j] != 0) && (table[i,j] == table[i+1,j]))
                {
                    table[i,j] *= 2;
                    table[i+1,j] = 0;
                    Score(table[i,j]);
                }
            }
        }
    }

    public static void AddDown()
    {
        for(int j = 0; j < 4; ++j)
        {
            for(int i = 3; i >= 1; --i)
            {
                if ((table[i,j] != 0) && (table[i,j] == table[i-1,j]))
                {
                    table[i,j] *= 2;
                    table[i-1,j] = 0;
                    Score(table[i,j]);
                }
            }
        }
    }

    public static void Init()
    {
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j < 4; ++j) table[i,j] = 0;
        }

        Drop();
        Drop();
        DebugLog();
    }

    public static void Drop()
    {
        int num; 

        if (IsFill()) return;

        num = Random.Range(0, 20);
        // 2 4 = 1:19
        if (num == 0) num = 4;
        else num = 2;

        while (true)
        {
            int x = Random.Range(0,4);
            int y = Random.Range(0,4);
            if (table[x,y] == 0)
            {
                table[x,y] = num;
                break;
            }
        }
        // DebugLog();
    }

    public static bool IsGameOver()
    {
        if (!IsFill()) return false;

        // 横方向に同じ数字が隣接していないか?
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j <= 2; ++j)
            {
                if (table[i,j] == table[i,j+1]) return false;
            }
        }
        // 縦方向に同じ数字が隣接していないか?
        for(int j = 0; j < 4; ++j)
        {
            for(int i = 0; i <= 2; ++i)
            {
                if (table[i,j] == table[i+1,j]) return false;
            }
        }
        return true;
    }

    public static int Score() { return score; }

    public static void ScoreReset() { score = 0; }

    public static void Score(int inc) { score += inc; }

    public static int GetTable(int x, int y) { return table[x,y]; }

    private static bool IsFill()
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (table[i, j] == 0) return false;
            }
        }
        return true;
    }

    public static bool IsMove(string dir)
    {
        int[,] table_copy = new int[4, 4]
        {
            { table[0,0],table[0,1],table[0,2],table[0,3] },
            { table[1,0],table[1,1],table[1,2],table[1,3] },
            { table[2,0],table[2,1],table[2,2],table[2,3] },
            { table[3,0],table[3,1],table[3,2],table[3,3] }
        };
        if (dir == "Left")
        {
            MoveLeft();
            AddLeft();
            MoveLeft();
        }
        else if (dir == "Right")
        {
            MoveRight();
            AddRight();
            MoveRight();
        }
        else if (dir == "Up")
        {
            MoveUp();
            AddUp();
            MoveUp();
        }
        else if (dir == "Down")
        {
            MoveDown();
            AddDown();
            MoveDown();
        }
        
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j < 4; ++j)
            {
                if (table_copy[i,j] != table[i,j]) return true;
            }
        }
        return false;
    }

    private static void DebugLog()
    {
        for(int i = 0; i < 4; ++i)
        {
            Debug.Log(table[i,0]+","+table[i,1]+","+table[i,2]+","+table[i,3]);
        }
        Debug.Log("--- score " + score);
    }
}
