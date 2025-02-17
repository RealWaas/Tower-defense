using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize; 
    [SerializeField] private float cellSize;

    [SerializeField] private int minPathCount = 30;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject floorPrefab;

    [SerializeField] private GameObject mainBasePrefab;

    int generationCount = 0;

    private List<Vector2Int> invalidCells = new List<Vector2Int>();

    private List<Vector2Int> mainPath = new List<Vector2Int>();

    /// <summary>
    /// (Direction / weight)
    /// </summary>
    Dictionary<Vector2Int, float> weightedDirections = new Dictionary<Vector2Int, float>()
    {
        {Vector2Int.up, 0.5f},
        {Vector2Int.down, 0.5f},
        {Vector2Int.left, 1f},
        {Vector2Int.right, 0.15f}
    };

    private void Awake()
    {
        GridManager.OnGridGenerationRequest += GenerateGrid;
    }

    private void OnDestroy()
    {
        GridManager.OnGridGenerationRequest -= GenerateGrid;
    }


    [ContextMenu("GeneratePath")]
    public void GenerateGrid()
    {
        generationCount = 0;
        TryGenerateGrid();
    }

    /// <summary>
    /// Try generating grid until a valid one is obtained.
    /// </summary>
    private void TryGenerateGrid()
    {
        StopAllCoroutines();
        ResetGrid();

        generationCount++;
        Debug.Log($"Generation n°{generationCount}");
        
        StartCoroutine(GeneratePathCoroutine());
    }

    private void ResetGrid()
    {
        mainPath.Clear();
        invalidCells.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Add a point on the path.
    /// </summary>
    /// <param name="_cell"></param>
    private void AddPointToPath(Vector2Int _cellPos)
    {
        mainPath.Add(_cellPos);
    }

    /// <summary>
    /// Remove and invalid a point on the path.
    /// </summary>
    /// <param name="_cell"></param>
    private void RemoveLastPointOnPath()
    {
        Vector2Int lastPoint = mainPath.Last();

        mainPath.Remove(lastPoint);
        invalidCells.Add(lastPoint);
    }

    /// <summary>
    /// Get a list of all childrens next to the target
    /// </summary>
    /// <param name="_cell"></param>
    /// <returns></returns>
    private List<Vector2Int> GetNeightborsFromCell(Vector2Int _originCell)
    {
        List<Vector2Int> _result = new List<Vector2Int>();

        // Check all directions
        foreach (Vector2Int dir in weightedDirections.Keys)
        {
            Vector2Int neightborCell = _originCell + dir;

            // Avoid the cells outside of the grid or on the border other than right
            if (neightborCell.x < 1 || neightborCell.x >= gridSize.x || neightborCell.y < 1 || neightborCell.y >= gridSize.y - 1)
                continue;

            _result.Add(neightborCell);
        }
        return _result;
    }

    private float GetCellDirectionWeight(Vector2Int cell) => weightedDirections[cell - mainPath.Last()];

    private Vector2Int GetRandomCellDirection(List<Vector2Int> _targetCells)
    {
        float totalWeight = 0f;

        // Get the total weight of all cells available
        foreach (Vector2Int cell in _targetCells)
            totalWeight += GetCellDirectionWeight(cell);

        // Get a random weight
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;


        foreach (Vector2Int cell in _targetCells)
        {
            cumulativeWeight += GetCellDirectionWeight(cell);

            if (randomValue <= cumulativeWeight)
                return cell;
        }

        Debug.LogError("no cell");
        return Vector2Int.zero;
    }

    /// <summary>
    /// Generate automaticaly the path.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GeneratePathCoroutine()
    {
        // Set StartingPoint
        int randomValue = Random.Range(1, gridSize.y - 1);
        AddPointToPath(new Vector2Int(0, randomValue));

        int safety = 0;

        while (true)
        {
            // Regenerate if the path get revert to its spawn because it cannot be completed
            if (mainPath.Count == 0)
                TryGenerateGrid();

            // Check if the path got to the other side of the grid
            if (mainPath.Last().x >= gridSize.x - 1)
            {
                // If the path isn't long enought, revert by one grid
                if (mainPath.Count <= minPathCount)
                    RemoveLastPointOnPath();
                else
                    break;
            }

            if (safety >= 100)
            {
                Debug.LogError("Safety got triggered");
                TryGenerateGrid();
            }

            yield return new WaitForSeconds(0.01f);

            
            Vector2Int lastPathPoint = mainPath.Last();

            List<Vector2Int> possiblePoints = GetNeightborsFromCell(lastPathPoint);


            // Remove invalid neightbors cells
            possiblePoints = possiblePoints.Where(e =>
            {
                return !invalidCells.Contains(e);
            }).ToList();

            // Remove path neightbors cells
            possiblePoints = possiblePoints.Where(e =>
            {
                return !mainPath.Contains(e);
            }).ToList();

            foreach (Vector2Int points in possiblePoints)
            {
                List<Vector2Int> targetNeightbors = GetNeightborsFromCell(points);

                foreach(Vector2Int neightbor in targetNeightbors)
                {
                    // Do not take into account the current point
                    if (neightbor == lastPathPoint)
                        continue;

                    // If a neightbor cell is already a Path cell, invalid the target
                    if(mainPath.Contains(neightbor))
                        invalidCells.Add(points);
                }
            }

            // Remove invalid cells again
            possiblePoints = possiblePoints.Where(e =>
            {
                return !invalidCells.Contains(e);
            }).ToList();

            // If there is no option to place a point, revert
            if (possiblePoints.Count == 0)
                RemoveLastPointOnPath();
            
            else // Randomly add a path on one of the targets
            {
                Vector2Int randomCell = GetRandomCellDirection(possiblePoints);

                AddPointToPath(randomCell);
            }

            safety++;
        }

        // Insert the enemy spawn point at the begining of list
        mainPath.Insert(0, new Vector2Int (mainPath[0].x -1, mainPath[0].y));

        GridManager.SetGridPath(mainPath, cellSize);

        StartCoroutine(GenerateVisualCoroutine());
    }

    private IEnumerator GenerateVisualCoroutine()
    {
        foreach (Vector2Int cell in mainPath)
        {
            GameObject goPath = Instantiate(pathPrefab, transform);
            //goPath.transform.position = new Vector3((cell.x - gridSize.x / cellSize) * cellSize, 2, (cell.y - gridSize.y / cellSize) * cellSize);
            goPath.transform.position = new Vector3(cell.x * cellSize, 0, cell.y * cellSize);

            yield return new WaitForSeconds(0.1f);
        }

        for (int xIndex = 0; xIndex < gridSize.x; xIndex++)
        {
            for (int yIndex = 0; yIndex < gridSize.y; yIndex++)
            {
                if (mainPath.Contains(new Vector2Int(xIndex, yIndex))) continue;

                GameObject goFloor = Instantiate(floorPrefab, transform);
                //goFloor.transform.position = new Vector3((xIndex - gridSize.x / cellSize) * cellSize, 2, (yIndex - gridSize.y / cellSize) * cellSize);
                goFloor.transform.position = new Vector3(xIndex * cellSize, 0, yIndex * cellSize);

            }
            yield return new WaitForSeconds(0.1f);
        }

        GameObject mainbase = Instantiate(mainBasePrefab, transform);
        mainbase.transform.position = new Vector3(mainPath.Last().x * cellSize, 0, mainPath.Last().y * cellSize);

        GameManager.WaveEnd();
    }
}