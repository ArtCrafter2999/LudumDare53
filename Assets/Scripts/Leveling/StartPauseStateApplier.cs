using UnityEngine;

namespace LudumDare53.Leveling
{
    public class StartPauseStateApplier : MonoBehaviour
    {
        [SerializeField] private bool _isPause;

        public void Start()
        {
            if (_isPause)
            {
                PauseManager.SetPause(PauseManager.PauseCause.Player);
            }
            else
            {
                PauseManager.SetResume();
            }
        }
    }
}