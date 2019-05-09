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

    public void SetCell(Cell cell)
    {
        this.cell = cell;
        PositionChecker();
        SetSelected(false);
    }

    private void PositionChecker()
    {
        transform.position = cell.transform.position + new Vector3(0, transform.localScale.y, 0);
    }

    private void OnMouseUp()
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
    
    public void SetSelected(bool isSelected)
    {
        if (this.isSelected == isSelected)
           return;

        this.isSelected = isSelected;

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
