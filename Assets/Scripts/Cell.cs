using UnityEngine;

public class Cell : MonoBehaviour
{
    public Checker checker;

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
