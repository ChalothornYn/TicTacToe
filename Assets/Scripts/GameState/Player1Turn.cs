using UnityEngine;

namespace TicTacToe.GameState
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

            var success = game.boardManager.PlayerSetMask(_player);

            if (!success) return;
            
            game.DetermineGameResult(_player, game.Player2Turn);
        }
    }
}