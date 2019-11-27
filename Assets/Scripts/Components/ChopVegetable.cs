using System.Collections;
using SaladChef.Events;
using UnityEngine;

namespace SaladChef.Core
{
    public class ChopVegetable : MonoBehaviour
    {
        ///<summary>
        /// ref to Move component for blocking move while chopping
        ///<summary>
        private Move _move = null;

        ///<summary>
        /// sprite object on chopping board 
        ///<summary>
        [SerializeField] private SpriteRenderer _image = null;

        ///<summary>
        /// event tot fire when vegetable is chopped
        ///<summary>
        public OnVegetableChoppedComplete onVegetableChoppedComplete = null;

        ///<summary>
        /// flag to avoid multiple chops
        ///<summary>
        private bool isBusy = false;

        private void OnEnable()
        {
            onVegetableChoppedComplete = new OnVegetableChoppedComplete();
            
            _move = GetComponent<Move>();
            if(_move == null)
                throw new System.Exception("Cannot find Move component...");
        }

        private void OnDisable()
        {
            onVegetableChoppedComplete.RemoveAllListeners();   
            onVegetableChoppedComplete = null;
        }

        public void ActivateChopping(Vegetable veggie)
        {
            if(isBusy)
                return;

            StartCoroutine(Chop(veggie));
        }

        private IEnumerator Chop(Vegetable veggie)
        {
            //block move
            _move.canMove = false;
            _image.sprite = veggie.GetComponent<SpriteRenderer>().sprite;

            var state = GetComponent<State>();
            state.CurrentState = Enums.EState.Chopping;

            yield return new WaitForSeconds(veggie.ChopTime);

            state.CurrentState = Enums.EState.Kitchen;
            
            _image.sprite = null;
            onVegetableChoppedComplete.Invoke(veggie);

            //unblock move
            _move.canMove = true;
        }
    }
}