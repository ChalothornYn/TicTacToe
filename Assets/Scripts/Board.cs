using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe
{
    public class Board
    {
        public Player.Marks[] BoardArray;
        public readonly int BoardDimension;
        public readonly Dictionary<int, Box> Boxes;
        public int MarkCount;

        public Board(Player.Marks[] boardArray, int boardDimension, Dictionary<int, Box> boxes)
        {
            BoardArray = boardArray;
            BoardDimension = boardDimension;
            Boxes = boxes;
        }

        public void ResetBoard()
        {
            BoardArray = new Player.Marks[BoardDimension * BoardDimension];

            MarkCount = 0;
            
            foreach (var box in Boxes.Values)
            {
                box.ResetMark();
            }
        }

        public void SetMark(int index, Player player)
        {
            Boxes[index].Set(player.mark, player.color);
            BoardArray[index] = player.mark;
            //MarkCount++;
        }
        
        public void SetMarkNotVisualize(int index, Player.Marks mark)
        {
            BoardArray[index] = mark;
            //if(mark == Player.Marks.None) return;
            //PrintBoard();
            //MarkCount++;
        }

        public void PrintBoard()
        {
            for (var i = 0; i < BoardDimension; i++)
            {
                Debug.Log($"{BoardArray[0 + 3*i].ToString()} {BoardArray[1 + 3*i].ToString()} {BoardArray[2 + 3*i].ToString()}");
            }
        }

        public List<int> GetEmptyBoxIndex()
        {
            var indexList = new List<int>();
            for (var i = 0; i < BoardArray.Length; i++)
            {
                if (BoardArray[i] == Player.Marks.None)
                {
                    indexList.Add(i);
                }
            }

            //indexList.ForEach(i => Debug.Log(i));

            return indexList;
        }
        
        public int GetEmptyCount()
        {
            return BoardArray.Count(marks => marks == Player.Marks.None);
        }

        private bool CheckTie()
        {
            return GetEmptyCount() == 0;
        }

        public int CheckWinner(Player.Marks mark)
        {
            var won = true;

            #region Horizontal
            
            for (var h = 0; h < BoardDimension; h++)
            {
                won = true;
                for (var w = 0; w < BoardDimension; w++)
                {
                    var index = w + (BoardDimension * h); // 0 1 2 // 3 4 5 // 6 7 8
                    if (BoardArray[index] != mark)
                    {
                        won = false;
                        break;
                    }
                }
                if (won) return 1;
            }

            #endregion
            
            #region Vertical

            for (var w = 0; w < BoardDimension; w++)
            {
                won = true;
                for (var h = 0; h < BoardDimension; h++)
                {
                    var index = w + (BoardDimension * h); // 0 3 6 // 1 4 7 // 2 5 8
                    if (BoardArray[index] != mark)
                    {
                        won = false;
                        break;
                    }
                }
                if (won) return 1;
            }

            #endregion

            #region Diagonal Right to Left

            var rightMost = BoardDimension - 1;
            won = true;
            for (var w = 1; w <= BoardDimension; w++)
            {
                var index = rightMost * w; // 2 4 6
                if (BoardArray[index] != mark)
                {
                    won = false;
                    break;
                }
            }
            if (won) return 1;

            #endregion

            #region Diagonal Left to Right
            
            var multiplier = ((BoardDimension * BoardDimension) - 1) / (BoardDimension - 1); 
            won = true;
            for (var h = 0; h < BoardDimension; h++)
            {
                var index = multiplier * h; // 0 4 8
                if (BoardArray[index] != mark)
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