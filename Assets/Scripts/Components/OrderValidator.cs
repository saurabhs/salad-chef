﻿using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class OrderValidator : MonoBehaviour
    {
        ///<summary>///
        /// ref to order creator for getting 
        // the order placed by customer
        ///</summary>///
        private OrderCreator _orderCreator;
        
        ///<summary>///
        /// event to invoke when correct order is received
        ///</summary>///
        public OnOrderValidated onOrderValidated = null;

        ///<summary>///
        /// event to invoke when wrong order is received
        ///</summary>///
        public OnWrongOrderReceived onWrongOrderReceived = null;

        private void OnEnable()
        {
            onOrderValidated = new OnOrderValidated();
            onWrongOrderReceived = new OnWrongOrderReceived();

             _orderCreator = GetComponent<OrderCreator>();
             if(_orderCreator == null)
                throw new System.Exception("Cannot find OrderCreator...");

            var orderReceiver = GetComponent<OrderReceiver>();
             if(orderReceiver == null)
                throw new System.Exception("Cannot find OrderReceiver...");
            orderReceiver.onOrderReceived.AddListener(OnOrderReceived);
        }

        private void OnDisable()
        {
            onOrderValidated.RemoveAllListeners();
            onOrderValidated = null;

            onWrongOrderReceived.RemoveAllListeners();
            onWrongOrderReceived = null;
            
             _orderCreator = null;
        }

        private void OnOrderReceived(GameObject player, string orderReceived, float timeRemaining, float totalTime)
        {
            if(orderReceived.Equals(_orderCreator.OrderPlaced))
                onOrderValidated.Invoke(player, CalculateScore(timeRemaining, totalTime), timeRemaining / totalTime > 0.3f);
            else
                onWrongOrderReceived.Invoke(player);
        }

        private float CalculateScore(float timeRemaining, float totalTime) => totalTime / timeRemaining;
    }
}