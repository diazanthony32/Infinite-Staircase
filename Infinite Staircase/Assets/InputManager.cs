using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameManager gameManager;

    public void OnJump(InputValue value)
    {
        Debug.Log("Jump Pressed");
        UpdateLoop();
    }

    public void OnRotate(InputValue value)
    {
        // Flipping the Player Sprite as soon as the tap rotate
        Debug.Log("Rotate Pressed");
        gameManager.player.FlipPlayer();
        UpdateLoop();
    }

    void UpdateLoop()
    {
        gameManager.levelMover.ShiftLevel();
        gameManager.player.UpdateGracePeriod();
        gameManager.player.CheckForPlatform();
        gameManager.scoreManager.UpdateScore();
    }
}
