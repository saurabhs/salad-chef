using System;
using SaladChef.Enums;
using SaladChef.Events;
using TMPro;
using UnityEngine;

namespace SaladChef.Core
{
    /// <summary>
    /// Handles movement
    /// </summary>
    public class Move : MonoBehaviour
    {
        /// <summary>
        /// Handles movement based on input
        /// </summary>
        [HideInInspector] public Vector3 target = Vector3.zero;

        /// <summary>
        /// Handles movement based on input
        /// </summary>
        [SerializeField] private float _speed = 1f;

        /// <summary>
        /// event to fire when the parent has complement movement
        /// </summary>
        public OnMoveComplete onMoveComplete = null;

        /// <summary>
        /// ref to State component
        /// <summary>
        private State _state = null;

        /// <summary>
        /// flag for blocking movement
        /// </summary>
        public bool canMove = true;

        /// <summary>
        /// State to be set after the move has been completed
        /// </summary>
        private EState _postMoveCompleteState;

        private void OnEnable()
        {
            onMoveComplete = new OnMoveComplete();
            _state = GetComponent<State>();
            if (_state == null)
                throw new Exception("Cannot find State component...");
        }

        private void OnDisable() => onMoveComplete = null;

        public void UpdateSpeed(float change) => _speed += change;

        public void ActivateMove(Enums.EState postState = EState.None)
        {
            if (!canMove)
                return;

            _postMoveCompleteState = postState;
            _state.CurrentState = Enums.EState.Walking;
            InvokeRepeating("MoveTo", 0, Time.deltaTime);
        }

        private void MoveTo()
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, _speed * Time.deltaTime);
            var position = transform.position;

            if (Math.Round(position.x, MidpointRounding.AwayFromZero) == Math.Round(target.x, MidpointRounding.AwayFromZero) &&
            Math.Round(position.y, MidpointRounding.AwayFromZero) == Math.Round(target.y, MidpointRounding.AwayFromZero) &&
            Math.Round(position.z, MidpointRounding.AwayFromZero) == Math.Round(target.z, MidpointRounding.AwayFromZero))
            {
                CancelInvoke("MoveTo");

                _state.CurrentState = _postMoveCompleteState;
                onMoveComplete.Invoke();
            }
        }
    }
}