using System.Collections.Generic;
using System.IO;

namespace NESTrisStatsViz
{
    public class LifeTimeState
    {
        private int totalGames;
        private int totalLines;
        private int totalSoftDrop;

        public List<GameStateSummary> games = new List<GameStateSummary>();
        static string GAME_STATS = "game_stats.txt";
        static string BASE_DIR = "C:/NESTrisStats/data/";
        public GameState current;

        public int TotalGames { get { return totalGames; } }
        public int TotalLines { get { return totalLines + (current == null ? 0 : current.LinesCleared); } }
        public int TotalSoftDrop { get { return totalSoftDrop + (current == null ? 0 : current.softDropTotal); } }

        public LifeTimeState()
        {
            System.IO.Directory.CreateDirectory(BASE_DIR);
            LoadFromFile();
        }

        public void LoadFromFile()
        {
            if (File.Exists(BASE_DIR + GAME_STATS))
            {
                string[] data = File.ReadAllLines(BASE_DIR + GAME_STATS);
                foreach (string line in data)
                {
                    GameStateSummary gs = new GameStateSummary(line);
                    totalGames += 1;
                    totalLines += gs.linesCleared;
                    totalSoftDrop += gs.softDrop;
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
            GameStateSummary summary = new GameStateSummary(gs);
            //save the summary.
            if (File.Exists(BASE_DIR + GAME_STATS))
            {
                File.Copy(BASE_DIR + GAME_STATS, BASE_DIR + GAME_STATS + ".bak", true);
            }
            File.AppendAllText(BASE_DIR + GAME_STATS, summary.ExportToFile() + "\r\n");
            this.current = null;
        }
    }
}