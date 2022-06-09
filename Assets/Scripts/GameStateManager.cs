﻿using System;
using TicTacToe.GameState;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace TicTacToe
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Player Setting : ")]
        public Player player1;
        public Player player2;

        private Player[] players;

        [Header("Mode : ")] 
        public bool goFirst = true;
        public bool cpuAsPlayer2 = true;

        public enum Level
        {
            Easy,
            Medium,
            Hard,
        }
        [Header("CPU Level : ")]
        public Level cpuLevel = Level.Easy;

        [Space] public BoardManager boardManager;

        public Player? Winner;

        // Possible State
        public readonly GameStart GameStart = new GameStart();
        public readonly Player1Turn Player1Turn = new Player1Turn();
        public readonly Player2Turn Player2Turn = new Player2Turn();
        public readonly GameOver GameOver = new GameOver();

        private GameBaseState _currentState;

        public GameResult GameResult = new GameResult(GameResult.GameStatus.ContinuePlaying, Player.Marks.None);

        private void Start()
        {
            players = new[] {player1, player2};
            
            boardManager.Initialize();
            
            _currentState = GameStart;
            
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState?.UpdateState(this);
        }

        public void ChangeState(GameBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        public void DetermineGameResult(GameBaseState nextState)
        {
            GameResult = boardManager.Board.CheckWinner();

            if (GameResult.Status == GameResult.GameStatus.ContinuePlaying)
            {
                ChangeState(nextState);
                return;
            }
            
            Winner = FindPlayerFromMark(players, GameResult.WinnerMark);
            
            ChangeState(GameOver);
        }
        
        private Player? FindPlayerFromMark(Player[] playersArray, Player.Marks? marks)
        {
            if (marks == null) return null;

            foreach (var player in playersArray)
            {
                if (player.mark == marks) return player;
            }
        
            return null;
        }
    }

    [Serializable]
    public struct Player
    {
        public enum Marks
        {
            None,
            X,
            O,
        }

        public string name;
        public Marks mark;
        public Color color;
    }

    public struct GameResult
    {
        public enum GameStatus
        {
            ContinuePlaying,   
            HaveWinner,
            Tie,
        }

        public GameStatus Status;
        public Player.Marks? WinnerMark;

        public GameResult(GameStatus status, Player.Marks? winnerMark)
        {
            Status = status;
            WinnerMark = winnerMark;
        }

        public void Reset()
        {
            Status = GameStatus.ContinuePlaying;
            WinnerMark = null;
        }
    }
}