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

        public bool hasValues()
        {
            return (score.HasValue &&
                   lines.HasValue &&
                   level.HasValue &&
                   t.HasValue &&
                   j.HasValue &&
                   z.HasValue &&
                   o.HasValue &&
                   s.HasValue &&
                   l.HasValue &&
                   i.HasValue);
        }

        //only call if isValid!
        public int numPieces()
        {
            return (t.Value + j.Value + z.Value + o.Value + s.Value + l.Value + i.Value);
        }

        private static int? valueToInt(SimpleJSON.JSONNode node, string key)
        {
            if (node[key] == null)
            {
                return null;
            }
            else
            {                
                return int.Parse(node[key]);
            }
        }

        private int? score = null;
        private int? lines = null;
        private int? level = null;
        private int? t = null;
        private int? j = null;
        private int? z = null;
        private int? o = null;
        private int? s = null;
        private int? l = null;
        private int? i = null;

        public int? Score { get { return score; } }
        public int? Lines { get { return lines; } }
        public int? Level { get { return level; } }
        public int? T { get { return t; } }
        public int? J { get { return j; } }
        public int? Z { get { return z; } }
        public int? O { get { return o; } }
        public int? S { get { return s; } }
        public int? L { get { return l; } }
        public int? I { get { return i; } }
    }
}