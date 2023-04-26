using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSet : MonoBehaviour
{
    private Slider volSlider;
    [SerializeField] private AudioMixer AudioManager;
    [SerializeField] private string NameOfParameter;
    const float StartVol = 0.30f; // default : 0.30f

    // Start is called before the first frame update
    void Start()
    {
        volSlider = GetComponent<Slider>();
        float defaultVol = PlayerPrefs.GetFloat(NameOfParameter, StartVol);
        volSlider.value = defaultVol;
        VolSet(defaultVol);
    }

    public void VolSet(float volume) 
    {
        AudioManager.SetFloat(NameOfParameter, Mathf.Log10(volume) * 25f); //default : 30f
        PlayerPrefs.SetFloat(NameOfParameter, volume);
    }
}
