using UnityEngine;

namespace DanPie.Framework.AudioManagement
{
    public abstract class AudioClipDataProvider : ScriptableObject
    {
        public abstract AudioClipData GetClipData();
    }
}
