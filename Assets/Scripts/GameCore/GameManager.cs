using System;
using UnityEngine;

namespace Sever.BoardGames
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= FindObjectOfType<GameManager>();

        public event Action<Player> PlayerChanged;
        public event Action<Player> PlayerWon;

        [SerializeField] private Player _whitePlayer;
        [SerializeField] private Player _blackPlayer;
        
        private Board _board;
        private int _boardSize;

        private Checker _selectedChecker;

        private RulesBase _rules;
        private OpeningPosition _openingPosition;

        private AudioSource _audioSource;
        private bool _soundsOn;


        public Player CurrentPlayer { get; private set; }
        public Player CurrentEnemy { get; private set; }


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _board = FindObjectOfType<Board>();

            SetupGame();
        }

        private void Start()
        {
            _board.CreateBoard(_boardSize);
            CurrentPlayer.SpawnCheckers(_openingPosition, _board);
            CurrentEnemy.SpawnCheckers(_openingPosition, _board);
            _rules.Initialize(_board);
        }

        public bool TrySelect(Checker checker)
        {
            if (!CurrentPlayer.OwnsChecker(checker))
            {
                return false;
            }

            SelectChecker(checker);
            return true;
        }

        public bool TryMove(Cell cell)
        {
            if (!CanMove(cell))
            {
                return false;
            }

            if (_rules.CanCaptureChecker(cell))
            {
                Checker checker = _rules.GetCheckerOnCell(cell);
                CaptureChecker(checker);
            }

            MoveChecker(cell);
            if (_soundsOn)
            {
                _audioSource.Play();
            }

            if (_rules.IsWin(CurrentPlayer))
            {
                PlayerWon?.Invoke(CurrentPlayer);
            }

            SwitchPlayer();
            return true;
        }

        private void SelectChecker(Checker checker)
        {
            if (_selectedChecker != null)
            {
                _selectedChecker.SetSelected(false);
                _selectedChecker = null;
            }

            if (checker == null)
            {
                return;
            }

            _selectedChecker = checker;
            _selectedChecker.SetSelected(true);
        }

        private void MoveChecker(Cell cell)
        {
            _selectedChecker.Move(cell);
            SelectChecker(null);
        }

        private void CaptureChecker(Checker checker)
        {
            CurrentEnemy.DestroyChecker(checker);
        }

        private bool CanMove(Cell cell)
        {
            return _selectedChecker != null && _rules.CanMove(_selectedChecker.Cell, cell);
        }

        private void SwitchPlayer()
        {
            (CurrentPlayer, CurrentEnemy) = (CurrentEnemy, CurrentPlayer);
            PlayerChanged?.Invoke(CurrentPlayer);
        }

        private void SetupGame()
        {
            CurrentPlayer = _whitePlayer;
            CurrentEnemy = _blackPlayer;
            
            _soundsOn = GameConfig.soundOn;
            _boardSize = GameConfig.boardSize;
            _rules = GameConfig.rules;
            _openingPosition = GameConfig.openingPosition;
            _openingPosition.SetBoardSize(_boardSize);
        }
    }
}