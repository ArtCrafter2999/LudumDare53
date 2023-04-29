using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerParameterSetter : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _parameterName;
    [SerializeField] private float _volume;

    [ContextMenu("SetData")]
    public void Set()
    {
        _audioMixer.SetFloat(_parameterName, _volume);
    }
}
