using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare53.Leveling
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private string _loadingLevelName = "main";

        public void LoadLevel()
        {
            SceneManager.LoadScene(_loadingLevelName);
        }
    }
}