using System;
using UnityEngine;

namespace SaladChef.Powerups
{
    public class PerformanceTracker : MonoBehaviour
    {
        /// <summary>
        /// no. of times player has served correct order
        /// </summary>
        private int _successCount = 0;

        /// <summary>
        /// min no. of correct orders before a powerup generation
        /// </summary>
        [SerializeField] private int _powerupThreshold = 3;

        /// <summary>
        /// location where powerup will be instantiated
        /// </summary>
        [SerializeField] private Transform location = null;

        /// <summary>
        /// gameobject which will instantiate powerup
        /// </summary>
        private PowerupCreator _powerup;

        private void OnEnable()
        {
            _successCount = 0;
            _powerup = FindObjectOfType<PowerupCreator>();
            if(_powerup == null)
                throw new System.Exception("Cannot find PowerupCreator...");
        }

        private void OnDisable()
        {
            _successCount = 0;
        }

        public void OnCorrectOrderServed()
        {
            _successCount++;
            if (_successCount >= _powerupThreshold)
            {
                _successCount = 0;
                _powerup.CreatePowerup(gameObject, location.position);
            }
        }
    }
}