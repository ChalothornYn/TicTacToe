using System;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    private Collider2D[] _grid = new Collider2D[9];

    private void Start()
    {
        _grid = GetComponentsInChildren<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            
            foreach (var col in _grid)
            {
                if (col.OverlapPoint(mousePosition))
                {
                    Debug.Log($"<color=red>{col.name}</color>");
                    break;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        throw new NotImplementedException();
    }
}