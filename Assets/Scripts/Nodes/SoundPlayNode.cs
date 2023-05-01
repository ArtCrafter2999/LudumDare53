using UnityEngine;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/SoundPlayNode")]
    public class SoundPlayNode : NodeBase
    {
        [SerializeField] private AudioClip sound; 
        public override bool Invoke()
        {
            if (sound == null ||NodePlayer.source == null) return true;
            NodePlayer.source.clip = sound;
            NodePlayer.source.Play();
            return true;
        }
    }
}   