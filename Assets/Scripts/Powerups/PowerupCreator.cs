using UnityEngine;

namespace SaladChef.Powerups
{
    public class PowerupCreator : MonoBehaviour
    {
        /// <summary>
        /// list of powerup avaiable
        /// </summary>
        [SerializeField] private Powerup[] _powers;

        public void CreatePowerup(GameObject owner, Vector3 position)
        {
            var index = UnityEngine.Random.Range(0, _powers.Length);
            var powerup = Instantiate(_powers[index].gameObject, position, Quaternion.identity) as GameObject;
            powerup.GetComponent<Powerup>().Owner(owner);
            powerup.SetActive(true);

            GameObject.Destroy(powerup, powerup.GetComponent<Powerup>().Lifetime);
        }
    }
}