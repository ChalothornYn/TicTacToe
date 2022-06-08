using System;
using UnityEngine;

namespace TicTacToe
{
    public class BoardStateManager : MonoBehaviour
    {
        [Header("Board setting : ")] 
        public int boardWidth;
        public int boardHigh;

        [Header("Player Setting : ")]
        public Player player1;
        public Player player2;

        [Header("Mode : ")]
        [SerializeField] private bool playAsPlayer1 = true;
        public bool cpuAsPlayer2;

        [Header("Input Setting : ")]
        public LayerMask layerMask;
        
        [Space] public Player.Marks[] boardArray;
        
        // Possible State
        public Player1Turn Player1Turn = new Player1Turn();
        public Player2Turn Player2Turn = new Player2Turn();
        public GameOver GameOver = new GameOver();
        
        private BoardBaseState _currentState;

        public Board board;
        
        private void Start()
        {
            boardArray = new Player.Marks[boardWidth * boardHigh];

            board = GetComponent<Board>();

            board.ResetMask();
            
            if (playAsPlayer1)
                _currentState = Player1Turn;
            else
                _currentState = Player2Turn;
            
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(BoardBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        public bool CheckWinner(Player.Marks mark)
        {
            var win = true;
            
            // horizontal
            for (var h = 0; h < boardHigh; h++)
            {
                win = true;
                for (var w = 0; w < boardWidth; w++)
                {
                    var index = w + (boardWidth * h); // 0 1 2 // 3 4 5 // 6 7 8
                    if (boardArray[index] != mark)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }
            return false;
        }
    }

    [Serializable]
    public struct Player
    {
        public enum Marks
        {
            None,
            X,
            O,
        }
        public Marks mark;
        public Color color;
    }
}