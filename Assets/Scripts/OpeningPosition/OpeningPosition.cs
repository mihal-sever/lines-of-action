using System.Collections.Generic;
using UnityEngine;

public abstract class OpeningPosition
{
    protected int boardSize;

    public void SetBoardSize(int boardSize)
    {
        this.boardSize = boardSize;
    }

    public List<Vector2Int> GetOpeningPosition(PlayerType playerType)
    {
        if (playerType == PlayerType.White)
            return GetWhitePositions();
        else
            return GetBlackPositions();

    }

    protected abstract List<Vector2Int> GetWhitePositions();
    protected abstract List<Vector2Int> GetBlackPositions();
}
