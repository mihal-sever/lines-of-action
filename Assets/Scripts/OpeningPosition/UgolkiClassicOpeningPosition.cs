using System.Collections.Generic;
using UnityEngine;

public class UgolkiClassicOpeningPosition : OpeningPosition
{
    protected override List<Vector2Int> GetWhitePositions()
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

    protected override List<Vector2Int> GetBlackPositions()
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
