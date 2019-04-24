using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace NESTrisStatsViz
{
    public class TimeStats : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                if (isPostTransition()) return 2;
                return 8;
            }
        }

        public Text totalTime;
        public Text gameTime;
        public Text hundredLine;
        public Text hundredLineCaption;


        public bool getHundredLine(GameState gs, out TimeSpan ts)
        {
            ts = new TimeSpan();
            if (gs == null) return false;
            List<LineScore> lineScores = gs.lineScores;
            if (lineScores.Count <= 0) return false;
            LineScore ls = lineScores[lineScores.Count - 1];
            int index = lineScores.Count - 1;
            bool valid = false;
            while (ls.LineCount >= 100)
            {
                valid = true;
                ts = ls.Time;
                index--;
                ls = lineScores[index];
            }
            return valid;
        }

        public override void ChildUpdate()
        {
            TimeSpan total = this.statsLogger.lifeTimeState.TotalGameTime;
            GameState game = this.statsLogger.gameState;
            TimeSpan gameDuration = game == null ? TimeSpan.Zero : this.statsLogger.gameState.Duration;
            
            totalTime.text = total.Days.ToString() + " days " + total.ToString(@"hh\:mm\:ss");
            gameTime.text = gameDuration.ToString(@"mm\:ss");

            //process 100 line timer

            TimeSpan sprintTime;
            if (getHundredLine(game, out sprintTime))
            {
                hundredLineCaption.color = Color.white;
                hundredLine.text = sprintTime.ToString(@"mm\:ss");
                hundredLine.color = Color.white;
            }
            else
            {
                hundredLine.color = Color.black;
                hundredLineCaption.color = Color.black;
            }
        } 

        protected override void ChildOnEnable()
        {

        }
    }
}