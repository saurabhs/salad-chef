using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class PlayerMoveActions : MonoBehaviour
    {
        /// <summary>
        /// saves the position of vegetables on the table
        /// </summary>
        [SerializeField] private List<Transform> veggies;

        /// <summary>
        /// saves the position of customers on the table
        /// </summary>
        [SerializeField] private List<Transform> customers;

        /// <summary>
        /// saves the position of kitchen/chopping board
        /// </summary>
        [SerializeField] private Transform kitchen;

        private void Start()
        {
            var input = GetComponent<Input>();
            if (input == null)
                throw new System.Exception("Cannot find Input component...");
            SubscribeToInputEvents(input);
        }

        private void SubscribeToInputEvents(Input input)
        {
            input.moveToTable.AddListener(MoveToTable);
            input.moveToKitchen.AddListener(MoveToKitchen);
            input.chopVegeable.AddListener(Chop);
            input.serveSalad.AddListener(Serve);
            input.discard.AddListener(Discard);
        }

        private void MoveToTable(int index)
        {
            print("move to table");
            var move = GetComponent<Move>();
            move.target = veggies[index].position;
            move.ActivateMove();
        }

        private void MoveToKitchen()
        {
            print("move to kitchen");
            var move = GetComponent<Move>();
            move.target = kitchen.position;
            move.ActivateMove();
        }

        private void Chop()
        {
            print("start chop");
            var chopping = GetComponent<ChopVegetable>();
            if(chopping == null)
                throw new System.Exception("Cannot find Chopping component...");

            var basket = GetComponent<Basket>();
            if(basket == null)
                throw new System.Exception("Cannot find Basket...");

            chopping.ActivateChopping(basket.Picked.Pop());
        }

        private void Serve(int index)
        {
            print("move to serve");
            var move = GetComponent<Move>();
            move.target = customers[index].position;
            move.ActivateMove();
        }

        private void Discard()
        {
            print("move to discard");
        }
    }
}