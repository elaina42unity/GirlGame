using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player_F player;

    void Start()
    {
        player = GetComponentInParent<Player_F>();
    }

    private void AnimationTrigger()
    {
        player.AttackOver();
    }

}
