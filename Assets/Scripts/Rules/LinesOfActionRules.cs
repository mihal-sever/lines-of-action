using System.Collections.Generic;
using UnityEngine;

public class LinesOfActionRules : RulesBase
{
    public override bool CanMove(Checker checker, Cell targetCell)
    {
        Vector2Int cellPos = GetCellPosition(checker.GetCell());
        Vector2Int targetCellPos = GetCellPosition(targetCell);

        if (CanMoveStraight(cellPos, targetCellPos) || CanMoveDiagonally(cellPos, targetCellPos))
            return true;

        return false;
    }

    public override bool CanCaptureChecker(Cell cell)
    {
        return CellOccupied(cell);
    }

    public override bool IsWin(Player player)
    {
        if (player.checkers.Count == 1)
            return true;

        List<Checker> linkedCheckers = new List<Checker>();

        FindLinkedCheckers(player.checkers[0], linkedCheckers, player.checkers);

        if (linkedCheckers.Count == player.checkers.Count)
            return true;

        return false;
    }

    public override void InitializeCheckers(IOpeningPosition openingPosition)
    {
        List<Vector2Int> playerPositions = openingPosition.GetPlayerPositions(boardSize);
        foreach (Vector2Int v in playerPositions)
        {
            GameManager.Instance.currentPlayer.CreateChecker(cells[v.x, v.y]);
        }

        List<Vector2Int> enemyPositions = openingPosition.GetEnemyPositions(boardSize);
        foreach (Vector2Int v in enemyPositions)
        {
            GameManager.Instance.currentEnemy.CreateChecker(cells[v.x, v.y]);
        }
    }


    private void FindLinkedCheckers(Checker checker, List<Checker> linkedCheckers, List<Checker> checkers)
    {
        linkedCheckers.Add(checker);

        Vector2Int pos = GetCellPosition(checker.GetCell());

        foreach (Checker c in checkers)
        {
            if (linkedCheckers.Contains(c))
                continue;

            Vector2Int posToCheck = GetCellPosition(c.GetCell());
            if ((pos.x == posToCheck.x || pos.x == posToCheck.x + 1 || pos.x == posToCheck.x - 1) &&
                (pos.y == posToCheck.y || pos.y == posToCheck.y + 1 || pos.y == posToCheck.y - 1))
            {
                FindLinkedCheckers(c, linkedCheckers, checkers);
            }
        }
    }

    private bool CanMoveStraight(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        bool isHorizontal = cellPos.x == targetCellPos.x;
        bool isVertical = cellPos.y == targetCellPos.y;

        if (!isHorizontal && !isVertical)
            return false;

        // check if there is an enemy on the way to target
        int indexFrom = isHorizontal ? cellPos.y + 1 : cellPos.x + 1;
        int indexTo = isHorizontal ? targetCellPos.y : targetCellPos.x;

        if (cellPos.y > targetCellPos.y)
        {
            var temp = indexFrom;
            indexFrom = indexTo + 1;
            indexTo = temp - 1;
        }

        for (int i = indexFrom; i < indexTo; i++)
        {
            Cell cell = isHorizontal ? cells[cellPos.x, i] : cells[i, cellPos.y];

            if (CellOccupiedBy(cell, GameManager.Instance.currentEnemy))
                return false;
        }

        int checkersOnLine = CountCheckersOnStraightLine(isHorizontal, cellPos);
        int stepLength = isHorizontal ? Mathf.Abs(targetCellPos.y - cellPos.y) : Mathf.Abs(targetCellPos.x - cellPos.x);

        if (stepLength != checkersOnLine)
            return false;

        return true;
    }

    private bool CanMoveDiagonally(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        if (!CellsOnDiagonalLine(cellPos, targetCellPos))
            return false;

        bool isMainDiagonal = ((targetCellPos.x - cellPos.x) * (targetCellPos.y - cellPos.y) > 0) ? true : false;

        // check if there is an enemy on the way to target
        for (int delta = 1; delta < boardSize; delta++)
        {
            Cell cellToCheck = null;

            if (cellPos.x + delta < targetCellPos.x)
            {
                cellToCheck = isMainDiagonal ? cells[cellPos.x + delta, cellPos.y + delta] : cells[cellPos.x + delta, cellPos.y - delta];
            }
            else if (cellPos.x - delta > targetCellPos.x)
            {
                cellToCheck = isMainDiagonal ? cells[cellPos.x - delta, cellPos.y - delta] : cells[cellPos.x - delta, cellPos.y + delta];
            }

            if (CellOccupiedBy(cellToCheck, GameManager.Instance.currentEnemy))
                return false;
        }

        int checkersOnLine = CountCheckersOnDiagonalLine(isMainDiagonal, cellPos);
        int stepLength = Mathf.Abs(targetCellPos.x - cellPos.x);

        if (stepLength != checkersOnLine)
            return false;

        return true;
    }

    private int CountCheckersOnStraightLine(bool isHorizontal, Vector2Int cellPos)
    {
        int checkersOnLine = 0;
        for (int i = 0; i < boardSize; i++)
        {
            Cell cell = isHorizontal ? cells[cellPos.x, i] : cells[i, cellPos.y];

            if (CellOccupied(cell))
                checkersOnLine++;
        }
        return checkersOnLine;
    }

    private int CountCheckersOnDiagonalLine(bool isMainDiagonal, Vector2Int cellPos)
    {
        int checkersOnLine = 1;
        int x = cellPos.x;
        int y = cellPos.y;

        for (int delta = 1; delta < boardSize; delta++)
        {
            if (isMainDiagonal)
            {
                if (x + delta < boardSize && y + delta < boardSize)
                {
                    if (CellOccupied(cells[x + delta, y + delta]))
                        checkersOnLine++;
                }

                if (x - delta >= 0 && y - delta >= 0)
                {
                    if (CellOccupied(cells[x - delta, y - delta]))
                        checkersOnLine++;
                }
            }
            else
            {
                if (x + delta < boardSize && y - delta >= 0)
                {
                    if (CellOccupied(cells[x + delta, y - delta]))
                        checkersOnLine++;
                }

                if (x - delta >= 0 && y + delta < boardSize)
                {
                    if (CellOccupied(cells[x - delta, y + delta]))
                        checkersOnLine++;
                }
            }
        }

        return checkersOnLine;
    }
    
    private bool CellsOnDiagonalLine(Vector2Int pos, Vector2Int targetPos)
    {
        for (int delta = 1; delta < boardSize; delta++)
        {
            if (pos.x + delta == targetPos.x || pos.x - delta == targetPos.x)
            {
                if (pos.y + delta == targetPos.y || pos.y - delta == targetPos.y)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
