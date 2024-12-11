using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private float blackholeTimer;

    private bool canGrow = true;
    private bool canShrink;
    private bool isShrinking = false;
    private bool canCreatHotKeys = true;
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKey = new List<GameObject>();

    public bool playerCanExitState { get; private set; }

    public void SetupBlackhole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCooldown, float _blackholeDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;
        blackholeTimer = _blackholeDuration;

        //when the skill is not use the clone 
        if (SkillManager.instance.clone.starInsteadOfClone)
        {
            playerCanDisapear = false;
        }

    }

    private void Update()
    {
        //timer update
        cloneAttackTimer -= Time.deltaTime;

        blackholeTimer -= Time.deltaTime;

        //check the duration
        if (blackholeTimer < 0)
        {
            blackholeTimer = Mathf.Infinity;

            if (targets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlackholeAbility();
        }

        CloneAttackLogic();

        //black hole grow
        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        //black hole shrink
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            //check if is shrinking to fix transparent bug
            if (transform.localScale.x < maxSize)
                isShrinking = true;

            if (transform.localScale.x < 0)
                Destroy(gameObject);

        }

        //attack check
        if ((Input.GetKeyDown(KeyCode.R) && !isShrinking)||(Input.GetKeyDown(KeyCode.Joystick1Button0) && !isShrinking))
        {
            ReleaseCloneAttack();
        }

        //[KnifeAttack]
        //if(Input.GetKeyDown(KeyCode.K) && !isShrinking)
        // {
        //SkillManager.instance.starMagic.CanUseSkill();
        //collision.GetComponent<StarMagicController>().FreezeTime(true);

        //  }
    }

    //attack check
    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;

        if (playerCanDisapear)
        {
            playerCanDisapear = false;
            PlayerManager.instance.player.MakeTransparent(true);
        }

        DestroyHotKeys();
        cloneAttackReleased = true;
        canCreatHotKeys = false;

    }

    //set the maxamount of attack and find the enemy tranform
    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0)
        {

            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
                xOffset = -2;

            if (SkillManager.instance.clone.starInsteadOfClone && targets[randomIndex]!=null)
            {
                SkillManager.instance.starMagic.CreateStar(true);

                SkillManager.instance.starMagic.CurrentStarChooseRandomTarget();

                SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

                

            }
            else
            {
                SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

            }

            amountOfAttacks--;

            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackholeAbility", .7f);
                return;
            }
        }
    }

    private void FinishBlackholeAbility()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;
    }

    private void DestroyHotKeys()
    {
        if (createdHotKey.Count <= 0)
            return;

        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }

    //freezing time reset
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(false);
        }
    }

    //set freeze
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0)
            return;

        if (!canCreatHotKeys)
            return;

        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 6), Quaternion.identity);

        createdHotKey.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        Blackhole_HotKeyController newHotKeyScript = newHotKey.GetComponent<Blackhole_HotKeyController>();
        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);

}
