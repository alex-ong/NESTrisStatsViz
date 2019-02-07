using UnityEngine;
using System.Collections.Generic;

namespace NESTrisStatsViz
{
    //All the info about a game.
    public class GameState
    {
        public int currentScore = 0;
        public int currentLevel = 0;
        public int startLevel = 0;
        
        public List<LineScore> lineScores = new List<LineScore>();
        public List<LineScore> lineScoresSD = new List<LineScore>(); //including softdrop
        public List<string> pieceHistory = new List<string>();
        public int totalPieces = 0;
        public int numSingle = 0;
        public int numDouble = 0;
        public int numTriple = 0;
        public int numTetris = 0;
        private StatState lastState = null;

        public void processEvent(StatState s)
        {
            if (lastState == null)
            {
                lastState = s;
                return;
            }
            StatState diff = lastState.diff(s);
            //todo: nifty stats go here!
            lastState = s;

        }
    }
}