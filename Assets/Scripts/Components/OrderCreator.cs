using System.Collections.Generic;
using System.Text;
using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class OrderCreator : MonoBehaviour
    {
        /// <summary>
        /// stores the food order customer ordered
        /// </summary>
        private string _order = string.Empty;

        /// <summary>
        /// stores the food order customer ordered
        /// </summary>
        public string OrderPlaced { get => _order; }

        /// <summary>
        /// stores the food order customer ordered
        /// </summary>
        public OnOrderPlaced onOrderPlaced = null;

        /// <summary>
        /// min hungry level
        /// </summary>
        [SerializeField] private int _minHungerLevel = 2;

        /// <summary>
        /// defines the max hungry level of customer
        /// the hungrier the customer, the higher the level
        /// hence higher the count of vegetables in the salad
        /// </summary>
        [SerializeField] private int _maxHungerLevel = 3;

        /// <summary>
        /// multiplier for calculating the customers' wait time
        /// </summary>
        [SerializeField] private float _serveTimeMultiplier = 2f;

        /// <summary>
        /// ref to store
        /// </summary>
        private VegetableStore _store = null;

        [SerializeField] private List<SpriteRenderer> _sprites = new List<SpriteRenderer>();

        private void OnEnable()
        {
            var initiateCustomer = GetComponent<InitiateCustomer>();
            if (initiateCustomer == null)
                throw new System.Exception("Cannot find EnterSaladBar comp...");

            initiateCustomer.onCustomerSeated.AddListener(Setup);

            _store = FindObjectOfType<VegetableStore>();
            if (_store == null)
                throw new System.Exception("Cannot find Vegetable Store...");

            if (_maxHungerLevel > 4)
                _maxHungerLevel = 4;
        }

        private void Setup()
        {
            onOrderPlaced = new OnOrderPlaced();
            _order = CreateOrder();
        }

        private string CreateOrder()
        {
            ///get order
            var order = GetOrder(Random.Range(_minHungerLevel, _maxHungerLevel + 1));

            var timerComp = GetComponent<Timer>();
            if (timerComp == null)
                throw new System.Exception("Timer Component required...");

            /// calculate time based on orders
            timerComp.SetWatingTime(GetWaitingTime(order));

            /// strinfy
            var orderString = StringifyOrder(order);
            orderPrint = orderString;
            onOrderPlaced.Invoke(orderString);

            return orderString;
        }

        private List<Vegetable> GetOrder(int hungerLevel)
        {
            var selected = new List<int>();
            var result = new List<Vegetable>();
            while (selected.Count < hungerLevel)
            {
                //pick a vegetable once
                var index = Random.Range(0, _store.Store.Count);
                if (!selected.Contains(index))
                {
                    var veggie = _store.Store[index];
                    result.Add(veggie);
                    selected.Add(index);
                    print($"{gameObject.name} -> {selected.Count}");
                    _sprites[selected.Count - 1].sprite = veggie.GetComponent<SpriteRenderer>().sprite;
                }
            }
            return result;
        }

        public string orderPrint = "";

        private string StringifyOrder(List<Vegetable> veggies)
        {
            var result = new StringBuilder();
            foreach (var veggie in veggies)
            {
                result.Append($"{veggie.Name}|");
            }

            return result.ToString();
        }

        private float GetWaitingTime(List<Vegetable> veggies)
        {
            var time = 0f;
            foreach (var veggie in veggies)
                time += veggie.ChopTime;

            return time * _serveTimeMultiplier;
        }
    }
}