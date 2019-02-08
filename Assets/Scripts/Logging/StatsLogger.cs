using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace NESTrisStatsViz
{
    public class StatsLogger : MonoBehaviour
    {
        public NetworkEventGrabber events;
        // Use this for initialization
        void Start()
        {
            events.onMessage += this.processEvent;
        }

        public StatState prevState = null;
        public GameState gameState = null;
        public LifeTimeState lifeTimeState = new LifeTimeState();

        private bool isNewGame(StatState prevState, StatState current)
        {
            return (!prevState.IsValid && current.Lines == 0);
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
            StatState currentState = new StatState(obj);
            if (currentState.IsValid)
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