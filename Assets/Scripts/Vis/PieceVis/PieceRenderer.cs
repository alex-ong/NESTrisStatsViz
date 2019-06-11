using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz.PieceStats
{
    public enum PIECE_TYPE
    {
        T,
        J,
        Z,
        O,
        S,
        L,
        I,
        NONE
    }

 

    public class PieceRenderer : MonoBehaviour
    {
        public BlockHandler[] blocks;

        


        public int pieceWidth = 8;
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 scale = new Vector2(pieceWidth, pieceWidth);
                blocks[i].GetComponent<RectTransform>().sizeDelta = scale;
            }

        }

        protected BlockTextureGenerator.BlockType PIECE_TYPE_to_BlockType(PIECE_TYPE t)
        {
            switch (t)
            {
                case PIECE_TYPE.T:
                    return BlockTextureGenerator.BlockType.WHITE;
                case PIECE_TYPE.J:
                    return BlockTextureGenerator.BlockType.PRIMARY;
                case PIECE_TYPE.Z:
                    return BlockTextureGenerator.BlockType.SECONDARY;
                case PIECE_TYPE.O:
                    return BlockTextureGenerator.BlockType.WHITE;
                case PIECE_TYPE.S:
                    return BlockTextureGenerator.BlockType.PRIMARY;
                case PIECE_TYPE.L:
                    return BlockTextureGenerator.BlockType.SECONDARY;
                case PIECE_TYPE.I:
                    return BlockTextureGenerator.BlockType.WHITE;
                default:
                    return BlockTextureGenerator.BlockType.WHITE;
            }
        }
        public void SetPieceType(PIECE_TYPE t, int level)
        {
            switch (t)
            {
                case PIECE_TYPE.T:
                    blocks[0].transform.localPosition = new Vector2(-1f, 0.5f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(0f, 0.5f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(+1f, 0.5f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(0f, -0.5f) * pieceWidth;
                    break;
                case PIECE_TYPE.J:
                    blocks[0].transform.localPosition = new Vector2(-1f, 0.5f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(0f, 0.5f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(+1f, 0.5f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(+1f, -0.5f) * pieceWidth;
                    break;
                case PIECE_TYPE.Z:
                    blocks[0].transform.localPosition = new Vector2(-1f, 0.5f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(0f, 0.5f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(0f, -0.5f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(1f, -0.5f) * pieceWidth;
                    break;
                case PIECE_TYPE.O:
                    blocks[0].transform.localPosition = new Vector2(-0.5f, -0.5f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(-0.5f, 0.5f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(0.5f, -0.5f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(0.5f, 0.5f) * pieceWidth;
                    break;
                case PIECE_TYPE.S:
                    blocks[0].transform.localPosition = new Vector2(-1f, -0.5f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(0f, -0.5f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(0f, 0.5f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(1f, 0.5f) * pieceWidth;
                    break;
                case PIECE_TYPE.L:
                    blocks[0].transform.localPosition = new Vector2(-1f, -0.5f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(-1f, 0.5f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(+0f, 0.5f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(+1f, 0.5f) * pieceWidth;
                    break;
                case PIECE_TYPE.I:
                    blocks[0].transform.localPosition = new Vector2(-1.5f, 0f) * pieceWidth;
                    blocks[1].transform.localPosition = new Vector2(-0.5f, 0f) * pieceWidth;
                    blocks[2].transform.localPosition = new Vector2(+0.5f, 0f) * pieceWidth;
                    blocks[3].transform.localPosition = new Vector2(+1.5f, 0f) * pieceWidth;
                    break;
                case PIECE_TYPE.NONE:
                default:
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                blocks[i].SetImage(BlockTextureGenerator.Instance.getLevelSprite(level, BlockTextureGenerator.Border.ORIGINAL, PIECE_TYPE_to_BlockType(t)));
            }
        }

    }
}