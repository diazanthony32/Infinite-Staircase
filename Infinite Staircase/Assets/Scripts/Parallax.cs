using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public CinemachineVirtualCamera GM_VCamera;

    public enum ScrollDirectionX { Left, Right };

    [Header("Horizontal Settings")]

    public bool loopX;
    public float parallaxEffectX;
    public bool autoScrollX;
    public float scollSpeedX;
    public ScrollDirectionX scrollDirectionXaxis;

    public enum ScrollDirectionY { Down, Up };
    
    [Header("Vertical Settings")]
    
    public bool loopY;
    public float parallaxEffectY;
    public bool autoScrollY;
    public float scollSpeedY;
    public ScrollDirectionY scrollDirectionYaxis;

    float startPosX;
    float boundsX;
    float startPosY;
    float boundsY;

    //
    private void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        boundsX = GetComponentInChildren<Renderer>().bounds.size.x;
        boundsY = GetComponentInChildren<Renderer>().bounds.size.y;
    }

    //
    void Update()
    {
        Vector2 desiredPositon;

        if (autoScrollX || autoScrollY)
        {
            desiredPositon = AutoScroll();
        }
        else
        {
            desiredPositon = ParallaxFX();
        }

        transform.position = desiredPositon;

        if(loopX || loopY)
        {
            Loop();
        }
    }

    // 
    Vector2 ParallaxFX()
    {
        float distanceX = (GM_VCamera.transform.position.x * parallaxEffectX);
        float desiredXPos = startPosX + distanceX;

        float distanceY = (GM_VCamera.transform.position.y * parallaxEffectY);
        float desiredYPos = startPosY + distanceY;

        return new Vector2(desiredXPos, desiredYPos);
    }

    // 
    Vector2 AutoScroll()
    {
        float desiredXPos = transform.position.x;
        float desiredYPos = transform.position.y;

        if (autoScrollX)
        {
            desiredXPos += ((scrollDirectionXaxis == ScrollDirectionX.Left ? -1 : 1) * scollSpeedX * Time.deltaTime);
        }

        if (autoScrollY)
        {
            desiredYPos += ((scrollDirectionYaxis == ScrollDirectionY.Down ? -1 : 1) * scollSpeedY * Time.deltaTime);
        }

        return new Vector2(desiredXPos, desiredYPos);
    }

    //
    void Loop()
    {
        float tempX = GM_VCamera.transform.position.x;
        float tempY = GM_VCamera.transform.position.y;

        // used for looping the item
        if (loopX)
        {
            if (transform.position.x + boundsX < tempX)
            {
                transform.position = new Vector2(transform.position.x + boundsX, transform.position.y);
            }
            else if (transform.position.x - boundsX > tempX)
            {
                transform.position = new Vector2(transform.position.x - boundsX, transform.position.y);
            }
        }

        if (loopY)
        {
            if (transform.position.y + boundsY < tempY)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + boundsY);
            }
            else if (transform.position.y - boundsY > tempY)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - boundsY);
            }
        }
    }
}
