using System;
using UnityEditor;
using UnityEngine;

namespace TicTacToe
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Player Setting : ")] public Player player1;
        public Player player2;

        [Header("Mode : ")] [SerializeField] private bool playAsPlayer1 = true;
        public bool cpuAsPlayer2;

        public Board board;

        public Player? Winner;

        // Possible State
        public Player1Turn Player1Turn = new Player1Turn();
        public Player2Turn Player2Turn = new Player2Turn();
        public GameOver GameOver = new GameOver();

        private GameBaseState _currentState;

        private void Start()
        {
            board = GetComponent<Board>();

            board.ResetBoard();

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

        public void ChangeState(GameBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        public bool DetermineGameResult(Player player)
        {
            // return true = continue playing, return false = have winner or tie go to GameOver state
            var score = board.CheckWinner(player.mark);

            if (score < 0)
            {
                return true;
            } 
            
            if (score == 0)
            {
                Winner = null;
            }
            else
            {
                Winner = player;
            }

            return false;

            // var isTie = board.CheckTie();
            // if (isTie)
            // {
            //     Winner = null;
            //     return false;
            // }
            //
            // var score = board.CheckWinner(player.mark);
            //
            // if (score < 0) return true; // Not win continue playing
            //
            // Winner = player;
            // return false;
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

        public string name;
        public Marks mark;
        public Color color;
    }
}