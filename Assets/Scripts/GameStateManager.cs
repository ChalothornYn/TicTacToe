using System;
using UnityEditor;
using UnityEngine;

namespace TicTacToe
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Player Setting : ")]
        public Player player1;
        public Player player2;

        [Header("Mode : ")] 
        public bool goFirst = true;
        public bool cpuAsPlayer2;

        [Space] public Board board;

        public Player? Winner;

        // Possible State
        public readonly Player1Turn Player1Turn = new Player1Turn();
        public readonly Player2Turn Player2Turn = new Player2Turn();
        public readonly GameOver GameOver = new GameOver();

        private GameBaseState _currentState;

        private void Start()
        {
            UIManager.Instance.ResetGame();
            
            board.ResetBoard();

            if (goFirst)
                _currentState = Player1Turn;
            else
                _currentState = Player2Turn;
            
            UIManager.Instance.StartGame();
            
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

        public void DetermineGameResult(Player player, GameBaseState nextState)
        {
            var score = board.CheckWinner(player.mark);

            if (score < 0) // Continue playing
            {
                ChangeState(nextState);
                return;
            } 
            
            if (score == 0)
            {
                Winner = null;
            }
            else
            {
                Winner = player;
            }

            ChangeState(GameOver);
            return;
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