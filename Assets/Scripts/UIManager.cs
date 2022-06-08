using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class UIManager : MonoBehaviour
    {
        [Header("Mode : ")] 
        public GameObject modeObj;
        
        [Header("Turn UI : ")]
        public TextMeshProUGUI playerTurn;
        public TextMeshProUGUI playerMark;
        public GameObject turnObj;

        [Header("Result UI :")] 
        public TextMeshProUGUI result;
        public GameObject resultObj;
        public GameObject tieObj;

        public static UIManager Instance { get; private set; }
        
        private void Awake() 
        { 
            // If there is an instance, and it's not me, delete myself.
    
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        public void ResetGame()
        {
            modeObj.SetActive(true);
            
            turnObj.SetActive(false);
            
            resultObj.SetActive(false);
            tieObj.SetActive(false);
        }

        public void StartGame()
        {
            modeObj.SetActive(false);
            
            turnObj.SetActive(true);

        }

        public void ShowTurn(Player player)
        {
            playerTurn.text = player.name;
            playerTurn.color = player.color;
            playerMark.text = "'" + player.mark.ToString().ToUpper() + "'";
        }
        
        public void GameOver(Player? winner)
        {
            turnObj.SetActive(false);
            
            if (winner == null)
            {
                tieObj.SetActive(true);
            }
            else
            {
                result.text = winner.Value.name;
                result.color = winner.Value.color;
                resultObj.SetActive(true);
            }
        }
    }
}