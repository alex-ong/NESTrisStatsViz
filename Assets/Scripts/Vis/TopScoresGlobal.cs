using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NESTrisStatsViz
{
    public class TopScoresGlobal : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                return 10;
            }
        }

        List<GameStateSummary> sortedData;
        public List<Text> scores;
        public List<Text> levels;
        public List<Text> lines;
        public List<Text> ratios;
        public List<Text> dates;

        private static string DateFormat = "yyyy/MM/dd";
        public override void FirstTimeSetup()
        {
            sortedData = new List<GameStateSummary>(statsLogger.lifeTimeState.games);
            sortedData.Sort(compareGSS);
        }

        public static int compareGSS(GameStateSummary a, GameStateSummary b)
        {
            if (a.score == b.score)
            {
                return b.linesCleared.CompareTo(a.linesCleared);
            }
            else
            {
                return b.score.CompareTo(a.score);
            }
        }

        protected override void ChildOnEnable()
        {
            sortedData = new List<GameStateSummary>(statsLogger.lifeTimeState.games);
            sortedData.Sort(compareGSS);
            RefreshText();
        }

        public override void ChildUpdate()
        {
            RefreshText();
        }

        protected void RefreshText()
        {
            int endindex = Mathf.Min(scores.Count, sortedData.Count);
            for (int i = 0; i < endindex; i++)
            {
                GameStateSummary gss = sortedData[i];
                scores[i].text = gss.score.ToString("000000");
                levels[i].text = gss.startLevel.ToString("00");
                lines[i].text = gss.linesCleared.ToString("00");
                int ratio = 0;
                if (gss.tetrisCount > 0)
                {
                    ratio = Mathf.RoundToInt(gss.tetrisCount / (float)gss.linesCleared * 100);
                }
                ratios[i].text = ratio.ToString("00") + "%";
                dates[i].text = gss.startTime.ToString(DateFormat);
            }
        }

    }
}