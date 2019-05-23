using UnityEngine;

public class GameConfigContainer : MonoBehaviour
{
    internal IRules rules;
    internal IOpeningPosition openingPosition;
    internal int boardSize;
    internal bool soundOn;

    private void Awake()
    {
        rules = new LinesOfActionRules();
        openingPosition = new LinesOfActionOpeningPosition();
        boardSize = 8;
        soundOn = true;

        DontDestroyOnLoad(this);
    }
}
