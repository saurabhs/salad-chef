using UnityEngine;
using UnityEngine.UI;

namespace SaladChef.Core
{
    public class Plate : MonoBehaviour
    {
        /// <summary>
        /// current vegetable in the plate
        /// null is empty plate
        /// </summary>
        private Vegetable _veggie = null;

        /// <summary>
        /// vegetable sprite on plate
        /// </summary>
        [SerializeField] private SpriteRenderer _sprite = null;

        public Vegetable Veggie => _veggie;

        private void OnEnable()
        {
            var input = GetComponent<Input>();
            if (input == null)
                throw new System.Exception("Cannot find Input...");
            input.onUsePlate.AddListener(UsePlate);
        }

        public void UsePlate()
        {
            var state = GetComponent<State>();
            if (state == null || state.CurrentState != Enums.EState.Kitchen)
                return;

            var basket = GetComponent<Basket>();
            if (basket == null)
                throw new System.Exception("Cannot find Basket...");

            if (_veggie == null)
            {
                if (basket.Picked.Count > 0)
                {
                    _veggie = basket.Picked.Dequeue();
                    _sprite.sprite = _veggie.GetComponent<SpriteRenderer>().sprite;
                }
            }
            else
            {
                basket.Picked.Enqueue(_veggie);
                _sprite.sprite = null;
                _veggie = null;
            }
        }
    }
}