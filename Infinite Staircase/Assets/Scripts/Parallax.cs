using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameManager gameManager;

    LevelGenerator GM_LevelGenerator;
    CameraManager GM_CameraManager;

    [System.Serializable]
    public class ParallaxEffectors
    {
        public bool loop;
        public GameObject layer;
        public float parallaxEFX;
        public float parallaxEFY;
    }

    public List<ParallaxEffectors> list = new List<ParallaxEffectors>();

    //
    private void Awake()
    {
        GM_LevelGenerator = gameManager.levelGenerator;
        GM_CameraManager = gameManager.cameraManager;
    }
    private void Update()
    {

    }

    //
    public void ShiftBackground()
    {
        foreach (ParallaxEffectors item in list)
        {
            Vector3 initPos = item.layer.transform.position;

            item.layer.transform.position = new Vector3(
                initPos.x + (((GM_LevelGenerator.gameManager.player.IsPlayerFacingLeft ? 1 : -1) * GM_LevelGenerator.MOVE_X) * (1 + item.parallaxEFX)),
                initPos.y - (GM_LevelGenerator.MOVE_Y * (1 + item.parallaxEFY)),
                0);

            if (item.loop)
            {
                // used for looping the item
                if (Mathf.Abs(item.layer.transform.position.x) > item.layer.GetComponentInChildren<Renderer>().bounds.size.x)
                {
                    item.layer.transform.position = new Vector3(0, item.layer.transform.position.y, 0);
                }
                else if (Mathf.Abs(item.layer.transform.position.y) > item.layer.GetComponentInChildren<Renderer>().bounds.size.y)
                {
                    item.layer.transform.position = new Vector3(item.layer.transform.position.x, 0, 0);
                }
            }
        }
    }
}
