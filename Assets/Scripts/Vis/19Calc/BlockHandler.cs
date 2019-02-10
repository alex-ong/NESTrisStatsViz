using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NESTrisStatsViz
{
    public class BlockHandler : MonoBehaviour
    {
        public Image image;
        public Image glow;

        public void SetGlow(float a)
        {
            Color c = this.glow.color;
            c.a = a;
            this.glow.color = c;
        }

        public void SetImage(Sprite s)
        {
            this.image.sprite = s;
        }
    }
}