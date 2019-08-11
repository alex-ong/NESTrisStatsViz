
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public class FallingBlockVid : FallingBlocks
    {
        public int numBlocks = 500;
        public float duration = 7f;
        public int level = 9;
        public override float Duration
        {
            get
            {                
                return duration;
            }
        }

        protected const string fakeString0 = "000000, ";
        protected const string fakeString1 = ", 0, 0, 0, 1990/01/01 00:00:00, 1990/01/01 00:00:00, 0";
        
        protected override void ChildOnEnable()
        {
            numCubesShown = 0;
            startTimeStamp = Time.realtimeSinceStartup;
            List<GameStateSummary> gss = new List<GameStateSummary>();
            for (int i = 0; i < numBlocks; i++)
            {
                GameStateSummary summary = new GameStateSummary(fakeString0+level.ToString()+fakeString1);
                gss.Add(summary);
            }
            this.games = gss;
            cubesMade = new List<GameObject>();
        }


        protected override void Hide()
        {
            foreach (GameObject go in cubesMade)
            {
                Destroy(go);
            }
            cubesMade = null;
        }

    }
}