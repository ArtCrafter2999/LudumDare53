using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Leveling
{
    public class DifficultyManager : MonoBehaviour
    {
        public static int Difficulty => PlayerPrefs.GetInt("DifficultyLevel", 0);
        public static readonly UnityEvent DifficultyChanged = new();
        public static void SetDifficulty(int level)
        {
            PlayerPrefs.SetInt("DifficultyLevel", level);
            PlayerPrefs.Save();
            DifficultyChanged.Invoke();
        }
    }
}