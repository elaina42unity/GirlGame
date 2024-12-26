using UnityEngine;

//set the magic type
public enum magicType
{
    magicBall,
    waterBall,
    flashBall,
    fireBall
}

public class PrimaryAttackMagic : Skill
{
    public magicType magicType = magicType.waterBall;

    [Header("WaterBall info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceSpeed;

    [Header("FireBall info")]
    [SerializeField] private int fireBallAmount;

    [Header("Prefab info")]
    [SerializeField] private GameObject MeteorMagicPrefab;
    [SerializeField] private GameObject magicBallPrefab;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private GameObject flashBallPrefab;
    [SerializeField] private GameObject waterBallPrefab;

    [Header("Magicball info")]
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    [Header("FlashBall info")]
    [SerializeField] private float hitCooldown;
    [SerializeField] private float maxTravelDistance;
    [SerializeField] private float spinDuration;

    [Header("Basic info")]
    public int damage;
    [SerializeField] private float magicGravity;
    private Vector2 attackDir;
    

    //initiate
    protected override void Start()
    {
        base.Start();

    }

    //initiate magic
    public void CreateMagic(int facingDir)
    {
       
        //set the random to get the magic
        switch (Random.Range(0,4))
        {
            case 0:
                magicType = magicType.magicBall;
                MeteorMagicPrefab = magicBallPrefab;
                break;
            case 1:
                magicType = magicType.waterBall;
                MeteorMagicPrefab = waterBallPrefab;
                break;
            case 2:
                magicType = magicType.flashBall;
                MeteorMagicPrefab = flashBallPrefab;
                break;
            case 3:
                magicType = magicType.fireBall;
                MeteorMagicPrefab = fireBallPrefab;
                break;
        }

        if (facingDir == 1)
        {
            attackDir = new Vector3(30, 5);
        }
        if (facingDir == -1)
        {
            attackDir = new Vector3(-30, 5);
        }

        GameObject newMagicBall = Instantiate(MeteorMagicPrefab, player.transform.position, transform.rotation);
        MagicBallSkillController newMagicScript = newMagicBall.GetComponent<MagicBallSkillController>();

        switch (magicType)
        {
            case magicType.magicBall:
                break;
            case magicType.waterBall:
                newMagicScript.SetupWaterBall(true, bounceAmount, bounceSpeed);
                break;
            case magicType.flashBall:
                newMagicScript.SetupFlashBall(true, maxTravelDistance, spinDuration, hitCooldown);
                break;
            case magicType.fireBall:
                newMagicScript.SetupFireBall(fireBallAmount);
                break;
        }

        newMagicScript.SetupMagic(attackDir, magicGravity, player, freezeTimeDuration, damage);

    }
}
