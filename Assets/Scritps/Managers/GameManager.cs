using System;
using UnityEngine;

public static class GameManager
{
    public static event Action OnInitGame;
    public static event Action OnWaveStart;
    public static event Action OnWaveEnd;

    public static void InitGame()
    {
        OnInitGame?.Invoke();
        GridManager.GenerateGrid();
    }

    public static void WaveStart() => OnWaveStart?.Invoke();

    public static void WaveEnd() => OnWaveEnd?.Invoke();
}
