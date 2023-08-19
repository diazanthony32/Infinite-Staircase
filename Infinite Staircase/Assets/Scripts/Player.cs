using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const float PLAYER_GRACE_PERIOD = 5.0f;

    [HideInInspector] public GameObject playerSprite;
    [HideInInspector] public Rigidbody2D playerRB;

    private bool isPlayerFacingLeft = true;
    private float gracePeriodTimer = float.PositiveInfinity;
    public bool isOnPlatform = true;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
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
            newPosition.x -= gameManager.PLATFORM_X_SPACING;
        }
        else
        {
            newPosition.x += gameManager.PLATFORM_X_SPACING;
        }
        newPosition.y += gameManager.PLATFORM_Y_SPACING;

        transform.position = newPosition;


        if (!IsOnPlatform())
        {
            isOnPlatform = false;
        }
        else {
            // score goes up
            gameManager.currentScore += 1;
        }
    }

    private bool IsOnPlatform()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
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