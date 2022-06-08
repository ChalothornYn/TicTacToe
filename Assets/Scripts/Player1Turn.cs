using UnityEngine;

namespace TicTacToe
{
    public class Player1Turn : GameBaseState
    {
        private Player _player;

        public override void EnterState(GameStateManager game)
        {
            Debug.Log("<color=lime>Player 1 turn!</color>");
            _player = game.player1;
            UIManager.Instance.ShowTurn(_player);
        }

        public override void UpdateState(GameStateManager game)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var success = game.board.SetMask(_player);

            if (!success) return;
            
            var canContinue = game.DetermineGameResult(_player);
            
            if (canContinue)
            {
                game.ChangeState(game.Player2Turn);
            }
            else
            {
                game.ChangeState(game.GameOver);
            }
        }
    }
}