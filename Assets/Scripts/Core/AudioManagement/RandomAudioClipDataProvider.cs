using System;
using System.Collections.Generic;
using DanPie.Framework.Randomnicity;
using UnityEngine;

namespace DanPie.Framework.AudioManagement
{
    [CreateAssetMenu(fileName = "RandomClipProvider", menuName = "AudioManagement/new RandomClipProvider")]
    public class RandomAudioClipDataProvider : AudioClipDataProvider
    {
        [Serializable]
        private class SelectableAudioClipData : IRandomSelectableItem
        {
            [SerializeField] private int _selectionChance;
            [SerializeField] private AudioClipData _clipData;

            public int SelectionChance { get => _selectionChance; }
            public AudioClipData ClipData { get => _clipData; }
        }

        [SerializeField] private List<SelectableAudioClipData> _clips;

        private RandomItemSelector<SelectableAudioClipData> _selector;

        public override AudioClipData GetClipData()
        {
            return _selector.GetRandomItem().ClipData;
        }

        protected void OnEnable()
        {
            if (_clips != null)
            {
                _selector = new RandomItemSelector<SelectableAudioClipData>(_clips);
            }
        }
    }
}
