using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sever.BoardGames.UI
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Text _currentPlayerText;
        [SerializeField] private GameObject _winnerPanel;
        [SerializeField] private Text _winnerText;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _reloadButton;


        private void Awake()
        {
            _backToMenuButton.onClick.AddListener(BackToMenu);
            _reloadButton.onClick.AddListener(ReloadLevel);

            GameManager.Instance.PlayerChanged += OnPlayerChanged;
            GameManager.Instance.PlayerWon += OnPlayerWon;
        }

        private void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnPlayerChanged(Player player)
        {
            _currentPlayerText.text = player.ToString();
        }

        private void OnPlayerWon(Player player)
        {
            _winnerPanel.SetActive(true);
            _winnerText.text = player.name + " WON";
        }
    }
}