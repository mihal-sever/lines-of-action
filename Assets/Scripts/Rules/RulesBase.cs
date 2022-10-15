using UnityEngine;

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
        return CellOccupiedBy(cell, GameManager.Instance.currentPlayer) ||
                CellOccupiedBy(cell, GameManager.Instance.currentEnemy);
    }

    internal bool CellOccupiedBy(Cell cell, Player player)
    {
        foreach (Checker checker in player.checkers)
        {
            if (checker.GetCell() == cell)
                return true;
        }
        return false;
    }

    internal Checker GetCheckerOnCell(Cell cell)
    {
        foreach (Checker checker in GameManager.Instance.currentEnemy.checkers)
        {
            if (checker.GetCell() == cell)
                return checker;
        }
        foreach (Checker checker in GameManager.Instance.currentPlayer.checkers)
        {
            if (checker.GetCell() == cell)
                return checker;
        }
        throw new UnityException("Cell not found.");
    }
}
