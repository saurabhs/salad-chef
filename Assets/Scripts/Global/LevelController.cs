using System.Collections.Generic;
using SaladChef.Core;
using UnityEngine;

namespace SaladChef.Globals
{
    public class LevelController : MonoBehaviour
    {
        /// <summary>
        /// list of players level controller will track for game over
        /// </summary>
        [SerializeField] private List<GameObject> _players = new List<GameObject>();

        int count = 0;

        private void OnEnable()
        {
            foreach(var player in _players)
            {
                player.GetComponent<Timer>().onTimeOver.AddListener(OnTimeOver);
            }
        }

        private void OnTimeOver()
        {
            count++;
            if(count == _players.Count)
            {
                InitiateGameOver();
            }
        }

        private void InitiateGameOver()
        {
            for(var i = 0; i < _players.Count; i++)
            {
                PlayerPrefs.SetInt($"Player{i + 1}Score", (int)_players[i].GetComponent<HUD>().Score);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}