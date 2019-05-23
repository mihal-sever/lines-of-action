using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Dropdown game;
    public Dropdown openingPosition;
    public Dropdown boardSize;
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
        game.onValueChanged.AddListener(delegate { GameChanged(game.value); });
        playButton.onClick.AddListener(delegate { OnHitPlay(); });

        game.value = PlayerPrefs.GetInt("game");
        openingPosition.value = PlayerPrefs.GetInt("openingPosition");
        boardSize.value = PlayerPrefs.GetInt("boardSize");
    }

    private void OnHitPlay()
    {
        PlayerPrefs.SetInt("game", game.value);
        PlayerPrefs.SetInt("openingPosition", openingPosition.value);
        PlayerPrefs.SetInt("boardSize", boardSize.value);
        PlayerPrefs.SetString("soundsOn", sounds.isOn.ToString());
        SceneManager.LoadScene("Lines of Action");
    }

    private void GameChanged(int index)
    {
        if (index == 0)
            openingPosition.options = openingPositionsForLines;
        else if (index == 1)
            openingPosition.options = openingPositionsForUgolki;
    }
}
