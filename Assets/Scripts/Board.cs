using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject whiteCellPrefab;
    public GameObject blackCellPrefab;

    internal Cell[,] Cells { get; private set; }
    internal int Size { get; private set; }
        
    public void CreateBoard(int boardSize)
    {
        Size = boardSize;
        Cells = new Cell[Size, Size];

        CreateCells();
    }

    private void CreateCells()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                GameObject cellPrefab = GetCellPrefab(x, y);

                Vector3 position = new Vector3(y, 0, x);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                Cells[y, x] = cell.GetComponent<Cell>();
            }
        }
    }

    private GameObject GetCellPrefab(int x, int y)
    {
        if ((x + y) % 2 == 0)
            return whiteCellPrefab;
        else
            return blackCellPrefab;
    }

}
