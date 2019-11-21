using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class OrderReceiver : MonoBehaviour
    {
        public OnOrderReceived onOrderReceived = null;

        private void Start()
        {
            onOrderReceived = new OnOrderReceived();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var timerComp = GetComponent<Timer>();
            if(timerComp == null)
                throw new System.Exception("Timer component required...");

            onOrderReceived.Invoke(timerComp.TimeLeft, timerComp.WaitingTIme);
        }
    }
}