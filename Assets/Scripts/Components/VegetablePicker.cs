using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class VegetablePicker : MonoBehaviour
    {
        public OnVegetablePicked onVegetablePicked = null;

        private void Start()
        {
            print("VegetablePicker::Start");
            onVegetablePicked = new OnVegetablePicked(); 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag.Equals("Vegetable"))
            {
                print("Veg name " + other.name);
                onVegetablePicked.Invoke(other.gameObject);
            }
        }
    }
}