using System.Collections;
using UnityEngine;

namespace SaladChef.Core
{
    public class ChopVegetable : MonoBehaviour
    {
        private Move _move = null;

        private void Start()
        {
            _move = GetComponent<Move>();
            if(_move == null)
                throw new System.Exception("Cannot find Move component...");
        }

        public void ActivateChopping(Vegetable veggie)
        {
            StartCoroutine(Chop(veggie));
        }

        private IEnumerator Chop(Vegetable veggie)
        {
            print("Chopping start, blocked move");

            //block move
            _move.canMove = false;

            yield return new WaitForSeconds(veggie.ChopTime);
            
            print("Chopping complete");
            
            //unblock move
            _move.canMove = true;

        }
    }
}