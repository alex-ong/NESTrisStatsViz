using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz.NineteenConverter
{
    public class BlockMaster : MonoBehaviour
    {
        public int xOffset;
        public int yOffset;
        private static int NUM_LEVELS = 11;
        private static int START_LEVEL = 19;
        public BlockHandler prefab;
        protected BlockHandler[][] data = new BlockHandler[NUM_LEVELS][];
        bool inited = false;
        private void Awake()
        {
            for (int x = 0; x < NUM_LEVELS; x++)
            {
                data[x] = new BlockHandler[10];

                for (int y = 0; y < 10; y++)
                {
                    GameObject go = GameObject.Instantiate(prefab.gameObject);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.SetParent(this.transform, true);
                    Vector3 pos = new Vector3();
                    pos.x = x * xOffset;
                    pos.y = y * yOffset;
                    rt.localPosition = pos;
                    //go.SetActive(true);
                    data[x][y] = go.GetComponent<BlockHandler>();
                }
            }
            inited = true;
        }

        public void Reset()
        {
            if (!inited) return;
            for(int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    data[i][j].gameObject.SetActive(false);
                }
            }
        }

        public void SetLine(int numLines, int finalLine)
        {
            int level = finalLine / 10;
            level += START_LEVEL;

            for (int i = finalLine - numLines; i < finalLine; i++)
            {
                BlockHandler block = GetBlock(i);
                if (block == null) { break; }
                BlockTextureGenerator.Border border;
                BlockTextureGenerator.BlockType blockType;
                float alpha;
                GetSprite(numLines, out border, out blockType, out alpha);
                Sprite s = BlockTextureGenerator.Instance.getLevelSprite(level, border, blockType);
                block.SetImage(s);
                block.SetGlow(alpha);
                block.gameObject.SetActive(true);
            }
        }

        public void GetSprite(int numLines, out BlockTextureGenerator.Border border, out BlockTextureGenerator.BlockType type, out float alpha)
        {
            alpha = 0.0f;
            border = BlockTextureGenerator.Border.BORDER;
            switch (numLines)
            {
                default:
                case 1:
                    type = BlockTextureGenerator.BlockType.WHITE;
                    break;
                case 2:
                    type = BlockTextureGenerator.BlockType.PRIMARY;
                    break;
                case 3:
                    type = BlockTextureGenerator.BlockType.SECONDARY;
                    break;
                case 4:
                    type = BlockTextureGenerator.BlockType.SECONDARY;
                    alpha = 1.0f;
                    break;
            }
        }

        protected BlockHandler GetBlock(int blockNumber)
        {
            blockNumber += 1;
            if (blockNumber >= 10 * NUM_LEVELS)
            {
                return null;
            }
            return data[blockNumber / 10][blockNumber % 10];
        }
    }
}