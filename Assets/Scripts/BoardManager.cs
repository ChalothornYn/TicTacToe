using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class BoardManager : MonoBehaviour
    {
        [Space] public int boardDimension;

        [Space] public int markCount = 0;
        
        [Space] public LayerMask boxLayerMask;

        private readonly Dictionary<int, Box> _boxes = new Dictionary<int, Box>();

        public Board Board;

        public void Initialize()
        {
            //boardArray = new Player.Marks[boardDimension * boardDimension];
            
            var boxes = GetComponentsInChildren<Box>();

            foreach (var box in boxes)
            {
                _boxes.Add(box.index, box);
            }

            Board = new Board(new Player.Marks[boardDimension * boardDimension], boardDimension, _boxes);
        }
        
        public bool PlayerSetMask(Player player)
        {
            var pos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(pos, boxLayerMask);

            if(hit == null) return false;
            
            Debug.Log($"<color=red>{hit.name} : {player.mark}</color>");
            
            var box = hit.GetComponent<Box>();
            
            Board.SetMark(box.index, player);
            
            return true;
        }
        
        public void CPUSetMark(Player player)
        {
           
        }
    }
    
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