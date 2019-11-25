using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class VegetablePicker : MonoBehaviour
    {
        public OnVegetablePicked onVegetablePicked = null;

        private void OnEnable()
        {
            onVegetablePicked = new OnVegetablePicked(); 
        }

        private void OnDisable()
        {
            onVegetablePicked.RemoveAllListeners();
            onVegetablePicked = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag.Equals("Vegetable"))
                onVegetablePicked.Invoke(other.gameObject);
        }
    }
}