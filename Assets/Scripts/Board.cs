using System.Collections.Generic;
using UnityEngine.UIElements;

namespace TicTacToe
{
    public class Board
    {
        private Player.Marks[] _boardArray;
        private readonly int _boardDimension;
        private readonly Dictionary<int, Box> _boxes;
        private int _markCount;

        public Board(Player.Marks[] boardArray, int boardDimension, Dictionary<int, Box> boxes)
        {
            _boardArray = boardArray;
            _boardDimension = boardDimension;
            _boxes = boxes;
        }

        public void ResetBoard()
        {
            _boardArray = new Player.Marks[_boardDimension * _boardDimension];

            _markCount = 0;
            
            foreach (var box in _boxes.Values)
            {
                box.ResetMark();
            }
        }

        public void SetMark(int index, Player player)
        {
            _boxes[index].Set(player.mark, player.color);
            _boardArray[index] = player.mark;
            _markCount++;
        }

        private bool CheckTie()
        {
            return _markCount == _boardDimension * _boardDimension;
        }

        public int CheckWinner(Player.Marks mark)
        {
            var won = true;

            #region Horizontal
            
            for (var h = 0; h < _boardDimension; h++)
            {
                won = true;
                for (var w = 0; w < _boardDimension; w++)
                {
                    var index = w + (_boardDimension * h); // 0 1 2 // 3 4 5 // 6 7 8
                    if (_boardArray[index] != mark)
                    {
                        won = false;
                        break;
                    }
                }
                if (won) return 1;
            }

            #endregion
            
            #region Vertical

            for (var w = 0; w < _boardDimension; w++)
            {
                won = true;
                for (var h = 0; h < _boardDimension; h++)
                {
                    var index = w + (_boardDimension * h); // 0 3 6 // 1 4 7 // 2 5 8
                    if (_boardArray[index] != mark)
                    {
                        won = false;
                        break;
                    }
                }
                if (won) return 1;
            }

            #endregion

            #region Diagonal Right to Left

            var rightMost = _boardDimension - 1;
            won = true;
            for (var w = 1; w <= _boardDimension; w++)
            {
                var index = rightMost * w; // 2 4 6
                if (_boardArray[index] != mark)
                {
                    won = false;
                    break;
                }
            }
            if (won) return 1;

            #endregion

            #region Diagonal Left to Right
            
            var multiplier = ((_boardDimension * _boardDimension) - 1) / (_boardDimension - 1); 
            won = true;
            for (var h = 0; h < _boardDimension; h++)
            {
                var index = multiplier * h; // 0 4 8
                if (_boardArray[index] != mark)
                {
                    won = false;
                    break;
                }
            }
            if (won) return 1;
            
            #endregion
            
            if (CheckTie()) return 0;
            
            return -1; // Game not over yet, continue playing
        }
    }
}