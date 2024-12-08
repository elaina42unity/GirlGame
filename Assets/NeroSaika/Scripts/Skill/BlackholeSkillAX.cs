using UnityEngine;

public class BlackholeSkillAX : SkillAX
{
    [SerializeField] private int amountOfAttacks = 4;
    [SerializeField] private float cloneCooldown = .3f;
    [SerializeField] private float blackholeDuration;
    [Space]
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private GameObject blackHolePrefab;

    BlackholeSkillControllerAX currentBlckhole;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    //set the black hole information
    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackHole = Instantiate(blackHolePrefab, player.transform.position, Quaternion.identity);

        currentBlckhole = newBlackHole.GetComponent<BlackholeSkillControllerAX>();

        currentBlckhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown, blackholeDuration);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();


    }

    //check the skill state
    public bool SkillCompleted()
    {
        if (!currentBlckhole)
            return false;

        if (currentBlckhole.playerCanExitState)
        {
            currentBlckhole = null;
            return true;
        }
        return false;
    }

    //get blackhole radius to skill range
    public float GetBlackholeRadius()
    {
        return maxSize / 2;
    }
}
