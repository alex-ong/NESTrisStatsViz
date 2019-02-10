using UnityEngine;
using System.Collections.Generic;
using System;

namespace NESTrisStatsViz
{
    //All the info about a game.
    public class GameState
    {
        public int startLevel = 0;
        public int currentLevel = 0;
        public List<LineScore> lineScores = new List<LineScore>();
        public List<int> tenLineScores = new List<int>();
        public List<int> tenLineGoal = new List<int>();
        public List<int> lineHistory = new List<int>();
        public int[] distClears = new int[4];
        public int[] distScores = new int[4];

        public int softDropTotal = 0;
        private StatState lastState = null;
        public int LinesCleared { get { return lastState.Lines; } }
        public int Score { get { return lastState.Score; } }
        private DateTime startTime;
        public DateTime StartTime { get { return startTime; } }
        private DateTime finishTime;
        public DateTime FinishTime { get { return finishTime; } }
        public TimeSpan Duration { get { return finishTime - startTime; } }

        private void GenerateTenLineGoal()
        {

        }

        public void processEvent(StatState current)
        {
            if (lastState == null)
            {
                lastState = current;
                startLevel = current.Level;
                currentLevel = current.Level;
                startTime = DateTime.Now;
                finishTime = DateTime.Now;
                GenerateTenLineGoal();
                return;
            }
            StatState diff = lastState.diff(current);

            //quick sanity check
            if (!LegitDiff(diff, current))
            {
                Debug.Log(":(");
                Debug.Log(lastState);
                Debug.Log(current);
                Debug.Log(diff);
                return;
            }

            finishTime = DateTime.Now;
            ProcessSoftDrop(diff, current);
            ProcessLineScore(diff, current);
            currentLevel = current.Level;
            lastState = current;
        }

        private bool LegitDiff(StatState diff, StatState current)
        {
            if (diff.Lines < 0 || diff.Lines > 4)
            {
                Debug.Log("lines" + diff.Lines.ToString());
                return false;
            }

            if (diff.Level < 0 || diff.Level > 1)
            {
                return false;
            }

            //we scored more than a tetris + softdrop...
            if (diff.Score > ScoreTable.getScore(4, current.Level) + 50)
            {
                Debug.Log(diff.Score.ToString() + (ScoreTable.getScore(4, current.Level) + 50).ToString());
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

            distClears[diff.Lines - 1] += 1;
            distScores[diff.Lines - 1] += ScoreTable.getScore(diff.Lines, newState.Level);
            if ((newState.Lines / 10) > tenLineScores.Count)
            {
                tenLineScores.Add(newState.Score);
            }
            lineHistory.Add(diff.Lines);
            lineScores.Add(new LineScore(newState.Lines, newState.Score, this.Duration));
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
        }
    }
}