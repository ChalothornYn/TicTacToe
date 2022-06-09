using UnityEngine;

namespace TicTacToe.GameState
{
    public class Player2Turn : GameBaseState
    {
        private Player _player;

        private Cpu _cpu;

        private float _time = 0;

        private readonly float[] _waitTime = new[] {0.5f, 1f, 1.5f};

        public override void EnterState(GameStateManager game)
        {
            Debug.Log("<color=lime>Player 2 turn!</color>");

            _time = 0;

            _player = game.player2;

            _cpu = new Cpu(cpu: game.player2, player: game.player1, game.cpuLevel);

            UIManager.Instance.ShowTurn(_player);
        }

        public override void UpdateState(GameStateManager game)
        {
            if (game.cpuAsPlayer2)
            {
                _time += Time.deltaTime;
                
                if (_time <= _waitTime[(int)game.cpuLevel]) return;
                
                Debug.Log($"<color=lime>CPU Thinking : {game.cpuLevel}</color>");
                
                _cpu.Move(game.boardManager.Board);
                
                game.DetermineGameResult(game.Player1Turn);
            }
            else
            {
                if (!Input.GetMouseButtonDown(0)) return;

                var success = game.boardManager.PlayerSetMask(_player);

                if (!success) return;
                
                game.DetermineGameResult(game.Player1Turn);
            }

            
        }
    }
}