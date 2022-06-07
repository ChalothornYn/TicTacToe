using System;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class Box : MonoBehaviour
    {
        public bool isMarked;
        public Player.Marks mark;
        private TextMeshProUGUI _textMeshProUGUI;
        private Collider2D _collider2D;

        public int index;

        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _collider2D = GetComponent<Collider2D>();

            index = transform.GetSiblingIndex();
        }

        public int SetMarkAs(Player.Marks msk, Color color)
        {
            mark = msk;
            _textMeshProUGUI.text = msk.ToString().ToUpper();
            _textMeshProUGUI.color = color;
            _collider2D.enabled = false;
            isMarked = true;

            return index;
        }

        public void ResetMark()
        {
            _textMeshProUGUI.text = "";
        }
    }
}