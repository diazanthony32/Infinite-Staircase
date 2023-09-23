using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelMover : MonoBehaviour
{
    public GameManager gameManager;

    LevelGenerator GM_LevelGenerator;

    //
    private void Awake()
    {
        GM_LevelGenerator = gameManager.levelGenerator;
    }

    //
    public void ShiftLevel()
    {
        AudioManager.PlaySound("Jump");

        // moving each respective platform down and in the direction that the player is facing to keep the player located at 0,0,0
        foreach (GameObject platform in GM_LevelGenerator.ActivePlatforms)
        {
            Vector3 initPlatPos = platform.transform.position;

            platform.transform.position = new Vector3(
                initPlatPos.x + ((GM_LevelGenerator.gameManager.player.IsPlayerFacingLeft ? 1 : -1) * (GM_LevelGenerator.MOVE_X)),
                initPlatPos.y - GM_LevelGenerator.MOVE_Y,
                0);
        }
    }
}
