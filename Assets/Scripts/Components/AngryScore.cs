﻿using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class AngryScore : MonoBehaviour
    {
        /// <summary>
        /// list of players who have served  wrong order to customer
        /// </summary>
        private List<GameObject> _faultedPlayers = new List<GameObject>();

        public List<GameObject> FaultedPlayers => _faultedPlayers;
        
        private void OnEnable()
        {
            var orderValidator = GetComponent<OrderValidator>();
            if(orderValidator == null)
                throw new System.Exception("Cannot find OrderValidator comp...");

            orderValidator.onWrongOrderReceived.AddListener(OnWrongOrderReceived);
        }

        private void OnWrongOrderReceived(GameObject player)
        {
            if(!_faultedPlayers.Contains(player))
                _faultedPlayers.Add(player);

            UpdateWaitingTimerMultiplier();
        }

        private void UpdateWaitingTimerMultiplier()
        {
            var timer = GetComponent<Timer>();
            if(timer == null)
                throw new System.Exception("Cannot find Timer comp...");

            timer.UpdateWaitMultiplier();
        }
    }
}