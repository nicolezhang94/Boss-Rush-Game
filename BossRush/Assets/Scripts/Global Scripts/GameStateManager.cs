using UnityEngine.SceneManagement;

namespace BossRush.Common
{
    public static class GameStateManager
    {
        public static string CurrentScene = "FirstOverworld";
        public static string PreviousScene = "FirstOverworld";
        public static bool RedShieldBossDead = false;
        public static bool GreenShieldBossDead = false;
        public static bool RedJewelBossDead = false;
        public static bool GreenJewelBossDead = false;

        public static void SaveGame()
        {
            var output = "{";
            output += "RedShieldBossDead: " + RedShieldBossDead.ToString() + ",";
            output += "GreenShieldBossDead: " + GreenShieldBossDead.ToString() + ",";
            output += "RedJewelBossDead: " + RedJewelBossDead.ToString() + ",";
            output += "GreenJewelBossDead: " + GreenJewelBossDead.ToString() + ",";

            output += "}";

            //Do file IO to save game
        }

        public static void LoadGame()
        {
            //Do file IO to load game
        }
    }
}
