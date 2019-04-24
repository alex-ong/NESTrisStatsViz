using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace NESTrisStatsViz
{
    public static class MainConfig
    {
        private static INIParser ini;
        private static INIParser Ini { get { CheckIniParser(); return ini;} }
        private static void CheckIniParser()
        {
            if (ini == null)
            {
                string iniPath = Path.Combine(Application.streamingAssetsPath, "config.ini");
                ini = new INIParser(iniPath);
            }
        }

        public static string ReadValue(string SectionName, string Key, string defaultValue)
        {
            return Ini.ReadValue(SectionName, Key, defaultValue);
        }

        public static int ReadValue(string SectionName, string Key, int defaultValue)
        {
            return Ini.ReadValue(SectionName, Key, defaultValue);
        }

        public static bool ReadValue(string SectionName, string Key, bool defaultValue)
        {
            return Ini.ReadValue(SectionName, Key, defaultValue);
        }

        public static long ReadValue(string SectionName, string Key, long defaultValue)
        {
            return Ini.ReadValue(SectionName, Key, defaultValue);
        }

        public static double ReadValue(string SectionName, string Key, double defaultValue)
        {            
            return Ini.ReadValue(SectionName, Key, defaultValue);
        }

    }
}