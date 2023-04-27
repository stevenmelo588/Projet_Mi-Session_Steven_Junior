using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// namespace SurviveTheRust.Assets.Scripts.Localization
// {
    public class ReadSpreadSheetLocalizationData
    {
        //private static readonly string pathToStreamingAssets = Application.streamingAssetsPath; //Move to GameManager 
        private static string SpreadSheet => Path.Combine(GameManager.PathToStreamingAssets, "strings1.txt");
        private static string[] SpreadSheetData => File.ReadAllLines(SpreadSheet);
        public static string CurrentLanguage { get; set; }

        private static string[][] test;

        public static string ChangeGameLanguage(string key)
        {
            Debug.Log(SpreadSheetData[0]);

            foreach (string s in SpreadSheetData) //Foreach row there are 6 columns -> 5 if we remove the first one witch is the Name strings
            {
                switch (s)
                {
                    case "Value.Key":
                        break;
                    case "Value.en":
                        break;
                    case "Value.fr":
                        break;
                    case "Value.es":
                        break;
                    case "Value.de":
                        break;
                    default:
                        break;
                }

                //_ = s.ToCharArray();
                //foreach(char c in s.ToCharArray())
                //{
                //    switch (c)
                //    {
                //        default:
                //    }
                //}
            }
            return "";
        }
    }
// }
