using System.Collections.Generic;
using UnityEngine;

namespace SaladChef.Core
{
    public class CustomerStore : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _store = null;
        public List<GameObject> Store => _store;
    }
}