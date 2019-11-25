using UnityEngine;

namespace SaladChef.Core
{
    public class Vegetable : MonoBehaviour
    {
        [SerializeField] private string _name = "Vegetable";
        public string Name => _name;
        
        [SerializeField] private float chopTime = 3f;

        public float ChopTime => chopTime;

        private void OnEnable() =>_name = gameObject.name;
    }
}