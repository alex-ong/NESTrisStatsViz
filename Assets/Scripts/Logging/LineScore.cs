namespace NESTrisStatsViz
{
    public class LineScore
    {
        public LineScore(int lines, int score)
        {
            this.lineCount = lines;
            this.score = score;
        }

        private int lineCount;
        private int score;

        public int LineCount { get { return lineCount; } }

        public int Score { get { return score; } }
    }
}