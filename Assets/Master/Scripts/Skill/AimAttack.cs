using UnityEngine;

//set the magic type
public enum magicType
{
    magicBall,
    waterBall,
    flashBall,
    fireBall
}

public class AimAttack : Skill
{
    public magicType magicType = magicType.waterBall;

    [Header("waterBall(bounce) info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float waterBallGravity;
    [SerializeField] private float bounceSpeed;


    [Header("fireBall(peirce) info")]
    [SerializeField] private int fireBallAmount;
    [SerializeField] private float FireBallGravity;

    [Header("Skill info")]
    [SerializeField] private GameObject magicBallPrefab;
    [SerializeField] private GameObject MeteorMagicPrefab;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private GameObject flashBallPrefab;
    [SerializeField] private GameObject waterBallPrefab;

    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float magicGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    [Header("FlashBall(Spin) info")]
    [SerializeField] private float hitCooldown = .35f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float flashBallGravity = 1;

    private Vector2 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    //initiate
    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetupGravity();
    }

    //initiate the gravity
    private void SetupGravity()
    {
        switch (magicType)
        {
            case magicType.magicBall:
                magicGravity = waterBallGravity;
                break;
            case magicType.waterBall:
                magicGravity = waterBallGravity;
                break;
            case magicType.flashBall:
                magicGravity = flashBallGravity;
                break;
            case magicType.fireBall:
                magicGravity = FireBallGravity;
                break;
        }
    }

    //check input
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }

    //initiate magic
    public void CreateMagic(int facingDir)
    {
       
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

        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition;
        if (facingDir == 1)
        {
            mousePosition = player.transform.position + new Vector3(30, 5);
            finalDir = mousePosition - playerPosition;
        }
        if (facingDir == -1)
        {
            mousePosition = player.transform.position + new Vector3(-30, 5);
            finalDir = mousePosition - playerPosition;
            //transform.rotation = Quaternion.Euler(0, 180f, 0f);
        }
        GameObject newWaterBall = Instantiate(MeteorMagicPrefab, player.transform.position, transform.rotation);
        MagicBallSkillController newMagicScript = newWaterBall.GetComponent<MagicBallSkillController>();

        switch (magicType)
        {
            case magicType.magicBall:
                break;
            case magicType.waterBall:
                magicGravity = waterBallGravity;
                newMagicScript.SetupWaterBall(true, bounceAmount, bounceSpeed);
                break;
            case magicType.flashBall:
                newMagicScript.SetupFlashBall(true, maxTravelDistance, spinDuration, hitCooldown);
                break;
            case magicType.fireBall:
                newMagicScript.SetupFireBall(fireBallAmount);
                break;
        }

        newMagicScript.SetupMagic(finalDir, magicGravity, player, freezeTimeDuration, returnSpeed);

        player.AssignNewWaterBall(newWaterBall);

        DotsActive(false);
    }

    //aim information
    #region Aim
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = player.transform.position + new Vector3(5, 0);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * magicGravity) * (t * t);

        return position;
    }
    #endregion
}
