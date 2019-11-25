using TMPro;
using UnityEngine;

namespace SaladChef.Core
{
    public class HUD : MonoBehaviour
    {
        ///<summary>///
        /// player's score
        ///</summary>///
        private float _score = 0;

        ///<summary>///
        ///score text comp
        ///</summary>///
        [SerializeField] private TextMeshProUGUI _scoreText = null;

        ///<summary>///
        ///time text comp
        ///</summary>///
        [SerializeField] private TextMeshProUGUI _timerText = null;

        ///<summary>///
        ///ref to timer component for updating timer text
        ///</summary>///
        private Timer _timer = null;

        private void OnEnable()
        {
            _timer = GetComponent<Timer>();
            if (_timer == null)
                throw new System.Exception("Cannot find Timer component...");

            if (_scoreText == null || _timerText == null)
                throw new System.Exception("Cannot find Text component...");

            InitHUD();

            InvokeRepeating("UpdateTimer", 0, 0.5f);
        }

        private void InitHUD()
        {
            _scoreText.text = $"SCORE 0";
            _timerText.text = $"TIMER 00:00";
        }

        public void UpdateScore(float score, bool bonus)
        {
            _score += score + (bonus ? 100 : 0);
            _scoreText.text = $"SCORE {_score.ToString()}";
        }

        private void UpdateTimer()
        {
            _timerText.text = $"TIMER {System.TimeSpan.FromSeconds(_timer.TimeLeft).ToString(@"mm\:ss")}";
        }
    }
}