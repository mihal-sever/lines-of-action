using UnityEngine;

public class Checker : MonoBehaviour
{
    private Cell cell;
    private Color normalColor;

    private void Awake()
    {
        normalColor = GetComponent<Renderer>().material.color;
    }

    public void Move(Cell _cell)
    {
        if (cell != null)
            cell.checker = null;
        cell = _cell;
        cell.checker = this;

        Position();
    }

    public Cell GetCell()
    {
        return cell;
    }

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
            GetComponent<Renderer>().material.color = Color.red;
        else
            GetComponent<Renderer>().material.color = normalColor;
    }

    private void Position()
    {
        transform.position = cell.transform.position + new Vector3(0, transform.localScale.y, 0);
    }

}
