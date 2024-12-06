using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillAX : SkillAX
{
    [SerializeField] private int amountOfAttacks = 4;
    [SerializeField] private float cloneCooldown = .3f;
    [Space]
    [SerializeField]private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private GameObject blackHolePrefab;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackHole= Instantiate(blackHolePrefab);

        BlackholeSkillControllerAX newBlackHoleScript = newBlackHole.GetComponent<BlackholeSkillControllerAX>();

        newBlackHoleScript.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
