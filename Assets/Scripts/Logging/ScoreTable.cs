using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public static class ScoreTable
    {
        public static int lineMult(int linesCleared)
        {
            switch (linesCleared)
            {
                case 1:
                    return 40;
                case 2:
                    return 100;
                case 3:
                    return 300;
                case 4:
                    return 1200;
                default:
                    return 0;
            }
        }

        public static int getScore(int linesCleared, int level)
        {
            return lineMult(linesCleared) * (level + 1);
        }
 
    }
}