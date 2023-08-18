using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const float PLAYER_GRACE_PERIOD = 5.0f;
    [HideInInspector] public const float PLATFORM_X_SPACING = 3.0f;
    [HideInInspector] public const float PLATFORM_Y_SPACING = 1.5f;

    public GameObject playerSprite;
    private Rigidbody2D playerRB;

    private bool isPlayerFacingLeft = true;
    private float gracePeriodTimer = float.PositiveInfinity;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gracePeriodTimer -= Time.deltaTime;
    }


    public void OnJump(InputValue value)
    {
        // move player in the direction they are facing
        //Debug.Log("Jump Pressed");
        MovePlayer();
    }

    public void OnRotate(InputValue value)
    {
        // Flipping the Player Sprite as soon as the tap rotate and then move
        //Debug.Log("Rotate Pressed");
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
            newPosition.x -= PLATFORM_X_SPACING;
        }
        else
        {
            newPosition.x += PLATFORM_X_SPACING;
        }
        newPosition.y += PLATFORM_Y_SPACING;

        transform.position = newPosition;

        if (!IsOnPlatform()){
            playerRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private bool IsOnPlatform()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2.0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Platform"))
            {
                return true;
            }
        }

        return false;
    }
}
