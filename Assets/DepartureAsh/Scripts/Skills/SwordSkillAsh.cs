using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSkillAsh : SkillAsh
{
    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab_;
    [SerializeField] private Vector2 launchDir_;
    [SerializeField] private float swordGravity_;

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab_, player_.transform.position, transform.rotation);
        SwordSkillControllerAsh newSwordScript = newSword.GetComponent<SwordSkillControllerAsh>();

        newSwordScript.SetupSword(launchDir_,swordGravity_);
    }
}
