using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagerAsh : MonoBehaviour
{
    public static SkillManagerAsh instance_;

    public DashSkillAsh Dash { get; private set; } 
    private void Awake()
    {
        if (instance_ != null)
            Destroy(instance_.gameObject);
        else
            instance_ = this;
    }

    private void Start()
    {
        Dash = GetComponent<DashSkillAsh>();
    }
}
