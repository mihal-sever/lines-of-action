﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject checkerPrefab;
    public string playerName;

    internal List<Checker> checkers = new List<Checker>();
    internal List<Cell> targetCells;

    public void CreateChecker(Cell cell)
    {
        GameObject instance = Instantiate(checkerPrefab, transform);
        Checker checker = instance.GetComponent<Checker>();        
        checker.Move(cell);
        checkers.Add(checker);
        checker.name = "checker " + checkers.Count;
    }

    public void DestroyChecker(Checker checker)
    {
        checkers.Remove(checker);
        Destroy(checker.gameObject);
    }

    public bool IsOwnChecker(Checker checker)
    {
        return checkers.Contains(checker);
    }
}
