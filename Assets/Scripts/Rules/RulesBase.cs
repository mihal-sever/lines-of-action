using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RulesBase
{
    internal Cell[,] cells;
    internal int boardSize;
    
    public abstract bool CanMove(Checker checker, Cell targetCell);
    public abstract bool IsWin(Player player);
    public abstract bool CanCaptureChecker(Cell cell);
    public abstract void InitializeCheckers(IOpeningPosition openingPosition);

    internal void Initialize(int boardSize, Cell[,] cells, IOpeningPosition openingPosition)
    {
        this.cells = cells;
        this.boardSize = boardSize;
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
        return cell?.checker != null;
    }

    internal bool CellOccupiedBy(Cell cell, Player player)
    {
        return cell?.checker?.GetComponentInParent<Player>() == player;
    }

}
