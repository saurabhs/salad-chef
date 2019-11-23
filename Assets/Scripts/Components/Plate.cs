using UnityEngine;

namespace SaladChef.Core
{
    public class Plate : MonoBehaviour
    {
        /// <summary>
        /// current vegetable in the plate
        /// null is empty plate
        /// <summary>
        private Vegetable _veggie = null;

        public Vegetable Veggie => _veggie;

        private void Start()
        {
            var input = GetComponent<Input>();
            if(input == null)
                throw new System.Exception("Cannot find Input...");
            input.onUsePlate.AddListener(UsePlate);
        }

        public void UsePlate()
        {
            var basket = GetComponent<Basket>();
            if (basket == null)
                throw new System.Exception("Cannot find Basket...");

            if (_veggie == null)
            {
                if (basket.Picked.Count > 0)
                {
                    _veggie = basket.Picked.Dequeue();
                    print($"Moved {_veggie.Name} to plate");
                }
            }
            else
            {
                print($"Moved {_veggie.Name} to basket");
                basket.Picked.Enqueue(_veggie);
                _veggie = null;
            }
        }
    }
}