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
        public Vector3 target = Vector3.zero;

        /// <summary>
        /// Handles movement based on input
        /// </summary>
        [SerializeField] private float _speed = 1f;

        /// <summary>
        /// event to fire when the parent has complement movement
        /// </summary>
        public OnMoveComplete onMoveComplete = null;

        /// <summary>
        /// flag for blocking movement
        /// </summary>
        public bool canMove = true;

        private EPlayerState _postMoveCompleteState;

        private void Start() => onMoveComplete = new OnMoveComplete();

        public void ActivateMove(EPlayerState postState = Enums.EPlayerState.None)
        {
            if (!canMove)
                return;

            _postMoveCompleteState = postState;

            var state = GetComponent<PlayerState>();
            if (state)
                state.State = Enums.EPlayerState.Walking;
                
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

                var state = GetComponent<PlayerState>();
                if (state)
                    state.State = _postMoveCompleteState;

                onMoveComplete.Invoke();
            }
        }
    }
}