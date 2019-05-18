using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void SetupSingelton()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple game managers exists: " + instance.name + " and now " + this.name);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public event Action<Player> onPlayerChanged;
    public event Action<Player> onWinner;

    public int boardSize = 8;

    public Player currentPlayer;
    public Player currentEnemy;

    internal Checker selectedChecker;
    
    private IOpeningPosition openingPosition;
    private IRules rules;

    private void Awake()
    {
        SetupSingelton();
        openingPosition = new ScrambledEggsOpeningPosition();
        rules = new LinesOfActionRules();
    }

    private void Start()
    {
        Board.Instance.CreateBoard(boardSize);
        rules.Initialize(Board.Instance.GetSize(), Board.Instance.cells, openingPosition);
    }
    
    public bool TrySelect(Checker checker)
    {    
        if (currentPlayer.IsOwnChecker(checker))
        {
            SelectChecker(checker);
            return true;
        }
        return false;
    }

    public bool TryMove(Cell cell)
    {
        if (!CanMove(cell))
            return false;
        
        if (rules.CanCaptureChecker(cell))
            CaptureChecker(cell.checker);
        
        MoveChecker(cell);

        if (rules.IsWin(currentPlayer))
            onWinner(currentPlayer);

        SwitchPlayer();
        return true;
    }

    private void SelectChecker(Checker checker)
    {
        if (selectedChecker != null)
        {
            selectedChecker.SetSelected(false);
            selectedChecker = null;
        }

        if (checker == null)
            return;

        selectedChecker = checker;
        selectedChecker.SetSelected(true);
    }

    private void MoveChecker(Cell cell)
    {
        selectedChecker.Move(cell);
        SelectChecker(null);
    }

    private void CaptureChecker(Checker checker)
    {
        currentEnemy.DestroyChecker(checker);
    }

    private bool CanMove(Cell cell)
    {
        if (selectedChecker == null)
            return false;

        return rules.CanMove(selectedChecker, cell);
    }
    
    private void SwitchPlayer()
    {
        var player = currentPlayer;
        currentPlayer = currentEnemy;
        currentEnemy = player;

        onPlayerChanged(currentPlayer);
    }
}
