using System;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager
{

    public static event Action OnGridGenerationRequest;
    public static List<Vector3> mainPath { get; private set; } = new List<Vector3>();

    public static void GenerateGrid()
    {
        OnGridGenerationRequest?.Invoke();
    }

    public static void SetGridPath(List<Vector2Int> _pathPoints, float _cellSize)
    {
        foreach (Vector2Int point in _pathPoints)
            mainPath.Add(new Vector3(point.x * _cellSize, 0, point.y * _cellSize));
    }
}
