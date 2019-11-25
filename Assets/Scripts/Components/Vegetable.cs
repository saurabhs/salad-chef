using UnityEngine;

namespace SaladChef.Core
{
    public class Vegetable : MonoBehaviour
    {
        ///<summary>
        /// name of the vegetable
        ///</summary>
        [SerializeField] private string _name = "Vegetable";

        ///<summary>
        /// time taken to chop the vegetable
        ///</summary>
        [SerializeField] private float chopTime = 3f;

        public string Name => _name;
        public float ChopTime => chopTime;
    }
}