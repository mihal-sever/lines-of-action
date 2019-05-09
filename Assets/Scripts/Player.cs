using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject checkerPrefab;
    public List<Checker> checkers;
    
    public void CreateChecker(Cell cell)
    {
        GameObject instance = Instantiate(checkerPrefab, transform);
        Checker checker = instance.GetComponent<Checker>();
        checker.SetCell(cell);
        checkers.Add(checker);
    }
    
}
