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
                var score = MiniMax(board, 0, false);
                board.SetMarkNotVisualize(index, Player.Marks.None);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = index;
                }
            }
            board.SetMark(bestMove, _cpu);
        }

        public int MiniMaxScore(GameResult result)
        {
            if(result.Status == GameResult.GameStatus.Tie) return 0;

            if (result.WinnerMark == _cpu.mark) return 1;

            return -1;
        }

        public int MiniMax(Board board, int depth, bool maximizingPlayer)
        {
            var result = board.CheckWinner();
            
            if (result.Status != GameResult.GameStatus.ContinuePlaying)
            {
                return MiniMaxScore(result);
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
    }
}