using System.Collections.Generic;
using UnityEngine;

public class ScrambledEggsOpeningPosition : MonoBehaviour, IOpeningPosition
{
    public void InitializeCheckers(Player currentPlayer, Player currentEnemy)
    {
        int boardSize = Board.Instance.GetSize();
        for (int i = 1; i < boardSize - 1; i++)
        {
            if (i % 2 == 1)
            {
                currentPlayer.CreateChecker(Board.Instance.cells[0, i]);
                currentPlayer.CreateChecker(Board.Instance.cells[i, boardSize - 1]);

                currentEnemy.CreateChecker(Board.Instance.cells[i, 0]);
                currentEnemy.CreateChecker(Board.Instance.cells[boardSize - 1, i]);
            }
            else
            {
                currentPlayer.CreateChecker(Board.Instance.cells[i, 0]);
                currentPlayer.CreateChecker(Board.Instance.cells[boardSize - 1, i]);

                currentEnemy.CreateChecker(Board.Instance.cells[0, i]);
                currentEnemy.CreateChecker(Board.Instance.cells[i, boardSize - 1]);
            }
        }
    }

    public List<Vector2Int> GetPlayerPositions(int boardSize)
    {
        List<Vector2Int> whitePositions = new List<Vector2Int>();

        for (int i = 1; i < boardSize - 1; i++)
        {
            if (i % 2 == 1)
            {
                whitePositions.Add(new Vector2Int(0, i));
                whitePositions.Add(new Vector2Int(i, boardSize - 1));
            }
            else
            {
                whitePositions.Add(new Vector2Int(i, 0));
                whitePositions.Add(new Vector2Int(boardSize - 1, i));
            }

        }
        return whitePositions;
    }

    public List<Vector2Int> GetEnemyPositions(int boardSize)
    {
        List<Vector2Int> blackPositions = new List<Vector2Int>();

        for (int i = 1; i < boardSize - 1; i++)
        {
            if (i % 2 == 1)
            {
                blackPositions.Add(new Vector2Int(i, 0));
                blackPositions.Add(new Vector2Int(boardSize - 1, i));
            }
            else
            {
                blackPositions.Add(new Vector2Int(0, i));
                blackPositions.Add(new Vector2Int(i, boardSize - 1));
            }

        }
        return blackPositions;
    }
}
