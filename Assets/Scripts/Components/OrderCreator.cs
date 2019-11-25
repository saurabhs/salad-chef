using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class OrderCreator : MonoBehaviour
    {
        ///<summary>///
        ///stores the food order customer ordered
        ///</summary>///
        private string _order = string.Empty;

        ///<summary>///
        ///stores the food order customer ordered
        ///</summary>///
        public string OrderPlaced { get => _order; }

        ///<summary>///
        ///stores the food order customer ordered
        ///</summary>///
        public OnOrderPlaced onOrderPlaced = null;

        private void OnEnable()
        {
            var initiateCustomer = GetComponent<InitiateCustomer>();
            if(initiateCustomer == null)
                throw new System.Exception("Cannot find EnterSaladBar comp...");

            initiateCustomer.onCustomerSeated.AddListener(Setup);
        }

        private void Setup()
        {
            print("OrderCreator::Setup");

            onOrderPlaced = new OnOrderPlaced();
            _order = CreateOrder();

            var timerComp = GetComponent<Timer>();
            if (timerComp == null)
                throw new System.Exception("Timer Component required...");

            timerComp.SetWatingTime(GetWaitingTime());
        }

        private string CreateOrder()
        {
            print("OrderCreator::CreateOrder");

            var order = "A|B|C";
            onOrderPlaced.Invoke(order);

            return order;
        }

        private float GetWaitingTime()
        {
            print("OrderCreator::GetWaitingTime");

            if (_order == string.Empty)
                throw new System.Exception("Invalid order...");

            return 10f;
        }
    }
}