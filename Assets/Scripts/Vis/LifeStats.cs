using UnityEngine.UI;
namespace NESTrisStatsViz
{
    public class LifeStats : AbstractAnimation
    {
        public override float Duration
        {
            get
            {
                return 5f;
            }
        }

        public Text linesCleared;
        public Text gamesPlayed;
        public Text totalSoftdrop;

        protected void UpdateText()
        {
            LifeTimeState lts = this.statsLogger.lifeTimeState;
            linesCleared.text = lts.TotalLines.ToString();
            gamesPlayed.text = lts.TotalGames.ToString();
            totalSoftdrop.text = lts.TotalSoftDrop.ToString();
        }

        public override void ChildUpdate()
        {
            UpdateText();
        }
    }
}