using UnityEngine;

namespace SaladChef.Core
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// time for which the game will lasts
        /// </summary>
        [SerializeField] private float _time = 300f;

        private void OnEnable()
        {
            var timer = GetComponent<Timer>();
            if(timer == null)    
                throw new System.Exception("Cannot find Timer...");
            timer.SetWatingTime(_time);
        }
    }
}