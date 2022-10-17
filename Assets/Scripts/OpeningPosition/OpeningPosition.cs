using System.Collections.Generic;
using UnityEngine;

namespace Sever.BoardGames
{
    public abstract class OpeningPosition
    {
        protected int boardSize;

        public void SetBoardSize(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public List<Vector2Int> GetOpeningPosition(PlayerType playerType)
        {
            return playerType == PlayerType.White ? GetWhitePositions() : GetBlackPositions();
        }

        protected abstract List<Vector2Int> GetWhitePositions();
        protected abstract List<Vector2Int> GetBlackPositions();
    }
}