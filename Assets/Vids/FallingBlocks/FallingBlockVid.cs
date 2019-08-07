
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public class FallingBlockVid : FallingBlocks
    {
        public override float Duration
        {
            get
            {                
                return 10f;
            }
        }

        protected const string fakeString = "000000, 9, 0, 0, 0, 1990/01/01 00:00:00, 1990/01/01 00:00:00, 0";
        
        protected override void ChildOnEnable()
        {
            numCubesShown = 0;
            startTimeStamp = Time.realtimeSinceStartup;
            List<GameStateSummary> gss = new List<GameStateSummary>();
            for (int i = 0; i < 1700; i++)
            {
                GameStateSummary summary = new GameStateSummary(fakeString);
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