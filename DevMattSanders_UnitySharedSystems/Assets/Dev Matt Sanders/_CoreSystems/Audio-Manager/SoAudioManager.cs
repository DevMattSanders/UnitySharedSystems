using System.Collections;
using System.Collections.Generic;
using DevMattSanders._CoreSystems;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class SoAudioManager : GlobalScriptable
{
    public static SoAudioManager instance;
    public override void SoSetStartingValue()
    {
        base.SoSetStartingValue();
        instance = this;
        pooledAudioInstances.Clear();
        activeMusicInstances.Clear();
    }
    public override void SoEnd()
    {
        base.SoEnd();
        pooledAudioInstances.Clear();
        activeMusicInstances.Clear();
    }
    public FloatReference crossfadeTime;
    private GameObject audioInstanceParent;
    public GameObject audioInstancePrefab;

    [SerializeField] private List<AudioInstance> pooledAudioInstances = new List<AudioInstance>();
    [SerializeField] private List<AudioInstance> activeMusicInstances = new List<AudioInstance>();

    public void PlayMusic(AudioClip audioClip, bool? instant = false)
    {
        if (Application.isPlaying == false) return;
        if (!enabled) return;
        if (audioClip == null) return;

        foreach(AudioInstance nextAudioInstance in activeMusicInstances)
        {
            if (nextAudioInstance.audioClip == audioClip)
            {
                Debug.Log("Already playing this music track");
                return;
            }
        }

        StopAllMusic((bool)instant);

        AudioInstance audio = GetAudioInstance();
        audio.Play(audioClip, crossfadeTime.Value, crossfadeTime.Value, (bool) instant);

    }
    public void StopAllMusic(bool instant)
    {
        foreach (AudioInstance next in activeMusicInstances) next.Stop();
    }

    private AudioInstance GetAudioInstance()
    {       
        if(pooledAudioInstances.Count > 0)
        {
            AudioInstance i = pooledAudioInstances[0];
            pooledAudioInstances.Remove(i);
            i.gameObject.SetActive(true);
            activeMusicInstances.Add(i);
            return i;
        }
        else
        {
            if (audioInstanceParent == null) audioInstanceParent = new GameObject("Audio Instance Parent");
            GameObject.DontDestroyOnLoad(audioInstanceParent);

            AudioInstance i = GameObject.Instantiate(audioInstancePrefab, audioInstanceParent.transform).GetComponent<AudioInstance>();
            GameObject.DontDestroyOnLoad(i);
            activeMusicInstances.Add(i);
            return i;
        }
    }

    public void ReturnToPool(AudioInstance audioInstance)
    {
        activeMusicInstances.Remove(audioInstance);
        audioInstance.gameObject.SetActive(false);
        pooledAudioInstances.Add(audioInstance);
    }
}