using System;

namespace NESTrisStatsViz
{
    public class LineScore
    {
        public LineScore(int lines, int score, TimeSpan time)
        {
            this.lineCount = lines;
            this.score = score;
            this.time = time;
        }

        private int lineCount;
        private int score;
        private TimeSpan time;

        public int LineCount { get { return lineCount; } }

        public int Score { get { return score; } }
        public TimeSpan Time {  get { return time; } }
    }
}