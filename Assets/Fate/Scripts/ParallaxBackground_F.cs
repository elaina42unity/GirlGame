using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground_F : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parllaxEffect;

    private float xPosition;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Virtual Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parllaxEffect);
        float distanceToMove = cam.transform.position.x * parllaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }



    }
}
