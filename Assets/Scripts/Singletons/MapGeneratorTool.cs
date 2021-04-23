using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapGeneratorTool
{
    public static Vector2 GetRandomPos(int x_GenerationPoint, int y_GenerationPoint, int lengthMap, int heightMap)
    {
        Vector2 result;
        result.x = Random.Range(x_GenerationPoint, x_GenerationPoint + lengthMap) + 0.5f;
        result.y = Random.Range(y_GenerationPoint, y_GenerationPoint + heightMap) + 0.5f;
        return result;
    }
    public static bool CheckAllVisitedCell(List<Cell> cellsGrid)
    {
        foreach (var item in cellsGrid)
        {
            if (item.visited == false)
                return false;
        }
        return true;
    }
    public static List<Cell> GetNeighboringCells(Vector2 cellPos, List<Cell> cellsGrid)
    {
        Vector2[] dirs = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };
        List<Cell> neighboringCells = new List<Cell>();
        for (int i = 0; i < 4; i++)
        {
            foreach (var cell in cellsGrid)
            {
                if (cell.pos == cellPos + dirs[i] && cell.visited == false)
                    neighboringCells.Add(cell);
            }
        }
        return neighboringCells;
    }
    public static List<Wall> GetNotBorders(List<Wall> walls, int x_GenerationPoint, int y_GenerationPoint, int lengthMap, int heightMap)
    {
        List<Wall> notBorders = new List<Wall>();
        foreach (var wall in walls)
        {
            Vector2 wallPos = wall.Pos;
            if (wallPos.x > x_GenerationPoint && wallPos.x < x_GenerationPoint + lengthMap && wallPos.y > y_GenerationPoint && wallPos.y < y_GenerationPoint + heightMap)
                notBorders.Add(wall);
        }
        return notBorders;

    }
}
