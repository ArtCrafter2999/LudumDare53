using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace LudumDare53.Leveling
{
    public class DifficultyManager : MonoBehaviour
    {
        public static int Difficulty => PlayerPrefs.GetInt("DifficultyLevel", 0);
        public static void SetDifficulty(int level)
        {
            PlayerPrefs.SetInt("DifficultyLevel", level);
            PlayerPrefs.Save();
        }
    }
}