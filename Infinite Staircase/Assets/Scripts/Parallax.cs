using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public GameManager gameManager;
    public enum scrollDirectionX { Left, Right };

    [Header("Horizontal Settings")]

    public bool loopX;
    public float parallaxEffectX;
    public bool autoScrollX;
    public scrollDirectionX scrollDirectionXaxis;
    public float scollSpeedX;

    public enum scrollDirectionY { Down, Up };
    
    [Header("Vertical Settings")]
    
    public bool loopY;
    public float parallaxEffectY;
    public bool autoScrollY;
    public scrollDirectionY scrollDirectionYaxis;
    public float scollSpeedY;

    float startPosX;
    float boundsX;
    float startPosY;
    float boundsY;
    CinemachineVirtualCamera GM_VCamera;

    //
    private void Awake()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        boundsX = GetComponentInChildren<Renderer>().bounds.size.x;
        boundsY = GetComponentInChildren<Renderer>().bounds.size.y;
        GM_VCamera = gameManager.VCamera;
    }

    //
    void Update()
    {
        float tempX = GM_VCamera.transform.position.x * (1 - parallaxEffectX);
        float distanceX = (GM_VCamera.transform.position.x * parallaxEffectX);
        float desiredXPos = startPosX + distanceX;

        float tempY = GM_VCamera.transform.position.y * (1 - parallaxEffectY);
        float distanceY = (GM_VCamera.transform.position.y * parallaxEffectY);
        float desiredYPos = startPosY + distanceY;

        if (autoScrollX)
        {
            // this will push the object to the left
            desiredXPos = transform.position.x + (scrollDirectionXaxis == scrollDirectionX.Left ? -1 : 1) * scollSpeedX;
        }

        if (autoScrollY)
        {
            // this will push the object down
            desiredYPos = transform.position.y + (scrollDirectionYaxis == scrollDirectionY.Down ? -1 : 1) * scollSpeedY;
        }

        transform.position = new Vector2(desiredXPos, desiredYPos);

        // used for looping the item
        if (loopX)
        {
            if (tempX > transform.position.x + boundsX)
            {
                startPosX += boundsX;
                transform.position = new Vector3(transform.position.x + boundsX, transform.position.y, 0);
            }
            else if (tempX < transform.position.x - boundsX)
            {
                startPosX -= boundsX;
                transform.position = new Vector3(transform.position.x + boundsX, transform.position.y, 0);
            }
        }

        if (loopY)
        {
            if (tempY > transform.position.y + boundsY)
            {
                startPosY += boundsY;
                transform.position = new Vector3(transform.position.x, transform.position.y + boundsY, 0);
            }
            else if (tempY < transform.position.y - boundsY)
            {
                startPosY -= boundsY;
                transform.position = new Vector3(transform.position.x, transform.position.y - boundsY, 0);
            }
        }
    }
}
