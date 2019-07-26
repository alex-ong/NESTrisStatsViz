using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NESTrisStatsViz.LastTenGames
{
    public class LastGameGUI : MonoBehaviour
    {
        public Text startLevel;
        public Text score;
        public Text lines;
        public Image startLevelBlockA;
        public Image startLevelBlockB;

        public void UpdateGUI(GameStateSummary gss)
        {
            if (gss == null)
            {
                this.gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);            
            startLevel.text = gss.startLevel.ToString("00");
            score.text = gss.score.ToString("00");
            lines.text = gss.linesCleared.ToString("000");
            startLevelBlockA.sprite = BlockTextureGenerator.Instance.getLevelSprite(gss.startLevel, BlockTextureGenerator.Border.ORIGINAL, BlockTextureGenerator.BlockType.PRIMARY);
            startLevelBlockB.sprite = BlockTextureGenerator.Instance.getLevelSprite(gss.startLevel, BlockTextureGenerator.Border.ORIGINAL, BlockTextureGenerator.BlockType.SECONDARY);
        }
    }
}