using UnityEngine;
using System.Collections.Generic;
using System;

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
        public int[] lineStats = new int[4];
        public int numDouble = 0;
        public int numTriple = 0;
        public int numTetris = 0;
        public int softDropTotal = 0;
        private StatState lastState = null;

        public void processEvent(StatState s)
        {
            if (lastState == null)
            {
                lastState = s;
                return;
            }
            StatState diff = lastState.diff(s);

            //quick sanity check
            if (!legitDiff(diff, s))
            {
                return;
            }
            Debug.Log("still happy");
            //todo: nifty stats go here!
            ProcessPieceHistory(diff);
            ProcessSoftDrop(diff, s);
            ProcessLineScore(diff, s);
            lastState = s;
        }

        private bool legitDiff(StatState diff, StatState current)
        {
            if (diff.Lines < 0 || diff.Lines > 4)
            {
                return false;
            }

            if (diff.Level > 1)
            {
                return false;
            }

            //we scored more than a tetris + softdrop...
            if (diff.Score > ScoreTable.getScore(4, current.Level) + 50)
            {
                return false;
            }

            if (diff.NumPieces < 0 || diff.NumPieces > 2)
            {
                //TODO: attempt to correct pieces...
                Debug.Log("warning, more than 1 piece difference...");
                Debug.Log(diff);
                Debug.Log(current);
                return false;
            }
            return true;
        }

        private void ProcessLineScore(StatState diff, StatState newState)
        {
            if (diff.Lines == 0)
            {
                return;
            }
            if (diff.Lines <= 4 && diff.Lines >= 1)
            {
                lineStats[diff.Lines - 1] += 1;
            }
            lineScores.Add(new LineScore(newState.Lines, newState.Score));
        }

        private void ProcessSoftDrop(StatState diff, StatState newState)
        {
            if (diff.Lines == 0)
            {
                softDropTotal += diff.Score;
            }
            else
            {
                softDropTotal += diff.Score - ScoreTable.getScore(diff.Lines, newState.Level);
            }
            Debug.Log("SoftDrop bonus cumul" + softDropTotal.ToString());
        }

        private void ProcessPieceHistory(StatState diff)
        {
            for (int i = 0; i < diff.T; i++) pieceHistory.Add("T");
            for (int i = 0; i < diff.J; i++) pieceHistory.Add("J");
            for (int i = 0; i < diff.Z; i++) pieceHistory.Add("Z");
            for (int i = 0; i < diff.O; i++) pieceHistory.Add("O");
            for (int i = 0; i < diff.S; i++) pieceHistory.Add("S");
            for (int i = 0; i < diff.L; i++) pieceHistory.Add("L");
            for (int i = 0; i < diff.I; i++) pieceHistory.Add("I");
        }
    }
}