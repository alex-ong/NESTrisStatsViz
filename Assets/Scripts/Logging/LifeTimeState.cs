using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
namespace NESTrisStatsViz
{
    public class LifeTimeState : MonoBehaviour
    {
        private int totalGames;
        private int totalLines;
        private int totalSoftDrop;

        public List<GameStateSummary> games = new List<GameStateSummary>();

        public GameState current;

        public int TotalGames { get { return totalGames; } }
        public int TotalLines { get { return totalLines + (current == null ? 0 : current.LinesCleared); } }
        public int TotalSoftDrop { get { return totalSoftDrop + (current == null ? 0 : current.softDropTotal); } }
        private TimeSpan totalGameTime;
        public TimeSpan TotalGameTime { get { return totalGameTime + (current == null ? TimeSpan.Zero : current.Duration); } }

        static string GAME_STATS { get { return MainConfig.ReadValue("data", "file", "game_stats.txt"); } }
        static string BASE_DIR { get { return MainConfig.ReadValue("data", "basedir", "C:/NESTrisStats/data/"); } }
        static string DATABASE { get { return System.IO.Path.Combine(BASE_DIR, GAME_STATS); } }

        public void Awake()
        {
            System.IO.Directory.CreateDirectory(BASE_DIR);
            LoadFromFile();
        }

        public void LoadFromFile()
        {
            if (File.Exists(DATABASE))
            {
                string[] data = File.ReadAllLines(DATABASE);
                foreach (string line in data)
                {
                    GameStateSummary gs = new GameStateSummary(line);
                    totalGames += 1;
                    totalLines += gs.linesCleared;
                    totalSoftDrop += gs.softDrop;
                    totalGameTime += gs.duration;
                    games.Add(gs);
                }
            }
        }

        public void SetGameState(GameState gs)
        {
            this.current = gs;
        }

        public void FlushGameState()
        {
            if (this.current == null) return;
            GameState gs = this.current;
            totalGames += 1;
            totalLines += gs.LinesCleared;
            totalSoftDrop += gs.softDropTotal;
            totalGameTime += gs.Duration;
            GameStateSummary summary = new GameStateSummary(gs);
            games.Add(summary);

            //save the summary.
            if (File.Exists(DATABASE))
            {
                File.Copy(DATABASE, DATABASE + ".bak", true);
            }
            File.AppendAllText(DATABASE, summary.ExportToFile() + "\r\n");
            this.current = null;
        }
    }
}