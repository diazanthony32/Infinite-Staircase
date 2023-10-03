using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameManager gameManager;

    public enum scrollDirection { Left, Right };

    [Space(10)]

    public bool loopX;
    public float parallaxEffectX;
    public bool autoScrollX;
    public scrollDirection scrollDirectionX;
    public float scollSpeedX;

    [Space(10)]

    public bool loopY;
    public float parallaxEffectY;
    public bool autoScrollY;
    public scrollDirection scrollDirectionY;
    public float scollSpeedY;

    Vector3 startPos;
    float boundsX;
    float boundsY;
    CinemachineVirtualCamera GM_VCamera;

    //
    private void Awake()
    {
        startPos = transform.position;
        boundsX = GetComponentInChildren<Renderer>().bounds.size.x;
        boundsY = GetComponentInChildren<Renderer>().bounds.size.y;
        GM_VCamera = gameManager.VCamera;
    }

    //
    void Update()
    {
        float tempX = GM_VCamera.transform.position.x * (1 - parallaxEffectX);
        float distanceX = (GM_VCamera.transform.position.x * parallaxEffectX);
        float desiredXPos = startPos.x + distanceX;

        float tempY = GM_VCamera.transform.position.y * (1 - parallaxEffectY);
        float distanceY = (GM_VCamera.transform.position.y * parallaxEffectY);
        float desiredYPos = startPos.y + distanceY;

        if (autoScrollX)
        {
            // this will push the object to the left
            desiredXPos = transform.position.x - scollSpeedX;
        }

        if (autoScrollY)
        {
            // this will push the object down
            desiredYPos = transform.position.y - scollSpeedY;
        }

        transform.position = new Vector2(desiredXPos, desiredYPos);

        // used for looping the item
        if (loopX)
        {
            if (GM_VCamera.transform.position.x > (transform.position.x + boundsX))
            {
                transform.position = new Vector3(GM_VCamera.transform.position.x, transform.position.y, 0);
            }
        }

        if (loopY) 
        {
            if (Mathf.Abs(GM_VCamera.transform.position.y) > (Mathf.Abs(transform.position.y) + boundsY))
            {
                transform.position = new Vector3(transform.position.x, GM_VCamera.transform.position.y, 0);
            }
        }
    }
}
