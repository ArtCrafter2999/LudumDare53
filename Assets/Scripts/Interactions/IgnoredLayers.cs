using UnityEngine;
using System.Linq;

namespace LudumDare53.Interactions
{
    [CreateAssetMenu(
        fileName = "IgnoredLayers", 
        menuName = "Interactions/IngoredLayers", 
        order = 0)]
    public class IgnoredLayers : ScriptableObject
    {
        [SerializeField] private string[] _ignoredLayersNames = new[] { "Ignore Raycast" };

        public int[] GetIgnoredLayers()
            => _ignoredLayersNames.Select((x) => LayerMask.NameToLayer(x)).ToArray();
    }
}