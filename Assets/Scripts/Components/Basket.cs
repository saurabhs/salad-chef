using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private Stack<Vegetable> _picked = new Stack<Vegetable>();
        public Stack<Vegetable> Picked => _picked;

        private void Start()
        {
            var vegPicker = GetComponent<VegetablePicker>();
            if(vegPicker == null)
                throw new System.Exception("Cannot find VegetablePicker...");
            vegPicker.onVegetablePicked.AddListener(OnPicked);
        }

        private void OnPicked(GameObject veggie)
        {
            var vegetable = veggie.GetComponent<Vegetable>();
            if(vegetable != null && _picked.Count < 2 && !_picked.Contains(vegetable))
            {
                _picked.Push(vegetable);
            }
        }
    }
}