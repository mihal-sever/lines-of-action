using UnityEngine;

public class Cell : MonoBehaviour
{
    internal Checker checker;

    private void OnMouseUp()
    {
        if (checker != null)
        {
            if (GameManager.Instance.TrySelect(checker))
                return;
        }

        GameManager.Instance.TryMove(this);
    }
}
