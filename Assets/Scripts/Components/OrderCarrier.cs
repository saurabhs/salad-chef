using UnityEngine;

namespace SaladChef.Core
{
    /// Contains the data for food cooked by player 
    /// and delivered to the customer
    public class OrderCarrier : MonoBehaviour
    {
        public string GetOrder()
        {
            print("Getting Correct Order");
            return "A|B|C";
        }

        public string GetWrongOrder()
        {
            print("Getting Wrong Order");
            return "A|D|C";
        }
    }
}