using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //AsyncOperationHandle<AsyncOperation> async;
    private AsyncOperationHandle<SceneInstance> AsyncHandle;
    private AsyncOperationHandle<SceneInstance> previousLoadedAsyncScene;

    SceneInstance previousSceneRef;

    private string previousSceneName;
    private string sceneName;
    private bool clearPreviousScene = false;

    public void Load(string sceneName)
    {
        //var test = this.sceneName.Last().ToString();

        // if (!string.IsNullOrEmpty(this.sceneName))
        //     Debug.Log(this.sceneName.Last() + " | " + int.TryParse(this.sceneName.Last().ToString(), out int t));

        this.sceneName = sceneName;

        Debug.Log(sceneName.Last());

        Debug.Log(SceneManager.GetActiveScene().name.Last());

        //Scene scene = sceneRef.Scene;
        //scene.name = this.sceneName;

        // if (this.sceneName.Last() != sceneName.Last())
        //     AssignPreviousScene();

        string label = "Level" + sceneName.Last();

        //Loads all the assets that have the same Label Name and once it's Completed it subcribes to the SceneLoader_Completed Methods
        Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string>() { label }, x => { }, Addressables.MergeMode.Union).Completed += SceneLoader_Completed;
    }

    static void Swap<T>(ref T newOperation, ref T previousOperation)
    {
        T temp;
        temp = newOperation;
        newOperation = previousOperation;
        previousOperation = temp;
    }

    //Asyncronously Loads the Scene when Called using the LoadSceneAsync from Addressables instead of the one inside SceneManager
    private void SceneLoader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        if (clearPreviousScene)
        {
            Addressables.UnloadSceneAsync(previousSceneRef).Completed += (AsyncHandle) =>
            {
                clearPreviousScene = false;
                previousSceneRef = new SceneInstance();
            };
        }

        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).Completed += (AsyncHandle) =>
        {
            clearPreviousScene = true;
            previousSceneRef = AsyncHandle.Result;
        };
        //Swap<AsyncOperationHandle<SceneInstance>>(ref newAsyncSceneToLoad, ref previousLoadedAsyncScene);
        //Swap(ref newAsyncSceneToLoad, ref previousLoadedAsyncScene);
        // Debug.Log(AsyncHandle.DebugName);

        // //Addressables.
        // //if()
        // AsyncHandle.Completed += SceneAsyncOperation_Completed;
        //OnSceneLoad();
        //AsyncOperationHandle<SceneInstance> tempAsyncOperation = newAsyncSceneToLoad;
        //Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        //newAsyncSceneToLoad = Addressables.UnloadSceneAsync(previousLoadedAsyncScene);
    }

    // public void AssignPreviousScene()
    // {
    //     previousLoadedAsyncScene = AsyncHandle;
    //     Debug.Log(previousLoadedAsyncScene.DebugName);
    //     //Addressables.UnloadSceneAsync();
    // }

    // private void SceneAsyncOperation_Completed(AsyncOperationHandle<SceneInstance> obj)
    // {
    //     if(AsyncHandle.Result.Scene.name.Last() != previousLoadedAsyncScene.Result.Scene.name.Last())
    //         Debug.Log(previousLoadedAsyncScene.DebugName);

    //     //obj.IsDone
    //     Debug.Log(obj.IsDone); 
    //     //previousLoadedAsyncScene = newAsyncSceneToLoad;
    //     //Addressables.UnloadSceneAsync(newAsyncSceneToLoad);
    // }

    public void Back()
    {
        Addressables.LoadSceneAsync("Prototype Scene", LoadSceneMode.Single);
    }
}
