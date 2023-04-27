using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// namespace SurviveTheRust.Assets.Localization
// {
    internal enum Language
    {
        En = 0,
        Fr = 1,
        Es = 2,
        De = 3,
        Ru = 4,
    }

    //: MonoBehaviour
    public class ReadJsonLocalizationData
    {
        //private static readonly string pathToStreamingAssets = Application.streamingAssetsPath; //Move to GameManager -> Moved
        private static string PathToJson => Path.Combine(GameManager.PathToStreamingAssets, "strings.json");
        private static StringsData JsonData => JsonUtility.FromJson<StringsData>(File.ReadAllText(PathToJson));

        //Step 3
        //private static readonly Dictionary<string, string> JsonStringsData = new();

        //Step 3.5 : Small Optimization
        //private static readonly IDictionary<string, string> JsonStringsData = new Dictionary<string, string>();

        //Step 5
        public static readonly IDictionary<string, string[]> JsonStringsData = new Dictionary<string, string[]>();
        public static string CurrentLanguage { get; set; }

        #region Commented Code

        //public static string CurrentLanguage { get; internal set; }

        //private void OnEnable()
        //{
        //    //data = JsonUtility.FromJson<StringsData>(File.ReadAllText(pathToJson));
        //}

        //private void Awake()
        //{


        //    //foreach(LanguageData s in data.strings)
        //    //{
        //    //    languages.Add(s.en);
        //    //    languages.Add(s.fr);
        //    //    languages.Add(s.es);
        //    //    languages.Add(s.de);
        //    //}

        //    //for (int i = 0; i < data.strings.Length; i++)
        //    //{
        //    //    stringsData.Add(data.strings[i].key, data.strings[i].en); //en, data.strings[i].fr, data.strings[i].es, data.strings[i].de
        //    //}


        //    //Debug.Log(data.strings.Length); 
        //    //string pathToJson = Path.Combine(Application.streamingAssetsPath, "strings.json");

        //    //foreach (var item in data.strings)
        //    //{
        //    //    for(int i = 0; i < data.strings.Length; i++)
        //    //        stringsData.Add(item.key, data.strings[i].ToString());
        //    //}
        //    //foreach (LanguageData key in t.strings)
        //    //{
        //    //    stringsData.Add(key.key, );
        //    //}
        //}

        // private void Start() {

        // }

        //static Languages languages = new Languages();

        //private void Awake()
        //{
        //    //for (int i = 0; i < JsonData.strings.Length; i++)
        //    //JsonStringsData.Add(JsonData.strings[i].key, new string[] { JsonData.strings[i].en, JsonData.strings[i].fr, JsonData.strings[i].es, JsonData.strings[i].de });
        //    //if (JsonData.strings[i].key == "key")

        //    //foreach (KeyValuePair<string, string[]> item in JsonStringsData)
        //    //Debug.Log($"{item.Key} | {item.Value[_ = (CurrentLanguage == Languages.EN) ? (int)LanguagesTypes.En : (CurrentLanguage == Languages.FR) ? (int)LanguagesTypes.Fr : (CurrentLanguage == Languages.ES) ? (int)LanguagesTypes.Es : (CurrentLanguage == Languages.DE) ? (int)LanguagesTypes.De : (int)LanguagesTypes.En]}");

        //}

        //public static void InitDictionary()
        //{
        //    //for (int i = 0; i < JsonData.strings.Length; i++)
        //    //if (!JsonStringsData.ContainsKey(JsonData.strings[i].key))
        //    //JsonStringsData.Add(JsonData.strings[i].key, new string[] { JsonData.strings[i].en, JsonData.strings[i].fr, JsonData.strings[i].es, JsonData.strings[i].de });
        //    //else
        //    //    continue;
        //}

        #endregion

        public static void PopulateLanguageDictionary()
        {
            for (int i = 0; i < JsonData.strings.Length; i++)
                if (!JsonStringsData.ContainsKey(JsonData.strings[i].key))
                    JsonStringsData.Add(JsonData.strings[i].key, new string[]
                    {
                        JsonData.strings[i].en,
                        JsonData.strings[i].fr,
                        JsonData.strings[i].es,
                        JsonData.strings[i].de,
                        $"{JsonData.strings[i].key}/Russian"
                    });
        }


        ///<summary>
        /// <para name="key">Step 1 : find the lenght of the array</para>
        /// <para>Step 2 : display the text in the specified language (for loop) (en -> english)                         Key  ,  Value</para>
        /// <para>Step 3 : Store the values contained inside of the (Json Files) strings array into a <c>Dictionary <para>Key</para>,<para>Value</para>></c> <value>string, string</value></para>
        /// <para>Step 4 : be able to change the language at runtime</para>
        /// <para>Step 5 : Optimize the solution using a Dictionary string, string[] (push it to the extremes - one liner edition)</para> 
        ///</summary>
        public static string ChangeGameLanguage(string key)
        {
            //Debug.Log(JsonData.strings.Length); //Step 1

            //return JsonData.strings[0].en; //Step 1.5

            //for (int i = 0; i < JsonData.strings.Length; i++) //Step 2
            //{
            //    if (JsonData.strings[i].key == key)
            //    {
            //        return JsonData.strings[i].en;
            //    }
            //}

            //for (int i = 0; i < JsonData.strings.Length; i++) //Step 3.A : Store the Key Value Pairs inside of a Dictionary<string, string>
            //{
            //    if (JsonData.strings[i].key == key)
            //    {
            //        JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en); //at each key the specified language will be added to the Dictionary
            //        //return JsonData.strings[i].en;
            //    }
            //}

            //foreach (KeyValuePair<string, string> item in JsonStringsData) //Step 3.B : Display the stored values using a KeyValuePair<string, string>
            //{
            //    if (item.Key == key)
            //    {
            //        Debug.Log(item.Key);
            //        return item.Value;
            //    }
            //}

            //Must clear the Dictionary before changing languages since there could be duplicate Key Values
            //JsonStringsData.Clear();

            //Step 4 : Display the UI by providing the language you want using an Input
            //switch (CurrentLanguage)
            //{
            //    //case "en":
            //    //    for (int i = 0; i < JsonData.strings.Length; i++)
            //    //    {
            //    //        JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en);
            //    //    }
            //    //    break;
            //    //case "fr":
            //    //    for (int i = 0; i < JsonData.strings.Length; i++)
            //    //        JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].fr);
            //    //    break;
            //    //case "es":
            //    //    for (int i = 0; i < JsonData.strings.Length; i++)
            //    //        JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].es);
            //    //    break;
            //    //case "de":
            //    //    for (int i = 0; i < JsonData.strings.Length; i++)
            //    //        JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].de);
            //    //    break;
            //    //default:
            //    //    for (int i = 0; i < JsonData.strings.Length; i++)
            //    //        JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en);
            //    //    break;
            //    case Languages.EN:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en);
            //        break;
            //    case Languages.FR:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].fr);
            //        break;
            //    case Languages.ES:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].es);
            //        break;
            //    case Languages.DE:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].de);
            //        break;
            //    case Languages.RU:
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, $"{JsonData.strings[i].key}/Russian");
            //        break;
            //    default:
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en);
            //        break;
            //}

            //Step 4.B : Display the stored values using a KeyValuePair< string, string>
            //foreach (KeyValuePair<string, string> item in JsonStringsData)
            //{
            //    if (item.Key == key)
            //    {
            //        Debug.Log(item.Key);
            //        return item.Value;
            //    }
            //}

            //return null;

            //Step 5 : Push it to the limit
            //for (int i = 0; i < JsonData.strings.Length; i++)
            //    if (!JsonStringsData.ContainsKey(JsonData.strings[i].key))
            //        JsonStringsData.Add(JsonData.strings[i].key, new string[]
            //        {
            //            JsonData.strings[i].en,
            //            JsonData.strings[i].fr,
            //            JsonData.strings[i].es,
            //            JsonData.strings[i].de,
            //            $"{JsonData.strings[i].key}/Russian"
            //        });
                // else
                //     continue;

            return (from item in JsonStringsData
                where item.Key == key
                select item.Value[_ = CurrentLanguage switch
                {
                    Languages.EN => (int)Language.En,
                    Languages.FR => (int)Language.Fr,
                    Languages.ES => (int)Language.Es,
                    Languages.DE => (int)Language.De,
                    Languages.RU => (int)Language.Ru,
                    _ => (int)Language.En
                }]).FirstOrDefault();

            // foreach (KeyValuePair<string, string[]> item in JsonStringsData)
            //     if (item.Key == key)    //(CurrentLanguage == "") ? (int)LanguagesTypes.En :
            //         return item.Value[_ = (CurrentLanguage == Languages.EN) ? (int)Language.En :
            //                               (CurrentLanguage == Languages.FR) ? (int)Language.Fr :
            //                               (CurrentLanguage == Languages.ES) ? (int)Language.Es :
            //                               (CurrentLanguage == Languages.DE) ? (int)Language.De :
            //                               (CurrentLanguage == Languages.RU) ? (int)Language.Ru :
            //                               (int)Language.En];
            //
            // return null;

            #region Previous Code

            //JsonStringsData.Clear();

            //foreach (KeyValuePair<string, string[]> item in JsonStringsData)
            //    Debug.Log($"{item.Key} | {item.Value[_ = (CurrentLanguage == Languages.EN) ? (int)LanguagesTypes.En : (CurrentLanguage == Languages.FR) ? (int)LanguagesTypes.Fr : (CurrentLanguage == Languages.ES) ? (int)LanguagesTypes.Es : (CurrentLanguage == Languages.DE) ? (int)LanguagesTypes.De : (int)LanguagesTypes.En]}");


            //string language = "";
            //for (int i = 0; i < JsonData.strings.Length; i++)
            //    if (JsonData.strings[i].key == key)
            //        return JsonData.strings[i].en;
            //return language;
            //foreach (LanguageData item in JsonData.strings)
            //{

            //    //return item.en;
            //}

            //JsonData.strings.ToList().ForEach(x => x.en.ToString());

            //return JsonData.strings[1].en;

            //Debug.Log(Data.strings.Length);      

            //jsonStringsData.Clear();

            //switch (CurrentLanguage)
            //{
            //    case Languages.EN:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en);
            //        break;
            //    case Languages.FR:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].fr);
            //        break;
            //    case Languages.ES:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].es);
            //        break;
            //    case Languages.DE:
            //        JsonStringsData.Clear();
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].de);
            //        break;
            //    //case "en":
            //    //    for (int i = 0; i < Data.strings.Length; i++)
            //    //        stringsData.Add(Data.strings[i].key, Data.strings[i].en);
            //    //    break;
            //    //case "fr":
            //    //    for (int i = 0; i < Data.strings.Length; i++)
            //    //        stringsData.Add(Data.strings[i].key, Data.strings[i].fr);
            //    //    break;
            //    //case "es":
            //    //    for (int i = 0; i < Data.strings.Length; i++)
            //    //        stringsData.Add(Data.strings[i].key, Data.strings[i].es);
            //    //    break;
            //    //case "de":
            //    //    for (int i = 0; i < Data.strings.Length; i++)
            //    //        stringsData.Add(Data.strings[i].key, Data.strings[i].de);
            //    //    break;
            //    default:
            //        for (int i = 0; i < JsonData.strings.Length; i++)
            //            JsonStringsData.Add(JsonData.strings[i].key, JsonData.strings[i].en);
            //        break;
            //}

            //foreach (KeyValuePair<string, string> item in jsonStringsData)
            //{
            //    if (item.Key == key)
            //        return item.Value;
            //}

            #endregion
        }
    }
// }