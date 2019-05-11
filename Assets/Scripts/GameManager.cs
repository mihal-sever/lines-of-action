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

    public Player currentPlayer;
    public Player currentEnemy;

    public Checker selectedChecker;

    private UIHandler uiHandler;

    private void Awake()
    {
        SetupSingelton();
        uiHandler = FindObjectOfType<UIHandler>();
    }

    private void Start()
    {
        Board.Instance.CreateBoard();
        InitializeCheckers();
        Rules.Initialize();
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

        if (cell.checker != null)
        {
            CaptureChecker(cell.checker);
        }
        
        MoveChecker(cell);
        if (Rules.IsWin(currentPlayer))
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

        return Rules.CanMove(selectedChecker, cell);
    }
    
    private void SwitchPlayer()
    {
        var player = currentPlayer;
        currentPlayer = currentEnemy;
        currentEnemy = player;

        onPlayerChanged(currentPlayer);
    }

    private void InitializeCheckers()
    {
        for (int i = 1; i < Board.Instance.size - 1; i++)
        {
            currentPlayer.CreateChecker(Board.Instance.cells[0, i]);
            currentPlayer.CreateChecker(Board.Instance.cells[Board.Instance.size - 1, i]);

            currentEnemy.CreateChecker(Board.Instance.cells[i, 0]);
            currentEnemy.CreateChecker(Board.Instance.cells[i, Board.Instance.size - 1]);
        }
    }
   
}
