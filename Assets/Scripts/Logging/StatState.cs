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
            t = valueToInt(node, "T");
            j = valueToInt(node, "J");
            z = valueToInt(node, "Z");
            o = valueToInt(node, "O");
            s = valueToInt(node, "S");
            l = valueToInt(node, "L");
            i = valueToInt(node, "I");
        }

        //Only call if both area isValid!
        //Call on the old one...
        public StatState diff(StatState other)
        {
            StatState result = new StatState();
            result.score = other.score - this.score;
            result.lines = other.lines - this.lines;
            result.level = other.level - this.level;
            result.t = other.t - this.t;
            result.j = other.j - this.j;
            result.z = other.z - this.z;
            result.o = other.o - this.o;
            result.s = other.s - this.s;
            result.l = other.l - this.l;
            result.i = other.i - this.i;
            return result;
        }

        public int NumPieces
        {
            get { return t + j + z + o + s + l + i; }
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
        private int t;
        private int j;
        private int z;
        private int o;
        private int s;
        private int l;
        private int i;

        public bool IsValid { get { return isValid; } }
        public int Score { get { return score; } }
        public int Lines { get { return lines; } }
        public int Level { get { return level; } }
        public int T { get { return t; } }
        public int J { get { return j; } }
        public int Z { get { return z; } }
        public int O { get { return o; } }
        public int S { get { return s; } }
        public int L { get { return l; } }
        public int I { get { return i; } }
        public override string ToString()
        {
            return Score.ToString() + "|" +
                    Lines.ToString() + "|" +
                    Level.ToString() + "|" +
                T.ToString() + "," +
                J.ToString() + "," +
                Z.ToString() + "," +
                O.ToString() + "," +
                S.ToString() + "," +
                L.ToString() + "," +
                I.ToString() + ",";
        }
    }
}