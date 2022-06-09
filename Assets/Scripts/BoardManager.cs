using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe
{
    public class BoardManager : MonoBehaviour
    {
        [Space] public int boardDimension;

        [Space] public LayerMask boxLayerMask;

        private readonly Dictionary<int, Box> _boxes = new Dictionary<int, Box>();

        public Board Board;

        public void Initialize()
        {
            //boardArray = new Player.Marks[boardDimension * boardDimension];
            
            var boxes = GetComponentsInChildren<Box>();

            foreach (var box in boxes)
            {
                _boxes.Add(box.index, box);
            }

            Board = new Board(new Player.Marks[boardDimension * boardDimension], boardDimension, _boxes);
        }
        
        public bool PlayerSetMask(Player player)
        {
            var pos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(pos, boxLayerMask);

            if(hit == null) return false;
            
            Debug.Log($"<color=red>{hit.name} : {player.mark}</color>");
            
            var box = hit.GetComponent<Box>();
            
            Board.SetMark(box.index, player);
            
            return true;
        }
        
        public void CPUSetMark(Cpu cpu)
        {
            cpu.CalculateBestMove(Board);
        }
    }
}