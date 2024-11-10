using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgruondAsh : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float startX_;
    private float tempX_;
    private float length;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startX_ = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = Mathf.Abs(cam.transform.position.x - startX_) * (1 - parallaxEffect);
        float distanceToMove = Mathf.Abs(cam.transform.position.x - startX_) * parallaxEffect;

        if (cam.transform.position.x - tempX_ > 0)
        {
            transform.position = new Vector3(tempX_ + distanceToMove, transform.position.y);
        }
        else if (cam.transform.position.x - tempX_ < 0)
        {
            transform.position = new Vector3(tempX_ - distanceToMove, transform.position.y);
        }

        if (distanceMoved > Mathf.Abs(tempX_) + length && cam.transform.position.x - tempX_ > 0)
            tempX_ = tempX_ + length;
        else if (distanceMoved > Mathf.Abs(tempX_) + length && cam.transform.position.x - tempX_ < 0)
            tempX_ = tempX_ - length;
    }
}
