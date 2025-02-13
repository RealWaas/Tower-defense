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

    public static void SetGridPath(List<Vector2Int> _pathPoints)
    {
        foreach (var point in _pathPoints)
            mainPath.Add(new Vector3(point.x, 0, point.y));
    }
}
