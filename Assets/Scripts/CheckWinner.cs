using UnityEngine;

namespace TicTacToe
{
    public class CheckWinner : BoardBaseState
    {
        public override void EnterState(BoardStateManager board)
        {
            Debug.Log("<color=lime>Check Winner!</color>");
            board.ChangeState(board.Player1Turn);
        }

        public override void UpdateState(BoardStateManager board)
        {
            throw new System.NotImplementedException();
        }
    }
}