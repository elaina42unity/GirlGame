using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggersRenne : MonoBehaviour
{
    private PlayerRenne player => GetComponentInParent<PlayerRenne>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
}
