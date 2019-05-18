using System.Collections.Generic;
using UnityEngine;

public interface IOpeningPosition
{
    List<Vector2Int> GetPlayerPositions(int boardSize);
    List<Vector2Int> GetEnemyPositions(int boardSize);
}
