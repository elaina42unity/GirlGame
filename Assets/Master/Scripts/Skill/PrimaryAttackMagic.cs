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

    //bounce information
    [Header("WaterBall info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceSpeed;

    //how many enemy can the fire ball attack through
    [Header("FireBall info")]
    [SerializeField] private int fireBallAmount;

    //different magic prefab to set up
    [Header("Prefab info")]
    [SerializeField] private GameObject MeteorMagicPrefab;
    [SerializeField] private GameObject magicBallPrefab;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private GameObject flashBallPrefab;
    [SerializeField] private GameObject waterBallPrefab;

    [Header("Magicball info")]
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float freezeTimeDuration;

    [Header("FlashBall info")]
    [SerializeField] private float hitCooldown;         //the time between each spinning damage
    [SerializeField] private float maxTravelDistance;   //the length the magic ball will travel
    [SerializeField] private float spinDuration;        //the time of spin

    [Header("Basic info")]
    public int damage;                                  //the damage one time 
    [SerializeField] private float magicGravity;        //the gravity of the magic
    private Vector2 attackDir;                          //the magic direction
    

    //initiate
    protected override void Start()
    {
        base.Start();

    }

    //initiate magic
    public void CreateMagic(int facingDir)
    {
       
        //set the random to get the magic's number
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

        //set the velocity due to the direction
        if (facingDir == 1)
        {
            attackDir = new Vector3(30, 5);
        }else 
        if (facingDir == -1)
        {
            attackDir = new Vector3(-30, 5);
        }

        //initiate the magicball to player's positoin
        GameObject newMagicBall = Instantiate(MeteorMagicPrefab, player.transform.position, transform.rotation);
        
        //get the script on the magicball
        MagicBallSkillController newMagicScript = newMagicBall.GetComponent<MagicBallSkillController>();

        //give the magic prefab their own magic code
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

        //set up the magic basic information
        newMagicScript.SetupMagic(attackDir, magicGravity, player, freezeTimeDuration, damage);

    }
}
