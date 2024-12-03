using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot1AnimationTriggersAX : MonoBehaviour
{
    private EnemyRobot1AX enemy => GetComponentInParent<EnemyRobot1AX>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
