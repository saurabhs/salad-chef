using SaladChef.Enums;
using UnityEngine;

namespace SaladChef.Core
{
    public class State : MonoBehaviour
    {
        [SerializeField] private EState _state;
        public EState CurrentState { get => _state; set => _state = value; }
    }
}