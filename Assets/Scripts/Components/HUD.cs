using TMPro;
using UnityEngine;

namespace SaladChef.Core
{
    public class HUD : MonoBehaviour
    {
        ///<summary>///
        /// player's score
        ///</summary>///
        [SerializeField] private float _score = 0;

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

        ///<summary>///
        /// for displaying score for player2
        ///</summary>///
        [SerializeField] private bool _isPlayer2 = false;

        ///<summary>///
        /// for displaying score for player2
        ///</summary>///
        private string _scoreFormat = string.Empty;

        ///<summary>///
        /// for displaying score for player2
        private string _timeFormat = string.Empty;

        public float Score => _score;

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
            _scoreFormat = _isPlayer2 ? "{0} SCORE" : "SCORE {0}";
            _timeFormat = _isPlayer2 ? "{0} TIMER" : "TIME {0}";

            _scoreText.text = string.Format(_scoreFormat, 0);
            _timerText.text = string.Format(_timeFormat, 0);
        }

        public void UpdateScore(float score, bool bonus)
        {
            _score += score + (bonus ? 100 : 0);
            _scoreText.text = string.Format(_scoreFormat, _score.ToString("0"));
        }

        public void UpdateScore(float multiplier)
        {
            _score *= multiplier;
            _scoreText.text = string.Format(_scoreFormat, _score.ToString("0"));
        }

        private void UpdateTimer()
        {
            _timerText.text = $"TIME {System.TimeSpan.FromSeconds(_timer.TimeLeft).ToString(@"mm\:ss")}";
            _timerText.text = string.Format(_timeFormat, System.TimeSpan.FromSeconds(_timer.TimeLeft).ToString(@"mm\:ss"));
        }
    }
}