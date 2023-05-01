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
        public enum PauseCause
        {
            Player,
            GameMenu,
            Tutorial,
        }

        public static PauseCause Cause { get; set; }
        private static readonly Dictionary<Type, bool> Include = new();
        public static bool IsPaused { get; private set; } = true;
        public static readonly UnityEvent Pause = new();
        public static readonly UnityEvent Resume = new();

        public static void SetInclude(params Type[] values)
        {
            foreach (var include in values)
            {
                if (Include.ContainsKey(include)) Include[include] = true;
                else Include.Add(include, true);
            }
        }

        public static void SetExclude(params Type[] values)
        {
            foreach (var exclude in values)
            {
                if (Include.ContainsKey(exclude)) Include[exclude] = false;
                else Include.Add(exclude, false);
            }
        }
        public static bool IsOnPause(this object obj)
        {
            return IsPaused && IsIncluded(obj.GetType());
        }

        public static bool IsIncluded(Type value)
        {
            return !Include.ContainsKey(value) || Include[value];
        }
        public static void SetPause(PauseCause cause)
        {
            Cause = cause;
            IsPaused = true;
            Pause.Invoke();
        }
        public static void SetResume()
        {
            Resume?.Invoke();
            IsPaused = false;
        }
        public static void SetResume(PauseCause ifCause)
        {
            if(Cause!=ifCause) return;
            Resume?.Invoke();
            IsPaused = false;
        }
    }
}