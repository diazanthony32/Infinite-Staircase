using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    [Space(10)]

    public CinemachineVirtualCamera vCamera;

    public float PLAYER_GRACE_PERIOD { get; private set; } = 3.5f;

    [SerializeField] private GameObject playerSprite;
    public Rigidbody2D playerRB { get; private set; }

    public float gracePeriodTimer { get; private set; } = float.PositiveInfinity;
    public bool isOnPlatform { get; private set; } = true;
    private bool isPlayerFacingLeft = true;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gracePeriodTimer < 0.0f || !isOnPlatform)
            StartCoroutine(Die());
        else
            gracePeriodTimer -= Time.deltaTime;
    }


    public void OnJump(InputValue value)
    {
        // move player in the direction they are facing
        MovePlayer();
    }

    public void OnRotate(InputValue value)
    {
        // Flipping the Player Sprite as soon as the tap rotate and then move
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
            newPosition.x -= (gameManager.levelGenerator.PLATFORM_X_SIZE + gameManager.levelGenerator.PLATFORM_PADDING);
        }
        else
        {
            newPosition.x += (gameManager.levelGenerator.PLATFORM_X_SIZE + gameManager.levelGenerator.PLATFORM_PADDING);
        }
        newPosition.y += (gameManager.levelGenerator.PLATFORM_Y_SIZE + gameManager.levelGenerator.PLATFORM_PADDING);
         
        transform.position = newPosition;


        if (!IsOnPlatform())
            StartCoroutine(Die());
        else
        {
            // score goes up
            gameManager.uiManager.currentScore += 1;
            gracePeriodTimer = Mathf.Clamp(gracePeriodTimer + (PLAYER_GRACE_PERIOD * 0.25f), 0, PLAYER_GRACE_PERIOD);
        }
    }

    private bool IsOnPlatform()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, gameManager.levelGenerator.PLATFORM_Y_SIZE);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Platform"))
            {
                return true;
            }
        }

        return false;
    }

    // On player "death", this coroutine gets enabled
    public IEnumerator Die()
    {
        isOnPlatform = false;
        vCamera.Follow = null;

        yield return new WaitForSeconds(1.5f);
        playerRB.bodyType = RigidbodyType2D.Dynamic;

        yield return null;
    }
}
