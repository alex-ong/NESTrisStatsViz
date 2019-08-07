﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NESTrisStatsViz
{
    public class FallingBlockVidControl : MonoBehaviour
    {

        public FallingBlockVid blockVid;
        public GameObject platform;
        private enum State
        {
            Start,
            BlocksEnabled,
            Finish
        }
        private State s = State.Start;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (s == State.Start)
                {
                    this.blockVid.enabled = true;
                    s++;
                } else if (s == State.BlocksEnabled)
                {
                    platform.SetActive(false);
                    s++;
                }
            }
        }
    }
}