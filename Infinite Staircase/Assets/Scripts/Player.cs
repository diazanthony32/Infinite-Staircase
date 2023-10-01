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

    public bool IsPlayerFacingLeft { get; private set; } = false;

    public float PLAYER_GRACE_PERIOD { get; private set; } = 3.5f;
    public float GracePeriodTimer { get; private set; } = float.PositiveInfinity;

    public bool IsGrounded { get; private set; } = true;

    // Start is called before the first frame update
    void Awake()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GracePeriodTimer < 0.0f || !IsGrounded)
        {
            StartCoroutine(Die());
        }
        else
        {
            CheckForPlatform();
            GracePeriodTimer -= Time.deltaTime;
        }
    }

    //
    public void FlipPlayer()
    {
        // Flips the transform of the Sprite by inverting its x scale value
        playerSprite.flipX = !playerSprite.flipX;

        // set bool to the oppisite of what it was
        IsPlayerFacingLeft = !IsPlayerFacingLeft;
    }

    // On player "death", this coroutine gets enabled
    public IEnumerator Die()
    {
        IsGrounded = false;

        yield return new WaitForSeconds(1.5f);
        PlayerRB.bodyType = RigidbodyType2D.Dynamic;

        yield return null;
    }

    // Begins the Grace period timer and adds a small percentage back avery time the player does an input
    public void UpdateGracePeriod()
    {
        // begins the countdown of the grace period timer
        if (GracePeriodTimer == float.PositiveInfinity)
            GracePeriodTimer = PLAYER_GRACE_PERIOD;
        else
            GracePeriodTimer = Mathf.Clamp(GracePeriodTimer + (PLAYER_GRACE_PERIOD * 0.2f), 0, PLAYER_GRACE_PERIOD);
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
}
