using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NESTrisStatsViz
{
    [RequireComponent(typeof(Image))]
    public class LinearSpriteAnimation : MonoBehaviour
    {
        public List<Sprite> sprites;
        private Image image;
        float index = 0;
        public float fps = 10;
        private void Awake()
        {
            image = this.GetComponent<Image>();
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            index += Time.deltaTime * fps;
            index %= sprites.Count;
            image.sprite = sprites[Mathf.FloorToInt(index)];
        }
    }
}