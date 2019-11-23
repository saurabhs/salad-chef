﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SaladChef.Core
{
    public class ServeOrder : MonoBehaviour
    {
        /*[SerializeField]*/ private List<Vegetable> _salad = new List<Vegetable>();
        public string Order {
            get {
                var sb = new StringBuilder();
                _salad.ForEach(obj => sb.Append($"{obj.Name}|"));
                return sb.ToString();
            }
        }

        private void Start()
        {
            var chop = GetComponent<ChopVegetable>();
            if (chop == null)
                throw new System.Exception("Cannot find ChopVegetable component....");
            chop.onVegetableChoppedComplete.AddListener(MoveChoppedVegetableToPlate);

            var input = GetComponent<Input>();
            if (input == null)
                throw new System.Exception("Cannot find Input component...");

            input.discard.AddListener(DiscardChoppedVegetable);
            input.serveSalad.AddListener(ServeSalad);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag.Equals("Customer"))
            {
                var customer = other.GetComponent<OrderReceiver>();
                customer.Receive(gameObject);
            }
        }

        private void ServeSalad(int index)
        {
            // throw new NotImplementedException();
        }

        private void MoveChoppedVegetableToPlate(Vegetable veggie)
        {
            print($"Adding chopped {veggie.name} to plate");
            _salad.Add(veggie);
        }

        private void DiscardChoppedVegetable()
        {
            //cannot discard an empty plate
            if (_salad.Count == 0)
                return;

            _salad.Clear();

            var hud = GetComponent<HUD>();
            if (hud == null)
                throw new System.Exception("Cannot find HD compnent...");

            //TODO: add proper scoring
            hud.UpdateScore(-50, false);
        }
    }
}