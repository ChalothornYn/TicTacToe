using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class Board : MonoBehaviour
    {
        public int boardHigh; 
        public int boardWidth;

        public int markCount = 0;
        
        public LayerMask boxLayerMask;
        
        private Box[] _boxes;
        private Player.Marks[] _boardArray;

        private void Start()
        {
            _boardArray = new Player.Marks[boardHigh * boardWidth];
            _boxes = GetComponentsInChildren<Box>();
        }

        public void SetMask(Player player)
        {
            var pos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(pos, boxLayerMask);

            if(hit == null) return;
            
            Debug.Log($"<color=red>{hit.name} : {player.mark}</color>");
            
            var box = hit.GetComponent<Box>();
            
            box.Set(player.mark, player.color);
            box.SetCollider(false); // Can't be press anymore
            
            _boardArray[box.index] = player.mark;
            markCount++;
        }

        public void ResetMask()
        {
            foreach (var box in _boxes)
            {
                box.ResetMark();
            }
        }
    }
}