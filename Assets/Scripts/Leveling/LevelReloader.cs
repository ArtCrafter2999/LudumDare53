using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare53.Leveling
{
    public class LevelReloader : MonoBehaviour
    {
        public void ReloadThisLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}