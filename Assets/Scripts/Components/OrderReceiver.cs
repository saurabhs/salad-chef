using System.Collections;
using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class OrderReceiver : MonoBehaviour
    {
        ///<summary>///
        ///event when the player submits the order
        ///</summary>///
        public OnOrderReceived onOrderReceived = null;

        private void OnEnable()
        {
            onOrderReceived = new OnOrderReceived();
        }

        private void OnDisable()
        {
            onOrderReceived.RemoveAllListeners();
            onOrderReceived = null;
        }

        public void Receive(GameObject player)
        {
            print("OrderReceiver:Receive");

            var timerComp = GetComponent<Timer>();
            if(timerComp == null)
                throw new System.Exception("Timer component required...");

            onOrderReceived.Invoke(player, player.GetComponent<ServeOrder>().Order, timerComp.TimeLeft, timerComp.WaitingTIme);
        }
    }
}