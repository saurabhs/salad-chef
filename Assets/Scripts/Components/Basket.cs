using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private Queue<Vegetable> _picked = new Queue<Vegetable>();
        public Queue<Vegetable> Picked => _picked;

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
                _picked.Enqueue(vegetable);
            }
        }
    }
}