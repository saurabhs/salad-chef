using UnityEngine;

namespace SaladChef.Core
{
    public class PlayerMoveActions : MonoBehaviour
    {
        /// <summary>
        /// saves the position of kitchen/chopping board
        /// </summary>
        [SerializeField] private Transform kitchen;

        /// <summary>
        /// ref to veggies store
        /// </summary>
        private VegetableStore _veggieStore = null;

        /// <summary>
        /// ref to customers
        /// </summary>
        private CustomerStore _customerStore = null;

        /// <summary>
        /// player current state
        /// </summary>
        private State _state;

        private void OnEnable()
        {
            _state = GetComponent<State>();
            if (_state == null)
                throw new System.Exception("Cannot find PlayerState component...");

            var input = GetComponent<Input>();
            if (input == null)
                throw new System.Exception("Cannot find Input component...");
            SubscribeToInputEvents(input);

            _veggieStore = FindObjectOfType<VegetableStore>();
            if (_veggieStore == null)
                throw new System.Exception("Cannot find Vegetable Store...");

            _customerStore = FindObjectOfType<CustomerStore>();
            if (_customerStore == null)
                throw new System.Exception("Cannot find Cusotmer Store...");
        }

        private void SubscribeToInputEvents(Input input)
        {
            input.moveToTable.AddListener(MoveToTable);
            input.moveToKitchen.AddListener(MoveToKitchen);
            input.chopVegeable.AddListener(Chop);
            input.serveSalad.AddListener(Serve);
            input.onPickupPowerup.AddListener(PickupPowerup);
        }

        private void MoveToTable(int index)
        {
            var move = GetComponent<Move>();
            move.target = _veggieStore.Store[index].transform.position;
            move.ActivateMove(Enums.EState.Table);
        }

        private void MoveToKitchen()
        {
            var move = GetComponent<Move>();
            move.target = kitchen.position;
            move.ActivateMove(Enums.EState.Kitchen);
        }

        private void Chop()
        {
            if (_state.CurrentState != Enums.EState.Kitchen)
                return;

            var basket = GetComponent<Basket>();
            if (basket == null)
                throw new System.Exception("Cannot find Basket...");

            //empty basket
            if (basket.Picked.Count == 0)
                return;

            var chopping = GetComponent<ChopVegetable>();
            if (chopping == null)
                throw new System.Exception("Cannot find Chopping component...");

            chopping.ActivateChopping(basket.Picked.Dequeue());
        }

        private void Serve(int index)
        {
            if (_state.CurrentState != Enums.EState.Kitchen)
                return;

            var move = GetComponent<Move>();
            move.target = _customerStore.Store[index].transform.position;
            move.ActivateMove(Enums.EState.Customer);
        }

        private void PickupPowerup()
        {
            var powerup = FindObjectOfType<Powerups.Powerup>();
            if (powerup)
            {
                var move = GetComponent<Move>();
                move.target = powerup.transform.position;
                move.ActivateMove(Enums.EState.None);
            }
        }
    }
}