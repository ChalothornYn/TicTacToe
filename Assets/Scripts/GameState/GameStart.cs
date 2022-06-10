namespace TicTacToe.GameState
{
    public class GameStart : GameBaseState
    {
        private bool _doneSetting = false;
        
        public override void EnterState(GameStateManager game)
        {
            UIManager.Instance.ResetGame();

            game.cpuAsPlayer2 = true;
            
            game.boardManager.Board.ResetBoard();

            game.GameResult.Reset();
            
            UIManager.Instance.easy.onClick.AddListener(() =>
            {
                game.cpuLevel = Level.Easy;
                _doneSetting = true;
            });
            
            UIManager.Instance.medium.onClick.AddListener(() =>
            {
                game.cpuLevel = Level.Medium;
                _doneSetting = true;
            });
            
            UIManager.Instance.hard.onClick.AddListener(() =>
            {
                game.cpuLevel = Level.Hard;
                _doneSetting = true;
            });
            
            UIManager.Instance.multiplayer.onClick.AddListener(() =>
            {
                game.cpuAsPlayer2 = false;
                _doneSetting = true;
            });

            _doneSetting = false;
        }

        public override void UpdateState(GameStateManager game)
        {
            game.goFirst = UIManager.Instance.player1GoFirst.isOn;
            
            if (!_doneSetting) return;
            
            UIManager.Instance.StartGame();
                
            if (game.goFirst)
                game.ChangeState(game.Player1Turn);
            else
                game.ChangeState(game.Player2Turn);
        }
    }
}