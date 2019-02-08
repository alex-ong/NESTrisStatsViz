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
                return 5;
            }
        }

        public Text totalTime;
        public Text gameTime;
        public override void ChildUpdate()
        {
            TimeSpan total = this.statsLogger.lifeTimeState.TotalGameTime;
            TimeSpan game = this.statsLogger.gameState == null ? TimeSpan.Zero : this.statsLogger.gameState.Duration;
            totalTime.text = total.Days.ToString() + " days " + total.ToString(@"hh\:mm\:ss");
            gameTime.text = game.ToString(@"mm\:ss");
        }

        protected override void ChildOnEnable()
        {

        }
    }
}