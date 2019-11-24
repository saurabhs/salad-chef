using SaladChef.Enums;
using UnityEngine;

namespace SaladChef.Core
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private EPlayerState _state;
        public EPlayerState State { get => _state; set => _state = value; }
    }
}