using System.Collections.Generic;
using UnityEngine;

public class ScrambledEggsOpeningPosition : IOpeningPosition
{
    public List<Vector2Int> GetPlayerPositions(int boardSize)
    {
        List<Vector2Int> whitePositions = new List<Vector2Int>();

        for (int i = 1; i < boardSize - 1; i++)
        {
            if (i % 2 == 1)
            {
                whitePositions.Add(new Vector2Int(0, i));
                whitePositions.Add(new Vector2Int(i, boardSize - 1));
            }
            else
            {
                whitePositions.Add(new Vector2Int(i, 0));
                whitePositions.Add(new Vector2Int(boardSize - 1, i));
            }

        }
        return whitePositions;
    }

    public List<Vector2Int> GetEnemyPositions(int boardSize)
    {
        List<Vector2Int> blackPositions = new List<Vector2Int>();

        for (int i = 1; i < boardSize - 1; i++)
        {
            if (i % 2 == 1)
            {
                blackPositions.Add(new Vector2Int(i, 0));
                blackPositions.Add(new Vector2Int(boardSize - 1, i));
            }
            else
            {
                blackPositions.Add(new Vector2Int(0, i));
                blackPositions.Add(new Vector2Int(i, boardSize - 1));
            }

        }
        return blackPositions;
    }
}
