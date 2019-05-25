using UnityEngine;

public class Checker : MonoBehaviour
{
    private Cell cell;
    private Color normalColor;

    private void Awake()
    {
        normalColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseUp()
    {
        if (!GameManager.Instance.TrySelect(this))
            GameManager.Instance.TryMove(cell);
    }

    public void Move(Cell newCell)
    {
        cell = newCell;
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
