using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    public string sceneName;
    #if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneAsset;

    public UnityEditor.SceneAsset SceneAsset
    {
        get => sceneAsset;
        set { 
            sceneAsset = value;
            sceneName = value.name;
        }
    }
    #endif
}
