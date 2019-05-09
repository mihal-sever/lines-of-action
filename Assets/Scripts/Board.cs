using UnityEngine;

public class Board : MonoBehaviour
{
    #region Singleton
    private static Board instance;
    
    public static Board Instance
    {
        get
        {
            return instance;
        }
    }
    private void SetupSingelton()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple boards exists: " + instance.name + " and now " + this.name);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public GameObject whiteCellPrefab;
    public GameObject blackCellPrefab;

    public int size = 8;

    internal Cell[,] cells;

    private void Awake()
    {
        SetupSingelton();
    }

    public bool CanMove(Checker checker, Cell targetCell)
    {
        Vector2Int cellPos = GetCellPosition(checker.GetCell());
        Vector2Int targetCellPos = GetCellPosition(targetCell);

        if (CanMoveHorizontally(cellPos, targetCellPos) || 
            CanMoveVertically(cellPos, targetCellPos) ||
            CanMoveDiagonally(cellPos, targetCellPos))
            return true;

        return false;
    }

    private bool CanMoveHorizontally(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        if (cellPos.x == targetCellPos.x)
        {
            int from = cellPos.y +1;
            int to = targetCellPos.y;

            if (cellPos.y > targetCellPos.y)
            {
                from = targetCellPos.y + 1;
                to = cellPos.y;
            }
            // check if there is an enemy on the way to target
            for (int i = from; i < to; i++)
            {
                if (cells[cellPos.x, i].checker?.GetComponentInParent<Player>() == GameManager.Instance.currentEnemy)
                {
                    return false;
                }
            }
            
            // count checkers on the line
            int checkersOnLine = 0;
            for (int i = 0; i < size; i++)
            {
                if (cells[cellPos.x, i].checker != null)
                    checkersOnLine++;
            }

            // count step length
            int stepLength = Mathf.Abs(targetCellPos.y - cellPos.y);
            if (stepLength == checkersOnLine)
                return true;
        }
        return false;
    }

    private bool CanMoveVertically(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        if (cellPos.y == targetCellPos.y)
        {
            int from = cellPos.x + 1;
            int to = targetCellPos.x;

            if (cellPos.x > targetCellPos.x)
            {
                from = targetCellPos.x + 1;
                to = cellPos.x;
            }
            // check if there is an enemy on the way to target
            for (int i = from; i < to; i++)
            {
                if (cells[i, cellPos.y].checker?.GetComponentInParent<Player>() == GameManager.Instance.currentEnemy)
                {
                    return false;
                }
            }

            // count checkers on the line
            int checkersOnLine = 0;
            for (int i = 0; i < size; i++)
            {
                if (cells[i, cellPos.y].checker != null)
                    checkersOnLine++;
            }

            // count step length
            int stepLength = Mathf.Abs(targetCellPos.x - cellPos.x);
            if (stepLength == checkersOnLine)
                return true;
        }
        return false;
    }

    private bool CanMoveDiagonally(Vector2Int cellPos, Vector2Int targetCellPos)
    {
        if (CellsOnDiagonalLine(cellPos, targetCellPos))
        {
            int checkersOnLine = 1;

            if ((targetCellPos.x - cellPos.x) * (targetCellPos.y - cellPos.y) > 0)
            {
                // main diagonal
                for (int delta = 1; delta < size; delta++)
                {
                    // check if there is an enemy on the way to target
                    if (cellPos.x + delta < targetCellPos.x)
                    {
                        if (cells[cellPos.x + delta, cellPos.y + delta].checker?.GetComponentInParent<Player>()
                            == GameManager.Instance.currentEnemy)
                        {
                            return false;
                        }
                    }
                    else if (cellPos.x - delta > targetCellPos.x)
                    {
                        if (cells[cellPos.x - delta, cellPos.y - delta].checker?.GetComponentInParent<Player>()
                            == GameManager.Instance.currentEnemy)
                        {
                            return false;
                        }
                    }

                    // count checkers
                    if (cellPos.x + delta < size && cellPos.y + delta < size)
                    {
                        if (cells[cellPos.x + delta, cellPos.y + delta].checker != null)
                        {
                            checkersOnLine++;
                        }
                    }

                    if (cellPos.x - delta >= 0 && cellPos.y - delta >= 0)
                    {
                        if (cells[cellPos.x - delta, cellPos.y - delta].checker != null)
                        {
                            checkersOnLine++;
                        }
                    }
                }
            }
            else
            {
                // secondary diagonal
                for (int delta = 1; delta < size; delta++)
                {
                    // check if there is an enemy on the way to target
                    if (cellPos.x + delta < targetCellPos.x)
                    {
                        if (cells[cellPos.x + delta, cellPos.y - delta].checker?.GetComponentInParent<Player>()
                            == GameManager.Instance.currentEnemy)
                        {
                            return false;
                        }
                    }
                    else if (cellPos.x - delta > targetCellPos.x)
                    {
                        if (cells[cellPos.x - delta, cellPos.y + delta].checker?.GetComponentInParent<Player>()
                            == GameManager.Instance.currentEnemy)
                        {
                            return false;
                        }
                    }

                    // count checkers
                    if (cellPos.x + delta < size && cellPos.y - delta >= 0)
                    {
                        if (cells[cellPos.x + delta, cellPos.y - delta].checker != null)
                        {
                            checkersOnLine++;
                        }
                    }

                    if (cellPos.x - delta >= 0 && cellPos.y + delta  < size)
                    {
                        if (cells[cellPos.x - delta, cellPos.y + delta].checker != null)
                        {
                            checkersOnLine++;
                        }
                    }
                }
            }

            int stepLength = Mathf.Abs(targetCellPos.x - cellPos.x);
            if (stepLength == checkersOnLine)
                return true;
        }
        
        return false;
    }

    private bool CellsOnDiagonalLine(Vector2Int pos, Vector2Int targetPos)
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

    private Vector2Int GetCellPosition(Cell cell)
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

    public void CreateBoard()
    {
        cells = new Cell[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 position = new Vector3(j, 0, i);
                GameObject cellPrefab = blackCellPrefab;
                if ((i + j) % 2 == 0)
                    cellPrefab = whiteCellPrefab;

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cells[j, i] = cell.GetComponent<Cell>();
            }
        }
    }

}
