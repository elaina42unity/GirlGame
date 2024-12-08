using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //set up the player to a static thing
    public static PlayerManager instance;
    public Player player;

    //initiate
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
