using UnityEngine;

public class Board : MonoBehaviour
{
    #region Singleton
    public static Board Instance { get; private set; }

    private void SetupSingelton()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple boards exists: " + Instance.name + " and now " + name);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public GameObject whiteCellPrefab;
    public GameObject blackCellPrefab;

    internal Cell[,] Cells { get; private set; }
    internal int Size { get; private set; }

    private void Awake()
    {
        SetupSingelton();
    }
    
    public void CreateBoard(int boardSize)
    {
        Size = boardSize;
        Cells = new Cell[Size, Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Vector3 position = new Vector3(j, 0, i);
                GameObject cellPrefab = blackCellPrefab;
                if ((i + j) % 2 == 0)
                    cellPrefab = whiteCellPrefab;

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                Cells[j, i] = cell.GetComponent<Cell>();
            }
        }
    }

}
