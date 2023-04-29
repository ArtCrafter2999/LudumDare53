using UnityEngine;

namespace DanPie.Framework.AudioManagement.Demo
{
    public class DemoSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClipData[] _clips;
        [SerializeField] private AudioSourcesManager _sourceProvider;

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _sourceProvider.GetAudioSourceController().Play(_clips[Random.Range(0, _clips.Length)]);
            }
        }
    }
}
