// using System.Collections;
// using System.Collections.Generic;
using TMPro;
using UnityEngine;

// namespace SurviveTheRust.Assets.Scripts.Localization
// {
    public class SpreadSheetTextLocalizer : MonoBehaviour
    {
        private TMP_Text TextComponent => GetComponent<TMP_Text>();

        [SerializeField] private string key;

        public void LocateKey() => TextComponent.text = ReadSpreadSheetLocalizationData.ChangeGameLanguage(key);
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
// }
