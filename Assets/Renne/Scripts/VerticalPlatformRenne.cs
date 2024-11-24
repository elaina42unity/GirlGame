using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatformRenne : MonoBehaviour
{
    private PlatformEffector2D effector;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (Input.GetKey(KeyCode.DownArrow)))
        {
            effector.rotationalOffset = 180.0f;
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            effector.rotationalOffset = 0.0f;
        }
    }
}
