using SaladChef.Core;
using UnityEngine;

namespace SaladChef.Powerups
{
    public class ScoreBoost : Powerup
    {
        [SerializeField] private float _multipier = 1.25f;

        protected override void Execute(GameObject player)
        {
            var hud = player.GetComponent<HUD>();
            hud.UpdateScore(_multipier);
            gameObject.SetActive(false);
        }
    }
}