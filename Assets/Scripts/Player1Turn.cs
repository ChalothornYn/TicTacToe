using UnityEngine;

namespace TicTacToe
{
    public class Player1Turn : BoardBaseState
    {
        private Player _player;
        
        public override void EnterState(BoardStateManager board)
        {
            Debug.Log("<color=lime>Player 1 turn!</color>");
            _player = board.player1;
        }

        public override void UpdateState(BoardStateManager board)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            
            
            board.ChangeState(board.Player2Turn);
        }
    }
}