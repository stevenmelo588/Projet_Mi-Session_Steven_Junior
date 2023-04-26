using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SurviveTheRust.Assets.Scripts.AddressablesScripts
{
    public class SceneLoader : MonoBehaviour
    {
        AsyncOperationHandle<SceneInstance> async;

        private string sceneName;

        public void Load(string sceneName)
        {
            this.sceneName = sceneName;
            string label = "Level" + sceneName.Last();

            //Loads all the assets that have the same Label Name and once it's Completed it subcribes to the SceneLoader_Completed Methods
            Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string>() { label }, x => { }, Addressables.MergeMode.Union).Completed += SceneLoader_Completed;
        }

        //Asyncronously Loads the Scene when Called using the LoadSceneAsync from Addressables instead of the one inside SceneManager
        private void SceneLoader_Completed(AsyncOperationHandle<IList<UnityEngine.Object>> obj)
        {
            async = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
           // Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public void Back()
        {
            Addressables.LoadSceneAsync("AssetMain", LoadSceneMode.Single);
        }
    }
}