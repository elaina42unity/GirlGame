using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillAsh : SkillAsh
{

    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab_;
    [SerializeField] private float cloneDuration_;
    [SerializeField] private bool canAttack_;

    public void CreateClone(Transform clonePosition)
    {
        GameObject newClone = Instantiate(clonePrefab_);
        newClone.GetComponent<CloneSkillControllerAsh>().SetupClone(clonePosition, cloneDuration_ , canAttack_);
    }
}
