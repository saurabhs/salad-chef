using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class VegetableStore : MonoBehaviour
    {
        [SerializeField] private List<Vegetable> _store = null;
        public List<Vegetable> Store => _store;
    }
}