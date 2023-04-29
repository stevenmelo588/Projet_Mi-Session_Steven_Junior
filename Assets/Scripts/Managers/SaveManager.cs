//using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// using SurviveTheRust.Assets.Managers;
// using SurviveTheRust.Assets.Scripts.PlayerScripts.PlayerData;
using UnityEngine;

// namespace SurviveTheRust.Assets.Scripts.Managers
// {
public class SaveManager : MonoBehaviour
{
    /// <summary> 
    ///Path to the PlayerSaves Directory
    /// </summary>
    private static readonly string FILE_PATH = GameManager.PathToPersistentData + "/PlayerSaves/";
    private const string SAVE_FILE_EXTENTION = ".bin";

    public static string CurrentSaveFile { get; private set; }

    //List of strings with a size of 3 Containing the Save Files 
    public static List<string> SaveFiles = new(3);

    /// <summary>
    /// <para>Creates the PlayerSaves directory and the player save file if they don't already exist</para>
    /// <para>Adds the Save File to the List of Save Files</para>
    /// </summary>
    /// <param name="saveFileName">Name of the save file</param>
    public static void CreateSaveFileAndDirectory(string saveFileName, int playerIndex)
    {
        Debug.Log(FILE_PATH);
        //Check to see if the player already has 3 saves files
        if (SaveFiles.Count == 3) return;

        //Check if the PlayerSaves Directory Doesn't Already Exist
        if (!Directory.Exists(FILE_PATH))
            Directory.CreateDirectory(FILE_PATH);

        CurrentSaveFile = FILE_PATH + $"player_{playerIndex}_{saveFileName.ToLower()}{SAVE_FILE_EXTENTION}";

        if (File.Exists(CurrentSaveFile)) return;

        //if (!File.Exists(CurrentSaveFile))
        //{
        //Create the SaveFile if it Doesn't Already Exist
        FileStream FS = new(CurrentSaveFile, FileMode.CreateNew, FileAccess.Write);
        FS.Close();
        //}

        //Add the Current Save File to the list of Save Files
        SaveFiles.Add(CurrentSaveFile);
    }

    #region Test -> TO BE REMOVED LATER
    //Might be removed later
    public static PlayerSaveData ReadLatestPlayerSaveFile()
    {
        if (File.Exists(CurrentSaveFile))
        {
            FileStream FS = new(CurrentSaveFile, FileMode.Open, FileAccess.Read);
            BinaryFormatter BF = new();

            PlayerSaveData playerSaveData = (PlayerSaveData)BF.Deserialize(FS);

            FS.Close();
            return playerSaveData;
        }
        else
        {
            return null;
        }
    }
    #endregion

    public static void SelectSaveFile(int selectedSaveFileFromList)
    {
        CurrentSaveFile = SaveFiles[selectedSaveFileFromList];
    }

    public static void FindLocalSaveFiles()
    {
        if (Directory.Exists(FILE_PATH))
            foreach (string file in Directory.GetFiles(FILE_PATH))
            {
                Debug.Log(file);
                SaveFiles.Add(file);
            }
        else
        {
            Directory.CreateDirectory(FILE_PATH);
            Debug.Log(Directory.Exists(FILE_PATH));
        }
    }

    /// <summary>
    /// Read the Player Data from the selected Save File from the List
    /// </summary>
    /// <param name="selectedSaveFileFromList"></param>
    /// <returns>The player's saved data</returns>
    public static PlayerSaveData ReadFromPlayerSaveFile(int selectedSaveFileFromList)
    {
        if (File.Exists(SaveFiles[selectedSaveFileFromList]))
        {
            FileStream FS = new(SaveFiles[selectedSaveFileFromList], FileMode.Open, FileAccess.Read);
            BinaryFormatter BF = new();

            PlayerSaveData playerSaveData = (PlayerSaveData)BF.Deserialize(FS);

            FS.Close();
            return playerSaveData;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Called when the Player dies
    /// <para>Writes the updated Player Data to the Save File</para>
    /// </summary>
    /// <param name="player"></param>
    public static void WriteToSaveFile(PlayerData player)
    {
        //Will be called from the Game Manager 
        //APIRequestManager.UploadSaveData();

        //Testing
        PlayerSaveData playerSaveData = new(player);

        FileStream FS = new(CurrentSaveFile, FileMode.Open, FileAccess.Write);
        BinaryFormatter BF = new();

        BF.Serialize(FS, playerSaveData);

        FS.Close();
    }

    public static void DeleteSaveFile(int selectedSaveFileFromList)
    {
        File.Delete(SaveFiles[selectedSaveFileFromList]);
        SaveFiles.RemoveAt(selectedSaveFileFromList);
    }
}
// }