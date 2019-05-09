using UnityEngine;

public class Checker : MonoBehaviour
{
    public Player player;

    private Cell cell;

    private bool isSelected = false;
    public Color normalColor;

    private void Awake()
    {
        normalColor = GetComponent<Renderer>().material.color;
    }

    public void SetCell(Cell _cell)
    {
        if (cell != null)
            cell.checker = null;
        cell = _cell;
        cell.checker = this;
        PositionChecker();
        SetSelected(false);
    }

    private void PositionChecker()
    {
        transform.position = cell.transform.position + new Vector3(0, transform.localScale.y, 0);
    }
    
    public void TrySelect()
    {
        if (!isSelected)
        {
            SetSelected(true);
        }
        else
        {
            SetSelected(false);
        }
    }

    public void SetSelected(bool _isSelected)
    {
        if (isSelected == _isSelected)
           return;

        isSelected = _isSelected;

        if (isSelected)
        {
            GetComponent<Renderer>().material.color = Color.red;
            GameManager.Instance.SelectedChecker = this;
        }
        else
        {
            GetComponent<Renderer>().material.color = normalColor;
            GameManager.Instance.SelectedChecker = null;
        }

    }

}
