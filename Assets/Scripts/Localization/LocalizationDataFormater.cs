using System;

// namespace SurviveTheRust.Assets.Localization
// {
    [Serializable]
    public class Languages
    {
        public const string EN = "en";
        public const string FR = "fr";
        public const string ES = "es";
        public const string DE = "de";
        public const string RU = "ru";
    }

    [Serializable]
    public struct LanguageData
    {
        public string key;
        public string en;
        public string fr;
        public string es;
        public string de;
    }

    [Serializable]
    public class StringsData
    {
        public LanguageData[] strings;
    }
// }
