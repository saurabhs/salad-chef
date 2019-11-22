using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class InitiateCustomer : MonoBehaviour
    {
        [SerializeField] private Vector3 _target = Vector3.zero;

        public OnCustomerSeated onCustomerSeated = null;

        private void Start()
        {
            onCustomerSeated = new OnCustomerSeated();

            //move in
            var move = GetComponent<Move>();
            if (move == null)
                throw new System.Exception("Canot find Move component...");

            move.onMoveComplete.AddListener(OnMoveComplete);
            move.target = _target;
            move.ActivateMove();
        }

        private void OnMoveComplete()
        {
            onCustomerSeated.Invoke();
        }
    }
}