using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

namespace NESTrisStatsViz.LineHistory
{
    public class LineHistory : MonoBehaviour
    {
        public BlockHandler prefab;
        private const int MAX_HISTORY = 20;
        public int blockSize;        
        public BlockHandler[][] blocks = new BlockHandler[MAX_HISTORY][];
        public void Awake()
        {
            for (int x = 0; x < MAX_HISTORY; x++)
            {
                blocks[x] = new BlockHandler[4];
                for (int y = 0; y < 4; y++)
                {
                    GameObject go = Instantiate(prefab.gameObject);
                    BlockHandler bh = go.GetComponent<BlockHandler>();
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.SetParent(this.gameObject.transform);
                    Vector3 location = new Vector3();
                    location.x = x * (blockSize+2);
                    location.y = y * blockSize;
                    rt.localPosition = location;
                    blocks[x][y] = bh;
                }
            }

        }

        public void Reset()
        {
            for (int x = 0; x < MAX_HISTORY; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    blocks[x][y].gameObject.SetActive(false);
                }
            }
        }

        public void SetHistory(int currentLevel, List<int> history)
        {
            history = history.LastN(MAX_HISTORY);
            for (int i = 0; i < MAX_HISTORY; i++)
            {
                int line = 0;
                if (i < history.Count)
                {
                    line = history[i];
                }
                SetLine(currentLevel, i, line);
            }
        }

        protected void SetLine(int currentLevel, int index, int line)
        {
            BlockTextureGenerator.BlockType type = (BlockTextureGenerator.BlockType)(line % 3);
            BlockTextureGenerator.Border border = BlockTextureGenerator.Border.BORDER;

            for (int y = 0; y < 4; y++)
            {
                if (y >= line)
                {
                    blocks[index][y].gameObject.SetActive(false);
                    continue;
                }
                BlockHandler block = blocks[index][y];
                block.SetGlow(line == 4 ? 1.0f : 0.0f);
                block.SetImage(BlockTextureGenerator.Instance.getLevelSprite(currentLevel, border, type));
                block.gameObject.SetActive(true);
            }
        }

    }
}