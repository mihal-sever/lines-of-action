using UnityEngine;

public class AdjustCameraPosition : MonoBehaviour
{
    void Start()
    {
        int boardSize = Board.Instance.Size;
        float centerPoint = boardSize / 2;
        transform.position = new Vector3(centerPoint + 0.5f, boardSize + 8f, centerPoint - 0.5f);
    }
}
