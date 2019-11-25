﻿using UnityEngine;

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
                    _veggie = basket.Picked.Dequeue();
            }
            else
            {
                basket.Picked.Enqueue(_veggie);
                _veggie = null;
            }
        }
    }
}