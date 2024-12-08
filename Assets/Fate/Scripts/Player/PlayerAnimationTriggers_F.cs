using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers_F : MonoBehaviour
{
    private Player_F player => GetComponentInParent<Player_F>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
}
