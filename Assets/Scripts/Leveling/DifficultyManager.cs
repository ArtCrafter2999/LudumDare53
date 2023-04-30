using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace LudumDare53.Leveling
{
    public class DifficultyManager : MonoBehaviour
    {
        [CanBeNull] private static DifficultyManager _instance;
        [SerializeField] private List<DifficultyParams> paramsLevels;
        public static DifficultyManager Instance {
            get
            {
                if (_instance != null) return _instance;
                var o = new GameObject("DifficultyManager");
                var component = o.AddComponent<DifficultyManager>();
                return component;
            }
        }
    }
}