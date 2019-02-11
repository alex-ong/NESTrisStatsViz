using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public abstract class AbstractAnimation : MonoBehaviour
    {
        public Camera alternateCamera;
        public StatsLogger statsLogger;
        public abstract float Duration { get; }
        protected float _timer;
        public float Timer { get { return _timer; } }

        public virtual void FirstTimeSetup() { }

        //called everytime it is enabled
        public void OnEnable()
        {
            _timer = 0.0f;
            ChildOnEnable();
        }

        protected virtual void ChildOnEnable() { }

        public void OnDisable()
        {
            Hide();
        }

        protected virtual void Hide() { }

        public void Update()
        {
            _timer += Time.deltaTime;
            ChildUpdate();
        }
        public virtual void ChildUpdate() { }

    }
}