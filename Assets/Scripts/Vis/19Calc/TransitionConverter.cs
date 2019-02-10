using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NESTrisStatsViz.NineteenConverter
{
    public class TransitionConverter : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                if (this.statsLogger.gameState == null)
                {
                    return 0.0f;
                }
                if (this.statsLogger.gameState.startLevel != 19)
                {
                    return 0.0f;
                }
                return 8f;
            }
        }

        public BlockMaster blockMaster;
        private GameState lastGameState;
        public Text realScoreUI;
        public Text adjScoreUI;
        public override void FirstTimeSetup()
        {
            this.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        protected override void ChildOnEnable()
        {
            blockMaster.Reset();
        }

        private void UpdateBlockMaster()
        {
            List<int> lines = lastGameState.lineHistory;
            int current = 0;

            foreach (int lineCount in lines)
            {
                current += lineCount;
                blockMaster.SetLine(lineCount, current);
            }
        }

        private void UpdateCorrectScore()
        {
            int totalLines = 0;
            int realScore = 0;
            int adjScore = 0;
            List<int> lines = lastGameState.lineHistory;
            
            foreach (int line in lines)
            {
                
                if (totalLines > 100)
                {
                    break;
                }
                totalLines += line;
                realScore += ScoreTable.getScore(line, 19);
                adjScore += ScoreTable.getScore(line, 19 + (totalLines / 10));
            }
            realScore += lastGameState.softDropTotal;
            adjScore += lastGameState.softDropTotal;

            realScoreUI.text = realScore.ToString("000000");
            adjScoreUI.text = adjScore.ToString("000000");
        }

        public override void ChildUpdate()
        {
            GameState gs = statsLogger.gameState;
            if (gs == null)
            {
                return;
            }

            if (gs != lastGameState)
            {
                blockMaster.Reset();
            }
            lastGameState = gs;

            UpdateBlockMaster();
            UpdateCorrectScore();
        }

        
    }
}