using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Sever.BoardGames.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Dropdown _gameSelector;
        [SerializeField] private Dropdown _openingPositionSelector;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Button _playButton;

        private readonly Dictionary<int, List<Dropdown.OptionData>> _openingPositions = new();


        private void Awake()
        {
            _openingPositions.Add(0, new List<Dropdown.OptionData> {new("Classic"), new("Diagonal")});
            _openingPositions.Add(1, new List<Dropdown.OptionData> {new("Classic"), new("Scrambled eggs")});
        }

        private void Start()
        {
            _gameSelector.onValueChanged.AddListener(OnGameChanged);
            _openingPositionSelector.onValueChanged.AddListener(OnOpeningPositionChanged);
            _soundToggle.onValueChanged.AddListener(ToggleSound);
            _playButton.onClick.AddListener(Play);
        }

        private void OnGameChanged(int index)
        {
            GameConfig.rules = index switch
            {
                0 => new LinesOfActionRules(),
                1 => new UgolkiRules()
            };

            _openingPositionSelector.options = _openingPositions[index];

            OnOpeningPositionChanged(_openingPositionSelector.value);
        }

        private void OnOpeningPositionChanged(int index)
        {
            if (_gameSelector.value == 0)
            {
                if (index == 0)
                {
                    GameConfig.openingPosition = new LinesOfActionOpeningPosition();
                    return;
                }

                GameConfig.openingPosition = new ScrambledEggsOpeningPosition();
                return;
            }

            if (index == 0)
            {
                GameConfig.openingPosition = new UgolkiClassicOpeningPosition();
                return;
            }

            GameConfig.openingPosition = new UgolkiDiagonalOpeningPosition();
        }

        private void ToggleSound(bool isOn)
        {
            GameConfig.soundOn = isOn;
        }

        private void Play()
        {
            SceneManager.LoadScene(1);
        }
    }
}