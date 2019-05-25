using UnityEngine;

public class Cell : MonoBehaviour
{
    private void OnMouseUp()
    {
        GameManager.Instance.TryMove(this);
    }
}
