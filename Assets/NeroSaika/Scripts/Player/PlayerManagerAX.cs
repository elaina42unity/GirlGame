using UnityEngine;

public class PlayerManagerAX : MonoBehaviour
{
    //set up the player to a static thing
    public static PlayerManagerAX instance;
    public PlayerAX player;

    //initiate
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
