using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.OdinInspector;
using Unity.RemoteConfig;
public class AudioFromURL : MonoBehaviour
{

    public struct userAttributes { }
    public struct appAttributes { }

    [Button]
    private void Find()
    {
        ConfigManager.FetchCompleted += configMethod;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
       
    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= configMethod;
    }

    public string key = "Music_0001";
    void configMethod(ConfigResponse response)
    {
        retrievedValue = ConfigManager.appConfig.GetString(key);
        SetMusic();
    }

    public string retrievedValue;

    AudioSource audioSource;
    AudioClip myClip;
    public string url = "https://ciihuy.com/downloads/music.mp3";
    public AudioClip clip;

    void SetMusic()
    {
        
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(LoadSong());
        Debug.Log("Starting to download the audio...");
    }

    /*
    IEnumerator GetAudioClip()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(retrievedValue, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                clip = myClip;
                audioSource.clip = myClip;
                audioSource.Play();
                Debug.Log("Audio is playing.");
            }
        }
    }
    */

    public IEnumerator LoadSong()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(retrievedValue, AudioType.MPEG))
        {
            Debug.Log("1");
            yield return www.SendWebRequest();
            Debug.Log("2");
            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
                yield break;
            }

            // this allows for streaming audio which may not be your solution but it works in my case
            ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = true;
            Debug.Log("3");
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // do whatever you need to with the clip
                AudioClip myClip = ((DownloadHandlerAudioClip)www.downloadHandler).audioClip;
                clip = myClip;
                audioSource.clip = myClip;
                audioSource.Play();
                Debug.Log("Audio is playing.");
            }
        }
    }

    [Button]
    public void pauseAudio()
    {
        audioSource.Pause();
    }
    [Button]
    public void playAudio()
    {
        audioSource.Play();
    }
    [Button]
    public void stopAudio()
    {
        audioSource.Stop();

    }
}
