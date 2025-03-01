using System;
using UnityEngine;

public static class GameManager
{
    public static bool isInWave { get; private set; } = false;

    public static event Action OnInitGame;
    public static event Action OnLoadGame;
    public static event Action OnWaveStart;
    public static event Action OnWaveEnd;

    public static void InitGame()
    {
        DataManager.ClearPlayerPrefs();
        OnInitGame?.Invoke();
        GridManager.GenerateGrid();
    }

    public static void LoadGame()
    {
        OnInitGame?.Invoke();
        DataManager.LoadGame();
    }

    public static void WaveStart()
    {
        isInWave = true;
        OnWaveStart?.Invoke();
    }

    public static void WaveEnd()
    {
        isInWave = false;
        PreparationManager.TrySetPreparationPhase();
    }

    public static void PauseTime() => Time.timeScale = 0;
    public static void ResumeTime() => Time.timeScale = 1;

    public static void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
				        Application.Quit();
    #endif
    }
}
