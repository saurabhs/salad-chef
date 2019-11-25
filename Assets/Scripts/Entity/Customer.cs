using UnityEngine;

namespace SaladChef.Entity
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private string _name = "Customer";

        public string Name => _name;

        private void OnEnable()
        {
            //set random colour
            GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        }
    }
}