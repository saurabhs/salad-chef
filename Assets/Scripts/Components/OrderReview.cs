﻿using UnityEngine;

namespace SaladChef.Core
{
    public class OrderReview : MonoBehaviour
    {
        private void OnEnable()
        {
            var timer = GetComponent<Timer>();
            if(timer == null) 
                throw new System.Exception("Cannot find Timer Component...");
            timer.onTimeOver.AddListener(OnTimeOver);

            var orderValidator = GetComponent<OrderValidator>();
            if(orderValidator == null)
                throw new System.Exception("Cannot find OrderValidity component...");
            orderValidator.onOrderValidated.AddListener(OnOrderValidated);
        }

        private void OnOrderValidated(GameObject player, float score, bool bonus)
        {
            print("OrderReview::OnOrderValidated");
            player.GetComponent<HUD>().UpdateScore(score, bonus);
        }

        private void OnTimeOver()
        {
            print("OrderReview::OnTimeOver");

            //handle players score
            var angryScores = GetComponent<AngryScore>();
            if(angryScores == null)
                throw new System.Exception("Cannot find AngryScore component...");
            
            //give angry score
            foreach(var player in angryScores.FaultedPlayers)
                player.GetComponent<HUD>().UpdateScore(-100, false);

            //move out
            var move = GetComponent<Move>();
            if(move == null)
                throw new System.Exception("Canot find Move component...");
            move.target = transform.position + new Vector3(0, 3f, 0);
            move.ActivateMove(Enums.EState.Exit);
        }
    }
}