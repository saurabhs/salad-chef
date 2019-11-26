using UnityEngine;

namespace SaladChef.Powerups
{
    public abstract class Powerup : MonoBehaviour
    {
        /// <summary>
        /// owner of the object, used when picking powerup
        /// </summary>
        protected GameObject _owner = null;

        /// <summary>
        /// duration for which the powerup is active
        /// </summary>
        protected float _lifetime = 5f;

        public float Lifetime => _lifetime;

        public void Owner(GameObject owner) => _owner = owner;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals(_owner.tag))
            {
                Execute(other.gameObject);
            }
        }

        protected abstract void Execute(GameObject player);
    }
}