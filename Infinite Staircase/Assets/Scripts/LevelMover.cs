using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LevelGenerator))]
public class LevelMover : MonoBehaviour
{
    LevelGenerator _levelGenerator;

    // Start is called before the first frame update
    void Start()
    {
        _levelGenerator = GetComponent<LevelGenerator>();
    }

    public void ShiftLevel()
    {
        // moving each respective platform down and in the direction that the player is facing to keep the player located at 0,0,0
        foreach (GameObject platform in _levelGenerator.ActivePlatforms)
        {
            Vector3 initPlatPos = platform.transform.position;

            platform.transform.position = new Vector3(
                initPlatPos.x + ((_levelGenerator.gameManager.player.IsPlayerFacingLeft ? 1 : -1) * (_levelGenerator.MOVE_X)),
                initPlatPos.y - _levelGenerator.MOVE_Y,
                0);
        }

        // "faking" making the player look like they are moving by moving the camera itself
        // TO DO : will be moved into its own script...
        _levelGenerator.gameManager.vCam.ForceCameraPosition(new Vector3((
            _levelGenerator.gameManager.player.IsPlayerFacingLeft ? 1 : -1) * (_levelGenerator.MOVE_X),
            _levelGenerator.gameManager.vCam.transform.position.y - _levelGenerator.MOVE_Y,
            _levelGenerator.gameManager.vCam.transform.position.z),
            Quaternion.Euler(Vector3.zero));

    }
}
