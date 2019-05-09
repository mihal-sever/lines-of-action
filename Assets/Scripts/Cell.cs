using UnityEngine;

public class Cell : MonoBehaviour
{
    public Checker checker;

    private void OnMouseUp()
    {
        var selectedChecker = GameManager.Instance.selectedChecker;

        if (selectedChecker == null)
        {
            // ignore hitting empty cell when no checker selected
            if (checker == null)
                return;

            // select checker
            if (checker != null)
                checker.SetSelected(true);
        }
        else
        {
            // capture enemy
            if (checker != null)
            {
                Destroy(checker.gameObject);
            }
            // move selected checker
            selectedChecker.SetCell(this);
        }
    }
}
