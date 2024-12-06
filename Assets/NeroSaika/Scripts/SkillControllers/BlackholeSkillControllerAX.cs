using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillControllerAX : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    public float maxSize;
    public float growSpeed;
    public float shrinkSpeed;

    private bool canGrow = true;
    private bool canShrink;
    private bool canCreatHotKeys = true;
    private bool cloneAttackReleased;

    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKey = new List<GameObject>();

    public void SetupBlackhole(float _maxSize,float _growSpeed,float _shrinkSpeed,int _amountOfAttacks,float _cloneAttackCooldown)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;
    }

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        if ((canGrow) && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
                Destroy(gameObject);

        }

        CloneAttackLogic();

    }

    private void ReleaseCloneAttack()
    {
        DestroyHotKeys();
        cloneAttackReleased = true;
        canCreatHotKeys = false;
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased)
        {
            if (amountOfAttacks <= 0)
            {
                canShrink = true;
                cloneAttackReleased = false;
                return;
            }

            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
                xOffset = -2;

            SkillManagerAX.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

            amountOfAttacks--;

        }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyAX>() != null)
            collision.GetComponent<EnemyAX>().FreezeTime(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyAX>() != null)
        {
            collision.GetComponent<EnemyAX>().FreezeTime(true);

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

        Blackhole_HotKeyControllerAX newHotKeyScript = newHotKey.GetComponent<Blackhole_HotKeyControllerAX>();
        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
