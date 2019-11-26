using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaladChef.Core
{
    [RequireComponent(typeof(BasketUI))]
    public class Basket : MonoBehaviour
    {
        /// <summary>
        /// vegetables picked up by the player
        /// <summary>
        [SerializeField] private Queue<Vegetable> _picked = new Queue<Vegetable>();

        private void OnEnable()
        {
            var vegPicker = GetComponent<VegetablePicker>();
            if(vegPicker == null)
                throw new System.Exception("Cannot find VegetablePicker...");
            vegPicker.onVegetablePicked.AddListener(OnPicked);
        }

        private void OnDisable() => _picked.Clear();

        private void OnPicked(GameObject veggie)
        {
            var vegetable = veggie.GetComponent<Vegetable>();
            if(vegetable != null && _picked.Count < 2 && !_picked.Contains(vegetable))
            {
                AddToBasket(vegetable);
            }
        }

        public int Size => _picked.Count;

        public void AddToBasket(Vegetable veggie)
        {
            _picked.Enqueue(veggie);
            GetComponent<BasketUI>().OnAdd(veggie, _picked.Count);
        }

        public Vegetable TakeFromBasket()
        {
            var veggie = _picked.Dequeue();
            GetComponent<BasketUI>().OnRemove(veggie, _picked.Count);
            return veggie;
        }
    }
}