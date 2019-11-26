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

        [SerializeField] private GameObject _levelOverUI = null;

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
                Invoke("InitiateGameOver", 1f);
            }
        }

        private void InitiateGameOver()
        {
            Time.timeScale = 0;
            _levelOverUI.SetActive(false);
        }
    }
}