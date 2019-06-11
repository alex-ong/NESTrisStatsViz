using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz.PieceStats
{
    [RequireComponent(typeof(PieceRenderer))]
    public class SetPiece : MonoBehaviour
    {

        public StatsLogger sl;
        private PieceRenderer pr;
        public PIECE_TYPE pieceType;
        // Use this for initialization
        bool ready = false;
        int lastLevel = -1;

        void Awake()
        {
            this.pr = this.GetComponent<PieceRenderer>();
        }
        IEnumerator Start()
        {
            while (!BlockTextureGenerator.Instance.Ready)
            {
                yield return null;
            }
            pr.SetPieceType(pieceType, 0);
            ready = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!ready) return;

            if (sl.gameState != null)
            {
                if (sl.gameState.currentLevel != lastLevel)
                {
                    pr.SetPieceType(pieceType, sl.gameState.currentLevel % 10);
                    lastLevel = sl.gameState.currentLevel;
                }
            }
        }
    }
}
