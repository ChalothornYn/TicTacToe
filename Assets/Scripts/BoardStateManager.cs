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
        public CheckWinner CheckWinner = new CheckWinner();
        
        private BoardBaseState _currentState;
        
        private void Start()
        {
            boardArray = new Player.Marks[boardWidth * boardHigh];

            foreach (var box in GetComponentsInChildren<Box>())
            {
                box.ResetMark();
            }
            
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