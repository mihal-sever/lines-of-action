using UnityEngine;

namespace Sever.BoardGames
{
    public abstract class RulesBase
    {
        internal Cell[,] cells;
        internal int boardSize;

        public abstract bool CanMove(Cell fromCell, Cell toCell);
        public abstract bool IsWin(Player player);
        public abstract bool CanCaptureChecker(Cell cell);

        public virtual void Initialize(Board board)
        {
            cells = board.Cells;
            boardSize = board.Size;
        }

        internal bool CellOccupied(Cell cell)
        {
            return CellOccupiedBy(cell, GameManager.Instance.CurrentPlayer) ||
                   CellOccupiedBy(cell, GameManager.Instance.CurrentEnemy);
        }

        internal bool CellOccupiedBy(Cell cell, Player player)
        {
            foreach (Checker checker in player.Checkers)
            {
                if (checker.Cell == cell)
                    return true;
            }

            return false;
        }

        internal Checker GetCheckerOnCell(Cell cell)
        {
            foreach (Checker checker in GameManager.Instance.CurrentEnemy.Checkers)
            {
                if (checker.Cell == cell)
                    return checker;
            }

            foreach (Checker checker in GameManager.Instance.CurrentPlayer.Checkers)
            {
                if (checker.Cell == cell)
                    return checker;
            }

            throw new UnityException("Cell not found.");
        }
    }
}