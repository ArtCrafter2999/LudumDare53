using UnityEngine;

namespace LudumDare53.Leveling
{
    public class DifficultyChangedObjectDestroyer : MonoBehaviour
    {
        protected void OnEnable()
        {
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);            
        }

        protected void OnDisable()
        {
            DifficultyManager.DifficultyChanged.RemoveListener(OnDifficultyChanged);
        }

        private void OnDifficultyChanged()
        {
            Destroy(gameObject);
        }
    }
}