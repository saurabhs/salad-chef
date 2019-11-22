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

        private void Start()
        {
            print("OrderValidator::Start");

            onOrderValidated = new OnOrderValidated();
            onWrongOrderReceived = new OnWrongOrderReceived();

             _orderCreator = GetComponent<OrderCreator>();
             if(_orderCreator == null)
                throw new System.Exception("Cannot find OrderCreator...");

            var orderReceiver = GetComponent<OrderReceiver>();
             if(orderReceiver == null)
                throw new System.Exception("Cannot find OrderReceiver...");
            orderReceiver.onOrderReceived.AddListener(OnOrderReceived);

            onWrongOrderReceived.AddListener(OnWrongOrderReceived);
        }

        private void OnOrderReceived(GameObject player, string orderReceived, float timeRemaining, float totalTime)
        {
            print("OrderValidator::OnOrderReceived");

            if(orderReceived.Equals(_orderCreator.OrderPlaced))
            {
                print("Correct Order Received");
                onOrderValidated.Invoke(player, CalculateScore(timeRemaining, totalTime), timeRemaining / totalTime > 0.3f);
            }
            else
            {
                print("Wrong Order Received");
                onWrongOrderReceived.Invoke();
            }
        }

        private float CalculateScore(float timeRemaining, float totalTime)
        {
            print("OrderValidator::CalculateScore");
            return totalTime / timeRemaining;
        }

        private void OnWrongOrderReceived()
        {
            var timer = GetComponent<Timer>();
            timer.UpdateWaitMultiplier();
        }
    }
}