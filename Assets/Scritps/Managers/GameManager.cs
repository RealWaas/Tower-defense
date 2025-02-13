using System;
using UnityEngine;

public static class GameManager
{

    [RuntimeInitializeOnLoadMethod]
    public static void InitGame()
    {
        GridManager.GenerateGrid();
    }
}
