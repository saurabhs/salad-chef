using SaladChef.Core;
using UnityEngine;

namespace SaladChef.Powerups
{
    public class SpeedBoost : Powerup
    {
        /// <summary>
        /// amount by which the speed will be increased
        /// </summary>
        [SerializeField] private float _boost = 2f;

        /// <summary>
        /// duration for which the speed will be increased
        /// </summary>
        [SerializeField] private float _duration = 3f;

        protected override void Execute(GameObject player)
        {
            _owner = player;
            _owner .GetComponent<Move>().UpdateSpeed(_boost);

            gameObject.SetActive(false);

            Invoke("DisablePowerup", _duration);
        }

        private void DisablePowerup()
        {
            _owner.GetComponent<Move>().UpdateSpeed(-_boost);
            _owner = null;
        }
    }
}