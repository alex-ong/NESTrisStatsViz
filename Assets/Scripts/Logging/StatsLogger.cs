﻿using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace NESTrisStatsViz
{
    [RequireComponent(typeof(LifeTimeState))]
    public class StatsLogger : MonoBehaviour
    {
        public NetworkEventGrabber events;
        public StatState prevState = null;
        public GameState gameState = null;
        public LifeTimeState lifeTimeState;
        private float lastMessageTimeStamp = 0.0f;
        public float LastMessageTimeStamp { get { return lastMessageTimeStamp; } }

        private void Awake()
        {
            lifeTimeState = this.GetComponent<LifeTimeState>();
        }

        void Start()
        {
            events.onMessage += this.processEvent;
        }

        private bool isNewGame(StatState prevState, StatState current)
        {
            return (!prevState.IsValidMainStats && current.Lines == 0);
        }

        private void OnNewGame()
        {
            if (gameState != null)
            {
                lifeTimeState.FlushGameState();
            }
            gameState = new GameState();
            lifeTimeState.SetGameState(gameState);
        }

        private void processEvent(JSONNode obj)
        {
            lastMessageTimeStamp = Time.realtimeSinceStartup;
            StatState currentState = new StatState(obj);
            if (currentState.IsValidMainStats)
            {
                if (prevState == null || isNewGame(prevState, currentState)) //first game
                {
                    OnNewGame();
                }
                gameState.processEvent(currentState);

            }
            prevState = currentState;
        }

    }
}