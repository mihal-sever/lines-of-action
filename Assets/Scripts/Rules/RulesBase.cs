using UnityEngine;

public abstract class RulesBase
{
    internal Cell[,] cells;
    internal int boardSize;
    
    public abstract bool CanMove(Checker checker, Cell targetCell);
    public abstract bool IsWin(Player player);
    public abstract bool CanCaptureChecker(Cell cell);
    public abstract void InitializeCheckers(IOpeningPosition openingPosition);

    internal void Initialize(IOpeningPosition openingPosition, Board board)
    {
        cells = board.Cells;
        boardSize = board.Size;
        InitializeCheckers(openingPosition);
    }

    internal Vector2Int GetCellPosition(Cell cell)
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (cells[i, j] == cell)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        throw new UnityException("Cell not found.");
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
