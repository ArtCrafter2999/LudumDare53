using UnityEngine;

namespace LudumDare53.Leveling
{
    public class DifficultyUser : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] private int _resetDifficultyValue = 0;

        public void ResetDifficulty()
        {
            DifficultyManager.SetDifficulty(_resetDifficultyValue);
        }

        public void SetCustomDifficulty(int difficultyValue)
        {
            DifficultyManager.SetDifficulty(difficultyValue);
        }

        public void IncreaseDifficulty()
        {
            DifficultyManager.SetDifficulty(DifficultyManager.Difficulty + 1);
        }

        public void DecreaseDifficulty()
        {
            DifficultyManager.SetDifficulty(Mathf.Max(0, DifficultyManager.Difficulty - 1));
        }
    }
}