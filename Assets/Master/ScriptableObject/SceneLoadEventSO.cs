using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    /// <summary>
    /// シーンのロードリクエスト
    /// </summary>
    /// <param name="locationToLoad">ロードしたいのシーン</param>
    /// <param name="posToGo">Playerの目的地の座標</param>
    /// <param name="fadeScreen">切り替える時fade効果をするかどうか</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadeScreen);
    }
}
