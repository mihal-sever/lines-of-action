using UnityEngine;

public class GameConfigContainer : MonoBehaviour
{
    internal RulesBase rules;
    internal OpeningPosition openingPosition;
    internal int boardSize;
    internal bool soundOn;

    private void Awake()
    {
        InitializeDefaultValues();
    }

    private void InitializeDefaultValues()
    {
        rules = new LinesOfActionRules();
        openingPosition = new LinesOfActionOpeningPosition();
        boardSize = 8;
        soundOn = true;
    }
}
