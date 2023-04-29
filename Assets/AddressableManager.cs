using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AddressableManager : MonoBehaviour
{
    private string sceneName;
    public void Load(string sceneName)
    {
        this.sceneName = sceneName;
        string label = "Level" + sceneName.Last();

        Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string>() { label },
            x => { }, Addressables.MergeMode.Union).Completed += Sceneloader_Completed;


    }


    private void Sceneloader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        SceneManager.LoadScene("CommonScene", LoadSceneMode.Additive);
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}







