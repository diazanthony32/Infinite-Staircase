using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    [Space(10)]

    private SpriteRenderer playerSprite;
    public Rigidbody2D PlayerRB { get; private set; }
    private Animator playerAnimator;

    public bool IsPlayerFacingLeft { get; private set; } = false;

    public float PLAYER_GRACE_PERIOD { get; private set; } = 3.0f;
    public float GracePeriodTimer { get; private set; } = float.PositiveInfinity;

    public bool IsGrounded { get; private set; } = true;

    private LevelGenerator GM_LevelGenerator;
    private CinemachineVirtualCamera GM_VCamera;

    // Start is called before the first frame update
    void Awake()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        PlayerRB = GetComponentInChildren<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();

        GM_LevelGenerator = gameManager.levelGenerator;
        GM_VCamera = gameManager.VCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (GracePeriodTimer < 0.0f || !IsGrounded)
        {
            GM_VCamera.Follow = null;
            StartCoroutine(Die());
        }
        else
        {
            GracePeriodTimer -= Time.deltaTime;
        }
    }

    //
    public void OnJump(InputValue value)
    {
        MovePlayer();
        CheckForPlatform();
    }

    //
    public void OnRotate(InputValue value)
    {
        // Flipping the Player Sprite as soon as the tap rotate
        FlipPlayer();
        MovePlayer();
        CheckForPlatform();
    }

    //
    public void FlipPlayer()
    {
        // Flips the transform of the Sprite by inverting its x scale value
        playerSprite.flipX = !playerSprite.flipX;

        // set bool to the oppisite of what it was
        IsPlayerFacingLeft = !IsPlayerFacingLeft;
    }

    //
    public void MovePlayer() 
    {
        AudioManager.PlaySound("Jump");

        //
        transform.position = new Vector3(
                transform.position.x + ((IsPlayerFacingLeft ? -1 : 1) * (GM_LevelGenerator.MOVE_X)),
                transform.position.y + GM_LevelGenerator.MOVE_Y,
                0);
    }

    // Begins the Grace period timer and adds a small percentage back avery time the player does an input
    public void UpdateGracePeriod()
    {
        playerAnimator.SetTrigger("Jump");

        // begins the countdown of the grace period timer
        if (GracePeriodTimer == float.PositiveInfinity)
            GracePeriodTimer = PLAYER_GRACE_PERIOD;
        else
            GracePeriodTimer = Mathf.Clamp(GracePeriodTimer + (PLAYER_GRACE_PERIOD * 0.15f), 0, PLAYER_GRACE_PERIOD);
    }

    // Checking if the player is on the Platform, duh...
    public void CheckForPlatform()
    {
        IsGrounded = false;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Platform"))
            {
                IsGrounded = true;
                break;
            }
        }
    }

    // On player "death", this coroutine gets enabled
    public IEnumerator Die()
    {
        playerAnimator.SetTrigger("Die");

        IsGrounded = false;

        yield return new WaitForSeconds(1.5f);
        PlayerRB.bodyType = RigidbodyType2D.Dynamic;
    }
}
