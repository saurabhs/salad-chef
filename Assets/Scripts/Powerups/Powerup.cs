using UnityEngine;

namespace SaladChef.Powerups
{
    public abstract class Powerup : MonoBehaviour
    {
        protected GameObject _owner = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag.Equals("Player"))
            {
                Execute(other.gameObject);
            }
        }

        protected abstract void Execute(GameObject player);
    }
}