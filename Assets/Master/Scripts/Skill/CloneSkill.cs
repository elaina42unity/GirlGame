using System.Collections;
using UnityEngine;

public class CloneSkill : Skill
{

    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;
    private Transform closestEnemy;


    [SerializeField] private bool createCloneOnDodgeStart;
    [SerializeField] private bool createCloneOnDodgeOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;

    [Header("Clone can duplicate")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    [Header("Star instead of clone")]
    public bool starInsteadOfClone;

    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {

        //check if can set the clone skill to stars
        if (starInsteadOfClone && Input.GetAxisRaw("Vertical")<0)
        {
            SkillManager.instance.starMagic.CreateStar(false);
            SkillManager.instance.starMagic.CurrentStarChooseRandomTarget();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().
            SetupClone(_clonePosition, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform), canDuplicateClone, chanceToDuplicate);
            
    }

    //when use the skill create clone
    public void CreateCloneOnDodgeStart()
    {
        if (createCloneOnDodgeStart)
            CreateClone(player.transform, Vector3.zero);
    }

    //when finish the skill create clone
    public void CreateCloneOnDodgeOver()
    {
        if (createCloneOnDodgeOver)
            CreateClone(player.transform, Vector3.zero);
    }

    //when counterattack finish create a clone attack
    public void CanCreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }

    //set up the delay
    private IEnumerator CreateCloneWithDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform, _offset);
    }
}
