using UnityEngine;

namespace Sever.BoardGames
{
    public class Board : MonoBehaviour
    {
        public Cell[,] Cells { get; private set; }
        public int Size { get; private set; }


        private void Awake()
        {
            var cells = GetComponentsInChildren<Cell>();
            Size = (int) Mathf.Sqrt(cells.Length);

            Cells = new Cell[Size, Size];

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Cells[x, y] = cells[x * Size + y];
                    Cells[x, y].Init(new Vector2Int(x, y));
                }
            }
        }
    }
}