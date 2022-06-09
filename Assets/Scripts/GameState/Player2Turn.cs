using UnityEngine;

namespace TicTacToe.GameState
{
    public class Player2Turn : GameBaseState
    {
        private Player _player;

        public override void EnterState(GameStateManager game)
        {
            Debug.Log("<color=lime>Player 2 turn!</color>");
            _player = game.player2;
            UIManager.Instance.ShowTurn(_player);
        }

        public override void UpdateState(GameStateManager game)
        {
            if (game.cpuAsPlayer2)
            {
                // CPU decide which box to mark
                game.boardManager.CPUSetMark(_player);
            }
            else
            {
                if (!Input.GetMouseButtonDown(0)) return;

                var success = game.boardManager.PlayerSetMask(_player);

                if (!success) return;
            }
            
            game.DetermineGameResult(_player, game.Player1Turn);
        }
    }
}