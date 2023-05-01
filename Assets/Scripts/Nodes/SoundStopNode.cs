using UnityEngine;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/SoundStopNode")]
    public class SoundStopNode : NodeBase
    {
        public override bool Invoke()
        {
            if (NodePlayer.source == null) return true;
            NodePlayer.source.Stop();
            return true;
        }
    }
}   