using UnityEngine.SceneManagement;

namespace LudumDare53.Leveling
{
    public static class LevelManager
    {
        public static void NewGame()
        {
            DifficultyManager.SetDifficulty(0);
            SceneManager.LoadScene("Level"); //TODO: Змінити на назву сцени гри
            PauseManager.SetResume();
        }

        public static void ContinueGame()
        {
            SceneManager.LoadScene("Level"); //TODO: Змінити на назву сцени гри
            PauseManager.SetResume();
        }

        public static void NextLevel()
        {
            DifficultyManager.SetDifficulty(DifficultyManager.Difficulty+1);
            SceneManager.LoadScene("Level"); //TODO: Змінити на назву сцени гри
            PauseManager.SetResume();
        }

        public static void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu"); //TODO: Замінити на назву сцени головного меню
            PauseManager.SetResume();
        }
    }
}