using UnityEngine;

namespace TicTacToe
{
    public class GameOver : GameBaseState
    {
        public override void EnterState(GameStateManager game)
        {
            if (game.Winner == null)
            {
                Debug.Log($"<color=orange>Game is tie</color>");
            }
            else
            {
                Debug.Log($"<color=orange>Winner is {game.Winner.Value.name}</color>");
            }
            
            UIManager.Instance.GameOver(game.Winner);
            
            //game.ChangeState(game.Player1Turn);
        }

        public override void UpdateState(GameStateManager game)
        {
            
        }
    }
}