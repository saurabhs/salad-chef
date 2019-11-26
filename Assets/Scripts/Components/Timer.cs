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
        ///stores the original wating time, used for progress basr
        ///</summary>///
        private float _waitingTime = 0;

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
            // print("Timer OnEnable Called -> " + gameObject.name);

            onTimeOver = new OnTimeOver();
        }

        private void OnDisable()
        {
            onTimeOver.RemoveAllListeners();
        }

        // private void OnGUI()
        // {
        //     if(GUI.Button(new Rect(200, 200, 200, 200), "Restart"))
        //     {
        //         UnityEngine.SceneManagement.SceneManager.LoadScene("Main_old");
        //     }
        // }

        public void AddToTimeLeft(float change) => _timeLeft += change;

        public void SetWatingTime(float waitingTime)
        {
            _waitingTime = waitingTime;
            if (_waitingTime <= 0)
                throw new System.Exception("Time allocated not set...");
            _timeLeft = _waitingTime;
            InvokeRepeating("CountdownTimer", 0, Time.deltaTime);
        }

        private void CountdownTimer()
        {
            _timeLeft -= Time.deltaTime * _waitMultiplier;
            if (_timeLeft < 0)
            {
                onTimeOver.Invoke();
                CancelInvoke("CountdownTimer");
            }
        }

        public void UpdateWaitMultiplier() => _waitMultiplier = 2;
    }
}