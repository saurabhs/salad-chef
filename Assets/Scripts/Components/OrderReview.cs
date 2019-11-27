using SaladChef.Powerups;
using UnityEngine;

namespace SaladChef.Core
{
    public class OrderReview : MonoBehaviour
    {
        /// <summary>
        /// score penalty for serving wrong order
        /// </summary>
        [SerializeField] private int _noOrderServedPenalty = -20;

        /// <summary>
        /// score penalty for serving wrong order
        /// </summary>
        [SerializeField] private int _angryScorePenalty = -50;

        private void OnEnable()
        {
            var timer = GetComponent<Timer>();
            if (timer == null)
                throw new System.Exception("Cannot find Timer Component...");
            timer.onTimeOver.AddListener(OnTimeOver);

            var orderValidator = GetComponent<OrderValidator>();
            if (orderValidator == null)
                throw new System.Exception("Cannot find OrderValidity component...");
            orderValidator.onOrderValidated.AddListener(OnOrderValidated);
        }

        private void OnOrderValidated(GameObject player, float score, bool bonus)
        {
            player.GetComponent<HUD>().UpdateScore(score, bonus);
            var timer = GetComponent<Timer>();
            if (timer == null)
                throw new System.Exception("Cannot find Timer Component...");

            // removing listeners to avoid playergetting -ve marks 
            // if the time runs over just before the customer is reset
            timer.onTimeOver.RemoveAllListeners();

            var performanceTracker = player.GetComponent<PerformanceTracker>();
            if (performanceTracker == null)
                throw new System.Exception("Cannot find PowerupCreator....");
            performanceTracker.OnCorrectOrderServed();

            MoveOut();
        }

        private void OnTimeOver()
        {
            //handle players score
            var angryScores = GetComponent<AngryScore>();
            if (angryScores == null)
                throw new System.Exception("Cannot find AngryScore component...");

            //give angry score
            if (angryScores.FaultedPlayers.Count > 0)
            {
                foreach (var player in angryScores.FaultedPlayers)
                    player.GetComponent<HUD>().UpdateScore(_angryScorePenalty, false);
            }
            else
            {
                ///since no one served the customer, 
                //incorrect order or otherwise, give penalty
                var players = FindObjectsOfType<HUD>();
                if(players.Length == 0)
                    throw new System.Exception("No player found...");
 
                foreach(var player in players)
                    player.GetComponent<HUD>().UpdateScore(_noOrderServedPenalty, false);
            }

            MoveOut();
        }

        private void MoveOut()
        {
            var move = GetComponent<Move>();
            if (move == null)
                throw new System.Exception("Canot find Move component...");
            move.target = transform.position + new Vector3(0, 3f, 0);
            move.ActivateMove(Enums.EState.Exit);
        }
    }
}