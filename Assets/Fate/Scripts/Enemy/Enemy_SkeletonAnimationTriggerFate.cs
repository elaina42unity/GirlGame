using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTriggerFate : MonoBehaviour
{
    private Enemy_SkeletonFate enemy => GetComponentInParent<Enemy_SkeletonFate>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }


}
