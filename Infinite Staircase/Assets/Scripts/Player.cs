using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    [Space(10)]

    [HideInInspector] public float PLAYER_GRACE_PERIOD = 3.5f;

    [HideInInspector] public GameObject playerSprite;
    [HideInInspector] public Rigidbody2D playerRB;

    private bool isPlayerFacingLeft = true;
    [HideInInspector] public float gracePeriodTimer = float.PositiveInfinity;
    [HideInInspector] public bool isOnPlatform = true;

    public CinemachineVirtualCamera vCamera;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gracePeriodTimer < 0.0f || !isOnPlatform)
            isOnPlatform = false;
        else
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

        AudioManager.PlaySound("Jump");

        // begins the grace period timer
        if (gracePeriodTimer == float.PositiveInfinity)
            gracePeriodTimer = PLAYER_GRACE_PERIOD;

        // move the player up and in the direction they are facing
        Vector3 newPosition = transform.position;
        if (isPlayerFacingLeft)
        {
            newPosition.x -= gameManager.levelGenerator.PLATFORM_X_SPACING;
        }
        else
        {
            newPosition.x += gameManager.levelGenerator.PLATFORM_X_SPACING;
        }
        newPosition.y += gameManager.levelGenerator.PLATFORM_Y_SPACING;
         
        transform.position = newPosition;


        if (!IsOnPlatform())
        {
            isOnPlatform = false;
            vCamera.Follow = null;
        }
        else {
            // score goes up
            gameManager.uiManager.currentScore += 1;
            gracePeriodTimer = Mathf.Clamp(gracePeriodTimer + (PLAYER_GRACE_PERIOD * 0.25f), 0, PLAYER_GRACE_PERIOD);
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
