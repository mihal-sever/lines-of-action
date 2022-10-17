using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sever.BoardGames
{
    public class UgolkiRules : RulesBase
    {
        private Dictionary<PlayerType, List<Vector2Int>> winningPositions = new Dictionary<PlayerType, List<Vector2Int>>();

        public override void Initialize(Board board)
        {
            base.Initialize(board);

            Player white = GameManager.Instance.CurrentPlayer;
            Player black = GameManager.Instance.CurrentEnemy;

            winningPositions.Add(PlayerType.White, black.StartPositions);
            winningPositions.Add(PlayerType.Black, white.StartPositions);
        }

        public override bool CanMove(Cell fromCell, Cell toCell)
        {
            if (CellOccupied(toCell))
                return false;

            Vector2Int from = fromCell.position;
            Vector2Int to = toCell.position;

            if (IsShortStep(from, to))
            {
                return CanMakeShortStep(from, to);
            }
            else
            {
                List<Vector2Int> traversedCells = new List<Vector2Int>();
                return FindPath(from, to, traversedCells);
            }
        }

        public override bool CanCaptureChecker(Cell cell)
        {
            return false;
        }

        public override bool IsWin(Player player)
        {
            List<Vector2Int> positions = winningPositions[player.Type];

            foreach (Checker checker in player.Checkers)
            {
                bool isCheckerOnPlace = positions.Any(x => x == checker.Position);

                if (!isCheckerOnPlace)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsShortStep(Vector2Int from, Vector2Int to)
        {
            int stepSize = (int) (from - to).magnitude;
            return stepSize == 1;
        }

        private bool CanMakeShortStep(Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> neighborCells = GetNeighborCells(from);

            foreach (Vector2Int v in neighborCells)
            {
                if (CellOccupied(cells[v.x, v.y]))
                {
                    continue;
                }

                if (v == to)
                    return true;
            }

            return false;
        }

        private bool FindPath(Vector2Int from, Vector2Int to, List<Vector2Int> traversedCells)
        {
            traversedCells.Add(from);
            List<Vector2Int> neighborCells = GetNeighborCells(from);

            foreach (Vector2Int neighbor in neighborCells)
            {
                Vector2Int? nextCell;
                if (CanJumpOverNeighborCell(neighbor, from, traversedCells, out nextCell))
                {
                    if (nextCell.Value == to)
                    {
                        return true;
                    }

                    if (FindPath(nextCell.Value, to, traversedCells))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanJumpOverNeighborCell(Vector2Int neighbor, Vector2Int from, List<Vector2Int> traversedCells, out Vector2Int? nextCell)
        {
            nextCell = null;

            // if neighbor is vacant
            if (!CellOccupied(cells[neighbor.x, neighbor.y]))
                return false;

            // if there is no next cell
            nextCell = GetNextCellOnLine(from, neighbor);
            if (!nextCell.HasValue)
                return false;

            // if next cell is occupied
            if (CellOccupied(cells[nextCell.Value.x, nextCell.Value.y]))
                return false;

            // if next cell is traversed
            if (traversedCells.Contains(nextCell.Value))
                return false;

            return true;
        }

        private Vector2Int? GetNextCellOnLine(Vector2Int from, Vector2Int to)
        {
            Vector2Int? next = null;

            if (from.x == to.x)
            {
                if (from.y > to.y)
                {
                    if (to.y - 1 >= 0)
                        next = new Vector2Int(to.x, to.y - 1);
                }
                else
                {
                    if (to.y + 1 < boardSize)
                        next = new Vector2Int(to.x, to.y + 1);
                }
            }
            else if (from.y == to.y)
            {
                if (from.x > to.x)
                {
                    if (to.x - 1 >= 0)
                        next = new Vector2Int(to.x - 1, to.y);
                }
                else
                {
                    if (to.x + 1 < boardSize)
                        next = new Vector2Int(to.x + 1, to.y);
                }
            }

            return next;
        }

        private List<Vector2Int> GetNeighborCells(Vector2Int cell)
        {
            List<Vector2Int> neighborCells = new List<Vector2Int>();

            if (cell.y + 1 < boardSize)
                neighborCells.Add(new Vector2Int(cell.x, cell.y + 1));
            if (cell.y - 1 >= 0)
                neighborCells.Add(new Vector2Int(cell.x, cell.y - 1));
            if (cell.x + 1 < boardSize)
                neighborCells.Add(new Vector2Int(cell.x + 1, cell.y));
            if (cell.x - 1 >= 0)
                neighborCells.Add(new Vector2Int(cell.x - 1, cell.y));

            return neighborCells;
        }
    }
}