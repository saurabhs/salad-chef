using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

namespace SaladChef.Globals
{
    public class Leaderboard : MonoBehaviour
    {
        /// <summary>
        /// list of top score, sorted
        /// </summary>
        [SerializeField] private List<int> _scores = new List<int>();

        /// <summary>
        /// max no. of score to be shown in leaderboard
        /// </summary>
        [SerializeField] private int _leaderboardSize = 10;

        /// <summary>
        /// ref to leaderboard UI
        /// </summary>
        [SerializeField] private TextMeshProUGUI _leaderboardUI = null;

        /// <summary>
        /// leaderboard file name
        /// </summary>
        private const string _filename = @"Assets/Resources/leaderboard.txt";

        public List<int> Score => _scores;

        private void OnEnable()
        {
            ReadLeaderboardScoreFromFile();

            AddToLeaderboard(PlayerPrefs.GetInt("Player1Score"));
            AddToLeaderboard(PlayerPrefs.GetInt("Player2Score"));
        }

        private void OnDisable()
        {
            _scores.Clear();
        }

        private void ReadLeaderboardScoreFromFile()
        {
            if (!File.Exists(_filename))
                File.Create(_filename);

            var line = string.Empty;
            // Read the file and display it line by line.  
            var file = new System.IO.StreamReader(_filename);
            while ((line = file.ReadLine()) != null)
            {
                _scores.Add(System.Int32.Parse(line));
            }
            file.Close();

            _scores.Sort((a, b) => -1 * a.CompareTo(b));
        }

        private void WriteLeaderboardScoreToFile()
        {
            var result = new StringBuilder();
            foreach (var score in _scores)
            {
                result.Append($"{score}\n");
            }
            File.WriteAllText(_filename, result.ToString());
        }

        private void AddToLeaderboard(int score)
        {
            if (_scores.Count == 0)
            {
                _scores.Add(score);
                return;
            }

            if (_scores.Count == _leaderboardSize)
            {
                if (score > _scores[_leaderboardSize - 1])
                    _scores[_leaderboardSize - 1] = score;
            }
            else
                _scores.Add(score);

            _scores.Sort((a, b) => -1 * a.CompareTo(b));
            WriteLeaderboardScoreToFile();
            UpdateUI();
        }

        private void UpdateUI()
        {
            var result = new StringBuilder();
            foreach (var score in _scores)
            {
                result.Append($"{score}\n");
            }
            _leaderboardUI.text = $"LEADERBOARD\n\n{result.ToString()}";
        }
    }
}