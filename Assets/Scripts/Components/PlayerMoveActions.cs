using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class PlayerMoveActions : MonoBehaviour
    {
        /// <summary>
        /// saves the position of customers on the table
        /// </summary>
        [SerializeField] private List<Transform> customers;

        /// <summary>
        /// saves the position of kitchen/chopping board
        /// </summary>
        [SerializeField] private Transform kitchen;

        /// <summary>
        /// ref to veggies store
        /// </summary>
        private VegetableStore _store = null;

        /// <summary>
        /// player current state
        /// </summary>
        private State _state;

        private void OnEnable()
        {
            _state = GetComponent<State>();
            if(_state == null)
                throw new System.Exception("Cannot find PlayerState component...");

            var input = GetComponent<Input>();
            if (input == null)
                throw new System.Exception("Cannot find Input component...");
            SubscribeToInputEvents(input);

            _store = FindObjectOfType<VegetableStore>();
            if (_store == null)
                throw new System.Exception("Cannot find Vegetable Store...");
        }

        private void SubscribeToInputEvents(Input input)
        {
            input.moveToTable.AddListener(MoveToTable);
            input.moveToKitchen.AddListener(MoveToKitchen);
            input.chopVegeable.AddListener(Chop);
            input.serveSalad.AddListener(Serve);
        }

        private void MoveToTable(int index)
        {
            print("move to table");
            var move = GetComponent<Move>();
            move.target = _store.Store[index].transform.position;
            move.ActivateMove(Enums.EState.Table);
        }

        private void MoveToKitchen()
        {
            print("move to kitchen");
            var move = GetComponent<Move>();
            move.target = kitchen.position;
            move.ActivateMove(Enums.EState.Kitchen);
        }

        private void Chop()
        {
            if(_state.CurrentState != Enums.EState.Kitchen)
                return;

            var basket = GetComponent<Basket>();
            if(basket == null)
                throw new System.Exception("Cannot find Basket...");
                
            //empty basket
            if(basket.Picked.Count == 0)
                return;

            print("start chop");

            var chopping = GetComponent<ChopVegetable>();
            if(chopping == null)
                throw new System.Exception("Cannot find Chopping component...");

            chopping.ActivateChopping(basket.Picked.Dequeue());
        }

        private void Serve(int index)
        {
            if(_state.CurrentState != Enums.EState.Kitchen)
                return;

            print("move to serve");

            var move = GetComponent<Move>();
            move.target = customers[index].position;
            move.ActivateMove(Enums.EState.Customer);
        }
    }
}