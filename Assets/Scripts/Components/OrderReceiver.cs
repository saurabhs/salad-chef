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

        private void Start()
        {
            print("OrderReceiver:Start");
            onOrderReceived = new OnOrderReceived();

            StartCoroutine(MockReceive());
        }

        private IEnumerator MockReceive()
        {
            print("OrderReceiver:MockReceive");

            yield return new WaitForSeconds(5f);

            var player1 = GameObject.Find("Player1");
            onOrderReceived.Invoke(player1, player1.GetComponent<OrderCarrier>().GetOrder(), 25f, 60f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var timerComp = GetComponent<Timer>();
            if(timerComp == null)
                throw new System.Exception("Timer component required...");

            var player = other.gameObject;
            onOrderReceived.Invoke(player, player.GetComponent<OrderCarrier>().GetOrder(), timerComp.TimeLeft, timerComp.WaitingTIme);
        }
    }
}