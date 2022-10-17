namespace Sever.BoardGames
{
    public static class GameConfig
    {
        public static RulesBase rules = new LinesOfActionRules();
        public static OpeningPosition openingPosition = new LinesOfActionOpeningPosition();
        public static int boardSize = 8;
        public static bool soundOn = true;
    }
}