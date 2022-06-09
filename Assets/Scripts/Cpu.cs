using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace TicTacToe
{
    public class Cpu
    {
        private readonly Player _cpu;
        private readonly Player _player;

        public Cpu(Player cpu, Player player)
        {
            _cpu = cpu;
            _player = player;
        }

        public void CalculateBestMove(Board board)
        {
            var bestScore = int.MinValue;
            var bestMove = 0;
            foreach (var index in board.GetEmptyBoxIndex())
            {
                board.SetMarkNotVisualize(index, _cpu.mark);
                var score = MiniMax(board, 0, true);
                board.SetMarkNotVisualize(index, Player.Marks.None);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = index;
                }
            }
            board.SetMark(bestMove, _cpu);
        }

        public int MiniMax(Board board, int depth, bool maximizingPlayer)
        {
            var checkPlayer = maximizingPlayer ? _cpu.mark : _player.mark;
            var result = board.CheckWinner(checkPlayer);
            Debug.Log($"maximizingPlayer : {maximizingPlayer}, check : {checkPlayer}, result : {result}");
            //var result = board.CheckWinner(maximizingPlayer ? _cpu.mark : _player.mark);
            if (result >= 0)
            {
                return result;
            }
            
            if (maximizingPlayer)
            {
                var bestScore = int.MinValue;
                foreach (var idx in board.GetEmptyBoxIndex())
                {
                    board.SetMarkNotVisualize(idx, _cpu.mark);
                    var score = MiniMax(board, depth + 1, false);
                    board.SetMarkNotVisualize(idx, Player.Marks.None);
                    bestScore = math.max(score, bestScore);
                }
            
                return bestScore;
            }
            else
            {
                var bestScore = int.MaxValue;
                foreach (var idx in board.GetEmptyBoxIndex())
                {
                    board.SetMarkNotVisualize(idx, _player.mark);
                    var score = MiniMax(board, depth + 1, true);
                    board.SetMarkNotVisualize(idx, Player.Marks.None);
                    bestScore = math.min(score, bestScore);
                }
            
                return bestScore;
            }
        }

        // private static int EvaluateNode(Player.Marks[] board, Player.Marks mark)
        // {
        //     var won = true;
        //
        //     var boardDimension = (int) math.rsqrt(board.Length);
        //
        //     #region Horizontal
        //     
        //     for (var h = 0; h < boardDimension; h++)
        //     {
        //         won = true;
        //         for (var w = 0; w < boardDimension; w++)
        //         {
        //             var index = w + (boardDimension * h); // 0 1 2 // 3 4 5 // 6 7 8
        //             if (board[index] != mark)
        //             {
        //                 won = false;
        //                 break;
        //             }
        //         }
        //         if (won) return 1;
        //     }
        //
        //     #endregion
        //     
        //     #region Vertical
        //
        //     for (var w = 0; w < boardDimension; w++)
        //     {
        //         won = true;
        //         for (var h = 0; h < boardDimension; h++)
        //         {
        //             var index = w + (boardDimension * h); // 0 3 6 // 1 4 7 // 2 5 8
        //             if (board[index] != mark)
        //             {
        //                 won = false;
        //                 break;
        //             }
        //         }
        //         if (won) return 1;
        //     }
        //
        //     #endregion
        //
        //     #region Diagonal Right to Left
        //
        //     var rightMost = boardDimension - 1;
        //     won = true;
        //     for (var w = 1; w <= boardDimension; w++)
        //     {
        //         var index = rightMost * w; // 2 4 6
        //         if (board[index] != mark)
        //         {
        //             won = false;
        //             break;
        //         }
        //     }
        //     if (won) return 1;
        //
        //     #endregion
        //
        //     #region Diagonal Left to Right
        //     
        //     var multiplier = ((boardDimension * boardDimension) - 1) / (boardDimension - 1); 
        //     won = true;
        //     for (var h = 0; h < boardDimension; h++)
        //     {
        //         var index = multiplier * h; // 0 4 8
        //         if (board[index] != mark)
        //         {
        //             won = false;
        //             break;
        //         }
        //     }
        //     if (won) return 1;
        //     
        //     #endregion
        //
        //     if (EmptyBoxCount(board) == 0) return 0;
        //     
        //     return -1; // Game not over yet, continue playing
        // }
        //
        // private static int EmptyBoxCount(Player.Marks[] board)
        // {
        //     return board.Count(m => m == Player.Marks.None);
        // }
    }
}