using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameManager gameManager;

    private LevelMover GM_LevelMover;
    private CameraManager GM_CameraManager;
    private Player GM_Player;
    private ScoreManager GM_ScoreManager;

    //
    private void Awake()
    {
        GM_LevelMover = gameManager.levelMover;
        GM_CameraManager = gameManager.cameraManager;
        GM_Player = gameManager.player;
        GM_ScoreManager = gameManager.scoreManager;
    }

    //
    public void OnJump(InputValue value)
    {
        StartCoroutine(UpdateLoop());
    }

    //
    public void OnRotate(InputValue value)
    {
        // Flipping the Player Sprite as soon as the tap rotate
        GM_Player.FlipPlayer();
        StartCoroutine(UpdateLoop());
    }

    //
    IEnumerator UpdateLoop()
    {
        GM_LevelMover.ShiftLevel();
        GM_Player.UpdateGracePeriod();

        yield return 0;

        GM_ScoreManager.UpdateScore();
        GM_CameraManager.ShiftCamera();
    }
}
