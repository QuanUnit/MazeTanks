using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map settings")]
    [SerializeField] private int lengthMap;
    [SerializeField] private int heightMap;
    [SerializeField] private int x_GenerationPoint;
    [SerializeField] private int y_GenerationPoint;
    [SerializeField] [Range(0, 1)] private float CoeffOfCountPassages;

    [Header("Links")]
    [SerializeField] private GameObject wallPrefab;

    private float wallSize;
    private List<Wall> allWalls = new List<Wall>();

    private void Awake()
    {
        wallSize = 1;
    }
    public GameObject MapGeneration()
    {
        GameObject map = new GameObject("Map");
        for (float x = x_GenerationPoint + 0.5f; x < lengthMap + x_GenerationPoint; x += wallSize)
        {
            for (float y = y_GenerationPoint; y <= heightMap + y_GenerationPoint; y += wallSize)
            {
                allWalls.Add(new Wall(new Vector2(x, y), new Vector3(0, 0, 0)));
            }
        }
        for (float x = x_GenerationPoint; x < lengthMap + x_GenerationPoint + 0.5f; x += wallSize)
        {
            for (float y = y_GenerationPoint + 0.5f; y < heightMap + y_GenerationPoint; y += wallSize)
            {
                allWalls.Add(new Wall(new Vector2(x, y), new Vector3(0, 0, 90)));
            }
        }
        DeleteWalls();
        for (int i = 0; i < allWalls.Count; i++)
        {
            Instantiate(wallPrefab, allWalls[i].Pos, Quaternion.Euler(allWalls[i].Rotation), map.transform);
        }
        return map;
    }
    private List<Cell> CreateCellsGrid()
    {
        List<Cell> cellsGrid = new List<Cell>();
        for (float x = x_GenerationPoint + 0.5f; x < x_GenerationPoint + lengthMap; x++)
        {
            for (float y = y_GenerationPoint + 0.5f; y < y_GenerationPoint + heightMap; y++)
            {
                cellsGrid.Add(new Cell(new Vector2(x, y), false));
            }
        }
        return cellsGrid;
    }

    private void DeleteWalls()
    {
        Stack<Vector2> stack = new Stack<Vector2>();
        List<Cell> CellsGrid = CreateCellsGrid();
        Vector2 CurrCellPos = MapGeneratorTool.GetRandomPos(x_GenerationPoint, y_GenerationPoint, lengthMap, heightMap);
        while (!MapGeneratorTool.CheckAllVisitedCell(CellsGrid))
        {
            List<Cell> neighboringCells = MapGeneratorTool.GetNeighboringCells(CurrCellPos, CellsGrid);
            if (neighboringCells.Count > 0)
            {
                stack.Push(CurrCellPos);
                Cell rndNeighboring = neighboringCells[Random.Range(0, neighboringCells.Count)];
                rndNeighboring.visited = true;
                Vector2 wallPosForDelete = new Vector2((CurrCellPos.x + rndNeighboring.pos.x) / 2, (CurrCellPos.y + rndNeighboring.pos.y) / 2);
                CurrCellPos = rndNeighboring.pos;
                for (int i = 0; i < allWalls.Count; i++)
                {
                    if (allWalls[i].Pos == wallPosForDelete)
                        allWalls.Remove(allWalls[i]);
                }
            }
            else
            {
                CurrCellPos = stack.Pop();
            }
        }
        List<Wall> notBorders = MapGeneratorTool.GetNotBorders(allWalls, x_GenerationPoint, y_GenerationPoint, lengthMap, heightMap);
        for (int i = 0; i < notBorders.Count * CoeffOfCountPassages; i++)
            allWalls.Remove(notBorders[Random.Range(0, notBorders.Count)]);
    }
}
public class Cell
{
    public bool visited;
    public Vector2 pos;
    public Cell(Vector2 pos, bool visited)
    {
        this.pos = pos;
        this.visited = visited;
    }
}
public class Wall
{
    public Vector2 Pos { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Wall(Vector2 Pos, Vector3 Rotation)
    {
        this.Pos = Pos;
        this.Rotation = Rotation;
    }
}
