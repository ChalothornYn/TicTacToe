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

        public Board(Player.Marks[] boardArray, int boardDimension, Dictionary<int, Box> boxes)
        {
            BoardArray = boardArray;
            BoardDimension = boardDimension;
            Boxes = boxes;
        }

        public void ResetBoard()
        {
            BoardArray = new Player.Marks[BoardDimension * BoardDimension];

            foreach (var box in Boxes.Values)
            {
                box.ResetMark();
            }
        }

        public void SetMark(int index, Player player)
        {
            Boxes[index].Set(player.mark, player.color);
            BoardArray[index] = player.mark;
        }

        public void SetMarkNotVisualize(int index, Player.Marks mark)
        {
            BoardArray[index] = mark;
        }

        public void PrintBoard()
        {
            for (var i = 0; i < BoardDimension; i++)
            {
                Debug.Log(
                    $"{BoardArray[0 + 3 * i].ToString()} {BoardArray[1 + 3 * i].ToString()} {BoardArray[2 + 3 * i].ToString()}");
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

        public GameResult CheckWinner()
        {
            var haveWinner = true;

            Player.Marks currentMark;

            #region Horizontal

            for (var h = 0; h < BoardDimension; h++)
            {
                currentMark = BoardArray[BoardDimension * h];

                if (currentMark != Player.Marks.None)
                {
                    haveWinner = true;
                    for (var w = 0; w < BoardDimension; w++)
                    {
                        var index = w + (BoardDimension * h); // 0 1 2 // 3 4 5 // 6 7 8
                        if (BoardArray[index] != currentMark)
                        {
                            haveWinner = false;
                            break;
                        }
                    }

                    if (haveWinner) return new GameResult(GameResult.GameStatus.HaveWinner, currentMark);
                }
            }

            #endregion

            #region Vertical

            for (var w = 0; w < BoardDimension; w++)
            {
                currentMark = BoardArray[w];
                if (currentMark != Player.Marks.None)
                {
                    haveWinner = true;
                    for (var h = 0; h < BoardDimension; h++)
                    {
                        var index = w + (BoardDimension * h); // 0 3 6 // 1 4 7 // 2 5 8
                        if (BoardArray[index] != currentMark)
                        {
                            haveWinner = false;
                            break;
                        }
                    }
                    
                    if (haveWinner) return new GameResult(GameResult.GameStatus.HaveWinner, currentMark);
                }
            }

            #endregion
            
            #region Diagonal Right to Left
            
            var rightMostIdx = BoardDimension - 1;
            currentMark = BoardArray[rightMostIdx];
            if (currentMark != Player.Marks.None)
            {
                haveWinner = true;
                for (var w = 1; w <= BoardDimension; w++)
                {
                    var index = rightMostIdx * w; // 2 4 6
                    if (BoardArray[index] != currentMark)
                    {
                        haveWinner = false;
                        break;
                    }
                }

                if (haveWinner) return new GameResult(GameResult.GameStatus.HaveWinner, currentMark);
            }

            
            #endregion

            #region Diagonal Left to Right
            
            var multiplier = ((BoardDimension * BoardDimension) - 1) / (BoardDimension - 1);
            currentMark = BoardArray[0];
            if (currentMark != Player.Marks.None)
            {
                haveWinner = true;
                for (var h = 0; h < BoardDimension; h++)
                {
                    var index = multiplier * h; // 0 4 8
                    if (BoardArray[index] != currentMark)
                    {
                        haveWinner = false;
                        break;
                    }
                }
                
                if (haveWinner) return new GameResult(GameResult.GameStatus.HaveWinner, currentMark);
            }

            #endregion

            if (CheckTie()) return new GameResult(GameResult.GameStatus.Tie, Player.Marks.None);

            // Game not over yet, continue playing
            return new GameResult(GameResult.GameStatus.ContinuePlaying, Player.Marks.None);
        }
    }
}