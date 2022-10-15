using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text currentPlayerText;
    public GameObject winnerPanel;
    public Text winnerText;

    private void Start()
    {
        GameManager.Instance.onPlayerChanged += OnPlayerChanged;
        GameManager.Instance.onWinner += OnWinner;
    }

    public void OnPlayerChanged(Player player)
    {
        currentPlayerText.text = player.playerName;
    }

    public void OnWinner(Player player)
    {
        winnerPanel.SetActive(true);
        winnerText.text = player.name + " WON";
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
