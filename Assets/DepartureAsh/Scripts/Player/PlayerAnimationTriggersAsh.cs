using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggersAsh : MonoBehaviour
{
    private PlayerAsh player_ => GetComponentInParent<PlayerAsh>();

    private void AnimationTrigger()
    {
        player_.AnimationTrigger();
    }
}
