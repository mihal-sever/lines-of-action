using Sever.Utilities;
using UnityEngine;

namespace Sever.BoardGames
{
    public class Checker : MonoBehaviour
    {
        private Cell _cell;
        private Renderer _renderer;

        private Color _defaultColor;
        private Color _selectedColor = Color.red;

        public Cell Cell => _cell;

        public Vector2Int Position => _cell.position;


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultColor = _renderer.material.color;
        }

        private void OnMouseUp()
        {
            if (GameManager.Instance.TrySelect(this))
            {
                return;
            }

            GameManager.Instance.TryMove(_cell);
        }

        public void Move(Cell newCell)
        {
            _cell = newCell;
            transform.position = _cell.transform.position + new Vector3(0, transform.localScale.y, 0);
        }

        public void SetSelected(bool isSelected)
        {
            _renderer.SetColor(isSelected ? _selectedColor : _defaultColor);
        }
    }
}