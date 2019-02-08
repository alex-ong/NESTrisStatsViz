using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public class GameStateSummary
    {
        public int score;
        public int startLevel;
        public int linesCleared;
        public int tetrisCount;
        public int softDrop;
        public string startTime;

        public GameStateSummary(string data)
        {
            data = data.Trim();
            string[] items = data.Split(new char[] { ',' });
            score = int.Parse(items[0]);
            startLevel = int.Parse(items[1]);
            linesCleared = int.Parse(items[2]);
            tetrisCount = int.Parse(items[3]);
            softDrop = int.Parse(items[4]);
            startTime = items[5];
        }

        public GameStateSummary(GameState gs)
        {
            score = gs.Score;
            startLevel = gs.startLevel;
            linesCleared = gs.LinesCleared;
            tetrisCount = gs.distClears[3];
            softDrop = gs.softDropTotal;
            startTime = gs.StartTime;
        }

        public string ExportToFile()
        {
            List<string> toJoin = new List<string>();
            toJoin.Add(score.ToString());
            toJoin.Add(startLevel.ToString());
            toJoin.Add(linesCleared.ToString());
            toJoin.Add(tetrisCount.ToString());
            toJoin.Add(softDrop.ToString());
            toJoin.Add(startTime.ToString());
            return string.Join(",", toJoin.ToArray());
        }

        public override string ToString()
        {
            return ExportToFile();
        }
    }
}