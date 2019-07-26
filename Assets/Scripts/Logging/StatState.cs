using UnityEngine;
namespace NESTrisStatsViz
{
    public class StatState
    {
        public StatState() { }
        public StatState(SimpleJSON.JSONNode node)
        {
            Score = valueToInt(node, "score", true);
            Lines = valueToInt(node, "lines");
            Level = valueToInt(node, "level");
            T = pieceStatToInt(node, "T");
            J = pieceStatToInt(node, "J");
            Z = pieceStatToInt(node, "Z");
            O = pieceStatToInt(node, "O");
            S = pieceStatToInt(node, "S");
            L = pieceStatToInt(node, "L");
            I = pieceStatToInt(node, "I");
        }

        //Only call if both area isValid!
        //Call on the old one...
        public StatState diff(StatState other)
        {
            StatState result = new StatState();
            result.Score = other.Score - this.Score;
            result.Lines = other.Lines - this.Lines;
            result.Level = other.Level - this.Level;
            result.T = other.T - this.T;
            result.J = other.J - this.J;
            result.Z = other.Z - this.Z;
            result.O = other.O - this.O;
            result.S = other.S - this.S;
            result.L = other.L - this.L;
            result.I = other.I - this.I;
            result.IsValidMainStats = other.IsValidMainStats || this.IsValidMainStats;
            result.IsValidPieceStats = other.IsValidPieceStats || this.IsValidPieceStats;

            return result;
        }

        private int HexToInt(string s)
        {
            return int.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }

        private int valueToInt(SimpleJSON.JSONNode node, string key, bool hexFirst = false)
        {
            if (node[key] == null)
            {
                IsValidMainStats = false;
                return -1;
            }
            else
            {
                if (hexFirst)
                {
                    string result = node[key];
                    string firstDigit = result.Substring(0, 1);
                    string remainder = result.Substring(1, result.Length - 1);
                    int first =  HexToInt(firstDigit) * 100000;
                    return int.Parse(remainder) + first;
                } else
                {
                    return int.Parse(node[key]);
                }
                
            }
        }

        private int pieceStatToInt(SimpleJSON.JSONNode node, string key)
        {
            if (node[key] == null)
            {
                IsValidPieceStats = false;
                return -1;
            }
            else
            {
                return int.Parse(node[key]);
            }
        }

        public bool IsValidMainStats { get; private set; } = true;
        public bool IsValidPieceStats { get; private set; } = true;
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