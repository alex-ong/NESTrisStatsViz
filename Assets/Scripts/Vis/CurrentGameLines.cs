using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NESTrisStatsViz
{
    public class CurrentGameLines : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                return 8.0f;
            }
        }
        public List<Text> scores;
        public List<Text> counts;
        public List<Text> linePerc;
        public Text softDropScore;
        public LineHistory.LineHistory history;
        public void RefreshText()
        {

            GameState gs = statsLogger.gameState;

            if (gs == null) return;
            
            for (int i = 0; i < 4; i++)
            {
                scores[i].text = gs.distScores[i].ToString("000000");
                counts[i].text = gs.distClears[i].ToString("000");
                if (gs.LinesCleared == 0)
                {
                    linePerc[i].text = "000";
                }
                else
                {
                    int linesCleared = gs.distClears[i] * (i + 1);
                    int total = gs.LinesCleared;
                    float perc = linesCleared / (float)total * 100;
                    linePerc[i].text = Mathf.RoundToInt(perc).ToString("000");
                }
            }
            softDropScore.text = gs.softDropTotal.ToString("000000");
        }

        public override void ChildUpdate()
        {
            RefreshText();
            RefreshLineHistory();
        }
        
        private void RefreshLineHistory()
        {
            GameState gs = statsLogger.gameState;
            if (gs == null) return;
            history.SetHistory(gs.currentLevel, gs.lineHistory);
        }

        protected override void ChildOnEnable()
        {
            RefreshText();
        }

        protected override void Hide()
        {

        }
    }
}