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
        /// event tot fire when vegetable is chopped
        ///<summary>
        public OnVegetableChoppedComplete onVegetableChoppedComplete = null;

        ///<summary>
        /// flag to avoid multiple chops
        ///<summary>
        private bool isBusy = false;

        private void Start()
        {
            onVegetableChoppedComplete = new OnVegetableChoppedComplete();
            
            _move = GetComponent<Move>();
            if(_move == null)
                throw new System.Exception("Cannot find Move component...");
        }

        public void ActivateChopping(Vegetable veggie)
        {
            if(isBusy)
                return;

            StartCoroutine(Chop(veggie));
        }

        private IEnumerator Chop(Vegetable veggie)
        {
            print("Chopping start, blocked move");

            //block move
            _move.canMove = false;

            yield return new WaitForSeconds(veggie.ChopTime);
            
            print("Chopping complete");
            onVegetableChoppedComplete.Invoke(veggie);

            //unblock move
            _move.canMove = true;
        }
    }
}