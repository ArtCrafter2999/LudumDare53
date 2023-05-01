using UnityEngine;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/SoundStopNode")]
    public class SoundStopNode : NodeBase
    {
        public override bool Invoke()
        {
            NodePlayer.source.Stop();
            return true;
        }
    }
}   