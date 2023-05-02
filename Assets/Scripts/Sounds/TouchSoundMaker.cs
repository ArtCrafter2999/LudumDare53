using System.Linq;
using DanPie.Framework.AudioManagement;
using UnityEngine;

namespace LudumDare53.Sounds
{
    public class TouchSoundMaker : MonoBehaviour
    {
        [SerializeField] private float _forceThreshold = 3f;
        [SerializeField] private AudioClipDataProvider _hitSoundProvider;

        private AudioSourcesManager _manager;

        protected void Start()
        {
            _manager 
                = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contacts.Where(x => x.relativeVelocity.magnitude < _forceThreshold).Count() > 0)
            {
                _manager.GetAudioSourceController().Play(_hitSoundProvider.GetClipData());
            }

        }
    }
}
