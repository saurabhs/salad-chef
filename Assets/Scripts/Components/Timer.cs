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

        private int _waitMultiplier = 1;

        public float TimeLeft { get => _timeLeft; }
        public float WaitingTIme { get => _waitingTime; }

        private void OnEnable()
        {
            print("Timer::Start");

            if(_image == null)
                throw new System.Exception("Cannot find waiting timer image...");

            onTimeOver = new OnTimeOver();
        }

        private void OnDisable() => onTimeOver.RemoveAllListeners();

        public void SetWatingTime(float waitingTime)
        {
            print("Timer::SetWaitingTime -> " + waitingTime);
            _waitingTime = waitingTime;
            if(_waitingTime <= 0)
                throw new System.Exception("Time allocated not set...");

            _timeLeft = _waitingTime;

            InvokeRepeating("CountdownTimer", 0, Time.deltaTime);
        }

        private void CountdownTimer()
        {
            _timeLeft -= Time.deltaTime * _waitMultiplier;
            _image.fillAmount = _timeLeft / _waitingTime;

            if(_timeLeft < 0)
            {
                onTimeOver.Invoke();
                CancelInvoke("CountdownTimer");
            }
        }

        public void UpdateWaitMultiplier() => _waitMultiplier = 2;
    }
}
