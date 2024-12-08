using UnityEngine;

public class SkillAX : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    Transform closestEnemy = null;

    protected PlayerAX player;

    protected virtual void Start()
    {
        player = PlayerManagerAX.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }
    //if can use skill then use it
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }

    //take the transform from target
    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        float closestDistance = Mathf.Infinity;


        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}
