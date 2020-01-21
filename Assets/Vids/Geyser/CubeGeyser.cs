using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
namespace NESTrisStatsViz
{
    public class CubeGeyser: AbstractAnimation
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
        private int numCubes { get { return 500; } }
        public int level;
        public GameObject planes;
        public override void ChildUpdate()
        {
            float perc = (Time.realtimeSinceStartup - startTimeStamp) / EndSpawn;
            int targetCubes = Mathf.FloorToInt(Mathf.Lerp(0, numCubes, perc));
            BlockTextureGenerator.Border b = BlockTextureGenerator.Border.BORDER;

            while (numCubesShown < targetCubes)
            {
                GameObject c = GameObject.Instantiate(cube);
                c.transform.SetParent(this.gameObject.transform);
                Vector2 circle = Random.insideUnitCircle * 2f;
                c.transform.localPosition = new Vector3(circle.x, 0.0f, circle.y);
                c.SetActive(true);
                c.transform.localScale = Vector3.one *0.5f;
                BlockTextureGenerator.BlockType blockType = (BlockTextureGenerator.BlockType)(numCubesShown % 3);
                Texture2D tex = BlockTextureGenerator.Instance.getLevelTexture(level, b, blockType);
                c.GetComponent<Renderer>().material.mainTexture = tex;

                Vector2 xz = Random.insideUnitCircle * 300f;
                c.GetComponent<Rigidbody>().AddForce(new Vector3(xz.x, 400.0f, xz.y));
                cubesMade.Add(c);
                numCubesShown++;
            }
        }

        protected int numCubesShown = 0;
        protected float startTimeStamp;
        protected List<GameStateSummary> games;
        public GameObject cube;
        protected List<GameObject> cubesMade;

        protected override void ChildOnEnable()
        {
            numCubesShown = 0;
            startTimeStamp = Time.realtimeSinceStartup;
            //games = statsLogger.lifeTimeState.games.LastN(300);
            cubesMade = new List<GameObject>();
            planes.SetActive(false);
            
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