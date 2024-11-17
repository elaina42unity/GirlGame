using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerAsh : MonoBehaviour
{
    public static PlayerManagerAsh instance_;
    public PlayerAsh player_;
    private void Awake()
    {
        if (instance_ != null)
            Destroy(instance_.gameObject);
        else
            instance_ = this;
    }

}
