using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/SceneLoadEventSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}