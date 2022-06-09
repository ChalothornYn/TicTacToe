using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.GameState
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
            
            UIManager.Instance.GameOver(game.Winner); ;
            UIManager.Instance.playAgain.GetComponent<Button>().onClick.AddListener(() => game.ChangeState(game.GameStart));
        }

        public override void UpdateState(GameStateManager game)
        {
            
        }
    }
}