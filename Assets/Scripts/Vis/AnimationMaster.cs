using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public class AnimationMaster : MonoBehaviour
    {
        public Camera mainCamera;
        public StatsLogger statsLogger;
        public List<AbstractAnimation> animations;
        // Use this for initialization
        public int currentIndex = 0;
        private AbstractAnimation current;
        bool isPaused = false;

        public PauseSprite pauseSprite;

        void Awake()
        {
            foreach (AbstractAnimation anim in animations)
            {
                anim.statsLogger = statsLogger;
            }
        }

        private void Start()
        {
            foreach (AbstractAnimation anim in animations)
            {
                anim.FirstTimeSetup();
            }
        }

        private void Next()
        {

            current.gameObject.SetActive(false);
            currentIndex = (currentIndex + 1) % animations.Count;
            current = animations[currentIndex];
            current.gameObject.SetActive(true);

        }

        private void Prev()
        {
            current.gameObject.SetActive(false);
            currentIndex = ((currentIndex - 1) + animations.Count) % animations.Count;
            current = animations[currentIndex];
            current.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (current == null)
            {
                current = animations[currentIndex];
                current.gameObject.SetActive(true);
            }

            if (current.Timer >= current.Duration && !isPaused)
            {
                Next();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Next();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                isPaused = !isPaused;
                pauseSprite.ChangeSprite(isPaused);
            }
        }

    }
}