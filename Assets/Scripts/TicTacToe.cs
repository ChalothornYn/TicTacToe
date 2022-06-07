using System;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    private Collider2D[] _grid = new Collider2D[9];

    [SerializeField] private string player1Symbol;
    [SerializeField] private string player2Symbol;

    [Space] [SerializeField] private bool playAsPlayer1 = true;

    [Space]
    [SerializeField] private int boardWidth;
    [SerializeField] private int boardHigh;

    private string[ , ] _board;

    private enum State
    {
        Rest,
        Player1Turn,
        Player2Turn,
        CheckResult,
    }

    private State _stateFlow = State.Rest;
    
    private void Start()
    {
        _grid = GetComponentsInChildren<Collider2D>();
        
        _board = new string[boardWidth, boardHigh];
        
        for (var i = 0; i < boardWidth; i++)
        {
            for (var j = 0; j < boardHigh; j++)
            {
                _board[i, j] = "";
            }
        }
    }

    private void Update()
    {
        switch (_stateFlow)
        {
            case State.Rest:
                // Choose Game Mode
                if(playAsPlayer1)
                    _stateFlow = State.Player1Turn;
                break;
            case State.Player1Turn:
                
                break;
            case State.Player2Turn:
                break;
            case State.CheckResult:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        


        // if (Input.GetMouseButton(0))
        // {
        //     Vector2 mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        //     
        //     foreach (var col in _grid)
        //     {
        //         if (col.OverlapPoint(mousePosition))
        //         {
        //             Debug.Log($"<color=red>{col.name}</color>");
        //             break;
        //         }
        //     }
        // }
    }
    
    

    private void OnMouseDown()
    {
        throw new NotImplementedException();
    }
}