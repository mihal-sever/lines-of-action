using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sever.BoardGames
{
    [RequireComponent(typeof(Image))]
    public class Checker : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _selectedSprite;

        private Image _image;

        public Cell Cell { get; private set; }

        public Vector2Int Position => Cell.Position;


        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Move(Cell newCell)
        {
            Cell = newCell;
            transform.position = Cell.transform.position;
        }

        public void SetSelected(bool isSelected)
        {
            _image.sprite = isSelected ? _selectedSprite : _defaultSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.Instance.TrySelect(this))
            {
                return;
            }

            GameManager.Instance.TryMove(Cell);
        }
    }
}