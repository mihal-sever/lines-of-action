using System.Collections.Generic;
using UnityEngine;

namespace Sever.BoardGames
{
    public enum PlayerType
    {
        White,
        Black
    }

    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject _checkerPrefab;
        [SerializeField] private PlayerType _type;

        public PlayerType Type => _type;
        public List<Checker> Checkers { get; } = new();
        public List<Vector2Int> StartPositions { get; private set; }


        public void SpawnCheckers(OpeningPosition openingPosition, Board board)
        {
            StartPositions = openingPosition.GetOpeningPosition(_type);
            foreach (Vector2Int v in StartPositions)
            {
                CreateChecker(board.Cells[v.x, v.y]);
            }
        }

        private void CreateChecker(Cell cell)
        {
            GameObject instance = Instantiate(_checkerPrefab, transform);
            Checker checker = instance.GetComponent<Checker>();
            checker.Move(cell);
            Checkers.Add(checker);
            checker.name = "checker " + Checkers.Count;
        }

        public void DestroyChecker(Checker checker)
        {
            Checkers.Remove(checker);
            Destroy(checker.gameObject);
        }

        public bool OwnsChecker(Checker checker)
        {
            return Checkers.Contains(checker);
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}