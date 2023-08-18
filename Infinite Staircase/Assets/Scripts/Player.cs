using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject playerSprite;

    private bool isPlayerFacingLeft = true;

    // Start is called before the first frame update
    void Awake()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnJump(InputValue value)
    {
        Debug.Log("Jump Pressed");
        MovePlayer();
    }

    public void OnRotate(InputValue value)
    {
        // Flipping the Player Sprite as soon as the tap rotate
        Debug.Log("Rotate Pressed");
        Flip();
        MovePlayer();
    }

    public void Flip()
    {
        // Flips the transform of the Sprite by inverting its x value
        Vector3 currentScale = playerSprite.transform.localScale;
        currentScale.x *= -1;
        playerSprite.transform.localScale = currentScale;

        // set bool to the oppisite of what it was
        isPlayerFacingLeft = !isPlayerFacingLeft;
    }

    public void MovePlayer()
    {
        // move the player up and in the direction they are facing
        Vector3 newPosition = transform.position;
        if (isPlayerFacingLeft)
        {
            newPosition.x -= 3;
        }
        else
        {
            newPosition.x += 3;
        }
        newPosition.y += 1.5f;

        transform.position = newPosition;
    }
}
