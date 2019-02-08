using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace NESTrisStatsViz
{
    public class GameStateSummary
    {
        public int score;
        public int startLevel;
        public int linesCleared;
        public int tetrisCount;
        public int softDrop;
        public DateTime startTime;
        public DateTime endTime;
        public TimeSpan duration;

        public static string DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
        public static string DurationFormat = "c";
        public GameStateSummary(string data)
        {
            data = data.Trim();
            string[] items = data.Split(new char[] { ',' });
            score = int.Parse(items[0]);
            startLevel = int.Parse(items[1]);
            linesCleared = int.Parse(items[2]);
            tetrisCount = int.Parse(items[3]);
            softDrop = int.Parse(items[4]);
            startTime = DateTime.Parse(items[5]);
            endTime = DateTime.Parse(items[6]);
            duration = TimeSpan.Parse(items[7]);
        }

        public GameStateSummary(GameState gs)
        {
            score = gs.Score;
            startLevel = gs.startLevel;
            linesCleared = gs.LinesCleared;
            tetrisCount = gs.distClears[3];
            softDrop = gs.softDropTotal;
            startTime = gs.StartTime;
            endTime = gs.FinishTime;
            duration = gs.Duration;
        }

        public string ExportToFile()
        {
            List<string> toJoin = new List<string>();
            toJoin.Add(score.ToString());
            toJoin.Add(startLevel.ToString());
            toJoin.Add(linesCleared.ToString());
            toJoin.Add(tetrisCount.ToString());
            toJoin.Add(softDrop.ToString());
            toJoin.Add(startTime.ToString(DateTimeFormat));
            toJoin.Add(endTime.ToString(DateTimeFormat));
            toJoin.Add(duration.ToString(DurationFormat));
            return string.Join(",", toJoin.ToArray());
        }

        public override string ToString()
        {
            return ExportToFile();
        }
    }
}