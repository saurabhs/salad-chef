using UnityEngine;

namespace SaladChef.Core
{
    public class OrderCreator : MonoBehaviour
    {
        ///<summary>///
        ///stores the food order customer ordered
        ///</summary>///
        private string _order = string.Empty;

        public string OrderPlaced { get => _order; }
        
        private void Start()
        {
            _order = CreateOrder();

            var timerComp = GetComponent<Timer>();
            if(timerComp == null)
                throw new System.Exception("Timer Component required...");

            timerComp.SetWatingTime(GetWaitingTime());
        }

        private string CreateOrder()
        {
            return "A|B|C";
        }

        private float GetWaitingTime()
        {
            if(_order == string.Empty) 
                throw new System.Exception("Invalid order...");

            return 10f;
        }
    }
}