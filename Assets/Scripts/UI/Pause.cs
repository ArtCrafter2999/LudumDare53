using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace LudumDare53.UI
{
    public static class PauseManager
    {
        private static List<IPausable> Objects => Object.FindObjectsByType(typeof(IPausable), FindObjectsInactive.Exclude, FindObjectsSortMode.None).Select(o => o as IPausable).ToList();
        
        public static void SetPause()
        {
            Objects.ForEach(o => o.Pause());
        }
        public static void SetResume()
        {
            Objects.ForEach(o => o.Resume());
        }
    }
}