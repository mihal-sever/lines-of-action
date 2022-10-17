using UnityEngine;

namespace Sever.BoardGames
{
    public class Cell : MonoBehaviour
    {
        internal Vector2Int position;

        private void OnMouseUp()
        {
            GameManager.Instance.TryMove(this);
        }
    }
}