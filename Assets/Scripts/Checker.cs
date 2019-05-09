using UnityEngine;

public class Checker : MonoBehaviour
{
    private Cell cell;
    private Color normalColor;

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
    
    public void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            GetComponent<Renderer>().material.color = Color.red;
            GameManager.Instance.selectedChecker = this;
        }
        else
        {
            GetComponent<Renderer>().material.color = normalColor;
            GameManager.Instance.selectedChecker = null;
        }

    }

}
