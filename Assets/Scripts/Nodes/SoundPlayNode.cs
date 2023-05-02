using DanPie.Framework.AudioManagement;
using UnityEngine;

namespace LudumDare53.Nodes
{
    [CreateAssetMenu(menuName = "Nodes/SoundPlayNode")]
    public class SoundPlayNode : NodeBase
    {
        [SerializeField] private AudioClipDataProvider sound;
        private AudioSourcesManager _manager;

        public override void Init()
        {
            _manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
        }
        public override bool Invoke()
        {
            if (sound == null ||_manager == null) return true;
            _manager.GetAudioSourceController().Play(sound.GetClipData());
            return true;
        }
    }
}   