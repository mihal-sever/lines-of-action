using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void SetupSingelton()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple game managers exists: " + Instance.name + " and now " + this.name);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public event Action<Player> onPlayerChanged;
    public event Action<Player> onWinner;
    
    public Player currentPlayer;
    public Player currentEnemy;

    internal Checker selectedChecker;

    private Board board;

    private RulesBase rules;
    private OpeningPosition openingPosition;
    private int boardSize;
    private bool soundsOn;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        board = FindObjectOfType<Board>();
        
        SetupGame();
        SetupSingelton();
    }

    private void Start()
    {
        board.CreateBoard(boardSize);
        currentPlayer.SpawnCheckers(openingPosition, board);
        currentEnemy.SpawnCheckers(openingPosition, board);
        rules.Initialize(board);
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
        {
            Checker checker = rules.GetCheckerOnCell(cell);
            CaptureChecker(checker);
        }
        
        MoveChecker(cell);
        if (soundsOn)
            audio.Play();

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

        return rules.CanMove(selectedChecker.GetCell(), cell);
    }
    
    private void SwitchPlayer()
    {
        var player = currentPlayer;
        currentPlayer = currentEnemy;
        currentEnemy = player;

        onPlayerChanged(currentPlayer);
    }

    private void SetupGame()
    {
        soundsOn = GameConfig.soundOn;
        boardSize = GameConfig.boardSize;
        rules = GameConfig.rules;
        openingPosition = GameConfig.openingPosition;
        openingPosition.SetBoardSize(boardSize);
    }
}
