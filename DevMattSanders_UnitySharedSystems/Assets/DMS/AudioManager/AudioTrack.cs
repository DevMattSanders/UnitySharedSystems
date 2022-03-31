using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.AudioManager
{
    [CreateAssetMenu(menuName = "MattSanders/Audio/Audio Track")]
    public class AudioTrack : ScriptableObject
    {
        public AudioClip clip;

        [Button]
        public void Play()
        {
            SoAudioManager.instance.PlayMusic(clip);
        }
    }
}
