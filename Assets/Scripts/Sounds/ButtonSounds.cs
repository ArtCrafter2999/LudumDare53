using System;
using DanPie.Framework.AudioManagement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare53.Sounds
{
    public class ButtonSounds : MonoBehaviour
    {
        [SerializeField] private AudioClipDataProvider hoverSound;
        [SerializeField] private AudioClipDataProvider clickSound;
        private AudioSourcesManager _manager;
        private void Start()
        {
            _manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
            var trigger = gameObject.AddComponent<EventTrigger>();
            var down = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            var hover = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            down.callback.AddListener((data) => { OnPointerDown(); });
            hover.callback.AddListener((data) => { OnPointerEnter(); });
            trigger.triggers.Add(down);
            trigger.triggers.Add(hover);
        }
        
        private void OnPointerDown()
        {
            _manager.GetAudioSourceController().Play(clickSound.GetClipData());
        }

        private void OnPointerEnter()
        {
            _manager.GetAudioSourceController().Play(hoverSound.GetClipData());
        }
        
    }
}