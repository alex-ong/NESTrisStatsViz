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
        public List<string> pieceHistory = new List<string>();
        public int[] distClears = new int[4];
        public int[] distScores = new int[4];

        public int softDropTotal = 0;
        private StatState lastState = null;
        public int LinesCleared { get { return lastState.Lines; } }
        public int Score { get { return lastState.Score; } }        
        public DateTime StartTime { get; private set;}        
        public DateTime FinishTime { get; private set; }
        public TimeSpan Duration { get { return FinishTime - StartTime; } }

        private bool leveledUp = false;
        private int firstLevelUpBoundary = 0;

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
                StartTime = DateTime.Now;
                FinishTime = DateTime.Now;
                GenerateTenLineGoal();
                ProcessPieceDiff(current); //get first piece.
                return;
            }

            //First, we have to fix the level.
            StatState diff = lastState.diff(current);
            ProcessLevel(diff, current);
            current.Level = this.currentLevel;
            diff = lastState.diff(current);

            //quick sanity check
            if (!LegitDiff(diff, current))
            {
                Debug.Log(":(");
                Debug.Log(lastState);
                Debug.Log(current);
                Debug.Log(diff);
                return;
            }

            FinishTime = DateTime.Now;
            ProcessSoftDrop(diff, current);
            ProcessLineScore(diff, current);
            ProcessPieceDiff(diff);
            
            
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
                Debug.Log("level" + diff.Level);
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

        /// <summary>
        /// Level is calculated purely on linecount. We get the first level transition to work off.
        /// </summary>
        /// <param name="diff"></param>
        /// <param name="current"></param>
        private void ProcessLevel(StatState diff, StatState current)
        {            
            if (!leveledUp && diff.Level != 0)
            {
                firstLevelUpBoundary = (current.Lines / 10) * 10;
                this.currentLevel = this.startLevel + 1;
                leveledUp = true;
            }
            else if (leveledUp)
            {                
                this.currentLevel = ((current.Lines - firstLevelUpBoundary) / 10) + 1 + startLevel;                
            }
        }

        private void ProcessPieceDiff(StatState diff)
        {
            if (diff.IsValidPieceStats)
            {
                if (diff.T == 1)
                {
                    pieceHistory.Add("T");
                }
                else if (diff.J == 1)
                {
                    pieceHistory.Add("J");
                }
                else if (diff.Z == 1)
                {
                    pieceHistory.Add("Z");
                }
                else if (diff.O == 1)
                {
                    pieceHistory.Add("O");
                }
                else if (diff.S == 1)
                {
                    pieceHistory.Add("S");
                }
                else if (diff.L == 1)
                {
                    pieceHistory.Add("L");
                }
                else if (diff.I == 1)
                {
                    pieceHistory.Add("I");
                }
            }
        }
    }
}