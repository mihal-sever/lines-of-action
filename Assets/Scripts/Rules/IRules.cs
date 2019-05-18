public interface IRules
{
    void Initialize(int boardSize, Cell[,] cells, IOpeningPosition openingPosition);
    bool CanMove(Checker checker, Cell targetCell);
    bool IsWin(Player player);
    bool CanCaptureChecker(Cell cell);
}
