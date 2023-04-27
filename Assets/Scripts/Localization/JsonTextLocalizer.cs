using System;
// using SurviveTheRust.Assets.Localization;
using TMPro;
using UnityEngine;


public enum ButtonKeys
{
    start_button,
    options_button,
    exit_button,
    credits_button,
}

public class JsonTextLocalizer : MonoBehaviour
{
    public ButtonKeys btnKey;

    private TMP_Text TextComponent => GetComponent<TMP_Text>();

    [SerializeField] string key;

    public void LocateKey() => TextComponent.text = ReadJsonLocalizationData.ChangeGameLanguage(key);
    //public void LocateKey()
    //{
    //    TextComponent.text = ReadJsonLocalizationData.ChangeGameLanguage(key);
    //}

    private void Awake() => LocateKey();
    //private void Awake()
    //{
    //    LocateKey();
    //}
}
