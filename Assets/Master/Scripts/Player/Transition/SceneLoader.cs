using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;

    [Header("イベントリスナー")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    [SerializeField] private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;

    private bool fadeScreen;
    private bool isLoading;
    public float fadeDuration;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentLoadedScene = firstLoadScene;
        currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        //イベントを登録
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        //イベントを取り消し
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading)
        {
            return;
        }
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;

        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //fade効果を実現する

        }

        yield return new WaitForSeconds(fadeDuration);

        yield return currentLoadedScene.sceneReference.UnLoadScene();

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// シーンのロード完了後
    /// </summary>
    /// <param name="handle"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        //currentシーンを次のシーンに遷移
        currentLoadedScene = sceneToLoad;

        //シーン遷移した後のplayerの移動
        playerTrans.position = positionToGo;

        if (fadeScreen)
        {

        }

        isLoading = false;
    }
}
