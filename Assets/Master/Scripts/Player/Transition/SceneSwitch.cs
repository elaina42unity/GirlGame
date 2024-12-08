using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour, IInteractable
{
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO sceneToGo;
    public Vector3 PositionToGo;
    public void ChangeRoom()
    {
        Debug.Log("switch room");

        loadEventSO.RaiseLoadRequestEvent(sceneToGo, PositionToGo, true);
    }
}
