using SaladChef.Core;
using UnityEngine;

namespace SaladChef.Powerups
{
    public class TimeBoost : Powerup
    {
        /// <summary>
        /// durtaion by which player's total time is increased
        /// </summary>
        [SerializeField] private float _boost = 10f;

        protected override void Execute(GameObject player)
        {
            var timer = player.GetComponent<Timer>();
            timer.AddToTimeLeft(_boost);
            gameObject.SetActive(false);
        }
    }
}