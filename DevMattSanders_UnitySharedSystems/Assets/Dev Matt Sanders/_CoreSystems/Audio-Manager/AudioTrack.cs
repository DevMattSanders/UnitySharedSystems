using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
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
