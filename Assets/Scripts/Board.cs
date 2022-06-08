using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class Board : MonoBehaviour
    {
        [Space] public int boardDimension;

        [Space] public int markCount = 0;
        
        [Space] public LayerMask boxLayerMask;
        
        [Space] public Player.Marks[] boardArray;
        
        private Box[] _boxes;

        private void Awake()
        {
            boardArray = new Player.Marks[boardDimension * boardDimension];
            _boxes = GetComponentsInChildren<Box>();
        }

        public void ResetBoard()
        {
            boardArray = new Player.Marks[boardDimension * boardDimension];

            markCount = 0;
            
            foreach (var box in _boxes)
            {
                box.ResetMark();
            }
        }
        
        public bool PlayerSetMask(Player player)
        {
            var pos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(pos, boxLayerMask);

            if(hit == null) return false;
            
            Debug.Log($"<color=red>{hit.name} : {player.mark}</color>");
            
            var box = hit.GetComponent<Box>();
            
            box.Set(player.mark, player.color);

            boardArray[box.index] = player.mark;
            markCount++;
            
            return true;
        }
        
        public void CPUSetMark(Player player)
        {
           
        }

        private bool CheckTie()
        {
            return markCount == boardDimension * boardDimension;
        }

        public int CheckWinner(Player.Marks mark)
        {
            var won = true;

            #region Horizontal
            
            for (var h = 0; h < boardDimension; h++)
            {
                won = true;
                for (var w = 0; w < boardDimension; w++)
                {
                    var index = w + (boardDimension * h); // 0 1 2 // 3 4 5 // 6 7 8
                    if (boardArray[index] != mark)
                    {
                        won = false;
                        break;
                    }
                }
                if (won) return 1;
            }

            #endregion
            
            #region Vertical

            for (var w = 0; w < boardDimension; w++)
            {
                won = true;
                for (var h = 0; h < boardDimension; h++)
                {
                    var index = w + (boardDimension * h); // 0 3 6 // 1 4 7 // 2 5 8
                    if (boardArray[index] != mark)
                    {
                        won = false;
                        break;
                    }
                }
                if (won) return 1;
            }

            #endregion

            #region Diagonal Right to Left

            var rightMost = boardDimension - 1;
            won = true;
            for (var w = 1; w <= boardDimension; w++)
            {
                var index = rightMost * w; // 2 4 6
                if (boardArray[index] != mark)
                {
                    won = false;
                    break;
                }
            }
            if (won) return 1;

            #endregion

            #region Diagonal Left to Right
            
            var multiplier = ((boardDimension * boardDimension) - 1) / (boardDimension - 1); 
            won = true;
            for (var h = 0; h < boardDimension; h++)
            {
                var index = multiplier * h; // 0 4 8
                if (boardArray[index] != mark)
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