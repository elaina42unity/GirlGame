using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Event Listen")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;

    public float fadeDuration;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentLoadedScene = firstLoadScene;
        currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequistEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequistEvent;
    }

    private void OnLoadRequistEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
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

        }

        yield return new WaitForSeconds(fadeDuration);
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true); ;
    }
}