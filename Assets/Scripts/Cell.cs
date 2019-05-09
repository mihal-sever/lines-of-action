using UnityEngine;

public class Cell : MonoBehaviour
{
    public Checker checker;

    private void OnMouseDown()
    {
        checker = GameManager.Instance.SelectedChecker;

        if (checker == null)
            return;

        checker.SetCell(this);
    }
}
