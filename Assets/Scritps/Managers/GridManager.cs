using System;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager
{

    public static event Action OnGridGenerationRequest;
    public static event Action OnGridVisualRequest;

    public const float CELL_SIZE = 2.4f;
    public static List<Vector2Int> mainPath { get; private set; } = new List<Vector2Int>();
    public static List<TurretPlacer> placerList { get; private set; } = new List<TurretPlacer>();

    public static void GenerateGrid()
    {
        OnGridGenerationRequest?.Invoke();
    }

    public static void SetGridPath(List<Vector2Int> _pathPoints)
    {
        foreach (Vector2Int point in _pathPoints)
            mainPath.Add(point);
            //mainPath.Add(new Vector3(point.x * CELL_SIZE, 0, point.y * CELL_SIZE));

        OnGridVisualRequest?.Invoke();
    }

    public static void SetHolderList(List<TurretPlacer> _list) => placerList = _list;

    public static Vector3 GetRelativePoint(Vector2Int _point)
    {
        return new Vector3(_point.x * CELL_SIZE, 0, _point.y * CELL_SIZE);
    }
}
