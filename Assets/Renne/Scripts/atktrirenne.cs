using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atktrirenne : MonoBehaviour
{
    private attackrenne player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<attackrenne>();
    }

    // Update is called once per frame
    private void AnimationTrigger()
    {
        player.AttackOver();
    }
}
