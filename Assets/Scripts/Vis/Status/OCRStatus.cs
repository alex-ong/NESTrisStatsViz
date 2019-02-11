using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

namespace NESTrisStatsViz
{
    [RequireComponent(typeof(Image))]
    public class OCRStatus : MonoBehaviour
    {
        public StatsLogger statsLogger;
        private Image image;
        public bool AutoRestart = false;
        public float AutoRestartTime = 5.0f;
        private float lastRestartTime;
        public string PythonExe;
        public string WorkingDir;
        public string PythonScript;

        protected float EarliestRestartTime()
        {
            return lastRestartTime + AutoRestartTime;
        }

        private void Awake()
        {
            image = this.GetComponent<Image>();
            lastRestartTime = float.Epsilon;
            //todo; read python,workingdir,file from config.
        }

        private void ChangeAlpha(float a)
        {
            Color c = image.color;
            c.a = a;
            image.color = c;
        }
        // Update is called once per frame
        void Update()
        {
            float alpha = 1.0f;
            if ((Time.realtimeSinceStartup - statsLogger.LastMessageTimeStamp) > AutoRestartTime)
            {                
                alpha = (Mathf.Sin(Time.realtimeSinceStartup * 4.0f) * 0.5f) + 0.5f;
                
                if (AutoRestart && Time.realtimeSinceStartup > EarliestRestartTime())
                {
                    lastRestartTime = Time.realtimeSinceStartup;
                    RestartDaemon();
                }
            }

            ChangeAlpha(alpha);
        }

        protected int? lastPID = null;
        protected void RestartDaemon()
        {
            //kill if open
            if (lastPID != null)
            {
                try
                {
                    Process toKill = Process.GetProcessById(lastPID.Value);
                    toKill.Kill();
                }
                catch (InvalidOperationException) //already dead
                {
                    //pass.
                }
                lastPID = null;
            }

            //start.
            ProcessStartInfo psi = new ProcessStartInfo(PythonExe, PythonScript);
            psi.WorkingDirectory = WorkingDir;
            Process p = Process.Start(psi);
            lastPID = p.Id;

        }

        public void OnDestroy()
        {
            if (lastPID != null)
            {
                try
                {
                    Process toKill = Process.GetProcessById(lastPID.Value);
                    toKill.Kill();
                }
                catch (InvalidOperationException) //already dead
                {
                    //pass.
                }
                lastPID = null;
            }
        }
    }
}