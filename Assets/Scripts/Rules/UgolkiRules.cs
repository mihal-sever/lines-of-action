using System.Collections.Generic;
using UnityEngine;

public class UgolkiRules : IRules
{
    private Cell[,] cells;
    private int boardSize;

    public void Initialize(int boardSize, Cell[,] cells, IOpeningPosition openingPosition)
    {
        this.cells = cells;
        this.boardSize = boardSize;
        InitializeCheckers(openingPosition);
    }

    public bool CanMove(Checker checker, Cell targetCell)
    {
        if (targetCell.checker != null)
            return false;

        Vector2Int cellPos = GetCellPosition(checker.GetCell());
        Vector2Int targetCellPos = GetCellPosition(targetCell);
        
        bool b = FindPath(cellPos, targetCellPos);

        return IsSimpleStep(cellPos, targetCellPos);
    }


    private bool IsSimpleStep(Vector2Int from, Vector2Int to)
    {
        List<Vector2Int> neighborCells = GetNeighborCells(from);

        foreach (Vector2Int v in neighborCells)
        {
            // if cell is vacant
            if (!CellOccupied(cells[v.x, v.y]))
            {
                //return true if this is the target cell
                if (v == to)
                    return true;
            }
        }
        return false;
    }

    private bool FindPath(Vector2Int from, Vector2Int to)
    {
        Debug.Log("find path from " + from + " to " + to);
        List<Vector2Int> neighborCells = GetNeighborCells(from);
        
        // for each neighbor cell
        foreach (Vector2Int v in neighborCells)
        {
            Vector2Int? nextCell;
            if (CanJumpOverNeighborCell(v, from, out nextCell))
            {
                Debug.Log("can jump over " + v);

                // check if cell is target cell
                if (nextCell.Value == to)
                {
                    return true;
                }
                else
                {
                    return FindPath(nextCell.Value, to);
                }
            }

        }

        return false;
    }

    private bool CanJumpOverNeighborCell(Vector2Int neighbor, Vector2Int from, out Vector2Int? nextCell)
    {

        // take next cell on that line
        nextCell = GetNextCellOnLine(from, neighbor);
        if (!nextCell.HasValue)
        {
            return false;
        }
        
        // if next cell is occupied
        if (CellOccupied(cells[nextCell.Value.x, nextCell.Value.y]))
        {
            return false;
        }
        
        return true;
    }

    private Vector2Int? GetNextCellOnLine(Vector2Int from, Vector2Int to)
    {
        Vector2Int? next = null;

        if (from.x == to.x)
        {
            if (from.y > to.y)
            {
                if (to.y - 1 >= 0)
                    next = new Vector2Int(to.x, to.y - 1);
            }
            else
            {
                if (to.y + 1 < boardSize)
                    next = new Vector2Int(to.x, to.y + 1);
            }
        }
        else if(from.y == to.y)
        {
            if (from.x > to.x)
            {
                if (to.x - 1 >= 0)
                    next = new Vector2Int(to.x - 1, to.y);
            }
            else
            {
                if (to.x + 1 < boardSize)
                    next = new Vector2Int(to.x + 1, to.y);
            }
        }

        return next;
    }
    
    private List<Vector2Int> GetNeighborCells(Vector2Int cell)
    {
        List<Vector2Int> neighborCells = new List<Vector2Int>();

        if (cell.y + 1 < boardSize)
            neighborCells.Add(new Vector2Int(cell.x, cell.y + 1));
        if (cell.y - 1 >= 0)
            neighborCells.Add(new Vector2Int(cell.x, cell.y - 1));
        if (cell.x + 1 < boardSize)
            neighborCells.Add(new Vector2Int(cell.x + 1, cell.y));
        if (cell.x - 1 >= 0)
            neighborCells.Add(new Vector2Int(cell.x - 1, cell.y));

        return neighborCells;
    }

    public bool CanCaptureChecker(Cell cell)
    {
        return false;
    }

    public bool IsWin(Player player)
    {
        foreach (Checker checker in player.checkers)
        {
            bool isCheckerOnPlace = false;
            foreach (Cell cell in player.targetCells)
            {
                if (checker.GetCell() == cell)
                {
                    isCheckerOnPlace = true;
                    break;
                }
            }
            if (!isCheckerOnPlace)
                return false;
        }

        return true;
    }

    private void InitializeCheckers(IOpeningPosition openingPosition)
    {
        List<Vector2Int> playerPositions = openingPosition.GetPlayerPositions(boardSize);
        List<Cell> targetEnemyCells = new List<Cell>();
        foreach (Vector2Int v in playerPositions)
        {
            Cell cell = cells[v.x, v.y];
            targetEnemyCells.Add(cell);
            GameManager.Instance.currentPlayer.CreateChecker(cell);
        }

        List<Vector2Int> enemyPositions = openingPosition.GetEnemyPositions(boardSize);
        List<Cell> targetPlayerCells = new List<Cell>();
        foreach (Vector2Int v in enemyPositions)
        {
            Cell cell = cells[v.x, v.y];
            targetPlayerCells.Add(cell);
            GameManager.Instance.currentEnemy.CreateChecker(cell);
        }

        GameManager.Instance.currentPlayer.targetCells = targetPlayerCells;
        GameManager.Instance.currentEnemy.targetCells = targetEnemyCells;
    }

    private Vector2Int GetCellPosition(Cell cell)
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
    
    private bool CellOccupied(Cell cell)
    {
        return cell?.checker != null;
    }

}
