using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerAX : MonoBehaviour
{
    public static PlayerManagerAX instance;
    public PlayerAX player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
