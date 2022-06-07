using System;
using UnityEngine;

namespace TicTacToe
{
    public class BoardStateManager : MonoBehaviour
    {
        [SerializeField] private bool playAsPlayer1;

        public Player1Turn Player1Turn = new Player1Turn();
        public Player2Turn Player2Turn = new Player2Turn();

        private BoardBaseState _currentState;
        
        private void Start()
        {

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
}