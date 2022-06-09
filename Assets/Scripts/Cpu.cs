using System;
using System.Linq;
using Unity.Mathematics;

namespace TicTacToe
{
    // public class Cpu
    // {
    //     private readonly Player _cpu;
    //
    //     public Cpu(Player cpu)
    //     {
    //         _cpu = cpu;
    //     }
    //
    //     public int ManiMax(Player.Marks[] board, int depth, bool maximizingPlayer)
    //     {
    //         if (depth == 0)
    //             return EvaluateNode(board, _cpu.mark);
    //
    //         if (maximizingPlayer)
    //         {
    //             var bestScore = int.MinValue;
    //             for (int index = 0; index < board; index++)
    //             {
    //                 
    //             }
    //         }
    //         else
    //         {
    //         }
    //     }
    //     
    //     private static int EvaluateNode(Player.Marks[] board, Player.Marks mark)
    //     {
    //         var won = true;
    //
    //         var boardDimension = (int) math.rsqrt(board.Length);
    //
    //         #region Horizontal
    //         
    //         for (var h = 0; h < boardDimension; h++)
    //         {
    //             won = true;
    //             for (var w = 0; w < boardDimension; w++)
    //             {
    //                 var index = w + (boardDimension * h); // 0 1 2 // 3 4 5 // 6 7 8
    //                 if (board[index] != mark)
    //                 {
    //                     won = false;
    //                     break;
    //                 }
    //             }
    //             if (won) return 1;
    //         }
    //
    //         #endregion
    //         
    //         #region Vertical
    //
    //         for (var w = 0; w < boardDimension; w++)
    //         {
    //             won = true;
    //             for (var h = 0; h < boardDimension; h++)
    //             {
    //                 var index = w + (boardDimension * h); // 0 3 6 // 1 4 7 // 2 5 8
    //                 if (board[index] != mark)
    //                 {
    //                     won = false;
    //                     break;
    //                 }
    //             }
    //             if (won) return 1;
    //         }
    //
    //         #endregion
    //
    //         #region Diagonal Right to Left
    //
    //         var rightMost = boardDimension - 1;
    //         won = true;
    //         for (var w = 1; w <= boardDimension; w++)
    //         {
    //             var index = rightMost * w; // 2 4 6
    //             if (board[index] != mark)
    //             {
    //                 won = false;
    //                 break;
    //             }
    //         }
    //         if (won) return 1;
    //
    //         #endregion
    //
    //         #region Diagonal Left to Right
    //         
    //         var multiplier = ((boardDimension * boardDimension) - 1) / (boardDimension - 1); 
    //         won = true;
    //         for (var h = 0; h < boardDimension; h++)
    //         {
    //             var index = multiplier * h; // 0 4 8
    //             if (board[index] != mark)
    //             {
    //                 won = false;
    //                 break;
    //             }
    //         }
    //         if (won) return 1;
    //         
    //         #endregion
    //
    //         if (EmptyBoxCount(board) == 0) return 0;
    //         
    //         return -1; // Game not over yet, continue playing
    //     }
    //
    //     private static int EmptyBoxCount(Player.Marks[] board)
    //     {
    //         return board.Count(m => m == Player.Marks.None);
    //     }
    // }
}