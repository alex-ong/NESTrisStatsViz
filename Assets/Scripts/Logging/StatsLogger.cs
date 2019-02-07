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
            return (s.Score == 0 && s.numPieces() <= 1 && prevState.numPieces() >= 1);
        }

        private void OnNewGame()
        {
            //todo: export old game somewhere, update global stats etc.
            gameState = new GameState();
        }

        private void processEvent(JSONNode obj)
        {
            StatState currentState = new StatState(obj);
            if (currentState.hasValues())
            {
                if (prevState == null || isNewGame(currentState)) //first game
                {
                    Debug.Log("newgame");
                    OnNewGame();
                }
                gameState.processEvent(currentState);
                prevState = currentState;
            } else
            {
                Debug.Log("invalid");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}