using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
namespace NESTrisStatsViz
{
    public class FallingBlocks : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                if (isPostTransition()) return 0;
                return 10f;
            }
        }

        private float EndSpawn { get { return Duration - 3f; } }
        private int numCubes { get { return games.Count; } }
        public override void ChildUpdate()
        {
            float perc = (Time.realtimeSinceStartup - startTimeStamp) / EndSpawn;
            int targetCubes = Mathf.FloorToInt(Mathf.Lerp(0, numCubes, perc));
            BlockTextureGenerator.Border b = BlockTextureGenerator.Border.BORDER;

            while (numCubesShown < targetCubes)
            {
                GameObject c = GameObject.Instantiate(cube);
                c.transform.SetParent(this.gameObject.transform);
                Vector2 circle = Random.insideUnitCircle * 9f;
                c.transform.position = new Vector3(circle.x, 0.0f, circle.y);
                c.SetActive(true);
                BlockTextureGenerator.BlockType blockType = (BlockTextureGenerator.BlockType)(numCubesShown % 3);
                Texture2D tex = BlockTextureGenerator.Instance.getLevelTexture(games[numCubesShown].startLevel, b, blockType);
                c.GetComponent<Renderer>().material.mainTexture = tex;
                //games[numCubesShown]
                cubesMade.Add(c);
                numCubesShown++;
            }
        }
        
        private int numCubesShown = 0;
        private float startTimeStamp;
        protected List<GameStateSummary> games;
        public GameObject cube;
        protected List<GameObject> cubesMade;

        protected override void ChildOnEnable()
        {
            numCubesShown = 0;
            startTimeStamp = Time.realtimeSinceStartup;
            games = statsLogger.lifeTimeState.games.LastN(1200);
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