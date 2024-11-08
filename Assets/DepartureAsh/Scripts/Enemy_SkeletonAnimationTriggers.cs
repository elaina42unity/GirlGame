using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy_=>GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        enemy_.AnimationFinishTrigger();
    }
}
