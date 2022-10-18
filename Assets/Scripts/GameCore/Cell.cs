using UnityEngine;
using UnityEngine.EventSystems;

namespace Sever.BoardGames
{
    public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject _hoverIndicator;

        public Vector2Int Position { get; private set; }


        private void Awake()
        {
            _hoverIndicator.SetActive(false);
        }

        public void Init(Vector2Int position)
        {
            Position = position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverIndicator.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoverIndicator.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.TryMove(this);
            _hoverIndicator.SetActive(false);
        }
    }
}