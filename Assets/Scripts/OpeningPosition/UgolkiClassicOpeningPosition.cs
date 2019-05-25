using System.Collections.Generic;
using UnityEngine;

public class UgolkiClassicOpeningPosition : IOpeningPosition
{
    public List<Vector2Int> GetPlayerPositions(int boardSize)
    {
        List<Vector2Int> whitePositions = new List<Vector2Int>();

        for (int i = 0; i < boardSize / 2 - 1; i++)
        {
            for (int j = boardSize / 2; j < boardSize; j++)
            {
                whitePositions.Add(new Vector2Int(i, j));
            }
        }
        return whitePositions;
    }

    public List<Vector2Int> GetEnemyPositions(int boardSize)
    {
        List<Vector2Int> blackPositions = new List<Vector2Int>();

        for (int i = boardSize / 2 + 1; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize / 2; j++)
            {
                blackPositions.Add(new Vector2Int(i, j));
            }
        }   
        return blackPositions;
    }
}
