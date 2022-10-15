using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Dropdown game;
    public Dropdown openingPosition;
    //public Dropdown boardSize;
    public Toggle sounds;
    public Button playButton;

    private List<Dropdown.OptionData> openingPositionsForUgolki;
    private List<Dropdown.OptionData> openingPositionsForLines;

    private void Awake()
    {
        openingPositionsForUgolki = new List<Dropdown.OptionData>() { new Dropdown.OptionData("Classic"), new Dropdown.OptionData("Diagonal") };
        openingPositionsForLines = new List<Dropdown.OptionData>() { new Dropdown.OptionData("Classic"), new Dropdown.OptionData("Scrambled eggs") };
    }

    private void Start()
    {
        game.onValueChanged.AddListener(delegate { OnGameChanged(game.value); });
        openingPosition.onValueChanged.AddListener(delegate { OnOpeningPositionChanged(openingPosition.value); });
        //boardSize.onValueChanged.AddListener(delegate { OnBoardSizeChanged(boardSize.value); });
        sounds.onValueChanged.AddListener(delegate { OnSoundToggle(sounds.isOn); });
        playButton.onClick.AddListener(delegate { OnHitPlay(); });
    }

    private void OnGameChanged(int index)
    {
        if (index == 0)
        {
            GameConfig.rules = new LinesOfActionRules();
            openingPosition.options = openingPositionsForLines;
        }
        else if (index == 1)
        {
            GameConfig.rules = new UgolkiRules();
            openingPosition.options = openingPositionsForUgolki;
        }
        OnOpeningPositionChanged(openingPosition.value);
    }

    private void OnOpeningPositionChanged(int index)
    {
        if (game.value == 0)
        {
            if (index == 0)
            {
                GameConfig.openingPosition = new LinesOfActionOpeningPosition();
            }
            else if (index == 1)
            {
                GameConfig.openingPosition = new ScrambledEggsOpeningPosition();
            }
        }
        else if (game.value == 1)
        {
            if (index == 0)
            {
                GameConfig.openingPosition = new UgolkiClassicOpeningPosition();
            }
            else if (index == 1)
            {
                GameConfig.openingPosition = new UgolkiDiagonalOpeningPosition();
            }
        }
    }

    //private void OnBoardSizeChanged(int index)
    //{
    //    if (index == 0)
    //    {
    //        config.boardSize = 8;
    //    }
    //    else if (index == 1)
    //    {
    //        config.boardSize = 10;
    //    }
    //}

    private void OnSoundToggle(bool isOn)
    {
        GameConfig.soundOn = isOn;
    }

    private void OnHitPlay()
    {
        SceneManager.LoadScene(1);
    }
}
