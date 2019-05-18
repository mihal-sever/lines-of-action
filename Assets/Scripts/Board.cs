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

    internal Cell[,] cells;

    private int size;

    private void Awake()
    {
        SetupSingelton();
    }

    public int GetSize()
    {
        return size;
    }
    public Cell[,] GetCells()
    { return cells; }

    public void CreateBoard(int boardSize)
    {
        size = boardSize;

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
