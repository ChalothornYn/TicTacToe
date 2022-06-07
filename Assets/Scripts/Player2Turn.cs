using UnityEngine;

namespace TicTacToe
{
    public class Player2Turn : BoardBaseState
    {
        private Player _player;
        
        public override void EnterState(BoardStateManager board)
        {
            Debug.Log("<color=lime>Player 2 turn!</color>");
            _player = board.player2;
        }

        public override void UpdateState(BoardStateManager board)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var pos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(pos, board.layerMask);

            if(hit == null) return;
            
            Debug.Log($"<color=red>{hit.name} : {_player.mark}</color>");
            
            var box = hit.GetComponent<Box>();
            var index = box.SetMarkAs(_player.mark, _player.color);
            
            board.boardArray[index] = _player.mark;
            
            board.ChangeState(board.CheckWinner);
        }
    }
}