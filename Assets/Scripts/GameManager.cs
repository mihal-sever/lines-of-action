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

    public Player playerWhite;
    public Player playerBlack;
    public Player currentPlayer;

    private Checker selectedChecker;
    public Checker SelectedChecker
    {
        get { return selectedChecker; }
        set
        {
            if (selectedChecker != null)
            {
                selectedChecker.SetSelected(false);
            }

            selectedChecker = value;
        }
    }

    private void Awake()
    {
        SetupSingelton();
    }

    private void Start()
    {
        Board.Instance.CreateBoard();
        InitializeCheckers();
    }

    private void InitializeCheckers()
    {
        for (int i = 1; i < Board.Instance.size - 1; i++)
        {
            playerWhite.CreateChecker(Board.Instance.cells[0, i]);
            playerWhite.CreateChecker(Board.Instance.cells[Board.Instance.size - 1, i]);

            playerBlack.CreateChecker(Board.Instance.cells[i, 0]);
            playerBlack.CreateChecker(Board.Instance.cells[i, Board.Instance.size - 1]);
        }
    }
   
}
