using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz.PieceStats
{
    public class PieceStats : MonoBehaviour
    {

        public StatsLogger statsLogger;
        private GameState lastGameState = null;
        private int lastPieceCount = 0;
        // Update is called once per frame

        public int[] droughtArray = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        private static readonly string[] PIECES = new string[] { "T", "J", "Z", "O", "S", "L", "I" };
        private void ResetValues()
        {
            lastGameState = statsLogger.gameState;
            for (int i = 0; i < 7; i++)
            {
                droughtArray[i] = 0;
            }
            lastPieceCount = 0;
        }

        void Update()
        {
            bool toUpdate = false;
            if (lastGameState != statsLogger.gameState)
            {
                toUpdate = true;
                ResetValues();
            }
            else if (lastGameState != null && lastGameState.pieceHistory.Count != lastPieceCount)
            {
                toUpdate = true;                
            }

            if (toUpdate)
            {
                UpdateInternalState();
            }
        }

        private void UpdateInternalState()
        {
            int lastIndex = lastGameState.pieceHistory.Count - 1;
            if (lastIndex >= 0)
            {
                for (int i = 0; i < PIECES.Length; i++)
                {
                    if (lastGameState.pieceHistory[lastIndex] == PIECES[i])
                    {
                        droughtArray[i] = 0;
                    } else
                    {
                        droughtArray[i]++;
                    }
                }                   
            }
            lastPieceCount = lastIndex + 1;
        }
    }
}