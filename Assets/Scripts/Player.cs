using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    White,
    Black
}

public class Player : MonoBehaviour
{
    public GameObject checkerPrefab;
    public string playerName;
    public PlayerType type;

    internal List<Checker> checkers;
    internal List<Vector2Int> startPositions;

    private void Awake()
    {
        checkers = new List<Checker>();
    }

    public void SpawnCheckers(OpeningPosition openingPosition, Board board)
    {
        startPositions = openingPosition.GetOpeningPosition(type);
        foreach (Vector2Int v in startPositions)
        {
            CreateChecker(board.Cells[v.x, v.y]);
        }
    }

    public void CreateChecker(Cell cell)
    {
        GameObject instance = Instantiate(checkerPrefab, transform);
        Checker checker = instance.GetComponent<Checker>();        
        checker.Move(cell);
        checkers.Add(checker);
        checker.name = "checker " + checkers.Count;
    }

    public void DestroyChecker(Checker checker)
    {
        checkers.Remove(checker);
        Destroy(checker.gameObject);
    }

    public bool IsOwnChecker(Checker checker)
    {
        return checkers.Contains(checker);
    }
}
