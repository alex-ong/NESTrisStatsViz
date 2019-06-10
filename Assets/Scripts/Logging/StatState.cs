using UnityEngine;
namespace NESTrisStatsViz
{
    public class StatState
    {
        public StatState() { }
        public StatState(SimpleJSON.JSONNode node)
        {
            Score = valueToInt(node, "score");
            Lines = valueToInt(node, "lines");
            Level = valueToInt(node, "level");

        }

        //Only call if both area isValid!
        //Call on the old one...
        public StatState diff(StatState other)
        {
            StatState result = new StatState();
            result.Score = other.Score - this.Score;
            result.Lines = other.Lines - this.Lines;
            result.Level = other.Level - this.Level;
            return result;
        }

        private int valueToInt(SimpleJSON.JSONNode node, string key)
        {
            if (node[key] == null)
            {
                IsValidMainStats = false;
                return -1;
            }
            else
            {
                return int.Parse(node[key]);
            }
        }
        


        public bool IsValidMainStats { get; private set; } = true;
        public int Score { get; private set; }
        public int Lines { get; private set; }
        public int Level { get; private set; }
        public int T { get; private set; }
        public int J { get; private set; }
        public int Z { get; private set; }
        public int O { get; private set; }
        public int S { get; private set; }
        public int L { get; private set; }
        public int I { get; private set; }

        public bool NonZero { get { return Score != 0 || Lines != 0 || Level != 0; } }

        public override string ToString()
        {
            return Score.ToString() + "|" +
                    Lines.ToString() + "|" +
                    Level.ToString() + "|";
        }
    }
}