using System;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare53.Leveling
{
    [RequireComponent(typeof(Toggle))]
    public class PauseToggleController : MonoBehaviour
    {
        private Toggle _toggle;
        private bool _isMySet = false;

        protected void OnEnable()
        {
            _toggle = _toggle??GetComponent<Toggle>();
            _toggle.isOn = PauseManager.IsPaused;
            _toggle.onValueChanged.AddListener(OnToggled);
            PauseManager.Pause.AddListener(OnPause);
            PauseManager.Resume.AddListener(OnResume);
        }

        protected void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggled);
            PauseManager.Pause.RemoveListener(OnPause);
            PauseManager.Resume.RemoveListener(OnResume);
        }

        private void OnPause()
        {
            if (_isMySet)
            {
                return;
            }
            _toggle.isOn = true;
        }

        private void OnResume()
        {
            if (_isMySet)
            {
                return;
            }
            _toggle.isOn = false;
        }

        private void OnToggled(bool value)
        {
            _isMySet = true;
            if (value)
            {
                PauseManager.SetPause();
            }
            else
            {
                PauseManager.SetResume();
            }
            _isMySet = false;
        }
    }
}