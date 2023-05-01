using UnityEngine;

namespace LudumDare53.Leveling
{
    public class PauseUser : MonoBehaviour
    {
        public void SetPause()
        {
            PauseManager.SetPause(PauseManager.PauseCause.Player);
        }

        public void SetResume()
        {
            PauseManager.SetResume();
        }
    }
}