using SaladChef.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SaladChef.Core
{
    // <summary>
    /// Track the time for customer (waiting) and player (time left to cook)
    /// </summary>
    public class Timer : MonoBehaviour
    {
        ///<summary>///
        /// progress bar image for timer
        ///</summary>///
        [SerializeField] private Image _image = null;

        ///<summary>///
        ///stores the original wating time, used for progress basr
        ///</summary>///
        [SerializeField] private float _waitingTime = 0;

        ///<summary>///
        ///time left at the moment
        ///</summary>///
        private float _timeLeft = 0;

        ///<summary>///
        ///event invoked when the time runs out
        ///</summary>///
        public OnTimeOver onTimeOver;

        public float TimeLeft { get => _timeLeft; }
        public float WaitingTIme { get => _waitingTime; }

        private void Start()
        {
            if(_image == null)
                throw new System.Exception("Cannot find waiting timer image...");

            onTimeOver = new OnTimeOver();
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            _image.fillAmount = _timeLeft / _waitingTime;

            if(_timeLeft < 0)
                onTimeOver.Invoke();
        }

        public void SetWatingTime(float waitingTime)
        {
            _waitingTime = waitingTime;
            if(_waitingTime <= 0)
                throw new System.Exception("Time allocated not set...");

            _timeLeft = _waitingTime;
        }
    }
}
