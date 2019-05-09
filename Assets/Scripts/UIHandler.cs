using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text currentPlayerText;

    public void UpdateText(Player player)
    {
        currentPlayerText.text = player.playerName;
    }
}
