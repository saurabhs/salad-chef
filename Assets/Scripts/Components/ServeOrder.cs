using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class ServeOrder : MonoBehaviour
    {
        [SerializeField] private List<Vegetable> salad = new List<Vegetable>();

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

        private void ServeSalad(int index)
        {
            throw new NotImplementedException();
        }

        private void MoveChoppedVegetableToPlate(Vegetable veggie)
        {
            print($"Adding chopped {veggie.name} to plate");
            salad.Add(veggie);
        }

        private void DiscardChoppedVegetable()
        {
            //cannot discard an empty plate
            if(salad.Count == 0)
                return;

            salad.Clear();

            var hud = GetComponent<HUD>();
            if (hud == null)
                throw new System.Exception("Cannot find HD compnent...");
            
            //TODO: add proper scoring
            hud.UpdateScore(-50, false);
        }
    }
}