using System;
using UnityEngine;
using UnityEngine.UI;

namespace NESTrisStatsViz
{
    [RequireComponent(typeof(Image))]
    public class PauseSprite : MonoBehaviour
    {
        public Sprite pause;
        public Sprite play;
        private Image image;

        public void Awake()
        {
            this.image = this.GetComponent<Image>();
        }

        public void ChangeSprite(bool isPaused)
        {
            if (isPaused)
            {
                this.image.sprite = pause;
            }
            else
            {
                this.image.sprite = play;
            }
        }
    }
}