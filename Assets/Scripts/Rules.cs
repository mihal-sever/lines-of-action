using UnityEngine;

public static class Rules
{
    private static Cell[,] cells;
    private static int size;

    public static bool CanMove(Checker checker, Cell targetCell)
    {
        cells = Board.Instance.cells;
        size = Board.Instance.size;

        Vector2Int cellPos = GetCellPosition(checker.GetCell());
        Vector2Int targetCellPos = GetCellPosition(targetCell);

        if (CanMoveStraight(cellPos, targetCellPos) || CanMoveDiagonally(cellPos, targetCellPos))
            return true;

        return false;
    }

    private static bool CanMoveStraight(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        bool isHorizontal = cellPos.x == targetCellPos.x;
        bool isVertical = cellPos.y == targetCellPos.y;

        if (!isHorizontal && !isVertical)
            return false;
        
        // check if there is an enemy on the way to target
        int indexFrom =  isHorizontal ? cellPos.y + 1 : cellPos.x + 1;
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

            if (CellOccupiedByEnemy(cell))
                return false;
        }
        
        int checkersOnLine = CountCheckersOnStraightLine(isHorizontal, cellPos);
        int stepLength = isHorizontal ? Mathf.Abs(targetCellPos.y - cellPos.y) : Mathf.Abs(targetCellPos.x - cellPos.x);

        if (stepLength != checkersOnLine)
            return false;
        
        return true;
    }

    private static bool CanMoveDiagonally(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        if (!CellsOnDiagonalLine(cellPos, targetCellPos))
            return false;

        bool isMainDiagonal = ((targetCellPos.x - cellPos.x) * (targetCellPos.y - cellPos.y) > 0) ? true : false;

        // check if there is an enemy on the way to target
        for (int delta = 1; delta < size; delta++)
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

            if (CellOccupiedByEnemy(cellToCheck))
                return false;
        }

        int checkersOnLine = CountCheckersOnDiagonalLine(isMainDiagonal, cellPos);
        int stepLength = Mathf.Abs(targetCellPos.x - cellPos.x);

        if (stepLength != checkersOnLine)
            return false;

        return true;
    }

    private static int CountCheckersOnStraightLine(bool isHorizontal, Vector2Int cellPos)
    {
        int checkersOnLine = 0;
        for (int i = 0; i < size; i++)
        {
            Cell cell = isHorizontal ? cells[cellPos.x, i] : cells[i, cellPos.y];

            if (CellOccupied(cell))
                checkersOnLine++;
        }
        return checkersOnLine;
    }

    private static int CountCheckersOnDiagonalLine(bool isMainDiagonal, Vector2Int cellPos)
    {
        int checkersOnLine = 1;
        int x = cellPos.x;
        int y = cellPos.y;

        for (int delta = 1; delta < size; delta++)
        {
            Cell cell = null;
            if (isMainDiagonal)
            {
                if (x + delta < size && y + delta < size)
                {
                    cell = cells[x + delta, y + delta];
                }

                if (x - delta >= 0 && y - delta >= 0)
                {
                    cell = cells[x - delta, y - delta];
                }
            }
            else
            {
                if (x + delta < size && y - delta >= 0)
                {
                    cell = cells[x + delta, y - delta];
                }

                if (x - delta >= 0 && y + delta < size)
                {
                    cell = cells[x - delta, y + delta];
                }
            }

            if (CellOccupied(cell))
                checkersOnLine++;
        }

        return checkersOnLine;
    }

    private static bool CellOccupied(Cell cell)
    {
        return cell?.checker != null;
    }

    private static bool CellOccupiedByEnemy(Cell cell)
    {
        return cell?.checker?.GetComponentInParent<Player>() == GameManager.Instance.currentEnemy;
    }

    private static bool CellsOnDiagonalLine(Vector2Int pos, Vector2Int targetPos)
    {
        for (int delta = 1; delta < size; delta++)
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

    private static Vector2Int GetCellPosition(Cell cell)
    {
        Vector2Int position = new Vector2Int();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (cells[i, j] == cell)
                {
                    position = new Vector2Int(i, j);
                }
            }
        }
        return position;
    }

}
