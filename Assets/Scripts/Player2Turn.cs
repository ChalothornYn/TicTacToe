using UnityEngine;

namespace TicTacToe
{
    public class Player2Turn : GameBaseState
    {
        private Player _player;
        
        public override void EnterState(GameStateManager game)
        {
            Debug.Log("<color=lime>Player 2 turn!</color>");
            _player = game.player2;
        }

        public override void UpdateState(GameStateManager game)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var success = game.board.SetMask(_player);

            if (!success) return;
            
            var canContinue = game.DetermineGameResult(_player);
            
            if (canContinue)
            {
                game.ChangeState(game.Player1Turn);
            }
            else
            {
                game.ChangeState(game.GameOver);
            }
        }
    }
}