using UnityEngine;

public class Cell : MonoBehaviour
{
    public Checker checker;

    private void OnMouseUp()
    {
        // ignore hitting empty cell when no checker selected
        if (GameManager.Instance.SelectedChecker == null && checker == null)
            return;

        // select or deselect checker
        else if (checker != null && (GameManager.Instance.SelectedChecker == null || checker == GameManager.Instance.SelectedChecker))
        {
            checker.TrySelect();
        }

        else if (GameManager.Instance.SelectedChecker != null)
        {
            // capture enemy
            if (checker != null)
            {
                Destroy(checker.gameObject);
            }
            // move selected checker
            checker = GameManager.Instance.SelectedChecker;
            checker.SetCell(this);
        }
    }
}
