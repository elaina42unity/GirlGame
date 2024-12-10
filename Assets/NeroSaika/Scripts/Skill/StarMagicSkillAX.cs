using System.Collections.Generic;
using UnityEngine;

public class StarMagicSkillAX : SkillAX
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private float starDuration;
    private GameObject currentStar = null;
    //private Transform closestEnemy;

    [Header("Explosive star")]
    [SerializeField] private bool canExplode;

    [Header("Moving Star")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canSetAroundEnemy;

    [Header("Multi Stacking Star")]
    [SerializeField] private bool canUseMultiStacks;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multiStackCooldown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> starLeft = new List<GameObject>();

    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiStar())
            return;

        if (currentStar == null)
        {
            CreateStar(false);
        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 playerPos = player.transform.position;

            player.transform.position = currentStar.transform.position;

            player.transform.position = currentStar.transform.position;

            currentStar.GetComponent<StarMagicControllerAX>()?.FinishStar();
        }
    }

    public void CreateStar(bool isBlackHoleSkill)
    {
        currentStar = Instantiate(starPrefab, player.transform.position - new Vector3(0, 1), Quaternion.identity);
        StarMagicControllerAX currentStarScript = currentStar.GetComponent<StarMagicControllerAX>();

        currentStarScript.SetupStar(starDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(currentStar.transform),canSetAroundEnemy, isBlackHoleSkill);
        
    }

    public void CurrentStarChooseRandomTarget() => currentStar.GetComponent<StarMagicControllerAX>().ChooseRandomEnemy();

    private bool CanUseMultiStar()
    {
        if (canUseMultiStacks)
        {
            if (starLeft.Count > 0)
            {
                if (starLeft.Count == amountOfStacks)
                    Invoke("ResetAbility", useTimeWindow);

                cooldown = 0;           //reset the cooldown when the stacks all used and then be refilled
                
                //make the Stacks of star to ready to use
                GameObject StarToSpawan = starLeft[starLeft.Count - 1];
                
                GameObject newStar = Instantiate(StarToSpawan, player.transform.position, Quaternion.identity);

                starLeft.Remove(StarToSpawan);

                //set up the star
                newStar.GetComponent<StarMagicControllerAX>().
                    SetupStar(starDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newStar.transform), canSetAroundEnemy,false);

                //closestEnemy = FindClosestEnemy(newStar.transform);
                //Debug.Log($"父级函数返回的敌人: {closestEnemy?.name}");
                //Debug.Log($"子级函数中的敌人位置: {closestEnemy?.position}");

                //if stack all used set cooldown
                if (starLeft.Count <= 0)
                {
                    cooldown = multiStackCooldown;
                    RefillStar();
                }

                return true;
            }
        }
        return false;
    }

    //fill the stacks
    private void RefillStar()
    {
        int amountToAdd = amountOfStacks - starLeft.Count;

        for (int i = 0; i < amountToAdd; i++)
        {
            starLeft.Add(starPrefab);
        }
    }

    //Because if use the skill one time and have a rest,the skill will always no cooldown
    //set a window that can make the skill be cooldowning even if did not use all of the stacks
    private void ResetAbility()
    {
        if (cooldownTimer > 0)
            return;

        cooldownTimer = multiStackCooldown;
        RefillStar();
    }
}

