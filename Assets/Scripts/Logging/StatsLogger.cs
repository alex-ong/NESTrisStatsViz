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
        public LifeTimeState lifeTimeState = null;

        private bool isNewGame(StatState s)
        {
            return (s.Score == 0 && s.NumPieces == 1 && prevState.NumPieces >= 1);
        }

        private void OnNewGame()
        {
            //todo: export old game somewhere, update global stats etc.
            gameState = new GameState();
        }

        private void processEvent(JSONNode obj)
        {
            StatState currentState = new StatState(obj);
            if (currentState.IsValid)
            {
                if (prevState == null || isNewGame(currentState)) //first game
                {
                    OnNewGame();
                }
                gameState.processEvent(currentState);
                prevState = currentState;
            } else
            {
                Debug.Log("Currently in menu?");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}