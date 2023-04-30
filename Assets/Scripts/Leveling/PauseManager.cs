using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare53.Leveling
{
    public static class PauseManager
    {
        private static bool _isPaused = true;
        public static bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (value) SetPause();
                else SetResume();
            } 
        }
        public static UnityEvent Pause = new();
        public static UnityEvent Resume = new();
        public static void SetPause()
        {
            Pause.Invoke();
            _isPaused = true;
        }
        public static void SetResume()
        {
            Resume?.Invoke();
            _isPaused = false;
        }
    }
}