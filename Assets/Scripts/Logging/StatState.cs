using UnityEngine;
namespace NESTrisStatsViz
{
    public class StatState
    {
        public StatState() { }
        public StatState(SimpleJSON.JSONNode node)
        {
            score = valueToInt(node, "score");
            lines = valueToInt(node, "lines");
            level = valueToInt(node, "level");
        }

        //Only call if both area isValid!
        //Call on the old one...
        public StatState diff(StatState other)
        {
            StatState result = new StatState();
            result.score = other.score - this.score;
            result.lines = other.lines - this.lines;
            result.level = other.level - this.level;
            return result;
        }

        private int valueToInt(SimpleJSON.JSONNode node, string key)
        {
            if (node[key] == null)
            {
                isValid = false;
                return -1;
            }
            else
            {
                return int.Parse(node[key]);
            }
        }

        private bool isValid = true;
        private int score;
        private int lines;
        private int level;

        public bool IsValid { get { return isValid; } }
        public int Score { get { return score; } }
        public int Lines { get { return lines; } }
        public int Level { get { return level; } }

        public bool NonZero { get { return score != 0 || lines != 0 || level != 0; } }

        public override string ToString()
        {
            return Score.ToString() + "|" +
                    Lines.ToString() + "|" +
                    Level.ToString() + "|";
        }
    }
}