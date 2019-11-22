using UnityEngine;

namespace SaladChef.Core
{
    /// <summary>
    /// Handles movement based on input
    /// </summary>
    public class Move : MonoBehaviour
    {
        public Vector3 target = Vector3.zero;

        private void Start()
        {
            var orderValidator = GetComponent<OrderValidator>();
            if(orderValidator == null)
                throw new System.Exception("Cannot find OrderValidaor...");
        }

        public void ActivateMove()
        {
            InvokeRepeating("MoveTo", 0, Time.deltaTime);
        }

        private void MoveTo()
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, 1 * Time.deltaTime);
        }
    }
}