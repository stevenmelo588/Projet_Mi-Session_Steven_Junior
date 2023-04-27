using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] string musicName;

    private void Awake()
    {
        Addressables.LoadAssetAsync<AudioClip>(musicName).Completed += MusicPlayer_Completed;
    }

    private void MusicPlayer_Completed(AsyncOperationHandle<AudioClip> audioClip)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip.Result;
        audioSource.Play();
    }
}
