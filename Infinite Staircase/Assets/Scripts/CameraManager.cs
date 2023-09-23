using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraManager : MonoBehaviour
{
    public GameManager gameManager;

    public CinemachineVirtualCamera CM_Camera { get; private set; }
    private LevelGenerator GM_LevelGen;
    private Player GM_Player;

    //
    private void Awake()
    {
        CM_Camera = GetComponent<CinemachineVirtualCamera>();

        GM_LevelGen = gameManager.levelGenerator;
        GM_Player = gameManager.player;
    }

    //
    private void Update()
    {
        // if the player "dies" then it stops folowing them
        if (!GM_Player.IsGrounded)
        {
            CM_Camera.Follow = null;
        }
    }

    // "faking" making the player look like they are moving by moving the camera itself
    public void ShiftCamera()
    {
        CM_Camera.ForceCameraPosition(new Vector3((
            GM_Player.IsPlayerFacingLeft ? 1 : -1) * (GM_LevelGen.MOVE_X),
            CM_Camera.transform.position.y - GM_LevelGen.MOVE_Y,
            CM_Camera.transform.position.z),
            Quaternion.Euler(Vector3.zero));
    }
}
