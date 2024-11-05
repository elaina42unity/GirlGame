using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player_Axium player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player_Axium>();
    }

    private void AnimationTriggers()
    {
        player.AttackOver();
    }
}
