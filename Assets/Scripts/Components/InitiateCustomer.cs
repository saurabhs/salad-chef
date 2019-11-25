using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class InitiateCustomer : MonoBehaviour
    {
        /// <summary>
        /// sitting position of the customer in the salad bar
        /// </summary>
        [SerializeField] private Vector3 _target = Vector3.zero;

        /// <summary>
        /// max time the customer to wait 
        /// before changing to new and renter
        //// the salad bar
        /// </summary>
        [SerializeField] private float _maxDelayBetweenReEntry = 3f;

        public OnCustomerSeated onCustomerSeated = null;

        private void OnEnable()
        {
            onCustomerSeated = new OnCustomerSeated();

            //move in
            var move = GetComponent<Move>();
            if (move == null)
                throw new System.Exception("Cannot find Move component...");

            move.onMoveComplete.AddListener(OnMoveComplete);
            move.target = new Vector3(transform.position.x, _target.y, transform.position.z);
            move.ActivateMove(Enums.EState.Sitting);
        }

        private void OnDisable()
        {
            onCustomerSeated.RemoveAllListeners();
            onCustomerSeated = null;
        }

        private void OnMoveComplete()
        {
            if (GetComponent<State>().CurrentState == Enums.EState.Sitting)
                onCustomerSeated.Invoke();
            else if (GetComponent<State>().CurrentState == Enums.EState.Exit)
            {
                gameObject.SetActive(false);
                Invoke("Reactivate", Random.value * _maxDelayBetweenReEntry);
            }
        }

        private void Reactivate()
        {
            gameObject.SetActive(true);
        }
    }
}