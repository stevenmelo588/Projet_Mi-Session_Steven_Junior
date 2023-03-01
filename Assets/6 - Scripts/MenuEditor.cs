#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MenuEditor : MonoBehaviour
{
    [MenuItem("Tools/Count Scene Number")]
    static void CountSceneNumber()
    {
        Debug.Log(GetSceneAssets().Count);
    }

    static string[] searchInFolders = new[] { "Assets/Scenes/" };
    
    static List<SceneAsset> GetSceneAssets()
    {
        string[] sceneGuids = AssetDatabase.FindAssets("t:SceneAsset", searchInFolders);
        var sceneAssets = new List<SceneAsset>();
        foreach (var sceneGuid in sceneGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath));
        }
        return sceneAssets;
    }

    [MenuItem("Tools/Update Scene Assets")]
    static void UpdateSceneAssets()
    {
        if (!AssetDatabase.IsValidFolder(Path.Combine("Assets", "ScriptableObjects"))) ;
        {
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
        }


        foreach (var sceneAsset in GetSceneAssets())
        {
            var sceneData = ScriptableObject.CreateInstance<Level>();
            sceneData.SceneAsset = sceneAsset;
            string assetPath = Path.Combine("Assets", "ScriptableObjects", sceneData.sceneName + ".asset");
            AssetDatabase.CreateAsset(sceneData, assetPath);
        }
    }

}
#endif
