using UnityEngine;

public class StaffMagicSkill : Skill
{
    [Header("Mirage")]
    [SerializeField] private bool cloneInsdeadOfStaff;
    [SerializeField] private GameObject staffPrefab;
    [SerializeField] private float maxDistancefromPlayer;
    private GameObject currentStaff = null;

    public override void UseSkill()
    {
        base.UseSkill();

        //if already create a staff player will be change to the staff's position
        if (currentStaff == null)
        {
            currentStaff = Instantiate(staffPrefab, player.transform.position - new Vector3(0, 1), Quaternion.identity);
        }
        else
        {
            Vector2 playerPos = player.transform.position;

            player.transform.position = currentStaff.transform.position;

            currentStaff.transform.position = playerPos;

            if (cloneInsdeadOfStaff)
            {
                SkillManager.instance.clone.CreateClone(currentStaff.transform, Vector3.zero);
                Destroy(currentStaff);
            }
            Destroy(currentStaff);
        }

    }

    //when player is too far from the staff will destroy the staff
    protected override void Update()
    {
        base.Update();
        if (currentStaff != null)
        {
            if (Vector2.Distance(currentStaff.transform.position, player.transform.position) > maxDistancefromPlayer)
            {
                Destroy(currentStaff);
            }
        }
    }
}
