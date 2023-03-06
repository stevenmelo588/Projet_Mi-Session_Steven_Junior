using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool dontInterupt = true;
    public static AudioManager instance = null;
    
    [Header("[ Music - Background Sound Tracks ]")]
    [Tooltip("\t[DEFAULT = NULL/EMPTY]\n1- NULL/EMPTY = Gets the Audio Source component in this asset.\n2- OVERIDEN = Uses the referenced Audio Source.")]
    public AudioSource backgroundMusicAudioSource = null; // Gets the audio source component 
    [Tooltip("[DEFAULT = NULL/EMPTY]\nList of Audio Clips you wish to use")]
    public AudioClip[] backgroundAudioClips = null;

    [Header("[ UI - Sound Effects ]")]
    [Tooltip("\t[DEFAULT = NULL/EMPTY]\n1- NULL/EMPTY = Gets the Audio Source component in this asset.\n2- OVERIDEN = Uses the referenced Audio Source.")]
    public AudioSource uiAudioSource = null; // Gets the audio source component 
    [Tooltip("[DEFAULT = NULL/EMPTY]\nList of Audio Clips you wish to use for the UI")]
    public AudioClip[] uiAudioSFX = null;  

    private bool starting = false, ending = false;

    // [Range(0f, 1f)]
    float lerpTime = 0.0f;
    float currentAudioTime = 0.0f;
    const float MIN = -0.5f; //-0.25f default
    const float MAX = 1f;
    private const float LERP_MULTIPLIER = 0.18f;

    // Start is called before the first frame update
    void Awake()
    {
        Math.Round(currentAudioTime, 0);

        if (backgroundMusicAudioSource == null)
            backgroundMusicAudioSource = GetComponent<AudioSource>();

        backgroundMusicAudioSource.playOnAwake = false;


        // backgroundMusicAudioSource.clip = backgroundAudioClips[UnityEngine.Random.Range(0, backgroundAudioClips.Length)];
        // backgroundMusicAudioSource.playOnAwake = false;
        // // backgroundMusicAudioSource.volume = 0;
        // starting = true;
        // // currentAudioTime = Math.Round(backgroundMusicAudioSource.time, 3);
        // backgroundMusicAudioSource.Play();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (dontInterupt)
            UnityEngine.Object.DontDestroyOnLoad(gameObject);

        StartCoroutine(ChangeAudioClip());
        // StartCoroutine(ChangeAudioClip(backgroundMusicAudioSource.clip.length));

        // while (backgroundMusicAudioSource.volume != 1 && starting)
        // {
        //     Debug.Log("test");
        //     lerpTime = Mathf.Clamp01(lerpTime + (0.25f * Time.deltaTime));
        //     backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);

        //     if(backgroundMusicAudioSource.volume == 1){
        //         starting = false;
        //         break;
        //     }
        // }

        // StartCoroutine(CheckIfAudioStarted(backgroundMusicAudioSource.clip.length));
    }

    private void OnSelectionChange() {
       uiAudioSource.PlayOneShot(uiAudioSFX[0]);
    }

    private void LateUpdate() {
        StartCoroutine(CheckIfAudioStarted(backgroundMusicAudioSource.clip.length));
    }

    private void Update() {
        if(starting){
            ending = false;
            VolumeFadeIn();
        }

        if(ending){
            starting = false;
            VolumeFadeOut();
        }
        // lerpTime += (0.25f * Time.deltaTime);
        // Debug.Log("test");
            // lerpTime = Mathf.Clamp01(lerpTime + (0.25f * Time.deltaTime));
            // backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);

        StartCoroutine(CheckAudioClipTime());

        // StartCoroutine(CheckIfAudioStarted(backgroundMusicAudioSource.clip.length));
    }
    
    IEnumerator CheckAudioClipTime()
    {
        yield return new WaitForSeconds(1f);
        currentAudioTime = (float)Math.Round(backgroundMusicAudioSource.time, 0);
        // && backgroundMusicAudioSource.volume <= 1
        if (currentAudioTime >= (backgroundMusicAudioSource.clip.length - 5.0f))
        {   
            if(currentAudioTime == (backgroundMusicAudioSource.clip.length - 5.0f))
            {
                for (int i = 0; i < 1; i++){
                    ending = true; 
                    Debug.Log("Audio Ending");
                    StartCoroutine(StopCurrentAudioSource());
                }
            }

            // if (backgroundMusicAudioSource.volume != MIN)
            // {
            //     // VolumeFadeOut();
            //     ending = true; 
            //     Debug.Log("Bitch 1");
            // }
            // else
            // {
            //     Debug.Log("Bitch 2");

            //     ending = false;
            //     lerpTime = 0;
            //     backgroundMusicAudioSource.Stop();
            // }

            // if (backgroundMusicAudioSource.volume == MIN){
            // }
            // lerpTime = 0;

            // VolumeFadeOut();
            // lerpTime = Mathf.Clamp01(lerpTime + (0.10f * Time.deltaTime));

            // starting = false;
            // backgroundMusicAudioSource.volume = Mathf.Lerp(MAX, MIN, lerpTime);
        }
        
// && backgroundMusicAudioSource.volume <= 1


        if(currentAudioTime == 30f)
            Debug.Log("30 seconds");        

        Debug.Log($"Real Time : {Math.Round(Time.timeAsDouble, 0)}  | Time : {currentAudioTime} | Clip lenght - 5 : {(currentAudioTime == (backgroundMusicAudioSource.clip.length - 5))}");
    }

    IEnumerator CheckIfAudioStarted(float clipLengthSeconds){        

        // if (currentAudioTime == (backgroundMusicAudioSource.clip.length - 5) && backgroundMusicAudioSource.volume == 1)
        // {
        //     // lerpTime = 0;

        //     VolumeFadeOut();
        //     ending = true; 
        //     Debug.Log("Audio Ending");
        //     // lerpTime = Mathf.Clamp01(lerpTime + (0.10f * Time.deltaTime));

        //     // starting = false;
        //     // backgroundMusicAudioSource.volume = Mathf.Lerp(MAX, MIN, lerpTime);
        //     StartCoroutine(StopCurrentAudioSource());
        // }

        // if(/*backgroundMusicAudioSource.volume >= 0 &&*/ backgroundMusicAudioSource.volume != 1 && !ending){
        //     lerpTime = Mathf.Clamp01(lerpTime + (0.25f * Time.deltaTime));
        //     backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);
        // }
        
        yield return new WaitForSeconds(4);
        if (backgroundMusicAudioSource.volume == MAX && starting)
        {
            starting = false;
            lerpTime = 0;
            Debug.Log("Audio Started");
        }
        // if (currAudioSource.volume == 0 && ending)
        // {
        //     ending = false;
        //     Debug.Log("Audio Ended");
        // }
        // if (currAudioSource.time == (clipLengthSeconds - 5))
        // {
        //     ending = true; 
        //     Debug.Log("Audio Ended");
        //     audioClipTime = Mathf.Clamp01(audioClipTime + (0.25f * Time.deltaTime));

        //     // starting = false;
        //     currAudioSource.volume = Mathf.Lerp(MAX, MIN, audioClipTime);
        //     StartCoroutine(StopCurrentAudioSource());
        // }
    }

    IEnumerator ChangeAudioClip(float clipLengthSeconds = 0)
    {        
        lerpTime = 0;
        if(backgroundMusicAudioSource.clip != null)
            yield return null;
        // if (backgroundMusicAudioSource.volume != 1)
        // {
        //     Debug.Log("test");
        //     // lerpTime += (0.25f * Time.deltaTime);
        //     lerpTime = Mathf.Clamp01(lerpTime + (0.25f * Time.deltaTime));
        //     // lerpTime = Mathf.Clamp01(lerpTime + (0.25f * 0.5f));
        //     backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);

        //     // if(backgroundMusicAudioSource.volume == 1){
        //     //     starting = false;
        //     //     break;
        //     // }
        // }

        // backgroundMusicAudioSource.Play();

        // backgroundMusicAudioSource.volume = 0;

        // while (backgroundMusicAudioSource.volume <= 1 && starting)
        // {
        //     Debug.Log("test");
        //     lerpTime = Mathf.Clamp01(lerpTime + (0.25f * Time.deltaTime));
        //     backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);

        //     if(backgroundMusicAudioSource.volume == 1){
        //         starting = false;
        //         break;
        //     }
        // }
        // if(/*backgroundMusicAudioSource.volume >= 0 &&*/ backgroundMusicAudioSource.volume != 1 && !ending){
        //     lerpTime = Mathf.Clamp01(lerpTime + (0.25f * Time.deltaTime));
        //     backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);
        // }

        starting = true;
        backgroundMusicAudioSource.clip = backgroundAudioClips[UnityEngine.Random.Range(0, backgroundAudioClips.Length)];
        // backgroundMusicAudioSource.volume = 0;
        // currentAudioTime = Math.Round(backgroundMusicAudioSource.time, 3);
        backgroundMusicAudioSource.Play();

        if(backgroundMusicAudioSource.clip == null){
            backgroundMusicAudioSource.clip = backgroundAudioClips[UnityEngine.Random.Range(0, backgroundAudioClips.Length)];
        }

        clipLengthSeconds = backgroundMusicAudioSource.clip.length;

        Debug.Log($"Current Audio Clip : {backgroundMusicAudioSource.clip.name}\nAudio Source Lenght : {backgroundMusicAudioSource.clip.length}\nIs Starting : {starting}\nIs Ending : {ending}\n Clip length - 5 : {backgroundMusicAudioSource.clip.length - 5}\n");
 
        yield return new WaitForSeconds(clipLengthSeconds);
        backgroundMusicAudioSource.clip = backgroundAudioClips[UnityEngine.Random.Range(0, backgroundAudioClips.Length)];
        lerpTime = 0;
        starting = true;
        backgroundMusicAudioSource.Play();
        Debug.Log($"Current Audio Clip : {backgroundMusicAudioSource.clip.name}\nAudio Source Lenght : {backgroundMusicAudioSource.clip.length}\nIs Starting : {starting}\nIs Ending : {ending}\n Clip length - 5 : {backgroundMusicAudioSource.clip.length - 5}\n");

        clipLengthSeconds = backgroundMusicAudioSource.clip.length;
        yield return new WaitForSeconds(clipLengthSeconds);
        StartCoroutine(ChangeAudioClip(backgroundMusicAudioSource.clip.length)); // Testing (Pretty sure it wont work)
    }

    IEnumerator StopCurrentAudioSource()
    {
        // VolumeFadeOut();
        // ending = true; 
        // Debug.Log("Bitch 1");
        yield return new WaitForSeconds(6f);
        backgroundMusicAudioSource.Stop();
        ending = false;
        Debug.Log($"Ending : {ending}");
        backgroundMusicAudioSource.Play();

        // StartCoroutine(ChangeAudioClip());
    }

    void VolumeFadeIn(){
        // if (backgroundMusicAudioSource.volume < 1 && starting)
        // {
            // ending = false;
            // Debug.Log("test starting");
            // lerpTime = 0;
            // lerpTime += (0.25f * Time.deltaTime);
            lerpTime = Mathf.Clamp01(lerpTime + LERP_MULTIPLIER * Time.deltaTime); // 0.25f default
            // lerpTime = Mathf.Clamp01(lerpTime + (0.25f * 0.5f));
            backgroundMusicAudioSource.volume = Mathf.Lerp(MIN, MAX, lerpTime);

            // if(backgroundMusicAudioSource.volume == MAX)
            //     lerpTime = 0;

            // if(backgroundMusicAudioSource.volume == 1){
            //     starting = false;
            //     break;
            // }
        // }
    }

    void VolumeFadeOut(){
        // if (backgroundMusicAudioSource.volume == 1 && ending)
        // {
            // starting = false;
            // Debug.Log("test ending");
            // lerpTime += (0.25f * Time.deltaTime);
            lerpTime = Mathf.Clamp01(lerpTime + (LERP_MULTIPLIER * Time.deltaTime));
            // lerpTime = Mathf.Clamp01(lerpTime + (0.25f * 0.5f));
            backgroundMusicAudioSource.volume = Mathf.Lerp(MAX, MIN, lerpTime);

            // if(backgroundMusicAudioSource.volume == MIN)
            //     lerpTime = 0;

            // if(backgroundMusicAudioSource.volume == 1){
            //     starting = false;
            //     break;
            // }
        // }
    }
}