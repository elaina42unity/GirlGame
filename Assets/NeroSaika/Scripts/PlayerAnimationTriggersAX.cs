using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggersAX : MonoBehaviour
{
    private PlayerAX player => GetComponentInParent<PlayerAX>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

}
