using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

namespace BossRush.Common
{
    public static class GameManager
    {
        public static List<LastBossAction> FinalBossAttempt = new List<LastBossAction>();

        public static string FinalBossFileName = "/FinalBossAttempts.sav";

        public static void ReloadSceneOnDeath()
        {
            string level = SceneManager.GetActiveScene().name;
            string levelToLoad = "";

            switch (level)
            {
                case "BeeBoss":
                    levelToLoad = "FirstOverworld";
                    break;
                case "ForestBoss":
                case "APIBoss":
                    levelToLoad = "SecondOverworld";
                    break;
                case "SimonBoss":
                    levelToLoad = "ThirdOverworld";
                    break;
                case "ElementalBoss":
                    levelToLoad = "ThirdOverworld";      // for testing
                    break;
                case "FinalBoss":
                    levelToLoad = "FinalBoss";
                    break;
                default:
                    levelToLoad = level;
                    break;

            }

            GameStateManager.CurrentScene = levelToLoad;
            GameStateManager.PreviousScene = level;
            SceneManager.LoadScene(levelToLoad);
        }

        public static KeyCode RollKey = KeyCode.LeftShift;
        public static KeyCode AttackKey = KeyCode.Space;

        public static void LogFinalBossAttempt()
        {
            var path = Application.persistentDataPath + FinalBossFileName;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(path, FileMode.Append);
            bf.Serialize(fs, FinalBossAttempt);
            fs.Close();

            FinalBossAttempt = new List<LastBossAction>();
        }
    }
}
