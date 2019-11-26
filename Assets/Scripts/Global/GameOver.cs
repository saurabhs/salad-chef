using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SaladChef.Globals
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI player1;

        [SerializeField] private TextMeshProUGUI player2;
        
        [SerializeField] private Button restart;
        
        private void OnEnable()
        {
            player1.text = $"PLAYER1 {PlayerPrefs.GetInt("Player1Score")}";
            player2.text = $"{PlayerPrefs.GetInt("Player2Score")} PLAYER2";

            restart.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene("Main"));
        }

        private void OnDisable()
        {
            restart.onClick.RemoveAllListeners();
        }
    }
}