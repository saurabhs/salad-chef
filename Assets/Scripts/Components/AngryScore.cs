using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class AngryScore : MonoBehaviour
    {
        private List<GameObject> _faultedPlayers = new List<GameObject>();

        public List<GameObject> FaultedPlayers => _faultedPlayers;
        
        private void Start()
        {
            var orderValidator = GetComponent<OrderValidator>();
            if(orderValidator == null)
                throw new System.Exception("Cannot find OrderValidator comp...");

            orderValidator.onWrongOrderReceived.AddListener(OnWrongOrderReceived);
        }

        private void OnWrongOrderReceived(GameObject player)
        {
            print("Angry::OnWrongOrderReceived");
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