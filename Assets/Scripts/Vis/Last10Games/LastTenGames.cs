using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;

namespace NESTrisStatsViz.LastTenGames
{

    public class LastTenGames : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                if (isPostTransition()) return 0;
                return 8f;
            }
        }
        public List<LastGameGUI> gameSummaries;
        public Text averageScore;

        public override void ChildUpdate()
        {
            List<GameStateSummary> gss = this.statsLogger.lifeTimeState.games;
            gss = gss.LastN(10);
            if (this.statsLogger.gameState != null)
            {
                gss.Add(new GameStateSummary(this.statsLogger.gameState));
            }
            gss.Reverse();

            int total = 0;
            int gameCount = 0;
            for (int i = 0; i < 10; i++)
            {
                GameStateSummary data = null;
                if (i < gss.Count)
                {
                    data = gss[i];
                    total += data.score;
                    gameCount += 1;
                } 
                
                gameSummaries[i].UpdateGUI(data);
            }

            if (gameCount > 0)
            {
                averageScore.text = Mathf.RoundToInt(total / (float)gameCount).ToString("000000");
            }
        }
    }
}