using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

namespace NESTrisStatsViz.PieceStats
{
    public class PieceStatViz : MonoBehaviour
    {
        public PieceStats ps;
        public BlockHandler prefab;
        public int blockYOffset;
        public Vector2 topLeft;
        public int blockSize;
        private const int HISTORY_LENGTH = 42;
        bool ready = false;
        protected BlockHandler[,] data;
        void Awake()
        {
            data = new BlockHandler[7, HISTORY_LENGTH];
            ps.OnGameStateChange += UpdatePieceStatViz;
            ps.OnGameReset += ResetPieceStatViz;
        }



        public IEnumerator Start()
        {

            while (!BlockTextureGenerator.Instance.Ready)
            {
                yield return null;
            }

            for (int piece = 0; piece < 7; piece++)
            {
                int yOffset = -blockYOffset * piece;
                for (int x = 0; x < HISTORY_LENGTH; x++)
                {
                    GameObject go = GameObject.Instantiate(prefab.gameObject);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.SetParent(this.transform);
                    rt.sizeDelta = new Vector2(blockSize, blockSize);
                    rt.localPosition = topLeft + new Vector2(x * blockSize, yOffset);
                    go.SetActive(true);
                    data[piece, x] = go.GetComponent<BlockHandler>();
                    data[piece, x].SetImage(BlockTextureGenerator.Instance.getLevelSprite(0,
                        BlockTextureGenerator.Border.ORIGINAL,
                        BlockTextureGenerator.BlockType.WHITE));
                    go.SetActive(false);

                }
                yield return null;
            }
            ready = true;
        }

        private void UpdatePieceStatViz()
        {
            if (!ready) return;
            if (ps.statsLogger.gameState != null)
            {
                int level = ps.statsLogger.gameState.currentLevel;
                List<string> history = ps.statsLogger.gameState.pieceHistory;
                history = history.LastN(HISTORY_LENGTH);
                for (int piece = 0; piece < 7; piece++)
                {
                    string pieceString = PIECE_TYPE_toString((PIECE_TYPE)piece);
                    for (int i = 0; i < history.Count; i++)
                    {
                        if (pieceString == history[i])
                        {
                            data[piece, i].gameObject.SetActive(true);
                            data[piece, i].SetImage(BlockTextureGenerator.Instance.getLevelSprite(level,
                                                    BlockTextureGenerator.Border.ORIGINAL,
                                                    BlockTextureGenerator.BlockType.WHITE));
                        }
                        else
                        {
                            data[piece, i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        private void ResetPieceStatViz()
        {
            for (int piece = 0; piece < 7; piece++)
            {
                for (int i = 0; i < HISTORY_LENGTH; i++)
                {
                    data[piece, i].gameObject.SetActive(false);
                }
            }
        }

        private static string PIECE_TYPE_toString(PIECE_TYPE p)
        {
            switch (p)
            {
                case PIECE_TYPE.T:
                    return "T";
                case PIECE_TYPE.J:
                    return "J";
                case PIECE_TYPE.Z:
                    return "Z";
                case PIECE_TYPE.O:
                    return "O";
                case PIECE_TYPE.S:
                    return "S";
                case PIECE_TYPE.L:
                    return "L";
                case PIECE_TYPE.I:
                    return "I";
                default:
                    return "None";
            }

        }
    }
}