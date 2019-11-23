using System.Collections.Generic;
using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    /// <summary>
    /// handles player imput
    /// </summary>
    public class Input : MonoBehaviour
    {
        /// <summary>
        /// stores the list of keys for movement
        /// </summary>
        [SerializeField] private List<KeyCode> moveKeys = new List<KeyCode>();

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private List<KeyCode> serveKeys = new List<KeyCode>();

        /// <summary>
        /// key to return to kitchen after picking up vegetable
        /// </summary>
        [SerializeField] private KeyCode kitchenKey;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private KeyCode chopKey;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private KeyCode discardKey;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private KeyCode pickPower;

        /// <summary>
        /// move a vegetable plate to that can be used later
        /// </summary>
        [SerializeField] private KeyCode usePlate;

        public OnMoveToTable moveToTable = null;
        public OnMoveToKitchen moveToKitchen = null;
        public OnChopVegetableStart chopVegeable = null;
        public OnServeToCustomer serveSalad = null;
        public OnDiscardVegetable discard = null;
        public OnUsePlate onUsePlate = null;

        private void Start()
        {
            moveToTable = new OnMoveToTable();
            moveToKitchen = new OnMoveToKitchen();
            chopVegeable = new OnChopVegetableStart();
            serveSalad = new OnServeToCustomer();
            discard = new OnDiscardVegetable();
            onUsePlate = new OnUsePlate();
        }

        private void Update()
        {
            foreach (var key in moveKeys)
            {
                if (UnityEngine.Input.GetKeyDown(key))
                {
                    var index = moveKeys.FindIndex(obj => key == obj);
                    if (index != -1)
                    {
                        moveToTable.Invoke(index);
                    }
                }
            }

            foreach(var key in serveKeys)
            {
                if (UnityEngine.Input.GetKeyDown(key))
                {
                    var index = serveKeys.FindIndex(obj => key == obj);
                    if (index != -1)
                    {
                        serveSalad.Invoke(index);
                    }
                }
            }

            if (UnityEngine.Input.GetKeyDown(kitchenKey))
            {
                moveToKitchen.Invoke();
            }
            else if (UnityEngine.Input.GetKeyDown(chopKey))
            {
                chopVegeable.Invoke();
            }
            else if (UnityEngine.Input.GetKeyDown(discardKey))
            {
                discard.Invoke();
            }
            else if (UnityEngine.Input.GetKeyDown(usePlate))
            {
                onUsePlate.Invoke();
            }
        }
    }
}