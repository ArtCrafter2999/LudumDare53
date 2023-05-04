using System.Linq;
using DanPie.Framework.AudioManagement;
using LudumDare53.Boxes;
using UnityEngine;

namespace LudumDare53.Sounds
{
    [RequireComponent(typeof(BoxDamage))]
    public class TouchSoundMaker : MonoBehaviour
    {
        [SerializeField] private AudioClipDataProvider _hitSoundProvider;

        private AudioSourcesManager _manager;

        protected void Start()
        {
            _manager
                = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
            GetComponent<BoxDamage>().damaged.AddListener(PlayHit);
        }

        private void PlayHit(float _)
        {
            _manager.GetAudioSourceController().Play(_hitSoundProvider.GetClipData());
        }
    }
}