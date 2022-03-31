using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using DMS.Extensions;

namespace DMS.AudioManager
{
    public class AudioInstance : MonoBehaviour
    {
   
        [SerializeField] private AudioSource audioSource;
        public AudioClip audioClip;
   
        private Tween volumeTween;
        [SerializeField] private float fadeIn;
        [SerializeField] private float fadeOut;
        [SerializeField] private bool loop;
        [SerializeField] private bool stopping = false;
        [SerializeField] private bool paused;
        [SerializeField] private bool triggeredNextTrack = false;
        [SerializeField] private float clipDuration = 0;
        [SerializeField] private float routineTimer = 0;
    
        [Button]
        public void Pause()
        {
            if (triggeredNextTrack == false)
            {
                if (volumeTween != null) volumeTween.Kill();
                volumeTween = audioSource.DOFade(0, fadeOut).OnComplete(PauseFinished);
            }
        }

   
        private void PauseFinished()
        {
            if (triggeredNextTrack == false)
            {
                audioSource.Pause();
                paused = true;
            }
        }

        [Button]
        public void UnPause()
        {
            if (triggeredNextTrack == false)
            {
                if (volumeTween != null) volumeTween.Kill();

                paused = false;
                audioSource.UnPause();
                volumeTween = audioSource.DOFade(1, fadeIn);
            }
        
        }
    
        public void Play(AudioClip clip, float? _fadeIn = 0.5f, float _fadeOut = 0.5f,bool? _loop = false)
        {
            if (clip == null)
            {
                this.CallAtEndOfFrame(ReturnToPool);
                return;
            }

            if (AudioSourcePlayingRoutine != null) { StopCoroutine(AudioSourcePlayingRoutine); }

            stopping = false;
            triggeredNextTrack = false;
            audioClip = clip;
            clipDuration = clip.length;

       
            fadeIn = (float)_fadeIn;
            fadeOut = (float)_fadeOut;
            loop = (bool)_loop;

            audioSource.clip = clip;
            audioSource.Play();
            paused = false;

            if (volumeTween != null) volumeTween.Kill();

            volumeTween = audioSource.DOFade(1, fadeIn);


       
            AudioSourcePlayingRoutine = AudioSourcePlaying();
            StartCoroutine(AudioSourcePlayingRoutine);

        }

        private IEnumerator AudioSourcePlayingRoutine;
   
        private IEnumerator AudioSourcePlaying()
        {
            routineTimer = 0;
            while (true)
            {
                if (audioSource.isPlaying == false && paused == false)
                {
                    Debug.Log("Audio Source Unpuased & Finished");
                    break;
                }
                else
                {
                    if (audioSource.isPlaying)
                    {
                        routineTimer += Time.deltaTime;
                    }
                }

                if(clipDuration - routineTimer <= fadeOut)
                {
                    break;
                }

                yield return null;
            }

            TrackFinished();
        }

        private void TrackFinished()
        {
            Debug.Log("Track Finished");
            if (loop || stopping == true)
            {
                return;
            }

            triggeredNextTrack = true;
            Stop();
        }

        public void Stop(bool? instant = false)
        {
            if (AudioSourcePlayingRoutine != null) { StopCoroutine(AudioSourcePlayingRoutine); }
            stopping = true;

            if (volumeTween != null) volumeTween.Kill();

            if ((bool)instant == true)
            {
                ReturnToPool();
            }
            else
            {
                volumeTween = audioSource.DOFade(0, fadeOut).OnComplete(ReturnToPool);
            }
        }

        private void ReturnToPool()
        {
            audioSource.Stop();
            audioClip = null;

            SoAudioManager.instance.ReturnToPool(this);
        }   
    }
}
